using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElevatorV2.Service
{
   
    public class Display : IDisplay
    {
        Label _lblCabinDisplay;
        Label _lblFloorDisplay;
        public Display(Label labelcabin, Label labelFloor)
        {
            _lblCabinDisplay = labelcabin;
            _lblFloorDisplay = labelFloor;
        }
        public void ShowFloor(int numberFloor)
        {
            _lblCabinDisplay.Content=numberFloor.ToString();
            _lblFloorDisplay.Content = numberFloor.ToString();
            
        }
    }
}
