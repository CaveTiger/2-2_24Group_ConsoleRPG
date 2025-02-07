using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleRPG24.Stat;

namespace ConsoleRPG24
{
    internal class Shop
    {
        private MercenaryManager mercenaryManager;
        
        public Shop(MercenaryManager manager)
        {
            mercenaryManager = manager;
        }
        public void EnterShop(string location)
        {
            if (location == "마을")
            {
                Console.WriteLine("마을");
                ShowVillageShop();
            }
            else if (location == "던전")
            {
                Console.WriteLine("?");
                ShowDungeonShop();
            }
            else
            {
                Console.WriteLine("?");
            }
        }

        public void ShowVillageShop()
        {
            Console.WriteLine("[마을 상점]");
            Console.WriteLine("1. 물약 구매 ");
            Console.WriteLine("2. 잡화 구매");
            Console.WriteLine("3. 장비 구매");
            Console.WriteLine("0. 나가기 ");
            Console.Write(">> ");
            string input = Console.ReadLine();
            if (input == "1")
            {

            }
            else if (input == "2")
            {

            }
            else if (input == "3")
            {

            }
            else if (input == "0")
            {
                Console.WriteLine("상점을 나갑니다.");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

        }

        public void ShowDungeonShop()
        {
            Console.WriteLine("[던전 상점]");
            Console.WriteLine("1. ");
            Console.WriteLine("2. ");
            Console.WriteLine("0. ");
            Console.Write(">> ");
            string input = Console.ReadLine();
            if (input == "1")
            {

            }
            else if (input == "2")
            {

            }
            else if (input == "0")
            {
                Console.WriteLine("상점을 나갑니다.");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

        }
        public void ShowMercenaryShop()
        {
            Console.WriteLine("[용병소]");
            Console.WriteLine("1. 용병 고용");
            Console.WriteLine("2. 나가기");
            Console.Write(">> ");
            string input = Console.ReadLine();
            if (input == "1")
            {
                // 용병리스트
                //1.
                //2.
                //용병 선택후 고용 -> Inventory 
                
            }
            else if (input == "2")
            {
                Console.WriteLine("용병 상점을 나갑니다.");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
        public void BuyMercenary(Player player, Mercenary merc, int price)
        {
            if (player.Gold >= price)
            {
                player.Gold -= price;
                Console.WriteLine($"용병을 {price} 골드에 고용했습니다!");
                // MercenaryManager를 통해 용병 추가
            }
            else
            {
                Console.WriteLine("골드가 부족합니다.");
            }
        }
    }
}
