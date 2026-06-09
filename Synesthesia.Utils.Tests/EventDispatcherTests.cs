// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.


using Synesthesia.Utils.Events;

namespace Synesthesia.Utils.Tests;

[TestFixture]
public class EventDispatcherTests
{

    [Test]
    public void Subscribe_WhenEventIsDispatched_InvokesSubscriber()
    {
        var dispatcher = new EventDispatcher<int>();
        var receivedValue = 0;

        dispatcher.Subscribe(value => receivedValue = value);

        dispatcher.Dispatch(42);

        Assert.That(receivedValue, Is.EqualTo(42));
    }

    [Test]
    public void Dispatch_WithMultipleSubscribers_InvokesAllSubscribers()
    {
        var dispatcher = new EventDispatcher<string>();
        var firstReceivedValue = string.Empty;
        var secondReceivedValue = string.Empty;

        dispatcher.Subscribe(value => firstReceivedValue = value);
        dispatcher.Subscribe(value => secondReceivedValue = value);

        dispatcher.Dispatch("test");

        Assert.Multiple(() =>
        {
            Assert.That(firstReceivedValue, Is.EqualTo("test"));
            Assert.That(secondReceivedValue, Is.EqualTo("test"));
        });
    }

    [Test]
    public void Unsubscribe_WhenSubscriberExists_RemovesOnlyThatSubscriber()
    {
        var dispatcher = new EventDispatcher<int>();
        var firstCallCount = 0;
        var secondCallCount = 0;

        var firstSubscriber = dispatcher.Subscribe(_ => firstCallCount++);
        dispatcher.Subscribe(_ => secondCallCount++);

        dispatcher.Unsubscribe(firstSubscriber);
        dispatcher.Dispatch(10);

        Assert.Multiple(() =>
        {
            Assert.That(firstCallCount, Is.EqualTo(0));
            Assert.That(secondCallCount, Is.EqualTo(1));
        });
    }

    [Test]
    public void Unsubscribe_WhenSubscriberDoesNotExist_DoesNothing()
    {
        var dispatcher = new EventDispatcher<int>();
        var callCount = 0;

        dispatcher.Subscribe(_ => callCount++);

        var unrelatedSubscriber = new EventSubscriber<int>(_ => { });

        Assert.DoesNotThrow(() => dispatcher.Unsubscribe(unrelatedSubscriber));

        dispatcher.Dispatch(10);

        Assert.That(callCount, Is.EqualTo(1));
    }

    [Test]
    public void UnsubscribeAll_RemovesAllSubscribers()
    {
        var dispatcher = new EventDispatcher<int>();
        var firstCallCount = 0;
        var secondCallCount = 0;

        dispatcher.Subscribe(_ => firstCallCount++);
        dispatcher.Subscribe(_ => secondCallCount++);

        dispatcher.UnsubscribeAll();
        dispatcher.Dispatch(10);

        Assert.Multiple(() =>
        {
            Assert.That(firstCallCount, Is.EqualTo(0));
            Assert.That(secondCallCount, Is.EqualTo(0));
        });
    }

    [Test]
    public void Reset_RemovesAllSubscribers()
    {
        var dispatcher = new EventDispatcher<int>();
        var callCount = 0;

        dispatcher.Subscribe(_ => callCount++);

        dispatcher.Reset();
        dispatcher.Dispatch(10);

        Assert.That(callCount, Is.EqualTo(0));
    }

    [Test]
    public void Subscribe_AfterDispose_ThrowsObjectDisposedException()
    {
        var dispatcher = new EventDispatcher<int>();

        dispatcher.Dispose();

        Assert.Throws<ObjectDisposedException>(() => dispatcher.Subscribe(_ => { }));
    }

    [Test]
    public void Dispatch_Concurrency_DoesNotAffectSnapshot()
    {
        var dispatcher = new EventDispatcher<int>();
        var firstCallCount = 0;
        var secondCallCount = 0;

        EventSubscriber<int>? secondSubscriber = null;

        dispatcher.Subscribe(_ =>
        {
            firstCallCount++;

            if (secondSubscriber is not null)
                dispatcher.Unsubscribe(secondSubscriber);
        });

        secondSubscriber = dispatcher.Subscribe(_ => secondCallCount++);

        dispatcher.Dispatch(10);
        dispatcher.Dispatch(20);

        Assert.Multiple(() =>
        {
            Assert.That(firstCallCount, Is.EqualTo(2));
            Assert.That(secondCallCount, Is.EqualTo(1));
        });
    }


}
