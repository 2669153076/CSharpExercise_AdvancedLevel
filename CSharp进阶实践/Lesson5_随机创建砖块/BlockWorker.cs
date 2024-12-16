using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp进阶实践
{
    /// <summary>
    /// 变形左右枚举
    /// 决定顺时针还是逆时针
    /// </summary>
    enum E_ChangeType
    {
        Left,
        Right,
    }

    internal class BlockWorker : IDraw
    {
        public BlockWorker()
        {
            //初始化砖块信息
            blockInfoDic = new Dictionary<E_DrawType, BlockInfo>()
            {
                {E_DrawType.Cube,new BlockInfo(E_DrawType.Cube)},
                {E_DrawType.Line,new BlockInfo(E_DrawType.Line)},
                {E_DrawType.Tank,new BlockInfo(E_DrawType.Tank)},
                {E_DrawType.Left_Ladder,new BlockInfo(E_DrawType.Left_Ladder)},
                {E_DrawType.Right_Ladder,new BlockInfo(E_DrawType.Right_Ladder)},
                {E_DrawType.Left_Long_Ladder,new BlockInfo(E_DrawType.Left_Long_Ladder)},
                {E_DrawType.Right_Long_Ladder,new BlockInfo(E_DrawType.Right_Long_Ladder)},
            };

            //绘制随机方块
            RandomCreatBlock();
        }

        /// <summary>
        /// 随机创建一个砖块
        /// </summary>
        public void RandomCreatBlock()
        {
            Random random = new Random();
            E_DrawType type = (E_DrawType)random.Next(1, 8);
            //每次新建一个砖块
            blocks = new List<DrawObject>
            {
                new DrawObject(type),
                new DrawObject(type),
                new DrawObject(type),
                new DrawObject(type),
            };
            //初始化方块位置
            //原点位置 随机
            blocks[0].pos = new Position(random.Next(4, Game.width / 2 - 4) * 2, -2);
            //其他三个方块的位置
            //取出方块的信息 来进行具体的随机
            //应该把取出来的 方块的具体信息存起来 用于之后变形
            nowBlockInfo = blockInfoDic[type];
            //随机几种形态中的一种 来设置方块的信息
            nowInfoIndex = random.Next(0, blocks.Count);
            Position[] pos = nowBlockInfo[nowInfoIndex];
            for (int i = 0; i < pos.Length; i++)
            {
                //取出来的pos是相对原点方块的坐标 所以需要进行计算
                blocks[i + 1].pos = blocks[0].pos + pos[i];
            }
        }

        public void Draw()
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].Draw();
            }
        }

        #region 变形相关方法
        /// <summary>
        /// 擦除方法
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].Clear();
            }
        }

        public void Change(E_ChangeType e_ChangeType)
        {
            //变之前把之前的位置擦除
            Clear();
            switch (e_ChangeType)
            {
                case E_ChangeType.Left:
                    --nowInfoIndex;
                    if (nowInfoIndex < 0)
                        nowInfoIndex = nowBlockInfo.Count - 1;
                    break;
                case E_ChangeType.Right:
                    ++nowInfoIndex;
                    if (nowInfoIndex >= nowBlockInfo.Count)
                        nowInfoIndex = 0;
                    break;
            }
            //得到索引目的是得到对应形态的位置偏移信息
            //用于设置另外的三个小方块
            Position[] pos = nowBlockInfo[nowInfoIndex];
            //将另外的三个小方块进行设置 计算
            for (int i = 0; i < pos.Length; i++)
            {
                blocks[i + 1].pos = blocks[0].pos + pos[i];
            }
            //变之后绘制
            Draw();
        }

        /// <summary>
        /// 是否可以进行变形
        /// </summary>
        /// <param name="type">变形方向</param>
        /// <param name="map">地图</param>
        /// <returns></returns>
        public bool CanChange(E_ChangeType type,Map map)
        {
            //用一个临时变量记录 当前索引 不变化当前索引
            //变化这个临时变量
            int nowIndex = nowInfoIndex;
            switch (type)
            {
                case E_ChangeType.Left:
                    --nowIndex;
                    if (nowIndex < 0)
                        nowIndex = nowBlockInfo.Count - 1;
                    break;
                case E_ChangeType.Right:
                    ++nowIndex;
                    if (nowIndex >= nowBlockInfo.Count)
                        nowIndex = 0;
                    break;
            }
            Position[] nowPos = nowBlockInfo[nowIndex];

            //判断是否超出边界
            Position tempPos;
            for (int i = 0; i < nowPos.Length; i++)
            {
                tempPos = blocks[i].pos + nowPos[i];
                //判断左右边界 和 下边界
                if (tempPos.x < 2 || tempPos.x >= Game.width - 2 || tempPos.y >= map.h)
                {
                    return false;
                }
            }

            //判断是否和地图上的动态方块重合
            for (int i = 0; i < nowPos.Length; i++)
            {
                tempPos = blocks[0].pos + nowPos[i];
                for (int j = 0; j < map.dynamicWalls.Count; j++)
                {
                    if (tempPos == map.dynamicWalls[j].pos)
                        return false;
                }
            }
            return true;
        }
        #endregion

        #region 左右移动相关
        public void Move(E_ChangeType type)
        {
            Clear();
            //根据传入的类型 决定左移还是右移
            //得到偏移位置
            Position movePos = new Position(type == E_ChangeType.Left ? -2 : 2, 0);
            //遍历所有小方块
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].pos += movePos;
            }
            Draw();
        }
        public bool CanMove(E_ChangeType type,Map map)
        {
            Position movePos = new Position(type == E_ChangeType.Left ? -2 : 2, 0);

            //与左右边界重合
            Position pos;
            for (int i = 0; i < blocks.Count; i++)
            {
                pos = blocks[i].pos + movePos;
                if (pos.x < 2 || pos.x >= Game.width - 2)
                    return false;
            }
            //与动态方块重合
            for (int i = 0; i < blocks.Count; i++)
            {
                pos = blocks[i].pos + movePos;
                for (int j = 0; j < map.dynamicWalls.Count; j++)
                {
                    if (pos == map.dynamicWalls[j].pos)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        #endregion

        #region 自动向下移动
        public void AutoFall()
        {
            Clear();
            //首先得到移动的多少
            Position fallMove = new Position(0, 1);
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].pos += fallMove;
            }
            Draw();
        }

        public bool CanFall(Map map)
        {
            Position fallPos = new Position(0, 1);
            Position pos;
            //边界
            for (int i = 0; i < blocks.Count; i++)
            {
                pos = blocks[i].pos+ fallPos;
                if (pos.y >= map.h)
                {
                    //停下来 将其变为地图动态方块
                    map.AddWalls(blocks);
                    //随机创建一个新的方块
                    RandomCreatBlock();
                    return false;
                }
            }
            //动态方块
            for (int i = 0; i < blocks.Count; i++)
            {
                pos = blocks[i].pos + fallPos;
                for (int j = 0; j < map.dynamicWalls.Count; j++)
                {
                    if (pos == map.dynamicWalls[j].pos)
                    {
                        //停下来 将其变为地图动态方块
                        map.AddWalls(blocks);
                        //随机创建一个新的方块
                        RandomCreatBlock();
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion

        private List<DrawObject> blocks;
        //记录各个方块的形态信息
        private Dictionary<E_DrawType,BlockInfo> blockInfoDic;
        //记录创造出来的方块的具体信息
        private BlockInfo nowBlockInfo;
        //当前形态索引
        private int nowInfoIndex;

    }
}
