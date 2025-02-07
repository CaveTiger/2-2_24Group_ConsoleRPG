using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleRPG24
{
    public class MainScreen
    {
        List<Item> itemlist = new List<Item>();

        public void GameStart()
        {
            string userName;

            /*
            Thread.Sleep(1000);
            Console.WriteLine("당신은 눈을 떴다.");
            Thread.Sleep(2500);
            Console.WriteLine("아주 긴 잠에서 깨어난 듯 하다.");
            Thread.Sleep(2500);
            */

            Console.Write("당신의 성함을 입력해 주십시오: ");
            userName = Console.ReadLine();
            Console.Clear();

            Console.WriteLine($"그래. 당신의 이름은 {userName}(이)다.");
            Thread.Sleep(2000);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("무엇을 할까?");
                Console.WriteLine(new string('=', 20));
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 던전 입장");
                Console.WriteLine(new string('=', 20));
                Console.WriteLine();
                Console.Write(">>");

                string chooseNum;
                chooseNum = Console.ReadLine();
            }
        }
    }
}

/*
switch (chooseNum)
{
    case ("1"):

        StatusScreen();

case ("2"):

        InventoryScreen();

case ("3"):

        ShopScreen();

    case ("4"):

        DungeonStage dungeonStage = new DungeonStage();
        dungeonStage.Start();

    default:


    Console.WriteLine("올바른 숫자를 입력해 주십시오.");
        continue;
}
}

}


public void StatusScreen()
{

}

public void InventoryScreen()
{

}

public void ShopScreen()
{

}

public void DungeonScreen()
{

}
}
}
*/