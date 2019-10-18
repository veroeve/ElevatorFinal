using Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElevatorV2.Service
{
    public class FloorPanel : IFloorPanel
    {
        List<FloorButton> ltFloorButton = new List<FloorButton>();

     
        public void CreateButton(string name, Button button)
        {
          FloorButton _floorButton= new FloorButton(name,button);
          ltFloorButton.Add(_floorButton);
        }
        public void EnableFloorButton(TypeFloor typeFloor)
        {
            FloorButton button = new FloorButton();

            if (typeFloor == TypeFloor.floorup)
            {
                var btnup = ltFloorButton.Find(name => name.Name == "up");
                var btndown = ltFloorButton.Find(name => name.Name == "down");
                button.HideButton(btndown._Button);
                button.ShowButton(btnup._Button);
            }
            if (typeFloor == TypeFloor.floordown)
            {
                var btnup = ltFloorButton.Find(name => name.Name == "up");
                var btndown = ltFloorButton.Find(name => name.Name == "down");
                button.ShowButton(btndown._Button);
                button.HideButton(btnup._Button);
            }
            if (typeFloor == TypeFloor.floorboth)
            {
                var btnup = ltFloorButton.Find(name => name.Name == "up");
                var btndown = ltFloorButton.Find(name => name.Name == "down");
                button.ShowButton(btndown._Button);
                button.ShowButton(btnup._Button);
            }
        }
     
        
    }
}
