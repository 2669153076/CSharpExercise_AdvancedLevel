using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp进阶实践
{
    /// <summary>
    /// 绘制类型 根据不同类型 绘制不同颜色的方块
    /// </summary>
    enum E_DrawType
    {
        /// <summary>
        /// 墙壁
        /// </summary>
        Wall,
        /// <summary>
        /// 正方形
        /// </summary>
        Cube,
        /// <summary>
        /// 长条
        /// </summary>
        Line,
        /// <summary>
        /// 凸
        /// </summary>
        Tank,
        /// <summary>
        /// 左梯子
        /// </summary>
        Left_Ladder,
        /// <summary>
        /// 右梯子
        /// </summary>
        Right_Ladder,
        /// <summary>
        /// 左长梯子
        /// </summary>
        Left_Long_Ladder,
        /// <summary>
        /// 右长梯子
        /// </summary>
        Right_Long_Ladder,
    }

    internal class DrawObject : IDraw
    {
        public DrawObject(E_DrawType type)
        {
            this.type = type;
        }
        public DrawObject(E_DrawType type,int x,int y):this(type)
        {
            this.pos = new Position(x, y);
        }

        public void Draw()
        {
            //屏幕外不用绘制
            if (pos.y < 0)
                return;

            Console.SetCursorPosition(pos.x, pos.y);
            
            //通过类型改变颜色
            switch (type)
            {
                case E_DrawType.Wall:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case E_DrawType.Cube:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case E_DrawType.Line:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case E_DrawType.Tank:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case E_DrawType.Left_Ladder:
                case E_DrawType.Right_Ladder:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case E_DrawType.Left_Long_Ladder:
                case E_DrawType.Right_Long_Ladder:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
            }

            Console.Write("■");
        }

        public void Clear()
        {
            if(pos.y < 0) return;   //屏幕外不用擦除
            Console.SetCursorPosition(pos.x,pos.y);
            Console.Write("  ");
        }

        /// <summary>
        /// 切换方块类型
        /// 主要用于砖块下落时，将其变为Wall类型
        /// </summary>
        /// <param name="type"></param>
        public void ChangeType(E_DrawType type)
        {
            this.type = type;
        }

        public Position pos;
        public E_DrawType type;
    }
}
