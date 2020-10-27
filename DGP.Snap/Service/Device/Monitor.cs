using DGP.Snap.Service.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Snap.Service.Device
{
    class Monitor
    {
        public static int CurrentRefreshRate()
        {
            var vDevMode = new NativeMethod.DEVMODE();
            return NativeMethod.EnumDisplaySettings(null, NativeMethod.ENUM_CURRENT_SETTINGS, ref vDevMode) ? vDevMode.dmDisplayFrequency : 60;
        }
    }
}
