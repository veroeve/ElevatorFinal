using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElevatorV2.Service
{
    public class Elevator : IElevator
    {
        IControllerManouver _controller;
        IButton _button;
        List<Floor> _ltFloor;
        TextBox _txtElevator;
        public Elevator(TextBox text, IButton button, IControllerManouver controller, List<Floor> ltFloor)
        {
            _txtElevator = text;
            _controller = controller;
            _ltFloor = ltFloor;
            _button = button;
        }
        public void SendRequest(int numberFloor, string typeRequest)
        {
           var numError= _controller.RegisterRequest(numberFloor, typeRequest);
            if(numError)
            {
                _txtElevator.AppendText("Reject request \r\n");
            }
            else
               
            {
                _txtElevator.AppendText("Request accepted \r\n");
            }
            
        }

        public void Execute()
        {
            _controller.ExecuteRequest(_ltFloor);
        }
    }
}
