using Personal_Backup_System.Controllers;

internal class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        MenuController.MenuHandler();

        Console.Clear();
        Console.WriteLine("*************************************");
        Console.WriteLine("***           Goodbye             ***");
        Console.WriteLine("*************************************");
        Console.ReadLine();
    }
}