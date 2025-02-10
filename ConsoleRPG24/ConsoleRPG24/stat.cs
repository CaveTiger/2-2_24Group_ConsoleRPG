using System;
using System.Collections.Generic;
using ConsoleRPG24;

namespace ConsoleRPG24
{
    // 🔹 기본 캐릭터 클래스 (부모 클래스)
    public class BaseCharacter
    {
        public string Name { get; set; }
        public int Atk { get; set; }  // 공격력
        public int Defen { get; set; }  // 방어력
        public float Health { get; set; }  // 현재 체력
        public float MaxHealth { get; set; }  // 최대 체력
        public int Speed { get; set; }  // 속도
        public bool IsDead { get; set; } // 사망 여부
        public bool IsTraitor { get; set; } // 배신 여부

        public BaseCharacter(string name, int atk, int defen, float health, float maxHealth, int speed)
        {
            Name = name;
            Atk = atk;
            Defen = defen;
            Health = health;
            MaxHealth = maxHealth;
            Speed = speed;
            IsDead = false;
            IsTraitor = false;
        }

        // 🔹 인벤토리 클래스
        public class Inventory
        {
            public List<Item> Inven { get; set; } = new List<Item>();

            // 🔹 인벤토리 열기
            public void OpenInventory()
            {
                while (true)
                {
                    Console.WriteLine("[인벤토리]");
                    Console.WriteLine("1. 장비 관리");
                    Console.WriteLine("0. 뒤로 가기");
                    Console.Write(">> ");
                    string input = Console.ReadLine();

                    if (input == "1")
                    {
                        ManageEquipment();
                    }
                    else if (input == "0")
                    {
                        Console.Clear();
                        return;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다. 다시 입력하세요.");
                    }
                }
            }

            // 🔹 아이템 추가
            public void AddItem(Item item)
            {
                Inven.Add(item);
                Console.WriteLine($"{item.ItemName}을(를) 인벤토리에 추가했습니다!");
            }

            // 🔹 장비 관리 (아이템 목록 출력 및 장착/해제 기능)
            public void ManageEquipment()
            {
                while (true)
                {
                    Console.WriteLine("[아이템 목록]");
                    for (int i = 0; i < Inven.Count; i++)
                    {
                        var item = Inven[i];
                        string equippedMark = item.IsEquipped ? "[E]" : "   ";
                        Console.WriteLine($"- {i + 1} {equippedMark} {item.ItemName} | {item.ItemDivision} +{item.Attack}/{item.Defense}/{item.Health} | {item.Description}");
                    }
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine("원하시는 행동을 입력해주세요: ");
                    Console.Write(">> ");
                    string input = Console.ReadLine();

                    if (input == "0") return;

                    if (int.TryParse(input, out int itemIndex) && itemIndex > 0 && itemIndex <= Inven.Count)
                    {
                        ToggleEquip(itemIndex - 1);
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다. 다시 입력하세요.");
                    }
                }
            }
            // 🔹 장착/해제 기능
            private void ToggleEquip(int index)
            {
                var item = Inven[index];
                item.IsEquipped = !item.IsEquipped;
                Console.WriteLine(item.IsEquipped ? $"{item.ItemName}을(를) 장착했습니다!" : $"{item.ItemName}을(를) 해제했습니다!");
            }
        }
    
        
        // 🔹 데미지를 받는 함수 (사망 여부 체크 포함)
        public virtual void TakeDamage(int damage)
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

        // 🔹 기본 공격 메서드 (모든 캐릭터가 사용 가능)
        public virtual void Attack(BaseCharacter target)
        {
            Console.WriteLine($"{Name}이 {target.Name}을(를) 공격합니다!");
            target.TakeDamage(Atk);
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



    // 🔹 플레이어 클래스
    public class Player : BaseCharacter
    {
        public string Job { get; set; }  // 직업
        public int Gold { get; set; }  // 돈
        public float Miss { get; set; }  // 회피 확률
        public int Mana { get; set; } // 마나
        public string Skill { get; set; }  // 스킬
        public float CritHit { get; set; }  // 치명타 확률 (%)
        public float CritDmg { get; set; }  // 치명타 피해 배율
        public Inventory Inventory { get; set; } // Inventory 속성 유지

        public Player(string name, string job)
           : base(name, 0, 0, 0, 0, 0) // 기본 스탯 0으로 초기화 후, SetJobStats()로 설정
        {
            Gold = 100;
            Miss = 0.1f;
            Mana = 100;
            Inventory = new Inventory(); // 인벤토리 생성
            SetJobStats(job);
        }

        public void EquipItem(Item item)
        {
            if (Inventory.Items.Contains(item))
            {
                Atk += item.AttackBoost;
                Defen += item.DefenseBoost;
                MaxHealth += item.HealthBoost;
                Console.WriteLine($"{Name}이(가) {item.Name}을(를) 장착했습니다!");
                Inventory.RemoveItem(item);
            }
            else
            {
                Console.WriteLine($"{item.Name}이(가) 인벤토리에 없습니다.");
            }
        }

        public void UseItem(Item item)
        {
            if (Inventory.Items.Contains(item))
            {
                Health += item.HealthBoost;
                if (Health > MaxHealth) Health = MaxHealth;
                Console.WriteLine($"{Name}이(가) {item.Name}을(를) 사용하여 체력이 {Health}이 되었습니다!");
                Inventory.RemoveItem(item);
            }
            else
            {
                Console.WriteLine($"{item.Name}이(가) 인벤토리에 없습니다.");
            }
        }

        // 🔹 직업 선택 시 스탯 설정
        public void SetJobStats(string job)
        {
            Job = job;

            switch (job)
            {
                case "전사":
                    Atk = 20;
                    Defen = 15;
                    MaxHealth = 150;
                    Health = MaxHealth;
                    Speed = 5;
                    CritHit = 0.1f;
                    CritDmg = 1.5f;
                    Skill = "강력한 일격!";
                    break;

                case "마법사":
                    Atk = 25;
                    Defen = 5;
                    MaxHealth = 100;
                    Health = MaxHealth;
                    Speed = 6;
                    Mana = 200;
                    CritHit = 0.15f;
                    CritDmg = 1.8f;
                    Skill = "파이어 볼!";
                    break;

                case "궁수":
                    Atk = 18;
                    Defen = 10;
                    MaxHealth = 120;
                    Health = MaxHealth;
                    Speed = 7;
                    CritHit = 0.2f;
                    CritDmg = 2.0f;
                    Skill = "관통의 화살!";
                    break;

                case "암살자":
                    Atk = 22;
                    Defen = 8;
                    MaxHealth = 110;
                    Health = MaxHealth;
                    Speed = 9;
                    CritHit = 0.3f;
                    CritDmg = 2.5f;
                    Skill = "그림자 일격!";
                    break;

                default:
                    Console.WriteLine("잘못된 직업 선택!");
                    break;
            }
        }

        public override bool IsAlly()
        {
            return true;
        }

        public bool EvadeAttack()
        {
            Random rand = new Random();
            return rand.NextDouble() < Miss;
        }

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

    // 🔹 용병 클래스
    public class Mercenary : BaseCharacter
    {
        public Mercenary(string name, int atk, int defen, float health, float maxHealth, int speed)
            : base(name, atk, defen, health, maxHealth, speed)
        {
        }

        public override bool IsAlly()
        {
            return !IsTraitor;
        }
    }

    // 🔹 몬스터 클래스
    
        public class Monster : BaseCharacter
        {
            public Monster(string name, int atk, int defen, float health, float maxHealth, int speed)
                : base(name, atk, defen, health, maxHealth, speed)
            {
            }

            public override bool IsAlly()
            {
                return false;
            }
        }

        public class Goblin : Monster ///고블린:속도가 빠름
        {
            public Goblin(string name) : base(name, 8, 3, 30f, 30f, 7) { }

            public override void Attack(BaseCharacter target)
            {
                Console.WriteLine($"{Name}이 빠르게 공격합니다! (속도 {Speed})");
                target.TakeDamage(Atk);
            }
        }

        public class Orc : Monster ///오크:강한 공격력
        {
            public Orc(string name) : base(name, 15, 5, 60f, 60f, 4) { }

            public override void Attack(BaseCharacter target)
            {
                Console.WriteLine($"{Name}이 강력한 일격을 가합니다!");
                target.TakeDamage(Atk + 5);
            }
        }

        public class Dragon : Monster ///드레곤:강력한 브레스 공격
        {
            public Dragon(string name) : base(name, 30, 10, 200f, 200f, 5) { }

            public override void Attack(BaseCharacter target)
            {
                Console.WriteLine($"{Name}이 불을 뿜습니다! (광역 공격)");
                target.TakeDamage(Atk * 2);
            }
        }

        public class Slime : Monster
        {
            public static List<Slime> SlimeList = new List<Slime>(); 

            public Slime(string name) : base(name, 5, 2, 20f, 20f, 2) { }

            // 🔹 BaseCharacter의 TakeDamage()를 override하여 분열 기능 추가
            public override void TakeDamage(int damage)
            {
                base.TakeDamage(damage); 

                Random rand = new Random();
                if (Health <= 0 && rand.NextDouble() < 0.5) 
                {
                    string newSlimeName = $"{Name} 분열체";
                    Slime newSlime = new Slime(newSlimeName);
                    SlimeList.Add(newSlime); 
                    Console.WriteLine($"{Name}이 분열하여 {newSlimeName}이 생성되었습니다!");
                }
            }
        }


    public class Vampire : Monster ///뱀파이어:공격시 흡혈
        {
            public Vampire(string name) : base(name, 18, 6, 70f, 70f, 6) { }

            public override void Attack(BaseCharacter target)
            {
                Console.WriteLine($"{Name}이 {target.Name}을(를) 공격하며 피를 흡수합니다!");
                target.TakeDamage(Atk);
                Health += 5; // 흡혈 효과
                if (Health > MaxHealth) Health = MaxHealth;
                Console.WriteLine($"{Name}의 체력이 {Health}로 회복되었습니다!");
            }
        }
    }


