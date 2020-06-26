using System.Collections.Generic;

namespace DGP.Snap.Helper.Extensions
{
    internal static class DictionaryExtensions
    {
        /// <summary>
        /// 在词典中添加键值对，遇到已存在的键值对时覆盖
        /// </summary>
        /// <typeparam name="TKey">key的类型</typeparam>
        /// <typeparam name="TValue">value的类型</typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
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
