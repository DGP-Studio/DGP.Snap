using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Snap.Service.Kernel
{
    class GCHelper
    {
        private static bool isPreparingCollect = false;
        public static void PerformAggressiveGC()
        {
            if (!isPreparingCollect)
            {
                isPreparingCollect = true;
                Task.Delay(1000).ContinueWith(t =>
                {
                    GC.Collect(GC.MaxGeneration);
                    isPreparingCollect = false;
                });
            }
        }
    }
}
