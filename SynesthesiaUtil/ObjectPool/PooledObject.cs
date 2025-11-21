using System;

namespace SynesthesiaUtil.ObjectPool
{
    public class PooledObject<T> : IDisposable where T : class
    {
        private readonly ObjectPool<T> _pool;
        public T Value { get; private set; }

        internal PooledObject(ObjectPool<T> pool, T value)
        {
            _pool = pool;
            Value = value;
        }

        public void Dispose()
        {
            _pool.Add(Value);
            GC.SuppressFinalize(this);
        }
    }
}