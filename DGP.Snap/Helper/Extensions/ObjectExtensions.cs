using System.Diagnostics;

namespace DGP.Snap.Helper.Extensions
{
    public static class ObjectExtensions
    {
        public static void Log(this object obj, string debugString = null)
        {
            Debug.WriteLine(obj);
            if (debugString != null)
            {
                Debug.WriteLine(debugString);
            }
        }
    }
}
