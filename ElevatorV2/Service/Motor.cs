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
        public void MotorDown()
        {
           
            _text.AppendText("Motor Down \r\n");
        }

        public void MotorUp()
        {
          
            _text.AppendText("Motor Up \r\n");
        }

        public void Notify(string state)
        {
            if (state == "Active")
            {
                Stop();
            }
        }

        public void Stop()
        {
            _text.AppendText("Motor Stop \r\n");
        }
    }
}
