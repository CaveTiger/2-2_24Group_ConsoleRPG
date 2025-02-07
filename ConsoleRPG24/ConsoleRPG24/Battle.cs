using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static ConsoleRPG24.Stat;
using static ConsoleRPG24.Stat.Player;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleRPG24
{
    internal class BattleSystem
    {
        bool isDead = false;

        public bool turnChance = false;
        public int playerSpeed = 8;

        public class Team
        {
            public int teamP { get; set; }
            public string teamName { get; set; }
            public List<Monster> monsters { get; set; }

            public Team(string name, int temaP)
            {
                teamName = name;
                monsters = new List<Monster>();
            }
        }

        public void CreateMonster()
        {
            Random rand = new Random();
            Console.WriteLine("랜덤한 적이 생성됨");
            Console.WriteLine("랜덤한 적이 팀에 배치됨");


        }
        public void BattleStart()
        {
            CreateMonster();

            Console.WriteLine("적을 조우했습니다.");
            Console.ReadKey();
            Console.Write($"뱀을 만났습니다.");
            Console.Write($"개구리을 만났습니다.");
            Console.Write($"뫼옹을 만났습니다.");
            Console.Write($"스타을 만났습니다.");
            Console.ReadKey();


        }

        public void PlayerAttack(Player name, Monster target, int damage)
        {
            Console.WriteLine($"{name.Name}이/가 {target.Name}에게 {damage} 만큼 피해를 입혔습니다.");
            target.TakeDamage(damage);
        }
        public void Chance()
        {
            for (int turnCheck = 21; turnCheck > 0; turnCheck--)
            {
                turnChance = true;
                Console.WriteLine("모든 턴 찬스 초기화");
                Console.WriteLine($"{turnCheck}.");
                Console.ReadKey();
                if (true)
                {
                    if (turnChance = true && playerSpeed == turnCheck) { PlayerTurn(); }
                    while (turnCheck >= 1)
                    {
                        int[] skillCheck = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                        Random rand = new Random();

                        int randomIndex = rand.Next(skillCheck.Length);
                        int randomSkill = skillCheck[randomIndex];
                        Console.WriteLine($"랜덤으로 선택된 스킬 사용 눈 : {randomSkill}");

                        if (randomSkill >= 7)
                        {
                            
                        }
                        else
                        {

                        }
                        break;
                    }
                }

            }
            void PlayerTurn()
            {
                Console.WriteLine("===========================");
                Console.WriteLine("\t적 \t적 \t적 \t적");
                Console.WriteLine("===========================");
                Console.WriteLine("---------당신의 턴---------");
                Console.WriteLine("===========================");
                Console.WriteLine("\t나 \t아군 \t아군 \t아군");
                Console.WriteLine("===========================");
                Console.WriteLine("공격대상 선택");
                Console.WriteLine("1.적 \t2.적 \t3.적 \t4.적");
                while (true)
                {
                    string input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            PlayerAttack();
                            break;
                        case "2":
                            PlayerAttack();
                            break;
                        case "3":
                            PlayerAttack();
                            break;
                        case "4":
                            PlayerAttack();
                            break;
                        default: Console.WriteLine("공격 할 적을 선택해주십시오.");break;
                    }
                }
            }
        }

        private void PlayerAttack()
        {
            throw new NotImplementedException();
        }

        public void IsAllDead()
        {
            if (isDead)
            {

            }
        }
        public void Battle(Team teamA, Team teamB)
        {
            Random rand = new Random();
            while (true)
            {
                
            }

        }

    }
}


