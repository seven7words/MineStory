using System;
using System.Collections.Generic;

namespace Wsc.STL
{
    public class DictionaryWithListKey<TKey, TValue>
    {
        private DictionaryWithListKey()
        {
            keys = new List<TKey>();
            dict = new Dictionary<TKey, TValue>();
        }
        public DictionaryWithListKey(TValue defaultValue) : this()
        {
            this.defaultValue = defaultValue;
        }

        TValue defaultValue;
        List<TKey> keys;
        Dictionary<TKey, TValue> dict;

        public void Add(TKey key, TValue value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] = value;
            }
            else
            {
                dict.Add(key, value);
                keys.Add(key);
            }
        }
        public void Set(TKey key, TValue value)
        {
            if (!dict.ContainsKey(key))
            {
                keys.Add(key);
            }
            dict[key] = value;
        }

        public void Reset()
        {
            for (int i = keys.Count - 1; i >= 0; i--)
            {
                dict[keys[i]] = defaultValue;
            }
        }

        public TValue Get(TKey key)
        {
            if (!dict.ContainsKey(key))
            {
                return defaultValue;
            }
            return dict[key];
        }

        public void Traversal(Func<TKey, TValue, bool> onTraversal)
        {
            if (onTraversal == null) { return; }
            for (int i = 0, count = keys.Count; i < count; i++)
            {
                if (onTraversal(keys[i], dict[keys[i]]))
                {
                    return;
                }
            }
        }

        public void Clear()
        {
            dict.Clear();
            keys.Clear();
        }
    }
}