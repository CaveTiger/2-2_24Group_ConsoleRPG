using System;

//git organazation

namespace ConsoleRPG24
{

    //던전 아직 구현 중입니다!!


    public class Stage
    {
        private Player player;


        public void Start()
        {
            BattleSystem battleSystem = new BattleSystem();
            battleSystem.BattleStart();
        }

        
        public void DungeonStart()
        {
            Start();

            //if (player.IsDead) return;

            Camp camp = new Camp();
            camp.Camping(player);

            Console.WriteLine("축하합니다! 모든 스테이지를 클리어했습니다!");
        }
    }
}

