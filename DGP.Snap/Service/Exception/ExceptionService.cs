using System;

namespace DGP.Snap.Service.Exception
{
    public class ExceptionService
    {
        public static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            new ExceptionDialog(sender, e).ShowDialog();
        }

    }
}
