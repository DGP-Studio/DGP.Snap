using System;
using System.Threading.Tasks;

namespace DGP.Snap.Service.Kernel
{
    class GCHelper
    {
        private static bool isGarbageCollecting = false;
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
