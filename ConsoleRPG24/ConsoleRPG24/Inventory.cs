using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleRPG24.Stat;


namespace ConsoleRPG24
{
    internal static class Inventory
    {
        public static List<Item> Inven { get; set; } = new List<Item>();
        public static void OpenInventory()
        {
            while (true)
            {
                Console.WriteLine("[인벤토리]");
                Console.WriteLine("1. 장비 관리");
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

        public static void AddItem(Item item)
        {
            Inven.Add(item);
            Console.WriteLine($"{item.ItemName}을(를) 인벤토리에 추가했습니다!");
        }

        public static void ManageEquipment()
        {
            while (true)
            {
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < Inven.Count; i++)
                {
                    var item = Inven[i];
                    string equippedMark = item.IsEquipped ? "[E]" : "   ";
                    Console.WriteLine($"- {i + 1}{item.ItemName} | {item.ItemDivision} +{item.Attack}{item.Defense}{item.Health} | {item.Description}");
                }
                Console.WriteLine("0. 나가기");
                Console.WriteLine("원하시는 행동을 입력해주세요: ");
                Console.Write(">>");
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
        private static void ToggleEquip(int index)
        {
            var item = Inven[index];
            item.IsEquipped = !item.IsEquipped;
            Console.WriteLine(item.IsEquipped ? $"{item.ItemName}을(를) 장착했습니다!" : $"{item.ItemName}을(를) 해제했습니다!");
        }
    }
}
    