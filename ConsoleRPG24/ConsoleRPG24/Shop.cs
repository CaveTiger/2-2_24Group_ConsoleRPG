namespace ConsoleRPG24
{
    internal class Shop
    {


        public void ShowVillageShop()
        {
            Console.WriteLine("[마을 상점]");
            Console.WriteLine("1. 장비 구매 ");
            Console.WriteLine("0. 뒤로가기 ");
            Console.Write(">> ");
            string input = Console.ReadLine();
            if (input == "1")
            {
                Console.WriteLine(" 장비 상점 ");
                VillageShop();
            }
            else if (input == "0")
            {
                Console.WriteLine("상점을 나갑니다.");
                return;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

        }

        public void VillageShop()
        {
            // MainScreen_JHK 아이템 리스크를 불러오기
            // Common 등급 아이템 3가지를 랜덤하게 플레이어에게 보여주기
            // (아이템 3개 옵션과 효과 보여주기)
            // 플레이어가 아이템을 택1 해서 구매 -> 인벤토리로 보내주기
        }

        public void ShowDungeonShop()
        {
            Console.WriteLine("[던전 상점]");
            Console.WriteLine("1. 장비 구매 ");
            Console.WriteLine("0. 뒤로가기 ");
            Console.Write(">> ");
            string input = Console.ReadLine();
            if (input == "1")
            {
                Console.WriteLine(" 장비 상점 ");
                DungeonShop();
            }
            else if (input == "0")
            {
                Console.WriteLine("상점을 나갑니다.");
                return;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

        }

        public void DungeonShop()
        {
            //
            //
            //
            //
            //
            //

        }

    }

}
