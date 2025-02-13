using System;
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
        public int Miss { get; set; }  // 회피 확률 추가 (기본값 0)


        public BaseCharacter(string name, int atk, int defen, float health, float maxHealth, int speed, int miss)
        {
            Name = name;
            Atk = atk;
            Defen = defen;
            Health = health;
            MaxHealth = maxHealth;
            Speed = speed;
            IsDead = false;
            IsTraitor = false;
            Miss = miss;
        }

        // 🔹 데미지를 받는 함수 (사망 여부 체크 포함)
        public virtual void TakeDamage(int damage)
        {
            if (IsDead)
            {
                Console.WriteLine($"{Name}는 이미 사망했습니다!");
                return;
            }

            int reducedDamage = Math.Max(damage - Defen, 1);
            Health -= reducedDamage;

            if (Health <= 0)
            {
                Health = 0;
                IsDead = true;
                Console.WriteLine($"{Name}가 사망했습니다!");

                // 🔹 플레이어일 경우에만 OnDeath() 실행
                if (this is Player)
                {
                    OnDeath();
                }
            }
            else
            {
                Console.WriteLine($"{Name}가 {reducedDamage}의 피해를 입었습니다. 남은 HP: {Health}");
            }
        }


        // 🔹 사망 시 처리할 메서드 (마을로 이동 추가)
        public void OnDeath()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n당신은 쓰러졌습니다... ");
            Console.WriteLine("눈 앞이 깜깜해졌다...");
            Console.ResetColor();
            Thread.Sleep(3000);

            Console.Clear();
            Console.WriteLine("\n깨어났다...");
            Thread.Sleep(2000);

            // 🔹 플레이어를 새로 생성 (초기화)
            MainScreen.instance.player = new Player();

            // 🔹 올바른 직업을 선택할 때까지 반복
            string jobChoice = "";
            while (true)
            {
                Console.WriteLine("당신의 직업을 선택하세요.");
                Console.WriteLine("1. 전사");
                Console.WriteLine("2. 마법사");
                Console.WriteLine("3. 궁수");
                Console.WriteLine("4. 암살자");
                Console.Write(">> ");
                jobChoice = Console.ReadLine();

                if (jobChoice == "1")
                {
                    MainScreen.instance.player.SetJobStats("전사");
                    break;
                }
                else if (jobChoice == "2")
                {
                    MainScreen.instance.player.SetJobStats("마법사");
                    break;
                }
                else if (jobChoice == "3")
                {
                    MainScreen.instance.player.SetJobStats("궁수");
                    break;
                }
                else if (jobChoice == "4")
                {
                    MainScreen.instance.player.SetJobStats("암살자");
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요.");
                    Thread.Sleep(1000);
                }
            }

            Console.WriteLine($"새로운 직업 {MainScreen.instance.player.Job}(으)로 다시 시작합니다!");
            Thread.Sleep(2000);

            // 🔹 게임 시작 화면으로 이동
            MainScreen.instance.Villige();
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

        // 🔹 Attack 메서드를 virtual로 선언 (재정의 가능)
        public virtual void Attack(BaseCharacter target)
        {
            Console.WriteLine($"{Name}이 {target.Name}을(를) 공격합니다!");
            target.TakeDamage(Atk);
        }
    }




    // 🔹 플레이어 클래스
    internal partial class Player : BaseCharacter
    {
        public int BaseAtk { get; private set; }  //원래 공격력 저장
        public int BaseDefen { get; private set; }  //원래 방어력 저장
        public float BaseHealth { get; private set; }  // 원래 체력 저장
        public string Job { get; set; }  // 직업
        public int Gold { get; set; }  // 돈
        public float Miss { get; set; }  // 회피 확률
        public int Mana { get; set; } // 마나
        public string Skill { get; set; }  // 스킬
        public float CritHit { get; set; }  // 치명타 확률 (%)
        public float CritDmg { get; set; }  // 치명타 피해 배율
        public Inventory Inventory { get; private set; }
        private int bonusAtk = 0;  // 추가 공격력 저장
        private int bonusDefen = 0;  // 추가 방어력 저장
        private float bonusHealth = 0;  // 추가 체력 저장

        // 🔹 기본 생성자 (매개변수 부족한 경우 사용)
        public Player() : base("Unknown", 0, 0, 100, 10000, 5, 0)
        {
            BaseAtk = 0;
            BaseDefen = 0;
            BaseHealth = 100;
            Gold = 10000;
            Mana = 100;
            Inventory = new Inventory(this);
        }


        public Player(string name, string job, int baseAtk, int baseDefen, float baseHealth, float maxHealth, int speed, int miss)
       : base(name, 0, 0, baseHealth, maxHealth, speed, miss)
        {
            BaseAtk = baseAtk;
            BaseDefen = baseDefen;
            BaseHealth = maxHealth;
            Gold = 10000;
            Mana = 100;
            Inventory = new Inventory(this);
            SetJobStats(job);

        }

        // Atk 계산
        public int CurrentAtk => BaseAtk + bonusAtk;
        public int CurrentDefen => BaseDefen + bonusDefen;
        public float CurrentHealth => BaseHealth + bonusHealth;

        //아이템을 장착할 경우
        internal void EquipItem(Item item)
        {
            //아이템을 가지고 있는지? (원래는 isOwned로 하려고 했지만 이거도 괜찮은것 같습니다!)

            //가지고 있지 않은 경우
            if (!Inventory.Inven.Contains(item))
            {
                PrintWarningForNoItem(item);
            }
            //가지고 있지만 장착중인 경우
            else if (Inventory.Inven.Contains(item) && !item.IsEquipped)
            {
                PrintWarningForEquipingItem(item);
            }
            //가지고 있지 않은 경우 && 장착중이 아닌 경우
            else
            {
                ApplyItemEffect(item);
                PrintPlayerEquipItem(item);
                item.IsEquipped = true;
            }

            if (item.ItemName == "그리웠던 그때 그곳으로")
            {
                Atk *= 2; // 🔹 공격력 2배 증가
                MaxHealth += (int)(MaxHealth * 0.05); // 🔹 체력 5% 증가 (소수점 제외)
                Health = MaxHealth; // 🔹 체력 풀로 회복
            }
            else
            {
                Atk += item.Attack;
                MaxHealth += item.Health;
                Health = MaxHealth;  // 최대 체력 증가 시 풀 체력 회복
            }

            item.IsEquipped = true;
            Console.WriteLine($"{item.ItemName}을(를) 장착했습니다!");

        }

        internal void UseItem(Item item)
        {
            if (Inventory.Inven.Contains(item))
            {
                Health += item.Health;
                if (Health > MaxHealth) Health = MaxHealth;
                Console.WriteLine($"{Name}이(가) {item.ItemName}을(를) 사용하여 체력이 {Health}이 되었습니다!");
                Inventory.RemoveItem(item);
            }
            else
            {
                Console.WriteLine($"{item.ItemName}이(가) 인벤토리에 없습니다.");
            }
        }
        //아이템을 장착해제할 경우
        internal void UnequipItem(Item item)
        {

            //가지고 있지 않은 경우
            if (!Inventory.Inven.Contains(item))
            {
                PrintWarningForNoItem(item);
            }
            //가지고 있지만 장착 해제중인 경우
            else if (Inventory.Inven.Contains(item) && item.IsEquipped)
            {
                PrintWarningForNotEquipingItem(item);
            }
            //가지고 있지 않은 경우 && 장착중이 아닌 경우
            else
            {
                ApplyItemEffect(item);
                PrintPlayerUnequipItem(item);
                item.IsEquipped = false;
            }

            // 🔹 장착 해제 시 원래 능력치로 복구
            if (item.ItemName == "그리웠던 그때 그곳으로")
            {
                Atk /= 2; // 🔹 공격력 원상 복구
                MaxHealth -= (int)(MaxHealth * 0.05); // 🔹 체력 감소 (소수점 제외)
                if (Health > MaxHealth) Health = MaxHealth; // 🔹 현재 체력이 최대 체력을 초과하지 않도록 조정
            }
            else
            {
                Atk -= item.Attack;
                MaxHealth -= item.Health;
                if (Health > MaxHealth) Health = MaxHealth;
            }

            item.IsEquipped = false;
            Console.WriteLine($"{item.ItemName}을(를) 해제했습니다!");

        }

        // 🔹 직업 선택 시 스탯 설정
        public void SetJobStats(string job)
        {
            Job = job;

            switch (job)
            {
                case "전사":
                    Atk = 130;
                    Defen = 80;
                    MaxHealth = 850;
                    Health = MaxHealth;
                    Speed = 5;
                    CritHit = 0.25f;
                    CritDmg = 1.5f;
                    Skill = "강력한 일격!";
                    break;

                case "마법사":
                    Atk = 180;
                    Defen = 70;
                    MaxHealth = 650;
                    Health = MaxHealth;
                    Speed = 6;
                    Mana = 200;
                    CritHit = 0.25f;
                    CritDmg = 1.8f;
                    Skill = "파이어 볼!";
                    break;

                case "궁수":
                    Atk = 180;
                    Defen = 70;
                    MaxHealth = 600;
                    Health = MaxHealth;
                    Speed = 7;
                    CritHit = 0.35f;
                    CritDmg = 2.0f;
                    Skill = "관통의 화살!";
                    break;

                case "암살자":
                    Atk = 135;
                    Defen = 80;
                    MaxHealth = 750;
                    Health = MaxHealth;
                    Speed = 9;
                    CritHit = 0.35f;
                    CritDmg = 2.5f;
                    Skill = "그림자 일격!";
                    break;

                default:
                    Console.WriteLine("잘못된 직업 선택!");
                    break;
            }
        }
        public override void Attack(BaseCharacter target)
        {
            Console.WriteLine($"{Name}이(가) {target.Name}을(를) 공격합니다!");

            // 🔹 0~100 사이의 난수 생성
            Random rand = new Random();
            float missChance = rand.Next(0, 101);
            float critChance = rand.Next(0, 101);  // 0~100 사이의 정수값

            // 🔹 치명타 여부 판별 (CritHit를 0~100 범위로 비교)
            bool isCritical = critChance < CritHit * 100;

            // 🔹 일반 공격 vs 치명타 공격
            int damage = isCritical ? (int)(Atk * CritDmg) : Atk;

            if (isCritical)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("치명타 공격!");
                Console.ResetColor();
            }

            target.TakeDamage(damage);

            // 🔹 공격이 빗나가는지 확인
            if (missChance < target.Miss)  // 대상의 회피 확률 적용
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"❌ {target.Name}이(가) 공격을 회피했습니다!");
                Console.ResetColor();
                return;  // 공격 실패
            }
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
        public Mercenary(string name, int atk, int defen, float health, float maxHealth, int speed, int miss)
            : base(name, atk, defen, health, maxHealth, speed, 0)
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
        public int CritHit { get; private set; }  // 치명타 확률 (%)
        public float CritDmg { get; private set; }  // 치명타 피해 배율
        public Monster(string name, int atk, int defen, float health, float maxHealth, int speed, int critHit = 0, float critDmg = 1.5f)
            : base(name, atk, defen, health, maxHealth, speed, 0) //회피 0
        {
            this.Atk = atk;  // 🔹 몬스터 공격력 설정
            this.Defen = defen;
            CritHit = critHit;
            CritDmg = critDmg;

        }

        public override bool IsAlly()
        {
            return false;
        }

        public override void Attack(BaseCharacter target)
        {
            Random rand = new Random();
            int critChance = rand.Next(0, 101);  // 0~100 사이 난수 생성

            bool isCritical = critChance < CritHit;  // 치명타 확률 비교
            int damage = isCritical ? (int)(Atk * CritDmg) : Atk;  // 치명타 발생 시 배율 적용

            Console.WriteLine($"{Name}이(가) {target.Name}을(를) 공격합니다!");

            if (isCritical)
            {
                Console.WriteLine("몬스터의 치명타 공격!");
            }

            target.TakeDamage(damage);
        }
    }


    // ============= 초반 몬스터 (1~15 스테이지) =============
    public class Goblin : Monster ///고블린:속도가
    {
        public Goblin(string name) : base(name, 120, 50, 250f, 250f, 7) { }

        public override void Attack(BaseCharacter target)
        {
            Console.WriteLine($"{Name}이 빠르게 공격합니다! (속도 {Speed})");
            target.TakeDamage(Atk);
        }
    }

    public class Slime : Monster
    {
        public static List<Slime> SlimeList = new List<Slime>();

        public Slime(string name) : base(name, 115, 50, 200f, 200f, 2) { }

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

    public class Wolf : Monster // 🔹 초반 몬스터 (속도 빠르고 공격력 중간)
    {
        public Wolf(string name) : base(name, 125, 80, 250f, 250f, 8) { }

        public override void Attack(BaseCharacter target)
        {
            Console.WriteLine($"{Name}이 빠르게 뛰어들어 공격합니다!");
            target.TakeDamage(Atk);
        }
    }

    public class Zombie : Monster // 🔹 초반 몬스터 (부활 기능)
    {
        private bool hasRevived = false;

        public Zombie(string name) : base(name, 120, 40, 300f, 300f, 2) { }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            if (Health <= 0 && !hasRevived)
            {
                hasRevived = true;
                Health = MaxHealth * 0.5f; // 50% 체력으로 부활
                Console.WriteLine($"{Name}이(가) 다시 일어났습니다! (체력 {Health})");
            }
        }
    }
    // ============= 중반 몬스터 (6~15 스테이지) =============
    public class Orc : Monster ///오크:강한 공격력
    {
        public Orc(string name) : base(name, 175, 80, 450f, 450f, 4) { }

        public override void Attack(BaseCharacter target)
        {
            Console.WriteLine($"{Name}이 강력한 일격을 가합니다!");
            target.TakeDamage(Atk + 25);
        }
    }

    public class Minotaur : Monster // 미노타우르스: <중반 몬스터> (공격력 높고, 2회 공격 확률)
    {
        public Minotaur(string name) : base(name, 180, 125, 550f, 550f, 4) { }

        public override void Attack(BaseCharacter target)
        {
            Console.WriteLine($"{Name}이(가) 거대한 도끼를 휘두릅니다!");
            target.TakeDamage(Atk);

            Random rand = new Random();
            if (rand.NextDouble() < 0.3) // 30% 확률로 2회 공격
            {
                Console.WriteLine($"{Name}이(가) 연속 공격을 시도합니다!");
                target.TakeDamage(Atk);
            }
        }
    }

    public class Ghost : Monster //고스트:<중반 몬스터> (방어력 무시)
    {
        public Ghost(string name) : base(name, 185, 0, 600f, 600f, 7) { }

        public override void Attack(BaseCharacter target)
        {
            Console.WriteLine($"{Name}이(가) 당신의 방어를 무시하고 공격합니다!");
            target.TakeDamage(Atk);
        }
    }

    public class Vampire : Monster ///뱀파이어:공격시 흡혈
    {
        public Vampire(string name) : base(name, 175, 120, 600f, 800f, 5) { }

        public override void Attack(BaseCharacter target)
        {
            Console.WriteLine($"{Name}이 {target.Name}을(를) 공격하며 피를 흡수합니다!");
            target.TakeDamage(Atk);
            Health += 5; // 흡혈 효과
            if (Health > MaxHealth) Health = MaxHealth;
            Console.WriteLine($"{Name}의 체력이 {Health}로 회복되었습니다!");
        }
    }

    // ============= 후반 몬스터 (16~19 스테이지) =============
    public class Lich : Monster // 리치:마법공격 체력흡수
    {
        public Lich(string name) : base(name, 200, 135, 1000f, 1000f, 5) { }

        public override void Attack(BaseCharacter target)
        {
            Console.WriteLine($"{Name}이(가) 강력한 흑마법을 시전합니다!");
            target.TakeDamage(Atk);
            Health += 10;
            if (Health > MaxHealth) Health = MaxHealth;
            Console.WriteLine($"{Name}의 체력이 {Health}로 회복되었습니다!");
        }
    }

    public class Golem : Monster // 골렘:높은 체력과 공격력
    {
        public Golem(string name) : base(name, 200, 130, 1350f, 1350f, 2) { }

        public override void Attack(BaseCharacter target)
        {
            Console.WriteLine($"{Name}이(가) 거대한 주먹으로 공격합니다!");
            target.TakeDamage(Atk);
        }
    }
    //앤드급 보스 드레곤
    public class Dragon : Monster ///드레곤:강력한 브레스 공격
    {
        public Dragon(string name) : base(name, 300, 150, 2000f, 2000f, 5) { }

        public override void Attack(BaseCharacter target)
        {
            Console.WriteLine($"{Name}이 불을 뿜습니다! (광역 공격)");
            target.TakeDamage(Atk * 2);
        }
    }





}


