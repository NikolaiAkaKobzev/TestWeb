using System.Collections.Concurrent;

namespace WebApi.Services
{
    public class StringStorage : IStringStorage
    {
        private object _lock = new object();
        private ConcurrentDictionary<int, string> _strings;

        public StringStorage()
        {
            _strings = new ConcurrentDictionary<int, string>();
        }

        public int AddString(string value)
        {
            lock (_lock)
            {
                var i = 0;
                if (_strings.Any())
                {
                    i = _strings.Max(s => s.Key);
                }

                i++;

                _strings.TryAdd(i, value);
                return i;
            }
        }

        public bool DeleteString(int key)
        {
            if (!_strings.ContainsKey(key))
            {
                return false;
            }

            return _strings.TryRemove(key, out var srt);
        }

        public string? GetString(int key)
        {
            _strings.TryGetValue(key, out var str);
            return str;
        }

        public bool UpdateString(int key, string value)
        {
            if (!_strings.ContainsKey(key))
            {
                return false;
            }

            _strings[key] = value;
            return true;
        }
    }
}
