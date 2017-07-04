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

        public void Set(TKey key, TValue value)
        {
            if (!dict.ContainsKey(key))
            {
                keys.Add(key);
            }
            dict[key] = value;
        }

        public void SetAll(TValue value)
        {
            for (int i = keys.Count - 1; i >= 0; i--)
            {
                dict[keys[i]] = value;
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

        public void Clear()
        {
            dict.Clear();
            keys.Clear();
        }
    }
}