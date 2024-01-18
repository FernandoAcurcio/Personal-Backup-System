using System.Text.Json;

namespace Personal_Backup_System.Data
{
    public class UserDataHandler
    {
        private const string UserDataFileName = "userData.json";

        public static void SaveUserDataToJson(UserData userData)
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            string filePath = Path.Combine(currentDirectory, UserDataFileName);

            string jsonData = JsonSerializer.Serialize(userData);
            File.WriteAllText(filePath, jsonData);
        }

        public static UserData? LoadUserDataFromJson()
        {
            if (File.Exists(UserDataFileName))
            {
                string jsonData = File.ReadAllText(UserDataFileName);
                return JsonSerializer.Deserialize<UserData>(jsonData);
            }

            return null;
        }
    }
}
