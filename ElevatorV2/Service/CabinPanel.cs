using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElevatorV2.Service
{
    class CabinPanel : ICabinPanel
    {
        List<CabinButton> ltCabinButton = new List<CabinButton>();
        public void CreateButton(string name, Button button)
        {
            CabinButton _cabinButton = new CabinButton(name, button);
            ltCabinButton.Add(_cabinButton);
        }

        public void TurnOffCabinButtonForFloor(string numberFloor)
        {
            CabinButton cabinButton = new CabinButton();
            foreach (var item in ltCabinButton)
            {
                if (item.Name == numberFloor)
                {
                    cabinButton.ChangeColor(item.Butons);
                }
            }
        }
    }
}
