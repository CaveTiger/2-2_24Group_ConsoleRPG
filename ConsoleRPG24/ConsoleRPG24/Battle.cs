using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static ConsoleRPG24.BattleSystem;
using static ConsoleRPG24.Stat;
using static ConsoleRPG24.Stat.Player;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleRPG24
{
    //전투 시퀀스를 정해보자
    //전투 시작됨 > CreateMonster() > 크리에잇 배틀에서 적을 체크하고 알려줌 > BattleStart()
    //턴을 체크 해야함 > Chance() > 전투에서 죽은걸 체크함 > IsAllDead(bool isAllDead)
    //어느 한쪽이 전멸했으니 보상 혹은 게임오버를 띄워야함
    internal class BattleSystem
    {

        public bool turnChance = false;
        public int playerSpeed = 8;

        public class Team
        {
            public int teamP { get; set; }
            public string teamName { get; set; }
            public List<Monster> monsters { get; set; }

            public Team(string name)
            {
                teamName = name;
                monsters = new List<Monster>();
            }
        }

        public void CreateMonster()
        {//이곳에 몬스터 생성을 구현해야함
            Random rand = new Random();
            Console.WriteLine("랜덤한 적이 생성됨");
            Console.WriteLine("랜덤한 적이 팀에 배치됨");


        }
        public void BattleStart()
        {//은 전투 진입점으로 몬스터 생성 메서드를 부르고 그 뒤에 생성된 애들을 체크하고 이제 턴을 진행 할 거임
            CreateMonster();

            Console.WriteLine("적을 조우했습니다.");
            Console.ReadKey();
            Console.WriteLine($"뱀을 만났습니다.");
            Console.WriteLine($"개구리을 만났습니다.");
            Console.WriteLine($"뫼옹을 만났습니다.");
            Console.WriteLine($"스타을 만났습니다.");
            Console.ReadKey();

            Chance();
        }

        public void PlayerAttack(Monster target, int damage)
        {
            //이건 플레이어가 적에게 피해를 줬을때
            target.TakeDamage(damage);
        }
        public void Chance()
        {
            //이건 턴의 '찬스'가 왔음을 체크해야할때.
            for (int turnCheck = 21; turnCheck >= 0; turnCheck--)
                //속도 최대 20 21부터 시작해 1씩 내려가 해당 속도와 == 이 뜰대 밑의 조건문을 실행함
            {
                turnChance = true;//턴 찬스 이게 있는 이유는 같은 속도에선 그 속도를 작동시키고 +1을 시킴
                //그에 따라 턴 기회가 있는 같은 속도 둘중 하나만 작동하고 다음놈이 작동해야함
                //실제로 작동할땐 그딴거 없고 한번에 작동할지도 모름.
                Console.WriteLine("모든 턴 찬스 초기화");//추후 삭제할 메시지
                Console.WriteLine($"{turnCheck}.");//현재 턴이 몇번을 도는지 디버깅
                Console.ReadKey();
                if (true)//그냥 저 밑의 둘을 묶기 위함
                {
                    if (turnChance = true && playerSpeed == turnCheck) { PlayerTurn(); } //플레이어 턴일땐 이걸 씀
                    else if (turnCheck >= 1)//턴 체크 1이상일때 이 조건을 따름
                    {
                        int[] skillCheck = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                        Random rand = new Random();

                        int randomIndex = rand.Next(skillCheck.Length);
                        int randomSkill = skillCheck[randomIndex];
                        Console.WriteLine($"랜덤으로 선택된 스킬 사용 눈 : {randomSkill}");


                    }
                    else if(turnCheck == 0) //모든 턴이 다 돌았고 전멸이 안떴을 경우 다시 돌림(지금은 올데드체크가 없음)
                {
                    turnCheck = 21;
                    Console.WriteLine("턴 초기화");
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
                            //PlayerAttack();
                            Console.WriteLine(Console.ReadLine());
                            break;
                        case "2":
                            //PlayerAttack();
                            Console.WriteLine(Console.ReadLine());
                            break;
                        case "3":
                            //PlayerAttack();
                            Console.WriteLine(Console.ReadLine());
                            break;
                        case "4":
                            //PlayerAttack();
                            Console.WriteLine(Console.ReadLine());
                            break;
                        default: Console.WriteLine("공격 할 적을 선택해주십시오.");break;
                    }
                }
            }
        }

        //public bool IsAllDead(bool isAllDead)//전투 상황 죽음을 체크 한다.
        //{//이건 파티편성의 리스트를 가져와야할거같다.
        //    if (Player.IsDead == false || Monster.IsDead == false) 
        //    {
        //        isAllDead = true;
        //    }
        //    if (Monster.IsDead == true) { }
        //    //몬스터 생성이 됐을때 시험해보자.
        //}

        public void Battle(Team teamA, Team teamB)
        {
            Random rand = new Random();
            while (true)
            {
                
            }

        }

    }
}


