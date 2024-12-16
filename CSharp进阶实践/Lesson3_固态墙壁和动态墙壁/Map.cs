using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp进阶实践
{
    class Map
    {
        //初始化固定墙壁
        public Map(GameScene nowGameScene)
        {
            this.nowGameScene = nowGameScene;

            h = Game.height - 8;
            w = 0;
            //代表对应每行的计数初始化 默认为0
            recordInfo = new int[h];

            for(int i = 0; i < Game.width; i+=2)
            {
                walls.Add(new DrawObject(E_DrawType.Wall, i, h));
                ++w;
            }
            w -= 2;
            for(int i = 0; i < h; i++)
            {
                walls.Add(new DrawObject(E_DrawType.Wall, 0, i));
                walls.Add(new DrawObject(E_DrawType.Wall, Game.width - 2, i));
            }
        }

        public void Draw()
        {
            //绘制固定墙壁
            for (int i = 0; i < walls.Count; i ++)
            {
                walls[i].Draw();
            }
            //绘制动态墙壁
            for (int i = 0; i < dynamicWalls.Count; i++)
            {
                dynamicWalls[i].Draw();
            }
        }

        public void Clear()
        {
            for (int i = 0; i < dynamicWalls.Count; i++)
            {
                dynamicWalls[i].Clear();
            }
        }

        /// <summary>
        /// 提供给外部添加动态方块
        /// </summary>
        /// <param name="walls"></param>
        public void AddWalls(List<DrawObject> walls)
        {
            for (int i = 0; i < walls.Count; i++)
            {
                //将方块的类型变为Wall
                walls[i].ChangeType(E_DrawType.Wall);
                dynamicWalls.Add(walls[i]);

                //在动态墙壁添加处，发现位置顶满了，就结束
                if (walls[i].pos.y <= 0)
                {
                    //关闭输入线程
                    this.nowGameScene.StopThread();
                    //切换到结束界面
                    Game.ChangeScene(E_SceneType.End);
                    return;
                }

                //添加动态墙壁的计数
                //根据索引得到行
                //h 是 Game.h-6
                //y 最大是 Game.h-7
                recordInfo[h - 1 - walls[i].pos.y] += 1;
            }

            Clear();
            //检测移除
            CheckClear();
            Draw();
        }


        #region 消除
        public void CheckClear()
        {

            List<DrawObject> delList = new List<DrawObject>();
            //要选择记录行中有多少个方块的容器
            //数组
            //判断这个一行是否满(方块)
            //遍历数组检测数组里面存的数
            //是不是w-2
            for (int i = 0; i < recordInfo.Length; i++)
            {
                if (recordInfo[i] == w)
                {
                    //1.这一行的所有小方块移除
                    for (int j = 0; j < dynamicWalls.Count; j++)
                    {
                        //当前通过动态方块的y计算它在哪一行
                        //如果行号 和当前记录索引一致
                        //就证明 应该移除
                        if (i == h - 1 - dynamicWalls[j].pos.y)
                        {
                            //为了安全移除 添加记录列表
                            delList.Add(dynamicWalls[j]);
                        }
                        //2.要这一行之上的所有小方块下移一个单位
                        //如果当前的这个位置 是在该行以上
                        //那就该小方块 下移一格
                        else if (h - 1 - dynamicWalls[j].pos.y>i)
                        {
                            ++dynamicWalls[j].pos.y;
                        }
                    }
                    //移除待删除的小方块
                    for (int j = 0; j < delList.Count; j++)
                    {
                        dynamicWalls.Remove(delList[j]);
                    }

                    //3.记录小方块数量的数组从上到下迁移
                    for (int j = i; j < recordInfo.Length-1; j++)
                    {
                        recordInfo[j] = recordInfo[j + 1];
                    }
                    //置空最顶层的计数
                    recordInfo[recordInfo.Length - 1] = 0;

                    //消除一行后 再次去进行判断
                    CheckClear();
                    break;
                }
            }
        }
        #endregion

        public int w;   //一行可以有多少个动态墙壁
        public int h;

        private GameScene nowGameScene;

        //记录每一行有多少个小方块的容器
        //索引就是行号
        private int[] recordInfo;

        //固定墙壁
        private List<DrawObject> walls = new List<DrawObject>();
        //动态墙壁
        public List<DrawObject> dynamicWalls = new List<DrawObject>();
    }
}
