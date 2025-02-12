using System;

namespace ConsoleRPG24
{
    internal class Shop
    {
        
        List<Item> itemList = new List<Item>();

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
        

        List<Item> ItemRankcommon = new List<Item>();
        List<Item> ItemRankrare = new List<Item>();
        List<Item> ItemRankepic = new List<Item>();
        List<Item> ItemRanklegend = new List<Item>();

        //Random random = new Random();

        // common 등급 아이템만 필터링
        //List<Item> commonItems = itemList.Where(item => item.ItemRank == Rank.common).ToList();

        // common 등급 아이템이 3개 이상 있어야 실행 가능
        //if (commonItems.Count >= 3)
        //{
        //    int index1 = random.Next(commonItems.Count);
        //    int index2 = random.Next(commonItems.Count);
        //    int index3 = random.Next(commonItems.Count);

        //    Item selectedItem1 = commonItems[index1];
        //    Item selectedItem2 = commonItems[index2];
        //    Item selectedItem3 = commonItems[index3];

        //    AddItemToRankList(selectedItem1, ItemRankcommon, ItemRankrare, ItemRankepic, ItemRanklegend);
        //    AddItemToRankList(selectedItem2, ItemRankcommon, ItemRankrare, ItemRankepic, ItemRanklegend);
        //    AddItemToRankList(selectedItem3, ItemRankcommon, ItemRankrare, ItemRankepic, ItemRanklegend);
        //}
        //else
        //{
        //    Console.WriteLine("⚠️ common 등급 아이템이 부족합니다.");
        //}
  
        private void AddItemToRankList(Item item, List<Item> common, List<Item> rare, List<Item> epic, List<Item> legend)
        {
           
            switch (item.ItemRank)  // ✅ 올바른 속성으로 변경
            {
                case Rank.common:
                    common.Add(item);
                    break;
                case Rank.rare:
                    rare.Add(item);
                    break;
                case Rank.epic:
                    epic.Add(item);
                    break;
                case Rank.legend:
                    legend.Add(item);
                    break;
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
            // MainScreen_JHK 아이템 리스크를 불러오기
            // 던전 상점 입장시, 입장한 던전의 스테이지에 따라 아이템 등급 나누기
            // 위에 나누어진 등급에 따라 아이템 3가지를 랜덤하게 플레이어에게 보여주기
            // (아이템 3개 옵션과 효과 보여주기)
            // 플레이어가 아이템을 택1 해서 구매 -> 인벤토리로 보내주기

        }
        



}

}
