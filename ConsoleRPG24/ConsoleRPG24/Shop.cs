namespace ConsoleRPG24
{
    internal class Shop
    {
    

        public void EnterShop(string location)
        {
            if (location == "마을")
            {
                Console.WriteLine("마을");
                ShowVillageShop();
            }
            else if (location == "던전")
            {
                Console.WriteLine("?");
                ShowDungeonShop();
            }
            else
            {
                Console.WriteLine("?");
            }
        }

        public void ShowVillageShop()
        {
            Console.WriteLine("[마을 상점]");
            Console.WriteLine("1. 물약 구매 ");
            Console.WriteLine("2. 잡화 구매");
            Console.WriteLine("3. 장비 구매");
            Console.WriteLine("0. 나가기 ");
            Console.Write(">> ");
            string input = Console.ReadLine();
            if (input == "1")
            {

            }
            else if (input == "2")
            {

            }
            else if (input == "3")
            {

            }
            else if (input == "0")
            {
                Console.WriteLine("상점을 나갑니다.");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

        }

        public void ShowDungeonShop()
        {
            Console.WriteLine("[던전 상점]");
            Console.WriteLine("1. ");
            Console.WriteLine("2. ");
            Console.WriteLine("0. ");
            Console.Write(">> ");
            string input = Console.ReadLine();
            if (input == "1")
            {

            }
            else if (input == "2")
            {

            }
            else if (input == "0")
            {
                Console.WriteLine("상점을 나갑니다.");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

        }
        
      
    }
}
