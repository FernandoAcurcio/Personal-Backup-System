using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Backup_System.Controllers
{
    public static class SynchronizeController
    {
        public static void Synchronize(string sourcePath, string destinationPath)
        {
            // Create the destination directory if it doesn't exist
            Directory.CreateDirectory(destinationPath);

            string[] sourceFiles = Directory.GetFiles(sourcePath);
            string[] destinationFiles = Directory.GetFiles(destinationPath);

            
            // copy or replace files from source to destination
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

            // recursively synchronize each subdirectory
            string[] sourceDirectories = Directory.GetDirectories(sourcePath);
            foreach (string sourceDir in sourceDirectories)
            {
                string dirName = Path.GetFileName(sourceDir);
                Synchronize(sourceDir, Path.Combine(destinationPath, dirName));
            }
        }

    }
}
