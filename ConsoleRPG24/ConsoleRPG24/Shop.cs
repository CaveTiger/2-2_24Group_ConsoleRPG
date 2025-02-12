using System.Numerics;

namespace ConsoleRPG24
{
    internal class Shop
    {
        Player player = new Player();

        List<Item> itemList = new List<Item>();         //받아올 아이템 리스트
        List<Item> newItems = new List<Item>();         //가지고 있지 않은 아이템 리스트
        List<Item> ItemCommon = new List<Item>();       //커먼 아이템 리스트
        List<Item> ItemRare = new List<Item>();         //레어 아이템 리스트
        List<Item> ItemEpic = new List<Item>();         //에픽 아이템 리스트
        List<Item> ItemLegend = new List<Item>();       //레전드 아이템 리스트

        public Shop(Player _player, List<Item> _itemList)
        {
            player = _player;
            itemList = _itemList;
            foreach (Item item in itemList)
            {
                if (!item.IsOwned)
                {
                    newItems.Add(item);
                }

                if (item.ItemRank == Rank.common && !item.IsOwned)
                {
                    ItemCommon.Add(item);
                }
                else if (item.ItemRank == Rank.rare && !item.IsOwned)
                {
                    ItemRare.Add(item);
                }
                else if (item.ItemRank == Rank.epic && !item.IsOwned)
                {
                    ItemEpic.Add(item);
                }
                else if (item.ItemRank == Rank.legend && !item.IsOwned)
                {
                    ItemLegend.Add(item);
                }
            }
            DisplayShopItems();
        }

        public void DisplayShopItems()
        {

            //43. 광대의 상자 : 상점 내 아이템 수량 1 감소, 대신 상점 진입시 랜덤 아이템 획득
            if (itemList[43].IsEquipped && itemList[43].IsOwned)
            {
                player.Inventory.AddItem(RandomItem());
            }

            int warningType = 0;                //경고 종류
            string infoText = "";               //안내문 내용

            //랜덤 아이템을 3번 중복없이 가져오기
            List<Item> randomThreeItems = new List<Item>();
            while (true)
            {
                //42. 택배 상자 : 상점 내 아이템 수량 1 증가
                if (itemList[42].IsEquipped && itemList[42].IsOwned)
                {
                    randomThreeItems.Add(RandomItem());
                }
                //43. 광대의 상자 : 상점 내 아이템 수량 1 감소, 대신 상점 진입시 랜덤 아이템 획득
                if (!(itemList[43].IsEquipped && itemList[43].IsOwned))
                {
                    randomThreeItems.Add(RandomItem());
                }
                randomThreeItems.Add(RandomItem());
                randomThreeItems.Add(RandomItem());
                if (randomThreeItems.Count() == randomThreeItems.Distinct().Count())
                {
                    break;
                }
            }

            //아이템 전시
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("==================[ 상 점 ]==================");
                Console.ResetColor();
                for (int i = 0; i < randomThreeItems.Count(); i++)
                {
                    switch (randomThreeItems[i].ItemRank)
                    {
                        case Rank.common:
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        case Rank.rare:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;
                        case Rank.epic:
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            break;
                        case Rank.legend:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        default: break;
                    }

                    //41. 할인 쿠폰 : 상점 내 아이템 가격 10% 감소(가격 표시)
                    if (itemList[41].IsOwned && itemList[41].IsEquipped)
                    {
                        Console.Write(i + 1 + " | " + String.Format("{0,-8}", randomThreeItems[i].ItemRank) +
                       " | " + String.Format("{0,-20}", randomThreeItems[i].ItemName) +
                       " | " + String.Format("{0,-3}원", randomThreeItems[i].ItemPrice * 9 / 10) +
                       " | " + String.Format("{0,-40}", randomThreeItems[i].EffectDescription));
                    }
                    else
                    {
                        Console.Write(i + 1 + " | " + String.Format("{0,-8}", randomThreeItems[i].ItemRank) +
                        " | " + String.Format("{0,-20}", randomThreeItems[i].ItemName) +
                        " | " + String.Format("{0,-3}원", randomThreeItems[i].ItemPrice) +
                        " | " + String.Format("{0,-40}", randomThreeItems[i].EffectDescription));

                    }
                    Console.ResetColor();
                    if (randomThreeItems[i].IsOwned)
                    {
                        Console.Write("[보유중]");
                    }
                    Console.WriteLine("");
                }

                //경고문 작성
                Console.WriteLine("");
                PrintWarningText(warningType);

                //안내문 작성
                PrintInfoText(infoText);
                Console.WriteLine("");

                //번호 선택 예외 고려 => -1로 처리
                Console.WriteLine($"현재 보유 골드 : {player.Gold}원");
                Console.WriteLine("");
                Console.Write("사고싶은 아이템의 번호를 입력해주세요 (나가기 : 0)>> ");
                int itemIndex = -1;
                try
                {
                    itemIndex = int.Parse(Console.ReadLine());
                }
                catch
                {
                    itemIndex = -1;
                }
                //구매
                if (itemIndex >= 1 && itemIndex <= randomThreeItems.Count)
                {
                    //구매 실패(이미 보유중)
                    if (randomThreeItems[itemIndex - 1].IsOwned)
                    {
                        warningType = 2;
                        infoText = "";
                    }
                    //구매 실패(돈 부족)
                    else if (player.Gold < randomThreeItems[itemIndex - 1].ItemPrice)
                    {
                        warningType = 1;
                        infoText = "";
                    }
                    //구매 성공
                    else
                    {
                        //41. 할인 쿠폰 : 상점 내 아이템 가격 10% 감소
                        if (itemList[41].IsOwned && itemList[41].IsEquipped)
                        {
                            player.Gold -= randomThreeItems[itemIndex - 1].ItemPrice * 9 / 10;
                        }
                        else
                        {
                            player.Gold -= randomThreeItems[itemIndex - 1].ItemPrice;
                        }

                        player.Inventory.AddItem(randomThreeItems[itemIndex - 1]);
                        warningType = 0;
                        infoText = $"{randomThreeItems[itemIndex - 1].ItemName} 아이템을 구매하였습니다!";
                    }

                }
                //나가기
                else if (itemIndex == 0)
                {
                    break;
                }
                //잘못된 입력
                else
                {
                    warningType = 3;
                    infoText = "";
                }
            }
        }

        //경고문 타입에 따른 경고문 출력
        public void PrintWarningText(int warningType)
        {
            //돈 없어서 못사는 경우
            if (warningType == 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("현재 보유한 골드가 부족하여 아이템 구매에 실패하였습니다!");
                Console.ResetColor();
            }
            //이미 보유중인 경우
            else if (warningType == 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("이미 보유중인 아이템입니다!");
                Console.ResetColor();
            }
            //잘못된 입력인 경우
            else if (warningType == 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("잘못된 입력입니다!");
                Console.ResetColor();
            }
            else
            {
                Console.Write("");
            }
        }

        public void PrintInfoText(string infoText)
        {
            if (infoText != "")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(infoText);
                Console.ResetColor();
            }
        }

        //랜덤 아이템 추출기
        public Item RandomItem()
        {
            Random randomRank = new Random();
            while (true)
            {
                int randomRankIndex = randomRank.Next(1, 101);
                //전설 10%
                if (randomRankIndex >= 1 && randomRankIndex <= 10 && ItemLegend.Count != 0)
                {
                    int legendIndex = randomRank.Next(0, ItemLegend.Count);
                    return ItemLegend[legendIndex];
                }
                //에픽 20%
                else if (randomRankIndex >= 11 && randomRankIndex <= 30 && ItemEpic.Count != 0)
                {
                    int epicIndex = randomRank.Next(0, ItemEpic.Count);
                    return ItemEpic[epicIndex];
                }
                //레어 30%
                else if (randomRankIndex >= 31 && randomRankIndex <= 60 && ItemRare.Count != 0)
                {
                    int rareIndex = randomRank.Next(0, ItemRare.Count);
                    return ItemRare[rareIndex];
                }
                //나머지(common) 40%
                else if (randomRankIndex >= 61 && randomRankIndex <= 100 && ItemCommon.Count != 0)
                {
                    int commonIndex = randomRank.Next(0, ItemCommon.Count);
                    return ItemCommon[commonIndex];
                }
                else { }
            }
        }
    }
}
