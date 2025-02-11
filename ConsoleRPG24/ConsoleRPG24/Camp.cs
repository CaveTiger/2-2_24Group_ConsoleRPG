using System;
using System.Numerics;

namespace ConsoleRPG24;

internal partial class Camp
{
    int campCount = 3;

    Player player;


    public Camp(Player player)
    {
        this.player = player;
        //여기서의 this는 캠프
    }


    public void CampCount()
    {
        //캠프를 차렸다.
        //캠프는 전체 3회 가능
        //하시겠습니까? (3/3)

        Console.WriteLine($"캠프를 차릴까? 남은 횟수: {campCount} / 3");
        Console.WriteLine();
        Console.WriteLine("1. 캠핑하기");
        Console.WriteLine("0. 무시하고 진행");
        Console.WriteLine();
        Console.WriteLine(">> ");
        string input01;
        input01 = Console.ReadLine();

        if (input01 == "1")
        {
            if (campCount == 0)
            {
                Console.WriteLine("더이상 캠프를 차릴 자재가 남아있지 않습니다.");

                Console.WriteLine();
                Console.WriteLine("0. 다음으로 진행");
                Console.WriteLine();
                Console.WriteLine(">> ");

                string input02;
                input02 = Console.ReadLine();

                if (input02 == "0")
                {
                    //다음 던전으로~!
                }
            }

            else
            {
                if (player.Health == player.MaxHealth)
                {
                    Console.WriteLine("당신은 이미 최대 체력이다.");
                    Console.WriteLine();
                    Console.WriteLine("0. 다음으로 진행");
                    Console.WriteLine();
                    Console.WriteLine(">> ");

                    string input03;
                    input03 = Console.ReadLine();

                    if (input03 == "0")
                    {
                        //다음 던전으로~!
                    }
                }
                else
                {
                    campCount--;
                    Camping(player);
                }
            }
        }
    }

    public void Camping(Player player)
    {

        Console.WriteLine("던전 한 켠에 작은 캠프를 차렸습니다.");
        Thread.Sleep(1500);

        player.Health = player.MaxHealth;
        Console.WriteLine($"{player.Name}은 휴식을 취했다.");
        Console.WriteLine();


        Console.WriteLine("0. 다음 층으로.");
        string input;
        input = Console.ReadLine();

        if (input == "0")
        {
            Console.Clear();
            //다음 던전으로~!
        }
    }
}

