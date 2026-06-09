// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Threading;
using Synesthesia.Utils.Types;

namespace Synesthesia.Utils.Events;

public class EventDispatcher<T> : IDisposable
{
    private volatile EventSubscriber<T>[] eventSubscribers = [];
    private readonly Lock writeLock = new();

    public bool IsDisposed { get; private set; }

    public EventSubscriber<T> Subscribe(Action<T> action)
    {
        var eventSubscriber = new EventSubscriber<T>(action);
        lock (writeLock)
        {
            ObjectDisposedException.ThrowIf(IsDisposed, this);
            var old = eventSubscribers;
            var next = new EventSubscriber<T>[old.Length + 1];
            old.CopyTo(next, 0);
            next[old.Length] = eventSubscriber;
            eventSubscribers = next;
        }

        return eventSubscriber;
    }

    public void Dispatch(T value)
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);

        using var snapshot = Snapshot.Rent(eventSubscribers);
        foreach (var subscriber in snapshot.Span)
        {
            subscriber.Action.Invoke(value);
        }
    }

    public void Unsubscribe(EventSubscriber<T> subscriber)
    {
        lock (writeLock)
        {
            var old = eventSubscribers;
            int idx = Array.FindIndex(old, s => ReferenceEquals(s, subscriber));
            if (idx < 0) return;

            var next = new EventSubscriber<T>[old.Length - 1];
            Array.Copy(old, 0, next, 0, idx);
            Array.Copy(old, idx + 1, next, idx, old.Length - idx - 1);
            eventSubscribers = next;
        }
    }

    public void UnsubscribeAll()
    {
        lock (writeLock)
            eventSubscribers = [];
    }

    public void Dispose()
    {
        lock (writeLock)
        {
            if (IsDisposed) return;
            eventSubscribers = [];
            IsDisposed = true;
        }

        GC.SuppressFinalize(this);
    }

    public void Reset()
    {
        UnsubscribeAll();
    }
}

public record EventSubscriber<T>(Action<T> Action);
