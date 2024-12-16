using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp进阶实践
{
    abstract class BeginOrEndBaseScene : ISceneUpdate
    {
        public void Update()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(Game.width/2 - strTitle.Length, Game.height / 2 - 4);
            Console.WriteLine(strTitle);

            Console.SetCursorPosition(Game.width / 2 - strOne.Length, Game.height / 2 + 2);
            Console.ForegroundColor = currentSelIndex == 0 ? ConsoleColor.Red : ConsoleColor.White;
            Console.WriteLine(strOne);
            Console.SetCursorPosition(Game.width / 2 - 4, Game.height / 2 + 4);
            Console.ForegroundColor = currentSelIndex == 1 ? ConsoleColor.Red : ConsoleColor.White;
            Console.WriteLine("结束游戏");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.W:
                    --currentSelIndex;
                    if (currentSelIndex <= 0)
                        currentSelIndex = 0;
                    break;
                case ConsoleKey.S:
                    ++currentSelIndex;
                    if (currentSelIndex >= 1)
                        currentSelIndex = 1;
                    break;
                case ConsoleKey.J:
                    EnterJDoSomthing();
                    break;
            }
        }

        public abstract void EnterJDoSomthing();

        protected string strTitle;
        protected string strOne;
        protected int currentSelIndex = 0;
    }
}
