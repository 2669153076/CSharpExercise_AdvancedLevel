using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp进阶实践
{
    internal class GameScene : ISceneUpdate
    {
        public GameScene()
        {
            map = new Map(this);
            blockWorker = new BlockWorker();
            InputThread.Instance.inputEvent += CheckInputThread;
            //inputThreed = new Thread(CheckInputThread);
            //inputThreed.IsBackground = true;
            //inputThreed.Start();
            //isRunning = true;
        }

        public void Update()
        {
            //锁里面不要包含 休眠
            lock (blockWorker)
            {
                //绘制
                map.Draw();
                blockWorker.Draw();
                //移动
                if (blockWorker.CanFall(map))
                    blockWorker.AutoFall();
            }
            Thread.Sleep(200);
        }

        private void CheckInputThread()
        {
            //while (isRunning)
            //{
            if (Console.KeyAvailable)
            {
                //为了避免影响主线程 在输入后加锁
                lock (blockWorker)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.LeftArrow:
                            if (blockWorker.CanChange(E_ChangeType.Left, map))
                                blockWorker.Change(E_ChangeType.Left);
                            break;
                        case ConsoleKey.RightArrow:
                            if (blockWorker.CanChange(E_ChangeType.Right, map))
                                blockWorker.Change(E_ChangeType.Right);
                            break;
                        case ConsoleKey.A:
                            if (blockWorker.CanMove(E_ChangeType.Left, map))
                                blockWorker.Move(E_ChangeType.Left);
                            break;
                        case ConsoleKey.D:
                            if (blockWorker.CanMove(E_ChangeType.Right, map))
                                blockWorker.Move(E_ChangeType.Right);
                            break;
                        case ConsoleKey.S:
                            if (blockWorker.CanFall(map))
                                blockWorker.AutoFall();
                            break;
                    }
                }
            }
            //}
        }
        //停止线程
        public void StopThread()
        {
            //isRunning = false;
            //inputThreed = null;
            InputThread.Instance.inputEvent -= CheckInputThread;
        }

        Map map;
        BlockWorker blockWorker;
        //Thread inputThreed;
        //bool isRunning;
    }
}
