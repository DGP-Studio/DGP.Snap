using System.IO;

namespace DGP.Snap.Service.Device
{
    public class DiskInfo
    {
        public string Name { get; set; }
        public string TotalSize { get; set; }
        public string Available { get; set; }
        public string Present { get => Available + " / " + TotalSize; }
        public double UsedPersentage { get; set; }
        public string DiskType { get; set; }

        public static implicit operator DiskInfo(DriveInfo driveInfo)
        {
            return new DiskInfo
            {
                Name = driveInfo.Name.Replace("\\", ""),
                TotalSize = $"{driveInfo.TotalSize / 1024 / 1024 / 1024:#0}G",
                Available = $"{driveInfo.AvailableFreeSpace / 1024 / 1024 / 1024:#0}G",
                UsedPersentage = (driveInfo.TotalSize - driveInfo.AvailableFreeSpace) * 1.0 / driveInfo.TotalSize,
                DiskType = driveInfo.DriveType == DriveType.Fixed ? "" : "",
            };

        }
    }
}
