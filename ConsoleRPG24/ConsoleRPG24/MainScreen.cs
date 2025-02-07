using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleRPG24
{
    internal partial class MainScreen
    {
        List<Item> itemlist = new List<Item>();

        public void GameStart()
        {
            string userName;

            /*
            Thread.Sleep(1000);
            Console.WriteLine("당신은 눈을 떴다.");
            Thread.Sleep(2500);
            Console.WriteLine("아주 긴 잠에서 깨어난 듯 하다.");
            Thread.Sleep(2500);
            */

            Console.Write("당신의 성함을 입력해 주십시오: ");
            userName = Console.ReadLine();
            Console.Clear();

            Console.WriteLine($"그래. 당신의 이름은 {userName}(이)다.");
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

                    //case ("3"):

                    //    MercenaryManager mercenaryManager = new MercenaryManager();
                    //    mercenaryManager.ShowMercenaries();
                    //    break;

                    case ("3"):

                        Village();
                        break ;

                    /*
                    case ("0"):

                        DungeonStage dungeonStage = new DungeonStage();
                        dungeonStage.Start();
                        break;
                    */
                        

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
                //Console.WriteLine("2. 용병소");
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

                        //VillageShop();
                        break;

                    ////case ("2"):

                    //    MercenaryShop();
                    //    break;

                    case ("0"):

                        GameStart();
                        break;

                    default:
                        Console.WriteLine("올바른 숫자를 입력해 주십시오.");
                        continue;
                }
            }
        }

        /*
        public void VillageShop()
        {
            Shop shop = new Shop();
            shop.ShowVillageShop();
        }
        */

        //public void MercenaryShop()

        public void DungeonScreen()
        {

        }
    }
}
