namespace Personal_Backup_System.Data
{
    public class UserData
    {
        public string? SourcePath { get; set; }
        public string? DestinationPath { get; set; }
        public string? LogPath { get; set; }
        public int SyncInterval { get; set; }
    }
}
