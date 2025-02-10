//using System;
//using System.Collections.Generic;
//using static ConsoleRPG24.Stat;

//namespace ConsoleRPG24
//{
//    internal class MercenaryManager
//    {
//        private List<Mercenary> mercenaries = new List<Mercenary>(); // 최대 9칸
//        private List<Mercenary> equippedMercenaries = new List<Mercenary>();
//        private const int MaxMercenaries = 9; // 최대 용병 9명 보유 가능
//        private const int MaxEquipped = 3; // 최대 장착 용병 3명

//        public MercenaryManager()
//        {
//            InitializeMercenaries(); //용병기본
//        }

//        // ✅ 기본 용병 3명 추가
//        private void InitializeMercenaries()
//        {
//            mercenaries.Add(new Mercenary("전사", 20, 10, 120, 120, 5));
//            mercenaries.Add(new Mercenary("궁수", 18, 8, 100, 100, 7));
//            mercenaries.Add(new Mercenary("도적", 15, 15, 150, 150, 3));
//        }

//        public void AddMercenary(Mercenary merc)
//        {
//            if (mercenaries.Count < MaxMercenaries)
//            {
//                mercenaries.Add(merc);
//                Console.WriteLine($"{merc.Name} 용병이 고용되었습니다!");
//            }
//            else
//            {
//                Console.WriteLine("더 이상 용병을 고용할 수 없습니다!");
//            }
//        }

//        public void RemoveMercenary(Mercenary merc)
//        {
//            if (mercenaries.Contains(merc))
//            {
//                mercenaries.Remove(merc);
//                equippedMercenaries.Remove(merc);
//                Console.WriteLine($"{merc.Name} 용병을 해고했습니다.");
//            }
//        }

//        public void EquipMercenary(Mercenary merc)
//        {
//            if (equippedMercenaries.Count < MaxEquipped && mercenaries.Contains(merc) && !equippedMercenaries.Contains(merc))
//            {
//                equippedMercenaries.Add(merc);
//                Console.WriteLine($"{merc.Name} 용병을 장착했습니다!");
//            }
//            else
//            {
//                Console.WriteLine("더 이상 용병을 장착할 수 없거나 이미 장착된 용병입니다.");
//            }
//        }

//        public void UnequipMercenary(Mercenary merc)
//        {
//            if (equippedMercenaries.Contains(merc))
//            {
//                equippedMercenaries.Remove(merc);
//                Console.WriteLine($"{merc.Name} 용병을 해제했습니다.");
//            }
//            else
//            {
//                Console.WriteLine("이 용병은 장착되어 있지 않습니다.");
//            }
//        }

//        public void ShowMercenaries()
//        {
//            Console.Clear();
//            Console.WriteLine("\n[보유 중인 용병 목록]");
//            for (int i = 0; i < mercenaries.Count; i++)
//            {
//                var merc = mercenaries[i];
//                string status = equippedMercenaries.Contains(merc) ? "[장착됨]" : "    ";
//                Console.WriteLine($"{i + 1}. {merc.Name} | ATK: {merc.Atk} | DEF: {merc.Defen} | HP: {merc.Health}/{merc.MaxHealth} {status}");
//            }

//            Console.WriteLine("\n0. 뒤로 가기");
//            Console.Write("\n>> 선택할 용병 번호 입력: ");
//            string input = Console.ReadLine();

//            if (int.TryParse(input, out int index) && index >= 1 && index <= mercenaries.Count)
//            {
//                ShowMercenaryDetails(mercenaries[index - 1]);
//            }
//            else if (input == "0")
//            {
//                Console.Clear();
//                return;
//            }
//            else
//            {
//                Console.WriteLine("잘못된 입력입니다. 다시 입력하세요.");
//            }
//        }

//        public void ShowMercenaryDetails(Mercenary merc)
//        {
//            while (true)
//            {
//                Console.Clear();
//                Console.WriteLine($"\n[용병 상세 정보]");
//                Console.WriteLine($"이름: {merc.Name}");
//                Console.WriteLine($"공격력: {merc.Atk}");
//                Console.WriteLine($"방어력: {merc.Defen}");
//                Console.WriteLine($"체력: {merc.Health}/{merc.MaxHealth}");
//                Console.WriteLine($"속도: {merc.Speed}");
//                Console.WriteLine("1. 장착");
//                Console.WriteLine("2. 해제");
//                Console.WriteLine("3. 해고");
//                Console.WriteLine("0. 뒤로 가기");
//                Console.Write(">>  ");

//                string input = Console.ReadLine();
//                if (input == "1")
//                {
//                    EquipMercenary(merc);
//                }
//                else if (input == "2")
//                {
//                    UnequipMercenary(merc);
//                }
//                else if (input == "3")
//                {
//                    Console.Write("정말 이 용병을 해고하시겠습니까? (Y/N): ");
//                    string confirm = Console.ReadLine().ToUpper();
//                    if (confirm == "Y")
//                    {
//                        RemoveMercenary(merc);
//                        break; // 해고 후 목록으로 돌아가기
//                    }
//                }
//                else if (input == "0")
//                {
//                    Console.Clear();
//                    return; // 이전 화면으로 돌아가기
//                }
//                else
//                {
//                    Console.WriteLine("잘못된 입력입니다. 다시 입력하세요.");
//                }
//            }
//        }
//    }
//}