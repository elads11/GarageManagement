// $G$ RUL-007 (-120) Late submission (2 points per day).
// $G$ RUL-001 (-20) Email - Wrong subject format.
using System;

namespace Ex03.ConsoleUI
{
    public class Program
    {
        public static void Main()
        {
            UserInterface UI = new UserInterface();

            UI.StartUpMenu();
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
