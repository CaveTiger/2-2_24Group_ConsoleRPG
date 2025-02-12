using System;

namespace ConsoleRPG24
{
    internal class Shop
    {
        List<Item> ItemRankcommon = new List<Item>();

        List<Item> ItemRankepic = new List<Item>();
        List<Item> ItemRanklegend = new List<Item>();

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
        Console.WriteLine("상점에 있는 아이템 목록:");

        ShowItemsForRank("Common", ItemRankcommon);
       
}

        private void ShowItemsForRank(string rankName, List<Item> rankList)
        {
            Console.WriteLine($"{rankName} 등급 아이템:");

            if (rankList.Count == 0)
            {
                Console.WriteLine("아이템이 없습니다.");
                return;
            }

            for (int i = 0; i < rankList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {rankList[i].ItemName} - 가격: {rankList[i].ItemPrice} 골드");
            }
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
           
        }
        



}

}
