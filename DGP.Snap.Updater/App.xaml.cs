using Microsoft.VisualBasic.Devices;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows;

namespace DGP.Snap.Updater
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            //    using (FileStream originalFileStream = zipfileToDecompress.OpenRead())
            //    {
            //        string currentFileName = zipfileToDecompress.FullName;
            //        string newFileName = currentFileName.Remove(currentFileName.Length - zipfileToDecompress.Extension.Length);

            //        using (FileStream decompressedFileStream = File.Create(newFileName))
            //        {
            //            using (DeflateStream decompressionStream = new DeflateStream(originalFileStream, CompressionMode.Decompress))
            //            {
            //                decompressionStream.CopyTo(decompressedFileStream);
            //                Console.WriteLine("Decompressed: {0}", zipfileToDecompress.Name);
            //            }
            //        }
            //    }

            //string updatepath = Environment.CurrentDirectory + @"\Update";
            using (ZipArchive archive = ZipFile.OpenRead("Package.zip"))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    try
                    {
                        entry.ExtractToFile(entry.FullName, overwrite: true);
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
            }
            //先解压到update文件夹
            
            //if (Directory.Exists(updatepath))//若存在先删除
            //    Directory.Delete(updatepath);
            //Directory.CreateDirectory(updatepath);
            ////解压
            //ZipFile.ExtractToDirectory("Package.zip", updatepath);
            Process.Start("DGP.Snap.exe");
            Current.Shutdown();
        }
    }
}
