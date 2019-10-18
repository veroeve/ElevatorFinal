using Services.Enums;
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
        IDirectorManouver _Director;
        IFloorPanel _floorPanel= new FloorPanel();
        ICabinPanel _cabinPanel = new CabinPanel();       
        IFloorDisplay _floorDisplay;
        ICabinDisplay _cabinDisplay;
        IMotor _motor;
        IDoor _floorDoor;
        IDoor _cabinDoor;        
        List<Floor> _ltFloor= new List<Floor>();
        TextBox _txtElevator;
        public Elevator(TextBox text, Label displayFloor, Label displayCabin, Dictionary<Direction, Button> dictionaryFloorButton, Dictionary<string, Button> dictionaryCabinButton)
        {
            _txtElevator = text;
            _floorDoor = new FloorDoor(_txtElevator);
            _cabinDoor = new CabinDoor(_txtElevator);
            _motor = new Motor(_txtElevator);
            _floorDisplay = new FloorDisplay(displayFloor);
            _cabinDisplay = new CabinDisplay(displayCabin);
            foreach (var item in dictionaryCabinButton)
            {
                _cabinPanel.CreateButton(item.Key, item.Value);
            }
            foreach (var item in dictionaryFloorButton)
            {
                _floorPanel.CreateButton(item.Key, item.Value);
            }
            _Director = new DirectorManouver(_txtElevator, _floorPanel, _cabinPanel,_floorDisplay,_cabinDisplay,_motor,_floorDoor,_cabinDoor);
        }
        public void RegisterRequest(int numberFloor, Direction typeRequest)
        {
            _Director.RegisterCall(numberFloor, typeRequest);         
            
        }

        public void ExecuteCalls()
        {
            _Director.ExecuteCalls(_ltFloor);
        }

        public void CreateFloor(int number, int height, TypeFloor typeFloor)
        {
            _ltFloor.Add(new Floor(number, height, typeFloor));
        }
 
        public void ChangeButtonFor(int floor)
        {            
            foreach(var item in _ltFloor)
            {
                if(item.Number == floor)
                {
                    _floorPanel.EnableFloorButton(item.TypeFloor);
                }
            }

        }
    }
}
