using System;
using System.Collections.Concurrent;

namespace DGP.Snap.Helper
{
    /// <summary>
    /// 单例
    /// </summary>
    /// <typeparam name="T">想要实现单例的类类型，必须具有无参数的构造函数</typeparam>
    public class Singleton
    {
        private static Singleton instance;
        private static object _lock = new object();

        private Singleton()
        {

        }

        public static Singleton GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                }
            }
            return instance;
        }

    }
}
