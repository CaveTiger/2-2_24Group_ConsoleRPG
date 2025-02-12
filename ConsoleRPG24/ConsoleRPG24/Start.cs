using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG24
{
    internal class Start
    {
        static void Main(string[] args)
        {
            Player player = new Player(); // Player 객체 생성 및 초기화
            List<Item> itemList = new List<Item>(); // Item 리스트 생성 및 초기화
            Stage stage = new Stage(); // Stage 객체 생성 및 초기화
            Monster monster = new Monster("",0,0,0,0,0); // Stage 객체 생성 및 초기화

            var battle = new BattleSystem(player,itemList,stage,monster);
            battle.Battle();
        }
    }
}
