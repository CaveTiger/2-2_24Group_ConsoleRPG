using System;
using System.Threading;

//git organazation

namespace ConsoleRPG24
{

    //던전 아직 구현 중입니다!!

    public class Stage
    {
        Player player;
        Monster monster;

        public Stage(Player player, Monster monster)
        {
            this.player = player;
            this.monster = monster;

            //??????????
        }


        //열심히 작성 중~
        public void Rewards()
        {
            if (monster.Health <= 0)
            {
                Console.WriteLine($"스테이지 클리어! {monster.Name}를 물리쳤습니다!");

                //골드 혹은 아이템 보상
                //다음 전투로~!

            }

            else if (player.Health <= 0)
            {
                Console.WriteLine("게임 오버! 패배했습니다...");
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
            while (true)
            {
                for (int i = 0; i <= 4; i++)
                {
                    Start();

                    if (player.IsDead)
                    {
                        return;
                    }

                    Rewards();

                    Camp camp = new Camp(player);
                    camp.CampCount();
                }

                ShopEncounter();

                //5번, 10번 15번 배틀 후 상점 등장!
                //20번 배틀에서는 최종보스 등장 → 이후 클리어~!

                break;
            }
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

