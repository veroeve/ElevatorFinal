using Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElevatorV2.Service
{
    class ControllerManouver : IControllerManouver
    {
        string _state;
        int _currentFloor = 0;
        int _currentHeight = 0;
        IDisplay _display;
        IDoor _door;
        ILevelSensor _levelSensor;
        IMotor _Motor;
        List<Request> _ltRequest = new List<Request>();
        TextBox _txtElevator;
        Label _lblCabinDisplay;
        Label _lblFloorDisplay;
        IButton _button;

        public ControllerManouver(TextBox text, IButton button, Label labelcabin, Label labelFloor)
        {
            _txtElevator = text;
            _lblCabinDisplay = labelcabin;
            _lblFloorDisplay = labelFloor;
            _display = new Display(_lblCabinDisplay, _lblFloorDisplay);
            _door = new Door(_txtElevator);
            _Motor = new Motor(_txtElevator);
            _button = button;
        }

        public bool RegisterRequest(int numberFloor, string typeRequest)
        {
            bool numError = true;

            if (_ltRequest.Count == 0)
            {
                if (numberFloor < _currentFloor)
                {
                    _state = TypeRequest.down.ToString();
                }
                else
                {
                    _state = TypeRequest.up.ToString();
                }

                Request request = new Request(numberFloor, _state);
                _ltRequest.Add(request);
                if (_state == TypeRequest.up.ToString())
                {
                    _ltRequest = _ltRequest.OrderBy(o => o.NumberFloor).ToList();
                }
                else
                {
                    _ltRequest = _ltRequest.OrderByDescending(o => o.NumberFloor).ToList();
                }

                numError = false;
            }
            else
            {
                if (typeRequest == string.Empty)
                {
                    if (numberFloor < _currentFloor)
                    {
                        typeRequest = TypeRequest.down.ToString();
                    }
                    else
                    {
                        typeRequest = TypeRequest.up.ToString();
                    }
                }
                //to do: controller the floor of cabin
                if (_state == typeRequest)
                {
                    Request request = new Request(numberFloor, typeRequest);
                    _ltRequest.Add(request);
                    if (_state == TypeRequest.up.ToString())
                    {
                        _ltRequest = _ltRequest.OrderBy(o => o.NumberFloor).ToList();
                    }
                    else
                    {
                        _ltRequest = _ltRequest.OrderByDescending(o => o.NumberFloor).ToList();
                    }
                    numError = false;
                }

            }

            return numError;
        }

        public void ExecuteRequest(List<Floor> ltFloor)
        {
            for (int i = 0; i < _ltRequest.Count; i++)
            {
                if (_ltRequest[i].NumberFloor == _currentFloor)
                {
                    _display.ShowFloor(_currentFloor);
                    _door.Open();
                    _door.Close();
                    string temp = _currentFloor.ToString();
                    ButtonCabin(temp);
                }
                else
                {
                    if (_ltRequest[i].TypeRequest == TypeRequest.up.ToString())
                    {
                        _Motor.MotorUp();

                    }
                    else
                    {
                        _Motor.MotorDown();
                    }
                    _levelSensor = new LevelSensor();
                    IObserver obs = new Door(_txtElevator);
                    IObserver obs1 = new Motor(_txtElevator);
                    _levelSensor.stateChange += new Notify(obs1.Notify);                  
                    _levelSensor.stateChange += new Notify(obs.Notify); 
                    ChangeLevel(ltFloor, _ltRequest[i].NumberFloor, _levelSensor, _ltRequest[i].TypeRequest);
                   
                    _door.Close();
                }
                _ltRequest.RemoveAt(i);
                i--;
            }
        }

        public void ChangeLevel(List<Floor> ltFloor, int floor, ILevelSensor level, string typeRequest)
        {
          
            while (floor != _currentFloor)
            {

                for (int i = 0; i <= ltFloor.Count - 1; i++)
                {
                    if (_currentHeight >= ltFloor[i].Height && _currentHeight < ltFloor[i + 1].Height)
                    {
                        _currentFloor = ltFloor[i].Number;                       
                        _display.ShowFloor(floor);
                        _txtElevator.AppendText($"Piso: {_currentFloor} \r\n");
                        ButtonFloor(ltFloor[i].TypeFloor);
                       
                    }
                }
                if (typeRequest == TypeRequest.up.ToString())
                {
                    _currentHeight++;

                }
                else
                {
                    _currentHeight--;
                }

            }
            if (floor == _currentFloor)
            {
                string temp = floor.ToString();
                ButtonCabin(temp);
                level.Change("Active");
            }
        }
       
        public void ButtonFloor(string typeFloor)
        {
            List<FloorButton> ltButton = _button.GetFloorButons();
            if (typeFloor == TypeFloor.floorup.ToString())
            {
                var btnup = ltButton.Find(name => name.Name == "up");
                var btndown = ltButton.Find(name => name.Name == "down");
                _button.Hide(btndown.Butons);
                _button.Show(btnup.Butons);
            }
            if (typeFloor == TypeFloor.floordown.ToString())
            {
                var btnup = ltButton.Find(name => name.Name == "up");
                var btndown = ltButton.Find(name => name.Name == "down");
                _button.Show(btndown.Butons);
                _button.Hide(btnup.Butons);
            }
            if (typeFloor == TypeFloor.floorboth.ToString())
            {
                var btnup = ltButton.Find(name => name.Name == "up");
                var btndown = ltButton.Find(name => name.Name == "down");
                _button.Show(btndown.Butons);
                _button.Show(btnup.Butons);
            }
        }
        public void ButtonCabin(string floor)
        {
            List<CabinButton> ltButton = _button.GetCabinButons();
            foreach (var item in ltButton)
            {
                if (item.Name == floor)
                {
                    _button.Change(item.Butons);
                }
            }
        }
    }
}
