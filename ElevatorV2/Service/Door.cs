using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElevatorV2.Service
{
    class Door : IObserver, IDoor
    {
        TextBox _text;
        public Door(TextBox text)
        {
            _text = text;
        }
        public void Close()
        {
            int count = 0;
           
            _text.AppendText("Close door \r\n");
        }

        public void Notify(string state)
        {
            if (state == "Active")
            {
                Open();
            }
        }

        public void Open()
        {
            _text.AppendText("Open door \r\n");

            _text.AppendText("Waiting door... \r\n");
            int count = 0;
            const int Timewait = 800;
            while (count < Timewait)
            {
                count++;
            }

        }
    }
}
