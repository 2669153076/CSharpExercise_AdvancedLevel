using CSharp进阶实践;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp进阶实践
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Init();
            game.Start();
        }
    }
}
