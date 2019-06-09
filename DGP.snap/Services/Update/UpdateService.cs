using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DGP.snap.Services.Update
{
    //class UpdateService
    //{
    //    public static void CheckForUpdates(bool silent = false)
    //    {
    //        if (App.IsUWP)
    //        {
    //            if (!silent)
    //                Process.Start("ms-windows-store://pdp/?productid=9NV4BS3L1H4S");

    //            return;
    //        }

    //        Task.Run(() =>
    //        {
    //            try
    //            {
    //                var json = DownloadJson("https://api.github.com/repos/xupefei/QuickLook/releases/latest");

    //                var nVersion = (string)json["tag_name"];
    //                //nVersion = "9.2.1";

    //                if (new Version(nVersion) <= Assembly.GetExecutingAssembly().GetName().Version)
    //                {
    //                    if (!silent)
    //                        Application.Current.Dispatcher.Invoke(
    //                            () => TrayIconManager.ShowNotification("",
    //                                TranslationHelper.Get("Update_NoUpdate")));
    //                    return;
    //                }

    //                CollectAndShowReleaseNotes();

    //                Application.Current.Dispatcher.Invoke(
    //                    () =>
    //                    {
    //                        TrayIconManager.ShowNotification("",
    //                            string.Format(TranslationHelper.Get("Update_Found"), nVersion),
    //                            timeout: 20000,
    //                            clickEvent:
    //                            () => Process.Start(
    //                                @"https://github.com/xupefei/QuickLook/releases/latest"));
    //                    });
    //            }
    //            catch (Exception e)
    //            {
    //                Debug.WriteLine(e.Message);
    //                Application.Current.Dispatcher.Invoke(
    //                    () => TrayIconManager.ShowNotification("",
    //                        string.Format(TranslationHelper.Get("Update_Error"), e.Message)));
    //            }
    //        });
    //    }

    //    private static void CollectAndShowReleaseNotes()
    //    {
    //        Task.Run(() =>
    //        {
    //            try
    //            {
    //                var json = DownloadJson("https://api.github.com/repos/xupefei/QuickLook/releases");

    //                var notes = string.Empty;

    //                var count = 0;
    //                foreach (var item in json)
    //                {
    //                    notes += $"# {item["name"]}\r\n\r\n";
    //                    notes += item["body"] + "\r\n\r\n";

    //                    if (count++ > 10)
    //                        break;
    //                }

    //                var changeLogPath = Path.GetTempFileName() + ".md";
    //                File.WriteAllText(changeLogPath, notes);

    //                PipeServerManager.SendMessage(PipeMessages.Invoke, changeLogPath);
    //                PipeServerManager.SendMessage(PipeMessages.Forget);
    //            }
    //            catch (Exception e)
    //            {
    //                Debug.WriteLine(e.Message);
    //                Application.Current.Dispatcher.Invoke(
    //                    () => TrayIconManager.ShowNotification("",
    //                        string.Format(TranslationHelper.Get("Update_Error"), e.Message)));
    //            }
    //        });
    //    }

    //    private static dynamic DownloadJson(string url)
    //    {
    //        var web = new WebClientEx(15 * 1000)
    //        {
    //            Proxy = WebRequest.DefaultWebProxy,
    //            Credentials = CredentialCache.DefaultCredentials
    //        };
    //        web.Headers.Add(HttpRequestHeader.UserAgent, "Wget/1.9.1");

    //        var response =
    //            web.DownloadDataStream(url);

    //        var json = JsonConvert.DeserializeObject<dynamic>(new StreamReader(response).ReadToEnd());
    //        return json;
    //    }
    //}

}
