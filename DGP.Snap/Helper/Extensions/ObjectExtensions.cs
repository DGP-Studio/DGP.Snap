using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Snap.Helper.Extensions
{
    public static class ObjectExtensions
    {
        public static void Log(this object obj,string debugString=null)
        {
            Debug.WriteLine(obj);
            if (debugString != null)
                Debug.WriteLine(debugString);
        }
    }
}
