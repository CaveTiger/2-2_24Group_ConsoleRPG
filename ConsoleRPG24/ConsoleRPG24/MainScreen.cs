using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace ConsoleRPG24
{

    internal partial class MainScreen
    {
        List<Item> itemList = new List<Item>();

        private Player player;

        public void GameStart()
        {
            InitItem();
            //DisplayItems();

            string userName;

            Thread.Sleep(1000);
            Console.WriteLine(new string('=', 20));
            Console.WriteLine();
            Console.WriteLine("당신은 눈을 떴다.");
            Console.WriteLine();
            Thread.Sleep(2500);
            Console.WriteLine("아주 긴 잠에서 깨어난 듯 하다.");
            Console.WriteLine();
            Thread.Sleep(2500);

            Console.Write("당신의 성함을 입력해 주십시오: ");
            userName = Console.ReadLine();


            Console.Clear();

            Console.WriteLine($"그래. 당신의 이름은 {userName}(이)다.");
            Thread.Sleep(2000);


            while (true)
            {
                player = new Player();

                string input;

                Console.WriteLine("당신은 이전부터 어떤 일을 해왔지?");
                Thread.Sleep(2000);
                Console.WriteLine();
                Console.WriteLine("1. 전사");
                Console.WriteLine("2. 마법사");
                Console.WriteLine("3. 궁수");
                Console.WriteLine("4. 암살자");
                Console.WriteLine();
                Console.Write(">> ");
                input = Console.ReadLine();

                if (input == "1")
                {
                    player.Job = "전사";
                }

                else if (input == "2")
                {
                    player.Job = "마법사";
                }

                else if (input == "3")
                {
                    player.Job = "궁수";
                }

                else if (input == "4")
                {
                    player.Job = "암살자";
                }

                else
                {
                    Console.WriteLine();
                    Console.WriteLine("자신이 했을만한 직업은 저 네가지 이외엔 없다는 확신이 든다.");
                    continue;
                }


                

                break;
            }


            Console.WriteLine($"당신의 직업은 {player.Job}(이)다.");
            Thread.Sleep(2000);


            while (true)
            {
                Console.Clear();
                Console.WriteLine("무엇을 할까?");
                Console.WriteLine(new string('=', 20));
                Console.WriteLine();
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 마을");
                Console.WriteLine();
                Console.WriteLine(new string('-', 20));
                Console.WriteLine("0. 던전 입장");
                Console.WriteLine(new string('-', 20));
                Console.WriteLine();
                Console.WriteLine(new string('=', 20));
                Console.WriteLine();
                Console.Write(">>");

                string chooseNum;
                chooseNum = Console.ReadLine();

                switch (chooseNum)
                {
                    case ("1"):

                        StatusScreen();
                        break;

                    case ("2"):

                        InventoryScreen();
                        break;

                    case ("3"):

                        Village();
                        break;


                    case ("0"):

                        DungeonScreen();
                        break;



                    default:
                        Console.WriteLine("올바른 숫자를 입력해 주십시오.");
                        continue;
                }
            }
        }


        public void StatusScreen()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                Console.WriteLine();

                Console.WriteLine($"{player.Name} ({player.Job})");
                Console.WriteLine($"공격력: {player.Atk}");
                Console.WriteLine($"방어력: {player.Defen}");
                Console.WriteLine($"체력: {player.Health}");
                Console.WriteLine($"Gold: {player.Gold}");

                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");

                string chooseNum;
                chooseNum = Console.ReadLine();

                switch (chooseNum)
                {
                    case ("0"):
                        Console.Clear();
                        return;

                    default:
                        Console.WriteLine("올바른 숫자를 입력해 주십시오.");
                        continue;
                }
            }
        }


        public void InventoryScreen()
        {
            Console.Clear();
            //inventory.OpenInventory(); // 기존 인벤토리를 유지하며 사용
            Inventory inventory = new Inventory();
            inventory.OpenInventory();

        }


        public void Village()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("무엇을 할까?");
                Console.WriteLine(new string('=', 20));
                Console.WriteLine("1. 상점");
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine(new string('=', 20));
                Console.WriteLine();
                Console.Write(">>");

                string chooseNum;
                chooseNum = Console.ReadLine();

                switch (chooseNum)
                {
                    case ("1"):

                        VillageShop();
                        break;

                    case ("0"):

                        Console.Clear();
                        return;

                    default:
                        Console.WriteLine("올바른 숫자를 입력해 주십시오.");
                        continue;
                }
            }
        }


        public void VillageShop()
        {
            Console.Clear();
            Shop shop = new Shop();
            shop.ShowVillageShop();
        }



        public void DungeonScreen()
        {
            Console.Clear();
            Stage stage = new Stage();
            stage.DungeonStart();
        }
    }
}
