using DGP.Snap.Helper;
using DGP.Snap.Services.Update;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Snap.Services.Update
{
    class UpdateService
    {
        public static async  void CheckForUpdate()
        {
            try
            {
                Release release = await Json.GetWebRequestJsonObject<Release>("https://api.github.com/repos/DGP-Studio/DGP.Snap/releases/latest");

                var newVersion = release.Tag_name;

                if (new Version(newVersion) >= Assembly.GetExecutingAssembly().GetName().Version)//有新版本
                {
                    //xiazi
                    Debug.WriteLine(newVersion);
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

    }

}
