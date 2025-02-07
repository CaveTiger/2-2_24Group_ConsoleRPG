using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG24
{
    internal class Inventory
    {
        public List<Item> Inven { get; set; }
        public void OpenInventory()
        {
            while (true)
            {
                Console.WriteLine("[인벤토리]");
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("2. 용병 관리");
                Console.WriteLine("0. 뒤로 가기");
                Console.Write(">> ");
                string input = Console.ReadLine();

                if (input == "1")
                {
                    ManageEquipment();
                }
                else if (input == "2")
                {

                }
                else if (input == "0")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력하세요.");
                }
            }

        }
        public void ManageEquipment()
        {
            while (true)
            {
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < Inven.Count; i++)
                {
                    var item = Inven[i];
                    string equippedMark = item.IsEquipped ? "[E]" : "   ";
                    Console.WriteLine($"- {i + 1} {equippedMark}{item.Name} | {item.Type} +{item.Stat} | {item.Description}");
                }
                Console.WriteLine("0. 나가기");

                Console.Write("원하시는 행동을 입력해주세요: ");
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
        private void ToggleEquip(int index)
        {
            var item = Inven[index];
            item.IsEquipped = !item.IsEquipped;
            Console.WriteLine(item.IsEquipped ? $"{item.Name}을(를) 장착했습니다!" : $"{item.Name}을(를) 해제했습니다!");
        }
    }
}

