using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Xml.Linq;
using static ConsoleRPG24.Stat.Player;

namespace ConsoleRPG24
{
    // 용병 클래스
    internal class MercenaryManager
    {
        private List<Mercenary> mercenaries = new List<Mercenary>();
        private List<Mercenary> equippedMercenaries = new List<Mercenary>();
        private const int MaxEquipped = 3;

        public void AddMercenary(Mercenary merc)
        {
            mercenaries.Add(merc);
            Console.WriteLine($"{merc.Name} 용병이 고용되었습니다!");
        }

        public void RemoveMercenary(Mercenary merc)
        {
            if (mercenaries.Contains(merc))
            {
                mercenaries.Remove(merc);
                equippedMercenaries.Remove(merc);
                Console.WriteLine($"{merc.Name} 용병을 해고했습니다.");
            }
        }

        public void EquipMercenary(Mercenary merc)
        {
            if (equippedMercenaries.Count < MaxEquipped && mercenaries.Contains(merc))
            {
                equippedMercenaries.Add(merc);
                Console.WriteLine($"{merc.Name} 용병을 장착했습니다!");
            }
            else
            {
                Console.WriteLine("더 이상 용병을 장착할 수 없습니다.");
            }
        }

        public void UnequipMercenary(Mercenary merc)
        {
            if (equippedMercenaries.Contains(merc))
            {
                equippedMercenaries.Remove(merc);
                Console.WriteLine($"{merc.Name} 용병을 해제했습니다.");
            }
        }

        public void ShowMercenaries()
        {
            Console.WriteLine("[보유 중인 용병]");
            foreach (var merc in mercenaries)
            {
                string status = equippedMercenaries.Contains(merc) ? "[장착됨]" : "[대기 중]";
                Console.WriteLine($"- {merc.Name} | ATK: {merc.Atk} | DEF: {merc.Defen} | HP: {merc.Health}/{merc.MaxHealth} {status}");
            }
        }
    }
}

