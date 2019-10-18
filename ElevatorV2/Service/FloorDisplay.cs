using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElevatorV2.Service
{
   
    public class FloorDisplay : IFloorDisplay 
    {
        Label _lblFloorDisplay;
        public FloorDisplay( Label labelFloor)
        {
            _lblFloorDisplay = labelFloor;
        }

        public void ShowFloor(int numberFloor)
        {
            _lblFloorDisplay.Content = numberFloor.ToString();            
        }
      
    }
}
