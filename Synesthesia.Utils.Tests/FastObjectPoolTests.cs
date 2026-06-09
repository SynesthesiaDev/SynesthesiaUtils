using Synesthesia.Utils.Pooling;

namespace Synesthesia.Utils.Tests;

[TestFixture]
public class FastObjectPoolTests
{
    [Test]
    public void Rent_WhenPoolIsEmpty_CreatesNewInstance()
    {
        var createdCount = 0;
        using var pool = new FastObjectPool<TestPooledObject>(() =>
        {
            createdCount++;
            return new TestPooledObject();
        });

        var item = pool.Rent();

        Assert.Multiple(() =>
        {
            Assert.That(item, Is.Not.Null);
            Assert.That(createdCount, Is.EqualTo(1));
        });
    }

    [Test]
    public void Return_ThenRent_ReturnsSameInstanceFromThreadLocalCache()
    {
        using var pool = new FastObjectPool<TestPooledObject>(() => new TestPooledObject());

        var item = pool.Rent();

        pool.Return(item);

        var rentedAgain = pool.Rent();

        Assert.That(rentedAgain, Is.SameAs(item));
    }

    [Test]
    public void Return_WhenThreadLocalCacheIsOccupied_StoresItemInSharedPool()
    {
        using var pool = new FastObjectPool<TestPooledObject>(() => new TestPooledObject(), capacity: 1);

        var first = pool.Rent();
        var second = pool.Rent();

        pool.Return(first);
        pool.Return(second);

        var fromThreadLocal = pool.Rent();
        var fromShared = pool.Rent();

        Assert.Multiple(() =>
        {
            Assert.That(fromThreadLocal, Is.SameAs(first));
            Assert.That(fromShared, Is.SameAs(second));
        });
    }

    [Test]
    public void Return_WhenSharedPoolIsFull_DropsExtraItem()
    {
        var createdCount = 0;

        using var pool = new FastObjectPool<TestPooledObject>(() =>
        {
            createdCount++;
            return new TestPooledObject();
        }, capacity: 1);

        var first = pool.Rent();
        var second = pool.Rent();
        var third = pool.Rent();

        pool.Return(first);
        pool.Return(second);
        pool.Return(third);

        var rentedFirst = pool.Rent();
        var rentedSecond = pool.Rent();
        var rentedThird = pool.Rent();

        Assert.Multiple(() =>
        {
            Assert.That(rentedFirst, Is.SameAs(first));
            Assert.That(rentedSecond, Is.SameAs(second));
            Assert.That(rentedThird, Is.Not.SameAs(third));
            Assert.That(createdCount, Is.EqualTo(4));
        });
    }

    [Test]
    public void Rent_WhenItemImplementsIPooledObject_SetsPoolingState()
    {
        using var pool = new FastObjectPool<TestPooledObject>(() => new TestPooledObject());

        var item = pool.Rent();

        Assert.Multiple(() =>
        {
            Assert.That(item.IsPooled, Is.True);
            Assert.That(item.ReturnAction, Is.Not.Null);
        });
    }

    [Test]
    public void Return_WhenItemImplementsIPooledObject_ResetsItem()
    {
        using var pool = new FastObjectPool<TestPooledObject>(() => new TestPooledObject());

        var item = pool.Rent();
        item.Value = 123;

        pool.Return(item);

        Assert.Multiple(() =>
        {
            Assert.That(item.ResetCallCount, Is.EqualTo(1));
            Assert.That(item.Value, Is.EqualTo(0));
        });
    }

    [Test]
    public void ReturnAction_WhenInvoked_ReturnsItemToPool()
    {
        using var pool = new FastObjectPool<TestPooledObject>(() => new TestPooledObject());

        var item = pool.Rent();

        item.ReturnAction?.Invoke(item);

        var rentedAgain = pool.Rent();

        Assert.That(rentedAgain, Is.SameAs(item));
    }

    [Test]
    public void PreAllocate_CreatesInstancesForPoolCapacity()
    {
        var createdCount = 0;

        using var pool = new FastObjectPool<TestPooledObject>(() =>
        {
            createdCount++;
            return new TestPooledObject();
        }, capacity: 4);

        pool.PreAllocate(4);

        Assert.That(createdCount, Is.EqualTo(4));
    }

    [Test]
    public void PreAllocate_IgnoresCountAndFillsEntireCapacity()
    {
        var createdCount = 0;

        using var pool = new FastObjectPool<TestPooledObject>(() =>
        {
            createdCount++;
            return new TestPooledObject();
        }, capacity: 4);

        pool.PreAllocate(1);

        Assert.That(createdCount, Is.EqualTo(4));
    }

    [Test]
    public void PreAllocate_WhenCalledMultipleTimes_DoesNotReplaceExistingSharedItems()
    {
        var createdCount = 0;

        using var pool = new FastObjectPool<TestPooledObject>(() =>
        {
            createdCount++;
            return new TestPooledObject();
        }, capacity: 4);

        pool.PreAllocate(4);
        pool.PreAllocate(4);

        Assert.That(createdCount, Is.EqualTo(4));
    }

    [Test]
    public void PreAllocate_AfterRentingSharedItem_OnlyFillsEmptySharedSlots()
    {
        var createdCount = 0;

        using var pool = new FastObjectPool<TestPooledObject>(() =>
        {
            createdCount++;
            return new TestPooledObject();
        }, capacity: 4);

        pool.PreAllocate(4);

        _ = pool.Rent();

        pool.PreAllocate(4);

        Assert.That(createdCount, Is.EqualTo(5));
    }

    [Test]
    public void Dispose_ClearsThreadLocalItem()
    {
        var createdCount = 0;

        var pool = new FastObjectPool<TestPooledObject>(() =>
        {
            createdCount++;
            return new TestPooledObject();
        });

        var item = pool.Rent();

        pool.Return(item);
        pool.Dispose();

        var rentedAfterDispose = pool.Rent();

        Assert.Multiple(() =>
        {
            Assert.That(rentedAfterDispose, Is.Not.SameAs(item));
            Assert.That(createdCount, Is.EqualTo(2));
        });
    }

    [Test]
    public void Dispose_DisposesSharedDisposableItems()
    {
        using var pool = new FastObjectPool<DisposablePooledObject>(() => new DisposablePooledObject(), capacity: 1);

        var first = pool.Rent();
        var second = pool.Rent();

        pool.Return(first);
        pool.Return(second);

        pool.Dispose();

        Assert.Multiple(() =>
        {
            Assert.That(first.IsDisposed, Is.False);
            Assert.That(second.IsDisposed, Is.True);
        });
    }

    [Test]
    public void Dispose_WhenCalledMultipleTimes_DoesNotThrow()
    {
        var pool = new FastObjectPool<TestPooledObject>(() => new TestPooledObject());

        pool.Dispose();

        Assert.DoesNotThrow(() => pool.Dispose());
    }

    [Test]
    public void SeparatePools_DoNotReuseEachOthersThreadLocalItems()
    {
        using var firstPool = new FastObjectPool<TestPooledObject>(() => new TestPooledObject());
        using var secondPool = new FastObjectPool<TestPooledObject>(() => new TestPooledObject());

        var firstItem = firstPool.Rent();

        firstPool.Return(firstItem);

        var itemFromSecondPool = secondPool.Rent();
        var itemFromFirstPool = firstPool.Rent();

        Assert.Multiple(() =>
        {
            Assert.That(itemFromSecondPool, Is.Not.SameAs(firstItem));
            Assert.That(itemFromFirstPool, Is.SameAs(firstItem));
        });
    }

    private sealed class TestPooledObject : IPooledObject
    {
        public int Value { get; set; }

        public int ResetCallCount { get; private set; }

        public bool IsPooled { get; set; }

        public Action<IPooledObject>? ReturnAction { get; set; }

        public void Reset()
        {
            ResetCallCount++;
            Value = 0;
            IsPooled = false;
            ReturnAction = null;
        }
    }

    private sealed class DisposablePooledObject : IDisposable
    {
        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            IsDisposed = true;
        }
    }
}
