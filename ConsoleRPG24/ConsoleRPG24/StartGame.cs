using System.Collections.Generic;

namespace ConsoleRPG24
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainScreen game = new MainScreen();

            game.GameStart();
            //game.InventoryScreen();
        }
    }
}
