namespace StartGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainScreen game = new MainScreen();

            game.GameStart();
            game.InventoryScreen();
        }
    }
}
