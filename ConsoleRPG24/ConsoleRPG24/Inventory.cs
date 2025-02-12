using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleRPG24
{
    internal class Inventory
    {
        private Player player;  // 🔹 Player 참조 추가
        private const int MaxEquippedItems = 9;  // 🔹 최대 장착 가능 아이템 개수
        internal List<Item> Inven { get; private set; } = new List<Item>();

        // 🔹 생성자: Player 객체를 참조
        public Inventory(Player player)
        {
            this.player = player;
        }

        // 🔹 아이템 추가
        internal void AddItem(Item item)
        {
            if (item.IsOwned) // 🔹 이미 소유한 아이템인지 확인
            {
                Console.WriteLine($"[경고] {item.ItemName}은(는) 이미 보유 중입니다!");
                return;
            }

            item.IsOwned = true; // 🔹 아이템을 추가하면 소유 여부 설정
            Inven.Add(item);
            Console.WriteLine($"[성공] {item.ItemName}을(를) 인벤토리에 추가했습니다!");
        }


        // 🔹 아이템 제거
        internal void RemoveItem(Item item)
        {
            if (Inven.Contains(item))
            {
                Inven.Remove(item);
                Console.WriteLine($"[성공] {item.ItemName}을(를) 인벤토리에서 제거했습니다.");
            }
            else
            {
                Console.WriteLine($"[오류] {item.ItemName}이(가) 인벤토리에 없습니다.");
            }
        }

        // 🔹 아이템 장착
        internal void EquipItem(Item item)
        {
            if (!Inven.Contains(item))
            {
                Console.WriteLine($"[오류] {item.ItemName}이(가) 인벤토리에 없습니다!");
                return;
            }

            if (item.IsEquipped)
            {
                Console.WriteLine($"[경고] {item.ItemName}은(는) 이미 장착 중입니다!");
                return;
            }

            int equippedCount = Inven.Count(i => i.IsEquipped);
            if (equippedCount >= MaxEquippedItems)
            {
                Console.WriteLine($"[경고] 최대 {MaxEquippedItems}개의 아이템만 장착할 수 있습니다.");
                return;
            }

            item.IsEquipped = true;
            player.ApplyItemEffect(item);
            Console.WriteLine($"[성공] {item.ItemName}을(를) 장착했습니다!");
        }

        // 🔹 아이템 해제
        internal void UnequipItem(Item item)
        {
            if (!Inven.Contains(item))
            {
                Console.WriteLine($"[오류] {item.ItemName}이(가) 인벤토리에 없습니다!");
                return;
            }

            if (!item.IsEquipped)
            {
                Console.WriteLine($"[경고] {item.ItemName}은(는) 장착 중이 아닙니다!");
                return;
            }

            item.IsEquipped = false;
            player.LoseItemEffect(item);
            Console.WriteLine($"[성공] {item.ItemName}을(를) 해제했습니다!");
        }

        // 🔹 인벤토리 표시
        public void ShowInventory()
        {
            if (Inven.Count == 0)
            {
                Console.WriteLine("[인벤토리] 비어 있습니다.");
                return;
            }

            Console.WriteLine("[인벤토리 목록]");
            for (int i = 0; i < Inven.Count; i++)
            {
                var item = Inven[i];
                string equippedMark = item.IsEquipped ? "[E] " : "    ";
                Console.WriteLine($"{equippedMark}{i + 1}. {item.ItemName} / {item.ItemRank} / {item.Description} " +
                    $"(효과: {item.EffectDescription})");

            }
        }

        // 🔹 인벤토리 UI
        public void OpenInventory()
        {
            while (true)
            {
                Console.Clear();
                ShowInventory();

                Console.WriteLine("\n[아이템 번호] 장착/해제  [0] 나가기");
                Console.Write(">> ");
                string input = Console.ReadLine();

                if (input == "0")
                {
                    Console.Clear();
                    return;
                }

                if (int.TryParse(input, out int itemIndex) && itemIndex > 0 && itemIndex <= Inven.Count)
                {
                    var item = Inven[itemIndex - 1];
                    if (item.IsEquipped)
                        UnequipItem(item);
                    else
                        EquipItem(item);
                }
                else if (input == "2")
                {
                    Console.Write("제거할 아이템 번호 입력: ");
                    string removeInput = Console.ReadLine();
                    if (int.TryParse(removeInput, out int removeIndex) && removeIndex > 0 && removeIndex <= Inven.Count)
                    {
                        RemoveItem(Inven[removeIndex - 1]);
                    }
                    else
                    {
                        Console.WriteLine("[오류] 잘못된 입력입니다.");
                    }
                }
                else
                {
                    Console.WriteLine("[오류] 올바른 입력이 아닙니다.");
                }
            }
        }
    }
}
