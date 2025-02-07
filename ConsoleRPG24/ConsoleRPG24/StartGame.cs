using System.Collections.Generic;

namespace StartGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainScreen game = new MainScreen();

            List<Item> itemlist = new List<Item>();

            game.GameStart();
            game.InventoryScreen();
        }
    }
}
