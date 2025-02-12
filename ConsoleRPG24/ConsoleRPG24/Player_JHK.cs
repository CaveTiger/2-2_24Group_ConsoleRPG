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
            Console.WriteLine($"[디버그] {item.ItemName} 능력치 적용 중...");
            Console.WriteLine($"[디버그] 기존 공격력: {Atk}, 기존 방어력: {Defen}, 기존 체력: {MaxHealth}");

            switch (item.ItemDivision)
            {
                case Division.atk:
                    Atk += (int)(Atk * (item.Attack / 100.0f));  // 🔹 퍼센트 기반 증가
                    break;
                case Division.def:
                    Defen += (int)(Defen * (item.Defense / 100.0f));
                    break;
                case Division.hp:
                    MaxHealth += (int)(Health * (item.MaxHealth / 100.0f));
                    break;
                case Division.cHit:
                    CritHit += item.CritHit / 100.0f;  // 🔹 3 → 0.03 (3%)
                    break;
                case Division.cDmg:
                    CritDmg += item.CritDmg / 100.0f;  // 🔹 50 → 0.5 (50%)
                    break;
                case Division.miss:
                    Miss += item.Miss / 100.0f;  // 🔹 회피율도 100분율 적용
                    break;
                case Division.spd:
                    Speed += item.Speed;  // 🔹 속도는 정수 값 유지
                    break;
            }

            Console.WriteLine($"[디버깅] 능력치 변경 후 공격력: {Atk}, 방어력: {Defen}, 체력: {MaxHealth}");
        }


        //능력치 효과 해제
        public void LoseItemEffect(Item item)
        {
            Console.WriteLine($"[디버그] {item.ItemName} 능력치 해제 중...");

            switch (item.ItemDivision)
            {
                case Division.atk:
                    Atk -= (int)(BaseAtk * (item.Attack / 100.0f));
                    break;
                case Division.def:
                    Defen -= (int)(BaseDefen * (item.Defense / 100.0f));
                    break;
                case Division.hp:
                    MaxHealth -= (int)(BaseHealth * (item.MaxHealth / 100.0f));
                    break;
                case Division.cHit:
                    CritHit -= item.CritHit / 100.0f;
                    break;
                case Division.cDmg:
                    CritDmg -= item.CritDmg / 100.0f;
                    break;
                case Division.miss:
                    Miss -= item.Miss / 100.0f;
                    break;
                case Division.spd:
                    Speed -= item.Speed;
                    break;
            }

            Console.WriteLine($"[디버깅] 능력치 변경 후 공격력: {Atk}, 방어력: {Defen}, 체력: {MaxHealth}");
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

        //플레이어가 아이템의 종류에 따라 효과를 적용(조건 아이템)
        public void ApplyItemEffectByTerm(Item item, int percentAmount)
        {
            switch (item.ItemDivision)
            {
                case Division.atk: Atk += (BaseAtk * (percentAmount / 100)); break;                         //퍼센트 계산
                case Division.def: Defen += (BaseDefen * (percentAmount / 100)); break;                    //퍼센트 계산
                case Division.hp: MaxHealth += (BaseHealth * (percentAmount / 100)); break;           //퍼센트 계산
                case Division.cHit: CritHit += percentAmount; break;                                 //단순 수치 증가
                case Division.cDmg: CritDmg += percentAmount; break;                                 //단순 수치 증가
                case Division.miss: Miss += percentAmount; break;                                       //단순 수치 증가
                case Division.spd: Speed += percentAmount; break;
            }
        }

        public void LoseItemEffectByTerm(Item item, int percentAmount)
        {
            switch (item.ItemDivision)
            {
                case Division.atk: Atk -= (BaseAtk * (percentAmount / 100)); break;                         //퍼센트 계산
                case Division.def: Defen -= (BaseDefen * (percentAmount / 100)); break;                    //퍼센트 계산
                case Division.hp: MaxHealth -= (BaseHealth * (percentAmount / 100)); break;           //퍼센트 계산
                case Division.cHit: CritHit -= percentAmount; break;                                 //단순 수치 증가
                case Division.cDmg: CritDmg -= percentAmount; break;                                 //단순 수치 증가
                case Division.miss: Miss -= percentAmount; break;                                       //단순 수치 증가
                case Division.spd: Speed -= percentAmount; break;
            }
        }
    }
}
