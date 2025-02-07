using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Threading;

/*
namespace ConsoleRPG24
    
{
        public void TakeDamege(int damage)
        {
            health -= damage;
        }


        public class DungeonStage
        {
            private ICharacter player;
            private ICharacter monster;

            //델리게이트
            public delegate void GameEvent(ICharacter character);
            public event GameEvent OnCharacterDeath;

            public Stage(ICharacter player, ICharacter monster)
            {
                this.player = player;
                this.monster = monster;
                OnCharacterDeath += StageClear;
            }

            public void Start()
            {
                Console.WriteLine(new string('=', 20));
                Console.WriteLine($"전투 개시! \n플레이어 {player.name} \n체력: {player.health}\n 마나: {player.mana}\n공격력/방어력: {player.atk}/{player.defen}");
                Console.WriteLine();
                Console.WriteLine($"적 {monster.name} \n체력: {monster.health} \n공격력/방어력: {monster.atk}/{monster.defen}");
                Console.WriteLine(new string('=', 20));
                Console.WriteLine();

                while (!player.IsDead && !monster.IsDead)
                {
                    Console.WriteLine($"{player.name}의 턴!");
                    monster.TakeDamage(player.attack);
                    Console.WriteLine();
                    Thread.Sleep(1000);

                    if (monster.IsDead) break;

                    Console.WriteLine($"{monster.name}의 턴!");
                    player.TakeDamage(monster.attack);
                    Console.WriteLine();
                    Thread.Sleep(1000);

                    if (player.IsDead) //플레이어 or 몬스터 죽었을 시의 이벤트 호출.
                    {
                        OnCharacterDeath?.Invoke(player);
                    }

                    else if (monster.IsDead)
                    {
                        OnCharacterDeath?.Invoke(monster);
                    }

                }
            }

            private void StageClear(Icharacter character)
            {
                if (character is monster)
                {
                    Console.WriteLine($"스테이지 클리어! {character.name}(을)를 물리쳤습니다!");
                }

                else 
                {
                    Console.WriteLine("게임 오버! 패배했습니다.");
                    Console.WriteLine($"{player.name}은 눈 앞이 깜깜해졌다...");
                }
            }

        }


        static void Main(string[] args)
        {
            player = new player;
            monster = new monster; //monster는 랜덤 생성(?

            for ()
            {
                Stage stage = new Stage(player, monster, rewards);
                stage.Start();

                if (player.IsDead) return;
            }


            //스테이지가 끝날때마다 랜덤하게 전투 || 랜덤 이벤트 || 상점 출현
        }
    } 
} //git organazation

    */
