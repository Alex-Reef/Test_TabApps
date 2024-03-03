using System.Collections.Generic;

namespace Utils
{
    public class Pool<T>
    {
        private Stack<T> _pool;

        public Pool()
        {
            _pool = new Stack<T>();
        }
        
        public bool TryGet(out T item)
        {
            if (_pool.Count > 0)
            {
                item = _pool.Pop();
                return true;
            }

            item = default;
            return false;
        }

        public void AddToPool(T value)
        {
            _pool.Push(value);
        }
    }
}