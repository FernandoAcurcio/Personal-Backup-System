using Personal_Backup_System.Data;

namespace Personal_Backup_System.Controllers
{
    public static class MenuController
    {
        public static UserData? UserData;

        public static int MenuHandler()
        {
            // load user data
            var userdata = UserDataHandler.LoadUserDataFromJson();
            // check if was retrived any user data
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
                        else if (result == 4) UserData.SyncInterval = ReadMenu(); 
                        else if (result == 5) UserDataHandler.SaveUserDataToJson(UserData); 
                        break;
                    case 2:
                        menuSelector = Menu2();
                        break;
                }
            } while (result != 0);

            return result;
        }

        private static int Menu0(string header)
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
        private static int Menu1(string header)
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
            Console.WriteLine($"Intervals: {UserData?.SyncInterval}");

            return ReadMenu();
        }

        private static int Menu2() 
        {
            Console.WriteLine("0- To Return");

            System.Timers.Timer timer = new System.Timers.Timer();

            timer.Interval = UserData.SyncInterval * 1000; // Convert seconds to milliseconds
            timer.Elapsed += (sender, e) => SynchronizeController.Synchronize(UserData.SourcePath, UserData.DestinationPath, UserData.LogPath);
            timer.Start();

            int menuSelector;
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    // check if the key that was pressed is 0
                    if (Console.ReadKey(true).Key == ConsoleKey.D0 || Console.ReadKey(true).Key == ConsoleKey.NumPad0)
                    {
                        timer.Stop();
                        menuSelector = 0;
                        break;
                    }
                }
            }
            return menuSelector;
        }


        private static int ReadMenu()
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
