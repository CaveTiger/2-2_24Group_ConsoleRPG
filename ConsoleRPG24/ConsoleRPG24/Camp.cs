using System;

namespace ConsoleRPG24;

internal partial class Camp
{

    //캠프 아직 구현 중입니다!!


    /// <summary>
    /// player 인스턴스 받아오기!
    /// </summary>
    /// <param name="player">야호~!</param>

    public void Camping(Player player)
    {

        //캠프를 차렸다.
        //캠프는 전체 3회 가능
        //하시겠습니까? (3/3)
        //모닥불 앞에서 몸을 녹였다.
        //최대 체력, 최대 마나 회복!
        //그리고 다음 이벤트로 넘어가기
        

        string input;

        Console.WriteLine($"{player.Name}은 휴식을 취했다.");
        Console.WriteLine();
        Console.WriteLine("0. 다음 층으로.");
        input = Console.ReadLine();

        if (input == "0")
        {
            Console.Clear();
            
        }

        else
        {
            Console.WriteLine();
            Console.WriteLine("당신은 선택지에 없는 다른 행동을 취하려했지만 몸이 생각대로 움직이지 않아 이내 그만두었다.");
            Thread.Sleep(1500);
        }
    }
}
