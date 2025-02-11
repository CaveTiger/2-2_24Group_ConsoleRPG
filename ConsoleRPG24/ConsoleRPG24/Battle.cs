using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
            class Battler
        {
            public string name {  get; set; }
            public int speed {  get; set; }
            public bool isDead {  get; set; }

        }
            public Monster RandomMonster()
            {
                Random random = new Random();
                int index = random.Next(1, 8);
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

        public void PlayerAttack(Monster target, int damage)
        {
            //이건 플레이어가 적에게 피해를 줬을때
            target.TakeDamage(damage);
        }
        public void Battle()
        {
            int pspeed = 5;
            bool playerTurn = true; 
            List<Monster> monsterTeam = new List<Monster>();
            {
                monsterTeam.Add(RandomMonster());
                monsterTeam.Add(RandomMonster());
                monsterTeam.Add(RandomMonster());
                monsterTeam.Add(RandomMonster());
            }// 몬스터를 생성
            bool BattleOn = true;
            while(BattleOn == true)
            {
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("}================================={");
                foreach (var number in monsterTeam)
                    {
                        Console.WriteLine($"{number.Name} : 체력 : {number.Health}/{number.MaxHealth} 속도 : {number.Speed}"); 
                        Console.WriteLine($"\t공격력 : {number.Atk} 방어력 : {number.Defen}");

                        Console.WriteLine("}================================={");
                    }// 생성된 몬스터의 정보를 띄워줍니다.

                for (int turnCheck = 21; turnCheck >= 0; turnCheck--)
                //속도 최대 20 21부터 시작해 1씩 내려가 해당 속도와 == 이 뜰대 밑의 조건문을 실행함
                {
                    //그에 따라 턴 기회가 있는 같은 속도 둘중 하나만 작동하고 다음놈이 작동해야함
                    //실제로 작동할땐 그딴거 없고 한번에 작동할지도 모름.
                    Console.WriteLine($"{turnCheck}.");//현재 턴이 몇번을 도는지 디버깅
                    Console.ReadKey();
                    for (int i = 0; i < monsterTeam.Count; i++)
                    {
                        if (monsterTeam[i].Speed == turnCheck)
                        {

                            Console.WriteLine($"{monsterTeam[i].Name}가 당신에게 피해를 주었습니다.");
                            // BaseCharacter.Attack(Player);
                        }
                        else if (pspeed == turnCheck)
                        {
                            playerTurn = true;
                            while (playerTurn == true)
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
                                        Console.WriteLine($"| 1. {monsterTeam[0].Name} | 2. {monsterTeam[1].Name} | 3. {monsterTeam[2].Name} | 4. {monsterTeam[3].Name} | 0. 뒤로 |");
                                        string inputt = Console.ReadLine();
                                        switch (inputt)
                                        {
                                            case "1":
                                                Console.WriteLine($"당신은 {monsterTeam[0].Name}를 공격했습니다.");
                                                monsterTeam[0].Health -= 10;
                                                Console.WriteLine($"{monsterTeam[0].Name}의 현재 체력은 {monsterTeam[0].Health}/{monsterTeam[0].MaxHealth}입니다.");
                                                playerTurn = false;
                                                break;
                                            case "2":
                                                Console.WriteLine($"당신은 {monsterTeam[1].Name}를 공격했습니다.");
                                                monsterTeam[1].Health -= 10;
                                                Console.WriteLine($"{monsterTeam[1].Name}의 현재 체력은 {monsterTeam[1].Health}/{monsterTeam[1].MaxHealth}입니다.");
                                                playerTurn = false;
                                                break;
                                            case "3":
                                                Console.WriteLine($"당신은 {monsterTeam[2].Name}를 공격했습니다.");
                                                monsterTeam[2].Health -= 10;
                                                Console.WriteLine($"{monsterTeam[2].Name}의 현재 체력은 {monsterTeam[2].Health}/{monsterTeam[2].MaxHealth}입니다.");
                                                playerTurn = false;
                                                break;
                                            case "4":
                                                Console.WriteLine($"당신은 {monsterTeam[3].Name}를 공격했습니다.");
                                                monsterTeam[3].Health -= 10;
                                                Console.WriteLine($"{monsterTeam[3].Name}의 현재 체력은 {monsterTeam[3].Health}/{monsterTeam[3].MaxHealth}입니다.");
                                                playerTurn = false;
                                                break;
                                            case "0":
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
            }
                
            

            //이건 턴의 '찬스'가 왔음을 체크해야할때.
            for (int turnCheck = 21; turnCheck >= 0; turnCheck--)
            //속도 최대 20 21부터 시작해 1씩 내려가 해당 속도와 == 이 뜰대 밑의 조건문을 실행함
            {
                //그에 따라 턴 기회가 있는 같은 속도 둘중 하나만 작동하고 다음놈이 작동해야함
                //실제로 작동할땐 그딴거 없고 한번에 작동할지도 모름.
                Console.WriteLine($"{turnCheck}.");//현재 턴이 몇번을 도는지 디버깅
                Console.ReadKey();
                for (int i = 0; i < monsterTeam.Count; i++)
                {
                    if (monsterTeam[i].Speed == turnCheck)
                    {
                        
                        Console.WriteLine($"{monsterTeam[i].Name}가 당신에게 피해를 주었습니다.");
                       // BaseCharacter.Attack(Player);
                    }
                }
            }
        }

    }
}


