
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleRPG24
{
    //전투 시퀀스를 정해보자
    //전투 시작됨 > CreateMonster() > 크리에잇 배틀에서 적을 체크하고 알려줌 > BattleStart()
    //턴을 체크 해야함 > Chance() > 전투에서 죽은걸 체크함 > IsAllDead(bool isAllDead)
    //어느 한쪽이 전멸했으니 보상 혹은 게임오버를 띄워야함
    internal partial class BattleSystem
    {
        public Player player { get; set; }
        public Stage stage { get; set; }
        public List<Item> itemList { get; set; }

        public BattleSystem(Player player, List<Item> itemList, Stage stage)
        {
            this.player = player;
            this.itemList = itemList;
            this.stage = stage;
        }

        public Monster RandomBoss()
        {
            Random random = new Random();
            int index = random.Next(2, 3);
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
        public void ItemLoraderBattleFront(List<Monster> monsterTeam)
        {
            ApplyEffectBeforeBattle(monsterTeam);
        }

        public void RandomEventBattle(Monster target, int damage, List<Monster> monsterTeam)
        {
            Random random = new Random();
            int REB = random.Next(0, 11);
            switch (REB)
            {
                case 0:
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("유니콘은 실존했다! 당신에게 색감이 더해집니다.");
                    break;
                    case 1:
                    Console.WriteLine("아무튼 생성됨 ");
                    Console.WriteLine("햄부기햄북 햄북어 햄북스딱스 함부르크햄부기우가햄비기햄부거 햄부가티햄부기온앤 온을 얻었습니다.");
                    Console.WriteLine("최대 체력 + 10");
                    player.MaxHealth += 10;
                    player.Health += 10;
                    break;
                    case 2:
                        Console.WriteLine("배가 고파졌습니다. 마침 주머니에 빵이 남았네요.");
                        Console.WriteLine("체력 + 20 회복");
                    player.Health += 20;
                    if(player.MaxHealth < player.Health)
                    {
                        player.Health = player.MaxHealth;
                    }
                    break;
                    case 3:
                        Console.WriteLine("분노의 영약");
                        Console.WriteLine("영구적인 공격력 + 1 증가");
                    player.Atk += 1;
                    break;
                    case 4:
                        Console.WriteLine("탈모!");
                        Console.WriteLine("기부니가 별로에요. 최대 체력 - 5");
                    player.MaxHealth -= 5;
                    player.Health -= 5;
                    break;
                    case 6:
                        Console.WriteLine("! 잠시 잠들었나보네요 !");
                        Console.WriteLine("머리를 찧었습니다. 체력 -20");
                    player.Health -= 20;
                    break;
                    case 7:
                        Console.WriteLine("얼음!");
                        Console.WriteLine("처럼 단단한 몸 방어력 +1");
                    player.Defen += 1;
                    break;
                    case 8:
                        Console.WriteLine("발이 미끄러졌습니다.");
                        Console.WriteLine("체력 -10");
                    player.Health -= 10;
                    break;
                    case 9:
                        Console.WriteLine("바나나 총을 찾았다!");
                        Console.WriteLine($"첫 번째 대상에게 피해 100");
                        PlayerAttack(monsterTeam[0], 100);
                    break;
                    case 10:
                        Console.WriteLine("!!정상화!!");
                        Console.WriteLine("내 체력이 정상화됩니다.");
                    player.Health = player.MaxHealth;
                    break;
                    default:
                    Console.WriteLine("꽝");
                        break;
            }
        }

        public void PlayerDebug()
        {
            Console.WriteLine("플레이어 체력을 임시로 설정");
            player.MaxHealth = float.Parse(Console.ReadLine());
            player.Health = player.MaxHealth;
            Console.WriteLine("플레이어 공격력을 임시로 설정");
            player.Atk = int.Parse(Console.ReadLine());
            Console.WriteLine("플레이어 방어력을 임시로 설정");
            player.Defen = int.Parse(Console.ReadLine());
        }
        public void PlayerAttack(Monster target, int damage)
        {
            //이건 플레이어가 적에게 피해를 줬을때
            player.Attack(target);
            if (target.Health <= 0)
            {
                target.IsDead = true;
                if (target.IsDead == true)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{target.Name}이/가 죽었습니다.");
                    Console.ResetColor();
                }
            }
        }
        public void Battle()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("적과 조우했습니다.");
            Console.WriteLine();
            //PlayerDebug();
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
            ApplyEffectBeforeBattle(monsterTeam);
            while (BattleOn == true)
            {
                Console.ReadLine();
                Console.Clear();
                ApplyEffectEveryTurn();
                Console.WriteLine("}================================={");
                Console.ForegroundColor= ConsoleColor.Green;
                Console.WriteLine($"이름 : {player.Name}  직업 : {player.Job}");
                Console.WriteLine($"속도 : {player.Speed}  체력 : {player.Health}/{player.MaxHealth}");
                Console.WriteLine($"공격력 : {player.Atk}  방어력 : {player.Defen}");
                Console.ResetColor();
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
                            Console.WriteLine();
                            monsterTeam[i].Attack(player);
                            Console.ReadKey();
                            
                        }
                        if (player.Speed == turnCheck)//플레이어의 속도를 체크
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
                                               if (monsterTeam[0].IsDead == false)
                                                {
                                                PlayerAttack(monsterTeam[0], player.Atk);
                                                Console.WriteLine($"{monsterTeam[0].Name}의 현재 체력은 {monsterTeam[0].Health}/{monsterTeam[0].MaxHealth}입니다.");
                                                playerTurn = false;
                                                }
                                                break;
                                            case 2:
                                            if (monsterTeam[1] != null && monsterTeam[1].IsDead == false)
                                            {
                                               
                                                PlayerAttack(monsterTeam[1], player.Atk);
                                                Console.WriteLine($"{monsterTeam[1].Name}의 현재 체력은 {monsterTeam[1].Health}/{monsterTeam[1].MaxHealth}입니다.");
                                                playerTurn = false;
                                            }
                                                break;
                                            case 3:
                                            if (monsterTeam[2] != null && monsterTeam[2].IsDead == false)
                                            {
                                                
                                                PlayerAttack(monsterTeam[2], player.Atk);
                                                Console.WriteLine($"{monsterTeam[2].Name}의 현재 체력은 {monsterTeam[2].Health}/{monsterTeam[2].MaxHealth}입니다.");
                                                playerTurn = false;
                                            }
                                                break;
                                            case 4 :
                                            if (monsterTeam[3] != null && monsterTeam[3].IsDead == false)
                                            {
                                                Console.WriteLine($"당신은 {monsterTeam[3].Name}를 공격했습니다.");
                                                PlayerAttack(monsterTeam[3], player.Atk);
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
                                        RandomEventBattle(monsterTeam[0], player.Atk, monsterTeam);
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
                LoseEffectEveryTurn();
                if (monsterTeam.TrueForAll(m => m.IsDead))
                {
                    LoseEffectAfterBattle(monsterTeam);
                    Console.WriteLine("모든 몬스터가 죽었습니다. 전투 종료!");
                    BattleOn = false;
                }
            }
            stage.Rewards();
        }

        public void BossBattle()
        {
            bool playerTurn = true;
            bool BattleOn = true;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("압도적인 힘을 느끼며 상대를 조우했습니다.");
            Console.WriteLine();
            //PlayerDebug();
            
            List<Monster> monsterTeam = new List<Monster>();
            {
                Random r = new Random();
                int spawnCount = r.Next(1, 2);
                for (int i = 0; i < spawnCount; i++)
                {
                    monsterTeam.Add(RandomMonster());
                }
            }// 몬스터를 생성
            ApplyEffectBeforeBattle(monsterTeam);
            while (BattleOn == true)
            {
                Console.ReadLine();
                Console.Clear();
                ApplyEffectEveryTurn();
                Console.WriteLine("}================================={");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"이름 : {player.Name}  직업 : {player.Job}");
                Console.WriteLine($"속도 : {player.Speed}  체력 : {player.Health}/{player.MaxHealth}");
                Console.WriteLine($"공격력 : {player.Atk}  방어력 : {player.Defen}");
                Console.ResetColor();
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
                            Random random = new Random();
                            int pattern = random.Next(0, 10);
                            if (pattern >= 6)
                            {
                                Console.WriteLine();
                                monsterTeam[i].Attack(player);
                                Console.ReadKey();
                            }
                            else if (pattern <= 3)
                            {
                                Console.WriteLine();
                                monsterTeam[0].Atk += 1;
                                Console.WriteLine($"{monsterTeam[0].Name}이/가 분노합니다..");
                                Console.WriteLine($"공격력이 +1 만큼 올랐습니다.");
                                Console.ReadKey();
                            }
                            else 
                            {
                                Console.WriteLine();
                                int bossHeal = random.Next(40, 101);
                                monsterTeam[0].Health += bossHeal;
                                Console.WriteLine($"{monsterTeam[0].Name}이/가 숨을 돌립니다.");
                                Console.WriteLine($"체력이 {bossHeal}만큼 회복됐습니다.");
                                Console.WriteLine($"현재 {monsterTeam[0].Name}의 체력은 {monsterTeam[0].Health}/{monsterTeam[0].MaxHealth} 입니다.");
                                Console.ReadKey();
                            }
                            

                        }
                        if (player.Speed == turnCheck)//플레이어의 속도를 체크
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
                                        PlayerAttack(monsterTeam[0], player.Atk);
                                                    Console.WriteLine($"{monsterTeam[0].Name}의 현재 체력은 {monsterTeam[0].Health}/{monsterTeam[0].MaxHealth}입니다.");
                                                    playerTurn = false;
                                        break;            
                                    case "2":
                                        playerTurn = false;
                                        break;
                                    case "3":
                                        playerTurn = false;
                                        break;
                                    case "4":
                                        RandomEventBattle(monsterTeam[0], player.Atk, monsterTeam);
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
                LoseEffectEveryTurn();
                if (monsterTeam.TrueForAll(m => m.IsDead))
                {
                    LoseEffectAfterBattle(monsterTeam);
                    Console.WriteLine("보스가 쓰러졌습니다. 전투 종료!");
                    BattleOn = false;
                }
            }
            stage.Rewards();
        }
    }
}


