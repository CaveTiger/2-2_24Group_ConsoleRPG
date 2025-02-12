using System;
using System.Runtime.CompilerServices;
using System.Threading;

//git organazation

namespace ConsoleRPG24
{

    //던전 아직 구현 중입니다!!

    internal class Stage
    {
        Player player;
        List<Item> itemList = new List<Item>();

        BattleSystem battleSystem;

        public int battleCount = 0;

        //열심히 작성 중~~

        public Stage(Player _player, List<Item> _itemList) //class Stage의 접근자를 internal로 해야 오류 안남
        {
            player = _player;
            itemList = _itemList;
        }

        //
        public void Rewards(Player player)
        {
            if (MainScreen.instance.player.IsDead)

                Console.WriteLine($"스테이지 클리어! 적을 물리쳤다!");
            Console.WriteLine("전리품으로써 골드 혹은 아이템을 보상으로 얻을 수 있다.");
            Console.WriteLine();
            Console.WriteLine("1. 골드를 얻는다");
            Console.WriteLine("2. 아이템을 얻는다");
            Console.WriteLine();
            Console.WriteLine("0. 무시한다");
            Console.WriteLine();
            Console.WriteLine("무엇을 하시겠습니까?");
            Console.WriteLine(">> ");

            string input;
            input = Console.ReadLine();

            if (input == "1")
            {
                player.Gold += 100;
                Console.WriteLine("당신은 100 G를 얻었다.");
            }

            else if (input == "2")
            {
                //List<Item> itemList = new List<Item>();

                Random random = new Random();

                int index1 = random.Next(itemList.Count);
                int index2 = random.Next(itemList.Count);

                //index1을 먼저 검사 갖고있는지 아닌지
                //index2를 검사할 때 갖고있나아닌가 index1이랑 동시에 검사
                while (itemList[index1].IsOwned)
                {
                    index1 = random.Next(itemList.Count);
                }


                while (index2 == index1 && itemList[index2].IsOwned)
                {
                    index2 = random.Next(itemList.Count);
                }

                Console.WriteLine($"{itemList[index1].ItemName}과 {itemList[index2].ItemName}중 무엇을 얻을까?");


                string input02;
                input02 = Console.ReadLine();

                if (input02 == "1")
                {
                    player.Inventory.Inven.Add(itemList[index1]);

                    Console.WriteLine($"{itemList[index1]}(을)를 획득했다!");
                }

                else
                {
                    player.Inventory.Inven.Add(itemList[index1]);

                    Console.WriteLine($"{itemList[index2]}(을)를 획득했다!");
                }
            }

            battleSystem.Battle();

        }

        //열심히 작성 중~
        //rewards에서는 보상만,,,


        //이 메소드로 던전 전투 시작!!
        public void Start()
        {
            //23. 포도주가 담긴 성배 : 다음 구역 진입시마다 + 최대 체력 2%
            if (itemList[23].IsOwned && itemList[23].IsEquipped)
            {
                player.MaxHealth += (player.BaseHealth * (2 / 100));
            }
            //37. 부자의 증표 : 다음 구역 진입시 보유 골드 5% 증가
            if (itemList[37].IsOwned && itemList[37].IsEquipped)
            {
                player.Gold += (player.Gold * (5 / 100));
            }

            battleCount++;

            BattleSystem battleSystem = new BattleSystem(player, itemList, this);
            battleSystem.Battle();
        }



        //던전 20번 깨는 반복문
        public void DungeonStart()
        {
            while (true)
            {
                for (; battleCount <= 19; battleCount++)
                {
                    Start();

                    if (player.IsDead)
                    {
                        return;
                    }

                    Rewards(player);

                    Camp camp = new Camp(player);
                    camp.CampCount();

                    if (battleCount == 20)
                    {
                        BattleSystem battleSystem = new BattleSystem(player, itemList, this);
                        battleSystem.BossBattle();
                    }

                    if (battleCount % 5 == 0)
                    {
                        ShopEncounter();
                    }
                }

                //5번, 10번 15번 배틀 후 상점 등장!
                //20번 배틀에서는 최종보스 등장 → 이후 클리어~!
                //int stage = 20일때 최종보스전
                break;
            }

            GameClear();
        }


        public void ShopEncounter()
        {
            Console.WriteLine("당신은 다음으로 나아가던 중, 던전 안에 숨겨져있던 비밀 상점을 발견했다.");
            Console.WriteLine();
            Console.WriteLine("1. 비밀 상점 진입");
            Console.WriteLine();
            Console.WriteLine("0. 무시한다");
            Console.WriteLine();
            Console.WriteLine("무엇을 하시겠습니까?");
            Console.WriteLine(">> ");
            string input;
            input = Console.ReadLine();

            if (input == "1")
            {
                Console.Clear();
                Shop shop = new Shop(player, itemList);
                //Shop shop = new Shop();
                //shop.ShowDungeonShop();
            }

            else
            {
                DungeonStart();
            }
        }



        public void GameClear()
        {
            Console.WriteLine("당신은 던전의 모든 스테이지를 클리어했다!!");
            Console.WriteLine("그 소문은 빠르게 퍼져, 대륙 전체가 당신의 위상을 알게 되었다.");
            Console.WriteLine();
            Thread.Sleep(1500);
            Console.WriteLine("당신은 전설적인 모험가가 되었습니다.");
        }
    }
}

