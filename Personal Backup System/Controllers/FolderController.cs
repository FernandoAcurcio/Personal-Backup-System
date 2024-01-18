namespace Personal_Backup_System.Controllers
{
    /// <summary>
    /// Opens a folder browser dialog and returns the selected folder path.
    /// </summary>
    public static class FolderController
    {
        public static string OpenFolderBrowser()
        {
            string folderPath = string.Empty;
            
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowserDialog.SelectedPath;
            }
            return folderPath;
        }
    }
}
