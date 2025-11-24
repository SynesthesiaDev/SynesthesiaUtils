using System;
using System.Collections.Concurrent;

namespace SynesthesiaUtil.ObjectPool
{
    public class ObjectPool<T> where T : class
    {
        private readonly Func<T> _supplier;

        private readonly Func<T, T> _sanitizer;

        private readonly ConcurrentQueue<WeakReference<T>> _pool = new();

        public int Size => _pool.Count;

        public ObjectPool(Func<T> supplier, Func<T, T>? sanitizer = null)
        {
            _supplier = supplier ?? throw new ArgumentNullException(nameof(supplier));

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