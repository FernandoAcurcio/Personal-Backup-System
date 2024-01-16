using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Backup_System.Controllers
{
    public static class SynchronizeController
    {
        public static void Synchronize()
        {
            var sourcePath = MenuController.UserData.SourcePath;
            var destinationPath = MenuController.UserData.DestinationPath;

            string[] sourceFiles = Directory.GetFiles(sourcePath);
            string[] destinationFiles = Directory.GetFiles(destinationPath);

            Console.WriteLine("");
            // Copy or replace files from source to replica
            foreach (string sourceFile in sourceFiles)
            {
                string fileName = Path.GetFileName(sourceFile);
                string destinationFile = Path.Combine(destinationPath, fileName);

                if (!File.Exists(destinationFile) || File.GetLastWriteTime(sourceFile) > File.GetLastWriteTime(destinationFile))
                {
                    File.Copy(sourceFile, destinationFile, true);
                    Console.WriteLine($"Copied or replaced file {fileName}");
                }
            }

            // Delete extra files in replica
            //foreach (string destinationFile in destinationFiles)
            //{
            //    string fileName = Path.GetFileName(destinationFile);
            //    string sourceFile = Path.Combine(sourcePath, fileName);

            //    if (!File.Exists(sourceFile))
            //    {
            //        File.Delete(destinationFile);
            //        Console.WriteLine($"Deleted file {fileName}");
            //    }
            //}
        }

    }
}
