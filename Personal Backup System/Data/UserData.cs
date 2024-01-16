using System.Text.Json;

namespace Personal_Backup_System.Data
{
    public class UserData
    {
        public string? SourcePath { get; set; }
        public string? DestinationPath { get; set; }
        public string? LogPath { get; set; }
        public int SyncIntervals { get; set; }
    }

    public class UserDataHandler
    {
        public static void SaveUserDataToJson(UserData userData, string fileName)
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            string filePath = Path.Combine(currentDirectory, fileName);

            string jsonData = JsonSerializer.Serialize(userData);
            File.WriteAllText(filePath, jsonData);
        }

        public static UserData? LoadUserDataFromJson(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<UserData>(jsonData);
            }

            return null;
        }
    }
}
