using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG24
{
    internal partial class Player
    {
        //능력치 효과 적용
        public void ApplyItemEffect(Item item)
        {
            switch (item.ItemDivision)
            {
                case Division.atk: Atk += (BaseAtk * (item.Attack / 100)); break;                         //퍼센트 계산
                case Division.def: Defen += (BaseDefen * (item.Defense / 100)); break;                    //퍼센트 계산
                case Division.hp: MaxHealth += (BaseHealth * (item.MaxHealth / 100)); break;           //퍼센트 계산
                case Division.cHit: CritHit += item.CritHit; break;                                 //단순 수치 증가
                case Division.cDmg: CritDmg += item.CritDmg; break;                                 //단순 수치 증가
                case Division.miss: Miss += item.Miss; break;                                       //단순 수치 증가
                case Division.spd: Speed += item.Speed; break;                                      //단순 수치 증가
                default: break;
            }
        }

        //능력치 효과 해제
        public void LoseItemEffect(Item item)
        {
            switch (item.ItemDivision)
            {
                case Division.atk: Atk -= (BaseAtk * (item.Attack / 100)); break;                         //퍼센트 계산
                case Division.def: Defen -= (BaseDefen * (item.Defense / 100)); break;                    //퍼센트 계산
                case Division.hp: MaxHealth -= (BaseHealth * (item.MaxHealth / 100)); break;           //퍼센트 계산
                case Division.cHit: CritHit -= item.CritHit; break;                                 //단순 수치 감소
                case Division.cDmg: CritDmg -= item.CritDmg; break;                                 //단순 수치 감소
                case Division.miss: Miss -= item.Miss; break;                                       //단순 수치 감소
                case Division.spd: Speed -= item.Speed; break;                                      //단순 수치 감소
                default: break;
            }
        }

        //아이템을 장착할 경우
        internal void EquipItem_JHK(Item item)
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

        //아이템을 장착해제할 경우
        internal void UnequipItem_JHK(Item item)
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

        //아이템이 없을 경우 나오는 경고문 출력
        public void PrintWarningForNoItem(Item item)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{item.ItemName}");
            Console.ResetColor();
            Console.WriteLine("이(가) 인벤토리에 없습니다!");
        }

        //아이템 장착중일때 나오는 경고문 출력
        public void PrintWarningForEquipingItem(Item item)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{item.ItemName}");
            Console.ResetColor();
            Console.WriteLine("을(를) 이미 장착하고 있습니다!");
        }

        //아이템 장착중이 아닐때 나오는 경고문 출력
        public void PrintWarningForNotEquipingItem(Item item)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{item.ItemName}");
            Console.ResetColor();
            Console.WriteLine("을(를) 이미 장착 해제중입니다!");
        }

        //플레이어가 장비를 장착했을때 나오는 문장 출력
        public void PrintPlayerEquipItem(Item item)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{Name}");
            Console.ResetColor();
            Console.Write("이(가) ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{item.ItemName}");
            Console.ResetColor();
            Console.WriteLine("을(를) 장착했습니다!");
        }

        //플레이어가 장비를 장착해제했을때 나오는 문장 출력
        public void PrintPlayerUnequipItem(Item item)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{Name}");
            Console.ResetColor();
            Console.Write("이(가) ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{item.ItemName}");
            Console.ResetColor();
            Console.WriteLine("을(를) 장착 해제했습니다!");
        }
    }
}
