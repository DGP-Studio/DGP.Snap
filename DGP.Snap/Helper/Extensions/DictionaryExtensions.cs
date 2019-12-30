using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Snap.Helper.Extensions
{
    static class DictionaryExtensions
    {
        public static void AddOrSet<TKey,TValue>(this Dictionary<TKey,TValue> dictionary,TKey key,TValue value)
        {
            if(dictionary.ContainsKey(key))
                dictionary.Remove(key);
            dictionary.Add(key, value);
        }
    }
}
