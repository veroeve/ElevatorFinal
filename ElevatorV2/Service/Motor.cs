using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElevatorV2.Service
{
    class Motor : IMotor, IObserver 
    {
        TextBox _text;
        public Motor(TextBox text)
        {
            _text = text;
        }
        public int MotorDown(int currentheight)
        {
            currentheight--;
            _text.AppendText("Motor Down \r\n");
            return currentheight;
        }

        public int MotorUp(int currentheight)
        {
            currentheight++;
            _text.AppendText("Motor Up \r\n");
            return currentheight;
        }

        public void Notify(string numberFloor)
        {
            Stop(numberFloor);
        }

        public void Stop(string numberFloor)
        {
            _text.AppendText($"Motor Stopped on the floor:{numberFloor} \r\n");
        }
    }
}
