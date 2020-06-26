using System;

namespace DGP.Snap.Helper.Extensions
{
    internal static class EventHandlerExtensions
    {
        public static void SafeInvoke<T>(this EventHandler<T> evt, object sender, T e) where T : EventArgs
        {
            evt?.Invoke(sender, e);
        }

        public static void SafeInvoke(this EventHandler evt, object sender, EventArgs e)
        {
            evt?.Invoke(sender, e);
        }
    }
}
