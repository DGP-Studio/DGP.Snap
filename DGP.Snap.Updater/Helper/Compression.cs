using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGP.Snap.Updater.Helper
{
    public class CompressionHelper
    {
        //public static void Compress(DirectoryInfo directorySelected)
        //{
        //    foreach (FileInfo file in directorySelected.GetFiles("*.xml"))
        //        using (FileStream originalFileStream = file.OpenRead())
        //        {
        //            if ((File.GetAttributes(file.FullName) & FileAttributes.Hidden)
        //                != FileAttributes.Hidden & file.Extension != ".cmp")
        //            {
        //                using (FileStream compressedFileStream = File.Create(file.FullName + ".cmp"))
        //                {
        //                    using (DeflateStream compressionStream = new DeflateStream(compressedFileStream, CompressionMode.Compress))
        //                    {
        //                        originalFileStream.CopyTo(compressionStream);
        //                    }
        //                }

        //                FileInfo info = new FileInfo(directoryPath + "\\" + file.Name + ".cmp");
        //                Console.WriteLine("Compressed {0} from {1} to {2} bytes.", file.Name, file.Length, info.Length);
        //            }
        //        }
        //}

        public static void Decompress(FileInfo zipfileToDecompress)
        {
            using (FileStream originalFileStream = zipfileToDecompress.OpenRead())
            {
                string currentFileName = zipfileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - zipfileToDecompress.Extension.Length);

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (DeflateStream decompressionStream = new DeflateStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        Console.WriteLine("Decompressed: {0}", zipfileToDecompress.Name);
                    }
                }
            }
        }
    }
}
