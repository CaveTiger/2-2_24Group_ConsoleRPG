using System;
using System.Runtime.CompilerServices;
using System.Threading;

//git organazation

namespace ConsoleRPG24
{

    //던전 아직 구현 중입니다!!

    public class Stage
    {
        Player player;
        Monster monster;
        //뭔가 이부분 수정이 필요해보임...

        //열심히 작성 중~~
        public void Rewards()
        {
            if(!(player.IsDead))
            {
                Console.WriteLine($"스테이지 클리어! {monster.Name}를 물리쳤다!");
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

                else
                {
                    List<Item> itemList = new List<Item>();
                    Random random = new Random();

                    int index1 = random.Next(itemList.Count);
                    int index2 = random.Next(itemList.Count);

                    while (index2 == index1)
                    {
                        index2 = random.Next(itemList.Count);
                    }

                    Console.WriteLine($"{itemList[index1]}과 {itemList[index2]}중 무엇을 얻을까?");

                    string input02;
                    input02 = Console.ReadLine();

                    if (input02 == "1")
                    {
                        Inventory inventory = new Inventory();
                        inventory.AddItem(itemList[index1]);

                        Console.WriteLine($"{itemList[index1]}(을)를 획득했다!");
                    }

                    else
                    {
                        Inventory inventory = new Inventory();
                        inventory.AddItem(itemList[index2]);

                        Console.WriteLine($"{itemList[index2]}(을)를 획득했다!");
                    }
                }

                //다음 전투로~!

            }

            else if (player.IsDead)
            {
                Console.WriteLine("당신은 눈 앞이 깜깜해졌다...");
                Console.WriteLine("게임 오버!......");
            }
        }

        //열심히 작성 중~


        public void Start()
        {
            BattleSystem battleSystem = new BattleSystem();
            battleSystem.BattleStart();

            //battleSystem.Rewards();
        }


        public void DungeonStart()
        {
            int battleCount = 0;

            while (true)
            {
                for (int i = 0; i <= 19; i++)
                {
                    Start();

                    if (player.IsDead)
                    {
                        return;
                    }

                    Rewards();

                    Camp camp = new Camp(player);
                    camp.CampCount();

                    battleCount ++;
                    if (battleCount == 5 || battleCount == 10 || battleCount == 15)
                    {
                        ShopEncounter();
                    }

                    if (battleCount == 20)
                    {
                        //최종보스전
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
                Shop shop = new Shop();
                shop.ShowDungeonShop();
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

