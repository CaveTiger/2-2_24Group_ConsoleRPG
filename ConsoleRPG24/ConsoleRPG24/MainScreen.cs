using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace ConsoleRPG24
{

    internal partial class MainScreen
    {
        List<Item> itemList = new List<Item>();
        public Player player;
        private int pityCounter = 0;  // 🔹 90회 뽑으면 확정 지급 (pity 시스템)
        private int totalDraws = 0;  // 🔹 총 뽑기 횟수
        private List<Item> obtainedItems = new List<Item>();  // 🔹 중복 방지를 위한 획득 아이템 리스트

        public static MainScreen instance;

        public void GameStart()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                return;
            }

            InitItem();

            player = new Player();

            Thread.Sleep(1000);
            Console.WriteLine(new string('-', 30));
            Console.WriteLine(new string('=', 40));
            Console.WriteLine(new string('=', 40));
            Thread.Sleep(1500);
            Console.WriteLine();
            Console.WriteLine("당신은 눈을 떴다.");
            Console.WriteLine();
            Thread.Sleep(2500);
            Console.WriteLine("아주 긴 잠에서 깨어난 듯 하다.");
            Console.WriteLine();
            Thread.Sleep(2500);

            Console.Write("당신의 성함을 입력해 주십시오: ");
            player.Name = Console.ReadLine();

            Console.Clear();

            Console.WriteLine($"그래. 당신의 이름은 {player.Name}(이)다.");
            Thread.Sleep(2000);

            while (true)
            {


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
                    player.SetJobStats("전사");
                }

                else if (input == "2")
                {
                    player.SetJobStats("마법사");
                }

                else if (input == "3")
                {
                    player.SetJobStats("궁수");
                }

                else if (input == "4")
                {
                    player.SetJobStats("암살자");
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

            Villige();

            // 🔹 플레이어가 null이면 새로 생성
            if (player == null)
            {
                player = new Player();
            }

            InitItem();

            Console.WriteLine("게임을 시작합니다...");
            Thread.Sleep(1000);

            Console.Clear();
            Console.WriteLine($"환영합니다, {player.Name}({player.Job})님!");
            Thread.Sleep(1500);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("무엇을 할까?");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 마을");
                Console.WriteLine("0. 던전 입장");
                Console.Write(">> ");

                string chooseNum = Console.ReadLine();

                switch (chooseNum)
                {
                    case "1":
                        if (player != null)  // 🔹 플레이어가 null인지 체크
                        {
                            StatusScreen();
                        }
                        else
                        {
                            Console.WriteLine("플레이어 데이터가 초기화되지 않았습니다!");
                        }
                        break;
                    case "2":
                        InventoryScreen();
                        break;
                    case "3":
                        Village();
                        break;
                    case "0":
                        DungeonScreen();
                        break;
                    default:
                        Console.WriteLine("올바른 숫자를 입력해 주십시오.");
                        continue;
                }
            }

        }


        public void Villige()
        {
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
                Console.WriteLine($"최대체력: {player.MaxHealth}");
                Console.WriteLine($"속도: {player.Speed} ");
                Console.WriteLine($"치명타 확률: {Math.Round(player.CritHit * 100)}%");
                Console.WriteLine($"치명타 피해: {Math.Round(player.CritDmg * 100)}%");
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
            player.Inventory.OpenInventory();
        }


        public void Village()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("무엇을 할까?");
                Console.WriteLine(new string('=', 20));
                Console.WriteLine("1. 상점");
                Console.WriteLine("2. 운명의 숭배");
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
                    case "2":
                        GachaSystem();  //뽑기 시스템 호출
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


        private void GachaSystem()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("[ 운명의 숭배 ]");
                Console.WriteLine("1. 1회 숭배 (100 골드)");
                Console.WriteLine("2. 10회 숭배 (1000 골드)");
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.Write(">> ");

                string input = Console.ReadLine();

                if (input == "1")
                {
                    DrawItem(1);
                }
                else if (input == "2")
                {
                    DrawItem(10);
                }
                else if (input == "0")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                }
            }
        }

        private void DrawItem(int times)
        {
            Random rand = new Random();

            if (player.Gold < times * 100)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("골드가 부족합니다!");
                Console.ResetColor();
                Thread.Sleep(1500);
                return;
            }

            player.Gold -= times * 100;  // 🔹 골드 차감
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("뽑는 중 . . .");
            Console.ResetColor();
            Thread.Sleep(2000); // 🔹 2초 지연 (긴장감 유도)

            for (int i = 0; i < times; i++)
            {
                totalDraws++; // 🔹 총 뽑기 횟수 증가
                int roll = rand.Next(0, 100);  // 🔹 0~99 사이의 난수 생성

                if (roll < 1 || pityCounter >= 90)  // 🔹 1% 확률 or 90회 보장 지급
                {
                    Item specialItem = new Item("그리웠던 그때 그곳으로",
                        "언젠가...우린 과거의 그때로 돌아갈꺼야 오래된 전설처럼.",
                        "시작시 공격력이 2배 증가하며 체력이 점차 성장한다",
                        Rank.legend, Division.atk, 0);

                    if (!obtainedItems.Contains(specialItem)) // 🔹 중복 방지
                    {
                        player.Inventory.AddItem(specialItem);
                        player.EquipItem(specialItem);
                        obtainedItems.Add(specialItem);
                        pityCounter = 0;  // 🔹 확정 횟수 초기화

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("축하합니다! ");
                        PrintRainbowText("그리웠던 그때 그곳으로");
                        Console.WriteLine(" 획득!");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("이미 획득한 아이템입니다. 대신 200골드 지급!");
                        Console.ResetColor();
                        player.Gold += 200;
                    }
                }
                else if (roll < 2)  // 🔹 1% 확률로 `Legend` 아이템 획득
                {
                    GiveItemByRank(Rank.legend);
                }
                else if (roll < 5)  // 🔹 3% 확률로 `Epic` 아이템 획득
                {
                    GiveItemByRank(Rank.epic);
                }
                else if (roll < 10)  // 🔹 5% 확률로 `Rare` 아이템 획득
                {
                    GiveItemByRank(Rank.rare);
                }
                else if (roll < 30)  // 🔹 20% 확률로 `Common` 아이템 획득
                {
                    GiveItemByRank(Rank.common);
                }
                else  // 🔹 70% 확률로 골드 획득
                {
                    int refundGold = rand.Next(1, times == 1 ? 10 : 100);
                    player.Gold += refundGold;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{refundGold} 골드를 획득했습니다.");
                    Console.ResetColor();
                    pityCounter++;
                }

                Console.WriteLine($"총 뽑기 횟수: {totalDraws} 회"); // 🔹 뽑기 횟수 출력
                Thread.Sleep(1000);
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("뽑기 완료!");
            Console.ResetColor();
            Thread.Sleep(1500);
        }

        // 🔹 아이템 등급별 지급 (중복 방지 포함)
        private void GiveItemByRank(Rank rank)
        {
            List<Item> availableItems = itemList.Where(item => item.ItemRank == rank && !obtainedItems.Contains(item)).ToList();

            if (availableItems.Count > 0)
            {
                Random rand = new Random();
                Item selectedItem = availableItems[rand.Next(availableItems.Count)];
                player.Inventory.AddItem(selectedItem);
                obtainedItems.Add(selectedItem);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{rank} 아이템 '{selectedItem.ItemName}' 획득!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"이미 모든 {rank} 등급 아이템을 보유하고 있습니다. 대신 150 골드 지급!");
                Console.ResetColor();
                player.Gold += 150;
            }
        }

        private void PrintRainbowText(string text)
        {
            ConsoleColor[] rainbowColors =
                {
                    ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green,
                    ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Magenta
                };

            for (int i = 0; i < text.Length; i++)
            {
                Console.ForegroundColor = rainbowColors[i % rainbowColors.Length]; // 글자마다 다른 색 적용
                Console.Write(text[i]);
                Thread.Sleep(100); // 0.1초 간격으로 표시 (조절 가능)
            }

            Console.ResetColor(); // 색상 초기화
            Console.WriteLine(); // 줄 바꿈
        }

        public void VillageShop()
        {
            Console.Clear();
            Shop shop = new Shop(player, itemList);
            //shop.DisplayShopItems();
        }


        public void DungeonScreen()
        {
            Console.Clear();
            Stage stage = new Stage(player, itemList);
            stage.DungeonStart();
        }
    }
}