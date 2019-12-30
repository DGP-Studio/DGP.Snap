using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Snap.Helper.Extensions
{
    internal static class ListExtensions
    {
        internal static bool RemoveFirstWhere<T>(this IList<T> list, Func<T, bool> shouldRemovePredicate)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (shouldRemovePredicate.Invoke(list[i]))
                {
                    list.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }
    }
}
