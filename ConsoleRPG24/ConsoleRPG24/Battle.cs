using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG24
{
    internal static class BattleSystem
    {
        public static bool turnChance = false;
        public static void CreateMonster()
        {
            Random rand = new Random();
        }
        public static void BattleStart()
        {
            Console.WriteLine("적을 조우했습니다.");
            Console.ReadKey();
            Console.Write($"뱀을 만났습니다.");
            Console.Write($"개구리을 만났습니다.");
            Console.Write($"뫼옹을 만났습니다.");
            Console.Write($"스타을 만났습니다.");

        }


        public static void Chance()
        {
            for (int turnCheck = 21; turnCheck > 0; turnCheck--)
            {
                Console.WriteLine("hellow");
                Console.WriteLine($"{turnCheck}.");
                Console.ReadKey();
                if (true)
                {
                    while (turnCheck >=1)
                    {   
                        int[] skillCheck = { 0,1,2,3,4,5,6,7,8,9 };
                        Random rand = new Random();

                        int randomIndex = rand.Next(skillCheck.Length);
                        int randomSkill = skillCheck[randomIndex];
                        Console.WriteLine($"랜덤으로 선택된 스킬 사용 눈 : {randomSkill}");
                        
                        if (randomSkill >= 7)
                        {
                            BattleSkill();
                        }
                        else
                        {
                            BattleAttack();
                        }
                        break;
                    }
                }

            }

            static void BattleAttack()
            {
                Console.WriteLine("일반 공격");
                if(monster.turnChance == )
                {

                }
            }
            static void BattleSkill()
            {
                Console.WriteLine("스킬 사용");
                Console.WriteLine("스킬 사용");
            }

        }
    }
}


