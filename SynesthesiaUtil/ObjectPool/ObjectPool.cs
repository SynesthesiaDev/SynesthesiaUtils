using System;
using System.Collections.Concurrent;

namespace SynesthesiaUtil.ObjectPool
{
    public class ObjectPool<T> where T : class
    {
        private const int QUEUE_SIZE = 32768;

        private readonly Func<T> _supplier;

        private readonly Func<T, T> _sanitizer;

        private readonly ConcurrentQueue<WeakReference<T>> _pool = new();

        public int Size => _pool.Count;

        public ObjectPool(Func<T> supplier, Func<T, T>? sanitizer = null)
        {
            _supplier = supplier ?? throw new ArgumentNullException(nameof(supplier));

            // If no sanitizer is provided, use a function that returns the object unchanged (identity)
            _sanitizer = sanitizer ?? (obj => obj);
        }

        public PooledObject<T> Get()
        {
            while (_pool.TryDequeue(out var weakRef))
            {
                if (weakRef.TryGetTarget(out T result))
                {
                    return new PooledObject<T>(this, result);
                }
            }

            // Pool is empty or only contained collected objects, so create a new one
            var newObj = _supplier.Invoke();
            return new PooledObject<T>(this, newObj);
        }

        public void Add(T obj)
        {
            var sanitized = _sanitizer.Invoke(obj);
            var weakRef = new WeakReference<T>(sanitized);

            _pool.Enqueue(weakRef);
        }

        public void Clear()
        {
            while (_pool.TryDequeue(out _))
            {
                //empty
            }
        }
    }
}