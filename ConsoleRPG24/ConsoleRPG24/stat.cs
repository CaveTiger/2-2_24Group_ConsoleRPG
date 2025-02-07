using System;

namespace ConsoleRPG24
{
    internal class Stat
    {
        public class BaseCharacter
        {
            public string Name { get; set; }
            public int Atk { get; set; }  // 공격력
            public int Defen { get; set; }  // 방어력
            public float Health { get; set; }  // 현재 체력
            public float MaxHealth { get; set; }  // 최대 체력
            public int Speed { get; set; }  // 속도
            public bool IsDead { get; set; } //사망 여부
            public bool IsTraitor { get; set; } // 배신 여부 (기본값 false)

            public BaseCharacter(string name, int atk, int defen, float health, float maxHealth, int speed)
            {
                Name = name;
                Atk = atk;
                Defen = defen;
                Health = health;
                MaxHealth = maxHealth;
                Speed = speed;
                IsDead = false;  // 처음 생성 시 살아 있음
                IsTraitor = false;  // 기본적으로 배신하지 않음
            }

            // 🔹 데미지를 받는 함수 (사망 여부 체크 포함)
            public void TakeDamage(int damage)
            {
                if (IsDead)
                {
                    Console.WriteLine($"{Name}는 이미 사망했습니다!");
                    return;
                }

                int reducedDamage = Math.Max(damage - Defen, 0);
                Health -= reducedDamage;

                if (Health <= 0)
                {
                    Health = 0;
                    IsDead = true;
                    Console.WriteLine($"{Name}가 사망했습니다!");
                }
                else
                {
                    Console.WriteLine($"{Name}가 {reducedDamage}의 피해를 입었습니다. 남은 HP: {Health}");
                }
            }

            // 🔹 배신 이벤트
            public void Betray()
            {
                IsTraitor = true;
                Console.WriteLine($"{Name}가 배신했습니다! 이제 적이 되었습니다.");
            }

            // 🔹 아군 여부 확인 (배신한 경우 false 반환)
            public virtual bool IsAlly()
            {
                return !IsTraitor;
            }

            // 🔹 적 여부 확인
            public bool IsEnemy()
            {
                return IsTraitor;
            }
        }

        // 🔹 플레이어 클래스 (Player)
        public class Player : BaseCharacter
        {
            public string Job { get; set; }  // 직업
            public int Gold { get; set; }  // 돈
            public string Betray { get; set; }  // 배신받은 사람
            public float Miss { get; set; }  // 회피 확률
            public int Mana { get; set; } // 마나
            public string Skill { get; set; }  // 스킬

            public Player(string name, string job, int atk, int defen, float health, float maxHealth, int mana, int speed,
                          int gold, string betray, float miss, string skill)
                : base(name, atk, defen, health, maxHealth, speed)
            {
                Job = job;
                Gold = gold;
                Betray = betray;
                Miss = miss;
                Mana = mana;
                Skill = skill;
            }

            // 🔹 플레이어는 항상 아군
            public override bool IsAlly()
            {
                return true;
            }

            // 🔹 회피 여부를 판단하는 함수
            public bool EvadeAttack()
            {
                Random rand = new Random();
                return rand.NextDouble() < Miss;
            }

            // 🔹 회피 기능을 포함한 데미지 처리
            public void TakeDamageWithEvade(int damage)
            {
                if (EvadeAttack())
                {
                    Console.WriteLine($"{Name}가 공격을 회피했습니다!");
                    return;
                }

                TakeDamage(damage);
            }
        }

        // 🔹 용병 클래스 (Mercenary)
        public class Mercenary : BaseCharacter
        {
            public Mercenary(string name, int atk, int defen, float health, float maxHealth, int speed)
                : base(name, atk, defen, health, maxHealth, speed)
            {
            }

            // 🔹 용병은 아군이지만 배신하면 적이 됨
            public override bool IsAlly()
            {
                return !IsTraitor;
            }
        }

        // 🔹 몬스터 클래스 (Monster)
        public class Monster : BaseCharacter
        {
            public Monster(string name, int atk, int defen, float health, float maxHealth, int speed)
                : base(name, atk, defen, health, maxHealth, speed)
            {
            }

            // 🔹 몬스터는 항상 적
            public override bool IsAlly()
            {
                return false;
            }
        }
    }
}
