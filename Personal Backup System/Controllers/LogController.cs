using System.Text.Json;

namespace Personal_Backup_System.Controllers
{
    public class LogController
    {
        private const string LogDataFileName = "logData.json";

        public static void Log(string message, string logFilePath)
        {
            var combinePath = Path.Combine(logFilePath, LogDataFileName);

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            LogMessage logMessage = new LogMessage { Timestamp = timestamp, Message = message };
            
            // Write logMessage to log file
            List<LogMessage> logMessages;
            if (File.Exists(combinePath))
            {
                // If the log file exists, read and deserialize the existing content
                string existingJson = File.ReadAllText(combinePath);
                logMessages = JsonSerializer.Deserialize<List<LogMessage>>(existingJson) ?? new List<LogMessage>();
            }
            else
            {
                // If the log file doesn't exist, create a new list
                logMessages = new List<LogMessage>();
            }

            // Add the new log message and serialize the list back to JSON
            logMessages.Add(logMessage);
            string json = JsonSerializer.Serialize(logMessages, new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON string to the log file, creating it if it doesn't exist
            File.WriteAllText(combinePath, json);
        }
    }
}
