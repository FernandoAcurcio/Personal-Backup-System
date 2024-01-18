namespace Personal_Backup_System.Controllers
{
    public static class SynchronizeController
    {
        /// <summary>
        /// Synchronizes files between source and destination folders, and logs the actions.
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        /// <param name="logFilePath"></param>
        public static void Synchronize(string sourcePath, string destinationPath, string logFilePath)
        {
            // Create the destination directory if it doesn't exist
            Directory.CreateDirectory(destinationPath);

            string[] sourceFiles = Directory.GetFiles(sourcePath);
            string[] destinationFiles = Directory.GetFiles(destinationPath);

            foreach (string sourceFile in sourceFiles)
            {
                string fileName = Path.GetFileName(sourceFile);
                string destinationFile = Path.Combine(destinationPath, fileName);

                string? sourceHash = CalculateHashController.CalculateMD5Hash(sourceFile);
                string? destinationHash = File.Exists(destinationFile) ? CalculateHashController.CalculateMD5Hash(destinationFile) : null;

                if (sourceHash == null || destinationHash == null || !sourceHash.Equals(destinationHash))
                {
                    try
                    {
                        File.Copy(sourceFile, destinationFile, true);
                        LogController.Log($"Copied or replaced file {fileName}", logFilePath);
                    }
                    catch (IOException ex)
                    {
                        LogController.Log($"Could not copy file {fileName}: {ex.Message}", logFilePath);
                    }
                }
            }

            // recursively synchronize each subdirectory
            string[] sourceDirectories = Directory.GetDirectories(sourcePath);
            foreach (string sourceDir in sourceDirectories)
            {
                string dirName = Path.GetFileName(sourceDir);
                Synchronize(sourceDir, Path.Combine(destinationPath, dirName), logFilePath);
            }

            // Delete extra files in replica
            foreach (string destinationFile in destinationFiles)
            {
                string fileName = Path.GetFileName(destinationFile);
                string sourceFile = Path.Combine(sourcePath, fileName);

                if (!File.Exists(sourceFile))
                {
                    File.Delete(destinationFile);
                    LogController.Log($"Deleted file {fileName}", logFilePath);
                }
            }

            // Delete extra directories in replica
            string[] replicaDirectories = Directory.GetDirectories(destinationPath);
            foreach (string destinationDir in replicaDirectories)
            {
                string dirName = Path.GetFileName(destinationDir);
                string sourceDir = Path.Combine(sourcePath, dirName);

                if (!Directory.Exists(sourceDir))
                {
                    Directory.Delete(destinationDir, true);
                    LogController.Log($"Deleted directory {dirName}", logFilePath);
                }
            }
        }
    }
}
