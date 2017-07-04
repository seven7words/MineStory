using Wsc.STL;

namespace Wsc.Input
{
    internal class Bus<T>
    {
        public Bus(T defaultValue)
        {
            inputs = new DictionaryWithListKey<string, T>(defaultValue);
        }
        ~Bus()
        {
            inputs.Clear();
            inputs = null;
        }
        private DictionaryWithListKey<string, T> inputs;

        public T Get(string key)
        {
            return inputs.Get(key);
        }

        public void Set(string key, T value)
        {
            inputs.Set(key, value);
        }

        public void Reset()
        {
            inputs.Reset();
        }
    }
}