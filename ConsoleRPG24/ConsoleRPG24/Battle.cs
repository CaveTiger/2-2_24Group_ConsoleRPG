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
            public Monster RandomMoster()
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
        public void CreateMonster()
        {
            RandomMoster();
        }
        public void Add(Monster monster)
        {

        }



            public void BattleStart()
        {
            //이곳은 전투 진입점으로 몬스터 생성 메서드를 부르고 그 뒤에 생성된 애들을 체크하고 이제 턴을 진행 할 거임
            //Console.WriteLine(RandomMoster().ToString());


            Battle();
        }

        public void PlayerAttack(Monster target, int damage)
        {
            //이건 플레이어가 적에게 피해를 줬을때
            target.TakeDamage(damage);
        }
        public void Battle()
        {

            List<Monster> monsterTeam = new List<Monster>();
            {
                monsterTeam.Add(RandomMoster());
                monsterTeam.Add(RandomMoster());
                monsterTeam.Add(RandomMoster());
                monsterTeam.Add(RandomMoster());
                Console.WriteLine(monsterTeam);
            }
            foreach (var number in monsterTeam)
            {
                Console.WriteLine(number); 
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


