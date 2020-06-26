using System;
using System.Threading.Tasks;

namespace DGP.Snap.Service.Kernel
{
    internal class GCHelper
    {
        private static bool isGarbageCollecting = false;
        /// <summary>
        /// 进行最大GC垃圾回收
        /// </summary>
        public static void PerformAggressiveGC()
        {
            if (!isGarbageCollecting)
            {
                isGarbageCollecting = true;
                Task.Delay(1000).ContinueWith(t =>
                {
                    GC.Collect(GC.MaxGeneration);
                    isGarbageCollecting = false;
                });
            }
        }
    }
}
