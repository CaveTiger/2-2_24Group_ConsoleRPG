﻿// 인벤토리 클래스
public class Inventory
{
    public List<Item> Inven { get; set; } = new List<Item>();

    //인벤토리 열기
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
    /// 아이템 추가
    public void AddItem(Item item)
    {
        Inven.Add(item);
        Console.WriteLine($"{item.ItemName}을(를) 인벤토리에 추가했습니다!");
    }
    /// 인벤토리 아이템을 가져올때 반복문
    public void ShowInventory()
    {
        foreach (var item in Inven)
        {
            Console.WriteLine($"아이템: {item.ItemName} | 설명: {item.Description}");
        }
    }
    public void RemoveItem(Item item)
    {
        if (Inven.Contains(item))
        {
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
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < Inven.Count; i++)
            {
                var item = Inven[i];
                string equippedMark = item.IsEquipped ? "[E]" : "   ";
                Console.WriteLine($"- {i + 1} {equippedMark} {item.ItemName} | {item.ItemDivision} +{item.Attack}/{item.Defense}/{item.Health} | {item.Description}");
            }
            Console.WriteLine("0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요: ");
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
        item.IsEquipped = !item.IsEquipped;
        Console.WriteLine(item.IsEquipped ? $"{item.ItemName}을(를) 장착했습니다!" : $"{item.ItemName}을(를) 해제했습니다!");
    }
}
// 🔹 기본 캐릭터 클래스 (부모 클래스)