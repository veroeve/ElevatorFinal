using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElevatorV2.Service
{
    class CabinDisplay : ICabinDisplay
    {
        Label _lblCabinDisplay;      
        public CabinDisplay(Label labelCabin)
        {
            _lblCabinDisplay = labelCabin;
        }
        public void ShowFloor(int numberFloor)
        {
            _lblCabinDisplay.Content = numberFloor.ToString();      

        }

    }
}
