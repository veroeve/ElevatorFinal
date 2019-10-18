using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElevatorV2.Service
{
    class FloorDoor : IObserver, IDoor
    {
        TextBox _text;
        bool _state;
        public FloorDoor(TextBox text)
        {
            _text = text;
        }
        public void Close()
        {
            _text.AppendText("Close floor door \r\n");
            _state = false;
        }

        public void Notify(string numberFloor)
        {
            Open(numberFloor);
        }

        public void Open(string numberFloor)
        {
            _text.AppendText($"Floor {numberFloor}: Open door \r\n");
            _state = true;
        }
        public bool GetState()
        {
            return _state;
        }
    }
}
