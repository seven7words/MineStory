using System;
using System.Collections.Generic;

namespace Wsc.STL
{
    public class DictionaryWithLinkedListKey<TKey, TValue>
    {
        public DictionaryWithLinkedListKey()
        {
            keys = new LinkedList<TKey>();
            dict = new Dictionary<TKey, TValue>();
        }
        private LinkedList<TKey> keys;
        private Dictionary<TKey, TValue> dict;
        public int Count { get { return dict.Count; } }
        public void Clear(Action<TValue> Destroy)
        {
            foreach (var key in keys)
            {
                Destroy(dict[key]);
            }
            keys.Clear();
            dict.Clear();
        }

        public bool ContainsKey(TKey key)
        {
            return dict.ContainsKey(key);
        }

        public void AddLast(TKey key, TValue value)
        {
            if (ContainsKey(key)) { return; }
            dict.Add(key, value);
            keys.AddLast(key);
        }
        public TValue Get(TKey key)
        {
            if (!ContainsKey(key))
            {
                return default(TValue);
            }
            return dict[key];
        }

        public void MoveToLast(TKey key)
        {
            if (!ContainsKey(key)) { return; }
            if (!keys.Last.Value.Equals(key))
            {
                var node = keys.Find(key);
                keys.Remove(node);
                keys.AddLast(node);
            }
        }

        // public void MoveToFirst(TKey key)
        // {
        //     if (!ContainsKey(key)) { return; }
        //     if (!keys.First.Value.Equals(key))
        //     {
        //         var node = keys.Find(key);
        //         keys.Remove(node);
        //         keys.AddFirst(node);
        //     }
        // }

        public TValue RemoveFirst()
        {
            var value = dict[keys.First.Value];
            dict.Remove(keys.First.Value);
            keys.RemoveFirst();
            return value;
        }
    }
}