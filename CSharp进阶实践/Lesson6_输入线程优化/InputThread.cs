using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp进阶实践
{
    internal class InputThread
    {
        private static InputThread instance = new InputThread();

        public static InputThread Instance { get => instance; }

        private InputThread()
        {
            inputThread = new Thread(InputCheck);
            inputThread.IsBackground = true;
            inputThread.Start();
        }

        private void InputCheck()
        {
            while (true)
            {
                inputEvent?.Invoke();
            }
        }

        //线程成员变量
        Thread inputThread;

        public event Action inputEvent;
    }
}
