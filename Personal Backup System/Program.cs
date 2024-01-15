using Microsoft.VisualBasic.ApplicationServices;
using Personal_Backup_System;
using Personal_Backup_System.Controllers;
using Personal_Backup_System.Data;
using System.Security.Cryptography.X509Certificates;

internal class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        UserData userData = new UserData();
        MenuController.MenuHandler();

        Console.Clear();
        Console.WriteLine("*************************************");
        Console.WriteLine("***           Goodbye             ***");
        Console.WriteLine("*************************************");
        Console.ReadLine();
    }
}