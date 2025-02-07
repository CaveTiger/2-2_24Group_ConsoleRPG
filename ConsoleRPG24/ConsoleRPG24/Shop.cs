using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG24
{
    internal class Shop
    {
        void EnterShop(string location)
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

        void ShowVillageShop()
        {
            Console.WriteLine("[마을 상점]");
            Console.WriteLine("1. ");
            Console.WriteLine("2. ");
            Console.WriteLine("0. ");
        }

        void ShowDungeonShop()
        {
            Console.WriteLine("[던전 상점]");
            Console.WriteLine("1. ");
            Console.WriteLine("2. ");
            Console.WriteLine("0. ");
        }
    }
}
