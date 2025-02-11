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
            var battle = new BattleSystem();
            battle.BattleStart();
        }
    }
}
