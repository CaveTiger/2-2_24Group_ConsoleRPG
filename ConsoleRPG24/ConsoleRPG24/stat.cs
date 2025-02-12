using ConsoleRPG24;

namespace ConsoleRPG24
{
    internal class GachaSystem
    {
        private Player player; // 플레이어 정보
        private int pityCounter; // 90회 확정 지급 카운트

        public GachaSystem(Player player)
        {
            this.player = player;
            pityCounter = 0; // 초기화
        }

        // 🔹 뽑기 실행 메서드
        public void Draw(int drawCount)
        {
            int cost = drawCount * 100; // 1회당 100골드
            if (player.Gold < cost)
            {
                Console.WriteLine("💰 골드가 부족합니다. 뽑기를 진행할 수 없습니다!");
                return;
            }

            player.Gold -= cost; // 골드 차감
            Console.WriteLine($"🎰 {drawCount}회 뽑기를 시작합니다...");

            for (int i = 0; i < drawCount; i++)
            {
                pityCounter++; // 뽑기 횟수 증가
                Thread.Sleep(2000); // 🔹 2초 지연 (애니메이션 효과)
                Console.WriteLine(". . 뽑기 완료!");

                if (pityCounter >= 90)
                {
                    GiveSpecialItem();
                    pityCounter = 0; // 확정 지급 후 초기화
                }
                else
                {
                    if (IsSpecialItem())
                    {
                        GiveSpecialItem();
                        pityCounter = 0; // 특수 아이템 획득 시 확정 카운트 초기화
                    }
                    else
                    {
                        GiveGoldReward();
                    }
                }
            }
        }

        // 🔹 1% 확률 체크
        private bool IsSpecialItem()
        {
            Random rand = new Random();
            return rand.Next(0, 100) < 1; // 1% 확률
        }

        // 🔹 특수 아이템 지급
        private void GiveSpecialItem()
        {
            Item specialItem = new Item(
                "그리웠던 그때 그곳으로",
                "언젠가...우린 과거의 그때로 돌아갈꺼야 오래된 전설처럼.",
                "시작시 공격력이 2배 증가하며, 체력이 매턴 5%씩 성장한다 (소수점 제외)",
                Rank.legend,
                Division.atk,
                0
            );

            player.Inventory.AddItem(specialItem);
            Console.WriteLine($"🎉 축하합니다! {specialItem.ItemName}을(를) 획득하였습니다!");
        }

        // 🔹 골드 보상 지급 (9골드 이하 / 10회 뽑기 시 99골드 이하)
        private void GiveGoldReward()
        {
            Random rand = new Random();
            int goldAmount = rand.Next(1, 10); // 1~9골드 (1회 기준)
            player.Gold += goldAmount;
            Console.WriteLine($"💰 {goldAmount}골드를 획득했습니다!");
        }
    }
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

            int reducedDamage = Math.Max(damage - Defen, 0);
            Health -= reducedDamage;

            if (Health <= 0)
            {
                Health = 0;
                IsDead = true;
                Console.WriteLine($"{Name}가 사망했습니다!");
                OnDeath(); // 🔹 사망 후 추가 처리
            }
            else
            {
                Console.WriteLine($"{Name}가 {reducedDamage}의 피해를 입었습니다. 남은 HP: {Health}");
            }
        }

        // 🔹 사망 시 처리할 메서드
        private void OnDeath()
        {
            Console.WriteLine("게임이 종료되었습니다!");
            // 추가적으로 게임 종료 로직을 넣거나, 부활 시스템을 구현 가능
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
        public Player() : base("Unknown", 0, 0, 100, 100, 5, 0)
        {
            BaseAtk = 0;
            BaseDefen = 0;
            BaseHealth = 100;
            Gold = 100;
            Mana = 100;
            Inventory = new Inventory();
        }


        public Player(string name, string job, int baseAtk, int baseDefen, float baseHealth, float maxHealth, int speed, int miss)
       : base(name, 0, 0, baseHealth, maxHealth, speed, miss)
        {
            BaseAtk = baseAtk;
            BaseDefen = baseDefen;
            BaseHealth = maxHealth;
            Gold = 10000;
            Mana = 100;
            Inventory = new Inventory();
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
                Console.WriteLine("💥 치명타 공격! 💥");
            }

            target.TakeDamage(damage);

            // 🔹 공격이 빗나가는지 확인
            if (missChance < target.Miss)  // 대상의 회피 확률 적용
            {
                Console.WriteLine($"❌ {target.Name}이(가) 공격을 회피했습니다!");
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
                Console.WriteLine("💥 몬스터의 치명타 공격! 💥");
            }

            target.TakeDamage(damage);
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


