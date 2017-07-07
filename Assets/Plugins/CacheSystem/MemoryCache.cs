using System;
using System.Collections;
using System.Collections.Generic;
using Wsc.STL;

namespace Wsc.Cache
{
    public interface IMemoryCache
    {
        bool ReLoad(byte[] bytes);
    }
    public class MemoryCache<T> where T : IMemoryCache
    {
        private MemoryCache()
        {
            caches = new DictionaryWithLinkedListKey<string, T>();
        }
        public MemoryCache(int maxCount, Func<byte[], T> Instantiate, Action<T> Destroy) : base()
        {
            this.maxCount = maxCount;
            this.Instantiate = Instantiate;
            this.Destroy = Destroy;
        }
        ~MemoryCache()
        {
            caches.Clear(Destroy);
            this.Destroy = null;
            this.Instantiate = null;
        }
        private int maxCount;
        private DictionaryWithLinkedListKey<string,T> caches;
        private Func<byte[], T> Instantiate;
        private Action<T> Destroy;

        public bool HasCache(string key)
        {
            return caches.ContainsKey(key);
        }

        public bool Load(string key, out T result)
        {
            result = default(T);
            if (!HasCache(key))
            {
                return false;
            }
            caches.MoveToLast(key);
            result = caches.Get(key);
            return true;
        }

        public void Cache(string key, byte[] bytes)
        {
            if (HasCache(key))
            {
                caches.Get(key).ReLoad(bytes);
            }
            else
            {
                caches.AddLast(key,Instantiate(bytes));

                // 超出上限要清理
                if (caches.Count > maxCount)
                {
                    Destroy(caches.RemoveFirst());
                }
            }
        }

        public bool ReCache(string key, byte[] bytes)
        {
            if (!HasCache(key))
            {
                return false;
            }
            return caches.Get(key).ReLoad(bytes);
        }
    }
}