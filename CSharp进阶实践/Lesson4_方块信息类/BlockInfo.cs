using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp进阶实践
{
    internal class BlockInfo
    {
        public BlockInfo(E_DrawType type) 
        {

            posList = new List<Position[]>();

            switch (type)
            {
                case E_DrawType.Cube:
                    posList.Add(new Position[3]
                    {
                        new Position(2,0),
                        new Position(0,1),
                        new Position(2,1)
                    });
                    break;
                case E_DrawType.Line:
                    posList.Add(new Position[3]
                    {
                        new Position(0,-1),
                        new Position(0,1),
                        new Position(0,2)
                    });
                    posList.Add(new Position[3]
                    {
                        new Position(2,0),
                        new Position(-2,0),
                        new Position(-4,0)
                    });
                    posList.Add(new Position[3]
                    {
                        new Position(0,1),
                        new Position(0,-1),
                        new Position(0,-2)
                    });
                    posList.Add(new Position[3]
                    {
                        new Position(-2,0),
                        new Position(2,0),
                        new Position(4,0)
                    });
                    break;
                case E_DrawType.Tank:
                    posList.Add(new Position[3]
                    {
                        new Position(-2,0),
                        new Position(0,1),
                        new Position(2,0)
                    });
                    posList.Add(new Position[3]
                    {
                        new Position(0,-1),
                        new Position(-2,0),
                        new Position(0,1)
                    });
                    posList.Add(new Position[3]
                    {
                        new Position(2,0),
                        new Position(0,-1),
                        new Position(-2,0)
                    });
                    posList.Add(new Position[3]
                    {
                        new Position(0,1),
                        new Position(2,0),
                        new Position(0,-1)
                    });
                    break;
                case E_DrawType.Left_Ladder:
                    posList.Add(new Position[3]
                    {
                        new Position(0,-1),
                        new Position(-2,0),
                        new Position(-2,1)
                    });
                    posList.Add(new Position[3]
                    {
                        new Position(2,0),
                        new Position(0,-1),
                        new Position(-2,-1)
                    });
                    posList.Add(new Position[3]
                    {
                        new Position(0,1),
                        new Position(2,0),
                        new Position(2,-1)
                    });
                    posList.Add(new Position[3]
                    {
                        new Position(-2,0),
                        new Position(0,1),
                        new Position(2,1)
                    });
                    break;
                case E_DrawType.Right_Ladder:
                    posList.Add(new Position[3]
                    {

                        new Position(0,-1),
                        new Position(2,0),
                        new Position(2,1)
                    });
                    posList.Add(new Position[3]
                    {
                        new Position(2,0),
                        new Position(0,1),
                        new Position(-2,1),
                    });
                    posList.Add(new Position[3]
                    {
                        new Position(0,1),
                        new Position(-2,0),
                        new Position(-2,-1),
                        
                    });
                    posList.Add(new Position[3]
                    {
                        new Position(2,-1),
                        new Position(0,-1),
                        new Position(-2,0),
                    });
                    break;
                case E_DrawType.Left_Long_Ladder:
                    posList.Add(new Position[3]
                    {
                        new Position(-2,-1),
                        new Position(0,-1),
                        new Position(0,1)
                    });
                    posList.Add(new Position[3]
                    {
                        new Position(2,-1),
                        new Position(2,0),
                        new Position(-2,0)
                    });
                    posList.Add(new Position[3]
                    {
                        new Position(2,1),
                        new Position(0,1),
                        new Position(0,-1),
                    });
                    posList.Add(new Position[3]
                    {
                        new Position(-2,1),
                        new Position(-2,0),
                        new Position(2,0)
                    });
                    break;
                case E_DrawType.Right_Long_Ladder:
                    posList.Add(new Position[3]
                    {
                        new Position(2,-1),
                        new Position(0,-1),
                        new Position(0,1)
                    });
                    posList.Add(new Position[3]
                    {
                        new Position(2,1),
                        new Position(2,0),
                        new Position(-2,0)
                    });
                    posList.Add(new Position[3]
                    {
                        new Position(-2,1),
                        new Position(0,-1),
                        new Position(0,1)
                    });
                    posList.Add(new Position[3]
                    {
                        new Position(-2,-1),
                        new Position(-2,0),
                        new Position(2,0)
                    });
                    break;
            }
        }

        /// <summary>
        /// 根据索引获取 位置偏移信息
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Position[] this[int index]
        {
            get
            {
                if (index < 0)
                    return posList[0];
                else if(index >= posList.Count)
                    return posList[posList.Count-1];
                else
                    return posList[index];
            }
        }

        public int Count { get => posList.Count; }

        //方块信息坐标的容器
        private List<Position[]> posList;
    }
}
