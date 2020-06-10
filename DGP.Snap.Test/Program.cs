using System;
using System.IO;

namespace DGP.Snap.Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TestDriveInfo();
            Console.ReadKey();
        }

        private static void TestDriveInfo()
        {
            DriveInfo[] allDirves = DriveInfo.GetDrives();
            //检索计算机上的所有逻辑驱动器名称
            foreach (DriveInfo item in allDirves)
            {
                //Fixed 硬盘
                //Removable 可移动存储设备，如软盘驱动器或USB闪存驱动器。
                Console.Write(item.Name + "---" + item.DriveType);
                if (item.IsReady)
                {
                    Console.Write(",容量：" + item.TotalSize + "，可用空间大小：" + item.TotalFreeSpace);
                }
                else
                {
                    Console.Write("没有就绪");
                }
                Console.WriteLine();
            }
        }
    }
}
