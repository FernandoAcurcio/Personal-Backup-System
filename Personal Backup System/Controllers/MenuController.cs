using Personal_Backup_System.Data;

namespace Personal_Backup_System.Controllers
{
    public static class MenuController
    {
        private static string? _jsonName;
        public static UserData? UserData;

        public static int MenuHandler()
        {
            _jsonName = "userData.json";

            // load user data
            var userdata = UserDataHandler.LoadUserDataFromJson(_jsonName);
            UserData = userdata != null ? userdata : new UserData();

            int menuSelector = 0;
            int result = 0;
            do
            {
                Console.Clear();
                switch (menuSelector)
                {
                    case 0:
                        result = Menu0("*** Welcome To File Backup System ***");
                        if (result == 1) menuSelector = 1;
                        else if (result == 2) menuSelector = 2;
                        break;
                    case 1:
                        result = Menu1("*** Options                       ***");
                        if (result == 0) { result = menuSelector; menuSelector--; }
                        else if (result == 1) UserData.SourcePath = FolderController.OpenFolderBrowser();
                        else if (result == 2) UserData.DestinationPath = FolderController.OpenFolderBrowser();
                        else if (result == 3) UserData.LogPath = FolderController.OpenFolderBrowser();
                        else if (result == 4) UserData.SyncIntervals = ReadMenu(); 
                        else if (result == 5) UserDataHandler.SaveUserDataToJson(UserData, _jsonName); 
                        break;
                    case 2:
                        SynchronizeController.Synchronize(MenuController.UserData.SourcePath, MenuController.UserData.DestinationPath);
                        break;
                }
            } while (result != 0);

            return result;
        }

        public static int Menu0(string header)
        {
            Console.WriteLine("*************************************");
            Console.WriteLine($"{header}");
            Console.WriteLine("*************************************");
            Console.WriteLine("\nSelect One Of The Following Options");
            Console.WriteLine("0- Exit");
            Console.WriteLine("1- Options");
            Console.WriteLine("2- Sync Folder");

            return ReadMenu();

        }


        // Initial Menu
        public static int Menu1(string header)
        {
            Console.WriteLine("*************************************");
            Console.WriteLine($"{header}");
            Console.WriteLine("*************************************");
            Console.WriteLine("\nSelect One Of The Following Options");
            Console.WriteLine("0- Return To Previous Menu");
            Console.WriteLine("1- Select Source Folder");
            Console.WriteLine("2- Select Destination Folder");
            Console.WriteLine("3- Select Log Path Folder");
            Console.WriteLine("4- Select Synchronization Intervals");
            Console.WriteLine("5- Save Options");
            Console.WriteLine($"\nSource Folder: {UserData?.SourcePath}");
            Console.WriteLine($"Destination Folder: {UserData?.DestinationPath}");
            Console.WriteLine($"Log Folder: {UserData?.LogPath}");
            Console.WriteLine($"Intervals: {UserData?.SyncIntervals}");

            return ReadMenu();
        }

        public static int Menu2(string header) 
        {
            Console.WriteLine("*************************************");
            Console.WriteLine($"{header}");
            Console.WriteLine("*************************************");
            Console.WriteLine("\nSelect One Of The Following Options");
            Console.WriteLine("0- Return To Previous Menu");
            Console.WriteLine("1- Select Folder");
            
            return ReadMenu();
        }

        public static int ReadMenu()
        {
            if (!int.TryParse(Console.ReadLine(), out var result))                
            {
                result = -1;
                Console.WriteLine("Invalid Input!!!");
                Console.ReadLine();
            }
            return result;
        }
    }

}
