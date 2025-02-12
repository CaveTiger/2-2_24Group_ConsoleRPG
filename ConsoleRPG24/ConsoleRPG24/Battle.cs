using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static ConsoleRPG24.BattleSystem;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleRPG24
{
    //전투 시퀀스를 정해보자
    //전투 시작됨 > CreateMonster() > 크리에잇 배틀에서 적을 체크하고 알려줌 > BattleStart()
    //턴을 체크 해야함 > Chance() > 전투에서 죽은걸 체크함 > IsAllDead(bool isAllDead)
    //어느 한쪽이 전멸했으니 보상 혹은 게임오버를 띄워야함
    internal class BattleSystem
    {
        private Player player;
        private List<Item> itemList;
        private Stage stage;

        public BattleSystem(Player player, List<Item> itemList, Stage stage)
        {
            this.player = player;
            this.itemList = itemList;
            this.stage = stage;
        }

        public Monster RandomMonster()
        {
            Random random = new Random();
            int index = random.Next(1, 6);
            if (index == 1)
            {
                Goblin goblin = new Goblin("고블린");
                return goblin;
            }
            else if (index == 2)
            {
                Dragon dragon = new Dragon("드래곤");
                return dragon;
            }
            else if (index == 3)
            {
                Vampire vampire = new Vampire("뱀파이어");
                return vampire;
            }
            else if (index == 4)
            {
                Orc orc = new Orc("오크");
                return orc;
            }
            else
            {
                Slime slime = new Slime("슬라임");
                return slime;
            }


        }



        public void BattleStart()
        {
            //여긴 배틀의 진행 전개를 적용시키는 곳 전투 상황 통제는 이 메서드서 한다.
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("적과 조우했습니다.");
            Console.WriteLine();
            Battle();
        }
        public void PlayerAttack(Monster target, int damage, List<Monster> monsterTeam)
        {
            //이건 플레이어가 적에게 피해를 줬을때
            target.TakeDamage(damage);
            if (target.Health <= 0)
            {
                target.IsDead = true;
                if (target.IsDead == true)
                {
                   // monsterTeam.Add(Console.ForegroundColor = ConsoleColor.DarkGray);
                    Console.WriteLine($"{target.Name}이/가 죽었습니다.");
                }
            }
        }
        public void Battle()
        {
            int pspeed = 5;
            bool playerTurn = true;
            List<Monster> monsterTeam = new List<Monster>();
            {
                Random r = new Random();
                int spawnCount = r.Next(1, 5);
                for (int i = 0; i < spawnCount; i++)
                {
                    monsterTeam.Add(RandomMonster());
                }
            }// 몬스터를 생성
            bool BattleOn = true;
            while (BattleOn == true)
            {
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("}================================={");
                foreach (var number in monsterTeam)
                {
                    if (number.IsDead)//몬스터 죽은 경우
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"{number.Name} : 죽음");
                        Console.ResetColor();
                    }
                    else //산 경우
                    { 
                        Console.WriteLine($"{number.Name} : 체력 : {number.Health}/{number.MaxHealth} 속도 : {number.Speed}");
                        Console.WriteLine($"\t공격력 : {number.Atk} 방어력 : {number.Defen}");
                    }

                    Console.WriteLine("}================================={");
                }// 생성된 몬스터의 정보를 띄워줍니다.

                for (int turnCheck = 21; turnCheck >= 0; turnCheck--)
                //속도 최대 20 21부터 시작해 1씩 내려가 해당 속도와 == 이 뜰대 밑의 조건문을 실행함
                {
                    playerTurn = true;//플레이어 전용으로 플레이어가 턴 

                    //Console.WriteLine($"{turnCheck}.");//현재 턴이 몇번을 도는지 디버깅
                    //Console.ReadKey();
                    for (int i = 0; i < monsterTeam.Count; i++)
                    {
                        if (monsterTeam[i].Speed == turnCheck && !monsterTeam[i].IsDead)//적의 턴이되면 적이 공격하게 됨
                        {

                            Console.WriteLine($"{monsterTeam[i].Name}가 당신에게 피해를 주었습니다.");

                        }
                        if (pspeed == turnCheck)//플레이어의 속도를 체크
                        {
                            while (playerTurn == true)//플레이어에게 턴 기회가 있는지 체크
                            {
                                Console.WriteLine("}================================={");
                                Console.WriteLine("당신의 턴입니다.");
                                Console.WriteLine("}======================================={");
                                Console.WriteLine("|1. 공격 | 2. 스킬 | 3. 아이템 | 4. ??? | ");
                                string input = Console.ReadLine();
                                switch (input)
                                {
                                    case "1":
                                        Console.WriteLine("누구를 공격하시겠습니까?");
                                        foreach (var monster in monsterTeam)
                                        {
                                            Console.Write(" | ");
                                            if (monster.IsDead)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                            }

                                            Console.Write($"{monster.Name}");
                                            Console.ResetColor();
                                        }
                                        Console.WriteLine(" | 0. 뒤로 |");

                                        int AttackCheck = -1;
                                        try
                                        {
                                            AttackCheck = int.Parse(Console.ReadLine());
                                        }
                                        catch
                                        {
                                            AttackCheck = -1;
                                        }
                                        switch (AttackCheck)
                                        {
                                            case 1 :
                                                Console.WriteLine($"당신은 {monsterTeam[0].Name}를 공격했습니다.");
                                                PlayerAttack(monsterTeam[0], 10, monsterTeam);
                                                Console.WriteLine($"{monsterTeam[0].Name}의 현재 체력은 {monsterTeam[0].Health}/{monsterTeam[0].MaxHealth}입니다.");
                                                playerTurn = false;
                                                break;
                                            case 2:
                                            if (monsterTeam[1] != null)
                                            {
                                                Console.WriteLine($"당신은 {monsterTeam[1].Name}를 공격했습니다.");
                                                PlayerAttack(monsterTeam[1], 10, monsterTeam);
                                                Console.WriteLine($"{monsterTeam[1].Name}의 현재 체력은 {monsterTeam[1].Health}/{monsterTeam[1].MaxHealth}입니다.");
                                                playerTurn = false;
                                            }
                                                break;
                                            case 3:
                                            if (monsterTeam[2] != null)
                                            {
                                                Console.WriteLine($"당신은 {monsterTeam[2].Name}를 공격했습니다.");
                                                PlayerAttack(monsterTeam[2], 10, monsterTeam);
                                                Console.WriteLine($"{monsterTeam[2].Name}의 현재 체력은 {monsterTeam[2].Health}/{monsterTeam[2].MaxHealth}입니다.");
                                                playerTurn = false;
                                            }
                                                break;
                                            case 4 :
                                            if (monsterTeam[3] != null)
                                            {
                                                Console.WriteLine($"당신은 {monsterTeam[3].Name}를 공격했습니다.");
                                                PlayerAttack(monsterTeam[3], 10, monsterTeam);
                                                Console.WriteLine($"{monsterTeam[3].Name}의 현재 체력은 {monsterTeam[3].Health}/{monsterTeam[3].MaxHealth}입니다.");
                                                playerTurn = false;
                                            }
                                                break;
                                            case 0:
                                                Console.WriteLine("뒤로 돌아갑니다.");
                                                break;
                                            default:
                                                Console.WriteLine("잘못된 입력입니다.");
                                                break;
                                            }
                                        break;
                                    case "2":
                                        playerTurn = false;
                                        break;
                                    case "3":
                                        playerTurn = false;
                                        break;
                                    case "4":
                                        playerTurn = false;
                                        break;
                                    default:
                                        Console.WriteLine("잘못된 입력입니다.");
                                        break;
                                }

                            }
                        }

                    }

                }
                if (monsterTeam.TrueForAll(m => m.IsDead))
                {
                    Console.WriteLine("모든 몬스터가 사망했습니다. 전투 종료!");
                    BattleOn = false;
                }
            }

        }

    }
}


