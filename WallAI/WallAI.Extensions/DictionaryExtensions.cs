using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallAI.Extensions
{
    public static class DictionaryExtensions
    {
        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            return dictionary.TryAdd(new KeyValuePair<TKey, TValue>(key, value));
        }

        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, KeyValuePair<TKey, TValue> kvPair)
        {
            TValue helper;

            if(!dictionary.TryGetValue(kvPair.Key, out helper))
            {
                dictionary.Add(kvPair);
                return true;
            }

            return false;
        }

        public static void AddNonPresent<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            foreach(var item in items)
            {
                dictionary.TryAdd(item);
            }
        }
    }
}
