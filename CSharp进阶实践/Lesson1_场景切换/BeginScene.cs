using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp进阶实践
{
    class BeginScene : BeginOrEndBaseScene
    {
        public BeginScene() {
            strTitle = "俄罗斯方块";
            strOne = "开始游戏";
        }

        public override void EnterJDoSomthing()
        {
            if(currentSelIndex == 0)
            {
                Game.ChangeScene(E_SceneType.Game);
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
