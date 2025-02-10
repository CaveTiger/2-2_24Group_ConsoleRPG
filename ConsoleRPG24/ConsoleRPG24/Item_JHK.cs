namespace ConsoleRPG24
{
    //아이템 등급
    enum Rank
    {
        common,
        rare,
        epic,
        legend
    }

    //아이템 분류
    enum Division
    {
        atk,
        def,
        hp,
        cHit,
        cDmg,
        miss,
        gold,
        spd,
        shop,
        mer
    }

    internal partial class Item
    {
        public string ItemName { get; set; }                //이름
        public string Description { get; set; }             //설명
        public string EffectDescription { get; set; }       //효과 설명
        public int Attack { get; set; }                     //공격력
        public int Defense { get; set; }                    //방어력
        public int Health { get; set; }                     //체력
        public int MaxHealth { get; set; }                  //최대 체력
        public int CritHit { get; set; }                    //치확
        public int CritDmg { get; set; }                    //치피
        public int Mana { get; set; }                       //마나
        public int Miss { get; set; }                       //회피
        public int Gold { get; set; }                       //골드
        public int Speed { get; set; }                      //속도
        public int ItemPrice { get; set; }                  //가격
        public Rank ItemRank { get; set; }                  //등급
        public Division ItemDivision { get; set; }          //분류

        public bool IsOwned { get; set; }                   //소유 여부
        public bool IsEquipped { get; set; }                //착용 여부

        //기본 생성자
        public Item()
        {
            ItemName = "기본 아이템";
            Description = "기본 설명";
            EffectDescription = "기본 효과 설명";
            Attack = 0;
            Defense = 0;
            Health = 0;
            MaxHealth = 0;
            CritHit = 0;
            CritDmg = 0;
            Mana = 0;
            Miss = 0;
            Gold = 0;
            Speed = 0;
            ItemPrice = 0;
            ItemRank = Rank.common;
            ItemDivision = Division.atk;

            IsOwned = false;
            IsEquipped = false;
        }

        //생성자 -> 아이템의 분류에 따라 어떤 값에 단순 변동을 줄지 정한다.
        public Item(string _itemName, string _description, string _effectdescription, Rank _itemRank, Division _itemDivision, int percentAmount)
        {
            ItemName = _itemName;
            Description = _description;
            EffectDescription = _effectdescription;
            ItemRank = _itemRank;
            ItemDivision = _itemDivision;

            switch (_itemRank)
            {
                case Rank.common: ItemPrice = 100; break;

                case Rank.rare: ItemPrice = 180; break;

                case Rank.epic: ItemPrice = 240; break;

                case Rank.legend: ItemPrice = 300; break;
            }

            switch (_itemDivision)
            {
                case Division.atk: Attack = percentAmount; break;

                case Division.def: Defense = percentAmount; break;

                case Division.hp: MaxHealth = percentAmount; break;

                case Division.cHit: CritHit = percentAmount; break;

                case Division.cDmg: CritDmg = percentAmount; break;

                case Division.miss: Miss = percentAmount; break;

                case Division.gold: break;

                case Division.spd: Speed = percentAmount; break;
            }

            IsOwned = false;
            IsEquipped = false;
        }
    }
}
