using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp进阶实践
{
    enum E_SceneType
    {
        Begin,
        Game,
        End
    }

    class Game 
    {
        //初始化控制台
        public void Init()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);

            ChangeScene(E_SceneType.Begin);
        }

        public void Start()
        {
            while (true)
            {
                if(currentScene!=null)
                {
                    currentScene.Update();
                }
            }
        }

        public static void ChangeScene(E_SceneType e_SceneType)
        {
            Console.Clear();
            switch (e_SceneType)
            {
                case E_SceneType.Begin:
                    currentScene = new BeginScene();
                    break;
                case E_SceneType.Game:
                    currentScene = new GameScene();
                    break;
                case E_SceneType.End:
                    currentScene = new EndScene();
                    break;
            }
        }

        public static ISceneUpdate currentScene;

        public const int width = 50;
        public const int height = 35;
    }
}
