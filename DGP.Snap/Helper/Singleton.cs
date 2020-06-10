using System;
using System.Collections.Concurrent;

namespace DGP.Snap.Helper
{
    /// <summary>
    /// 由开源项目Windows Template Studio 提供的单例实现方式
    /// 单例可以解决静态类的诸多不足
    /// 单例主要是用于维护信息的类的
    /// 静态方法调用比单例快，但静态类不适合维护一个对象
    /// 也可以在延迟初始化类时使用单例
    /// </summary>
    /// <typeparam name="T">想要实现单例的类类型，必须具有无参数的构造函数</typeparam>
    public static class Singleton<T> where T : ISupportSingleton, new()
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
