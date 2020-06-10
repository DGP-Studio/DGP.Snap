using System.Collections.Generic;

namespace DGP.Snap.Helper.Extensions
{
    internal static class DictionaryExtensions
    {
        public static void AddOrSet<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary.Remove(key);
            }

            dictionary.Add(key, value);
        }
    }
}
