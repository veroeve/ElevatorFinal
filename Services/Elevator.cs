using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Services
{
    public class Elevator : IElevator
    {
        IController _controller;
        ICabin _cabin;
        TextBox _txtElevator;
        public Elevator(TextBox Text)
        {
            _txtElevator = Text;
        }
        public void SendRequest(int numberFloor, string typeRequest)
        {
            _controller.RegisterRequest(numberFloor, typeRequest);
            _txtElevator.AppendText("");
            
        }
    }
}
