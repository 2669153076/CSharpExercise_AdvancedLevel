using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp进阶实践
{
    class EndScene : BeginOrEndBaseScene
    {
        public EndScene() {
            strTitle = "游戏结束";
            strOne = "返回主菜单";
        }

        public override void EnterJDoSomthing()
        {
            if(currentSelIndex==0)
            {
                Game.ChangeScene(E_SceneType.Begin);
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
