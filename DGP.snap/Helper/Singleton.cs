using System;
using System.Collections.Concurrent;

namespace DGP.Snap.Helper
{
    /// <summary>
    /// 由UWP开源项目Windows Template Studio 提供的单例实现方式
    /// 单例可以解决静态类的诸多不足
    /// </summary>
    /// <typeparam name="T">想要实现单例的类类型</typeparam>
    public static class Singleton<T> where T : new()
    {
        private static ConcurrentDictionary<Type, T> _instances = new ConcurrentDictionary<Type, T>();

        public static T Instance
        {
            get
            {
                return _instances.GetOrAdd(typeof(T), (t) => new T());
            }
        }
    }
}
