using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
