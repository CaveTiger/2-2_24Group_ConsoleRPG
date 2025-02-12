using System.Globalization;

namespace ConsoleRPG24
{

    public class Inventory
    {
        internal List<Item> Inven { get; set; } = new List<Item>();
        private const int MaxEquippedItems = 12;
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
        internal void AddItem(Item item)
        {
            item.IsOwned = true;
            Inven.Add(item);
            Console.WriteLine($"{item.ItemName}을(를) 인벤토리에 추가했습니다!");
        }

        // 🔹 인벤토리 아이템을 가져올 때 반복문
        public void ShowInventory()
        {
            if (Inven.Count == 0)
            {
                Console.WriteLine("인벤토리가 비어 있습니다.");
                return;
            }

            Console.WriteLine("[인벤토리 아이템 목록]");
            foreach (var item in Inven)
            {
                Console.WriteLine($"아이템: {item.ItemName} | 설명: {item.Description}");
            }
        }

        // 🔹 아이템 제거
        internal void RemoveItem(Item item)
        {
            if (Inven.Contains(item))
            {
                item.IsOwned = false;
                Inven.Remove(item);
                Console.WriteLine($"{item.ItemName}을(를) 인벤토리에서 제거했습니다.");
            }
            else
            {
                Console.WriteLine($"{item.ItemName}이(가) 인벤토리에 없습니다.");
            }
        }

        // 🔹 장비 관리 (아이템 목록 출력 및 장착/해제 기능)
        public void ManageEquipment()
        {
            while (true)
            {
                if (Inven.Count == 0)
                {
                    Console.WriteLine("인벤토리가 비어 있습니다.");
                    return;
                }
                Console.Clear();
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < Inven.Count; i++)
                {
                    var item = Inven[i];
                    string equippedMark = item.IsEquipped ? "[E]" : "   ";
                    Console.WriteLine($"- {i + 1} {equippedMark} {item.ItemName} | {item.EffectDescription} | {item.ItemRank}| {item.Description}");
                }

                Console.WriteLine("0. 나가기");
                Console.Write(">> ");
                string input = Console.ReadLine();

                if (input == "0")
                {
                    Console.Clear();
                    break;
                }

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

            // 이미 장착된 아이템이 아니면 장착 가능한 아이템 수 확인
            if (item.IsEquipped)
            {
                item.IsEquipped = false;
                Console.WriteLine($"{item.ItemName}을(를) 해제했습니다!");
            }
            else
            {
                int equippedCount = Inven.Count(i => i.IsEquipped);  // 장착된 아이템 수 계산

                if (equippedCount >= MaxEquippedItems)
                {
                    Console.WriteLine($"최대 {MaxEquippedItems}개의 아이템만 장착할 수 있습니다.");
                    return;
                }

                item.IsEquipped = true;
                Console.WriteLine($"{item.ItemName}을(를) 장착했습니다!");
            }
        }
    }
}