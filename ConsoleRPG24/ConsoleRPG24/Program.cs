namespace ConsoleRPG24
{
    internal class Program
    {
        public class BaseCharacter
        {
            public string Name { get; set; }
            public int Atk { get; set; }  // 공격력
            public int Defen { get; set; }  // 방어력
            public float Health { get; set; }  // 현재 체력
            public float MaxHealth { get; set; }  // 최대 체력
            public int Mana { get; set; }  // 마나
            public int Speed { get; set; }  // 속도

            public BaseCharacter(string name, int atk, int defen, float health, float maxHealth, int mana, int speed)
            {
                Name = name;
                Atk = atk;
                Defen = defen;
                Health = health;
                MaxHealth = maxHealth;
                Mana = mana;
                Speed = speed;
            }

            public void TakeDamage(int damage)
            {
                float reducedDamage = Math.Max(damage - Defen, 0);
                Health -= reducedDamage;
                if (Health < 0) Health = 0;
                Console.WriteLine($"{Name}가 {reducedDamage}의 피해를 입었습니다. 남은 HP: {Health}");
            }
        }

        public class Player : BaseCharacter
        {
            public string Job { get; set; }  // 직업
            public int Gold { get; set; }  // 돈
            public string Betray { get; set; }  // 배신받은 사람
            public int CritHit { get; set; }  // 치명타 확률
            public int CritDmg { get; set; }  // 치명타 피해
            public float Miss { get; set; }  // 회피 확률
            public string Skill { get; set; }  // 스킬

            public Player(string name, string job, int atk, int defen, float health, float maxHealth, int mana, int speed,
                          int gold, string betray, int critHit, int critDmg, float miss, string skill)
                : base(name, atk, defen, health, maxHealth, mana, speed)
            {
                Job = job;
                Gold = gold;
                Betray = betray;
                CritHit = critHit;
                CritDmg = critDmg;
                Miss = miss;
                Skill = skill;
            }

            public bool EvadeAttack() // 회피 여부를 판단하는 함수
            {
                Random rand = new Random();
                return rand.NextDouble() < Miss; // Miss 확률에 따라 회피
            }

            public void TakeDamageWithEvade(int damage)
            {
                if (EvadeAttack())
                {
                    Console.WriteLine($"{Name}가 공격을 회피했습니다!");
                    return;
                }

                TakeDamage(damage); // 기본 데미지 처리 함수 호출
            }

            public class Mercenary : BaseCharacter
            {
                public Mercenary(string name, int atk, int defn, float health, float maxHealth, int mana, int speed) : base(name, atk, defn, health, maxHealth, mana, speed)
                {

                }
            }

            public class Monster : BaseCharacter
                {
                public Monster(string name, int atk, int defen, float health, float maxHealth, int mana, int speed)
                        : base(name, atk, defen, health, maxHealth, mana, speed)
                {
                }
            }
        }
    }
}