using System.Security.Cryptography;

namespace Personal_Backup_System.Controllers
{
    public static class CalculateHashController
    {
        /// <summary>
        /// calculate the MD5 of a given file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string? CalculateMD5Hash(string filename)
        {
            try
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(filename))
                    {
                        var hash = md5.ComputeHash(stream);
                        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Could not calculate hash for file {filename}: {ex.Message}");
                return null;
            }
        }
    }
}
