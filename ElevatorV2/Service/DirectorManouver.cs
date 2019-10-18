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
    class DirectorManouver : IDirectorManouver
    {
        ICallControl _callControl;
        IFloorPanel _floorPanel;
        ICabinPanel _cabinPanel;
        IFloorDisplay _floorDisplay;
        ICabinDisplay _cabinDisplay;
        IMotor _motor;
        IDoor _floorDoor;
        IDoor _cabinDoor;
        ILevelSensor _levelSensor;
        ICabinSensor _cabinSensor;
        ITimerSensor _timer = new TimerSensor();
        string _currentDirection;
        int _currentFloor = 0;
        int _currentHeight = 0;
        TextBox _txtElevator;
        bool _isOpen;

        public DirectorManouver(TextBox text, IFloorPanel floorPanel, ICabinPanel cabinpanel, IFloorDisplay floorDisplay, ICabinDisplay cabinDisplay, IMotor motor, IDoor floorDoor, IDoor cabinDoor)
        {
            _txtElevator = text;
            _floorPanel = floorPanel;
            _cabinPanel = cabinpanel;
            _floorDisplay = floorDisplay;
            _cabinDisplay = cabinDisplay;
            _motor = motor;
            _floorDoor = floorDoor;
            _cabinDoor = cabinDoor;
            _callControl = new CallControl();
            _cabinSensor = new CabinSensor();
        }

        public void RegisterCall(int numberFloor, string typeCall)
        {
            if (_currentDirection==null)
            {
                _currentDirection = typeCall;
            }
            _callControl.RegisterCall(numberFloor, typeCall);
            _txtElevator.AppendText($"Order accepted to go to floor{numberFloor}\r\n");

        }
        public void ExecuteCalls(List<Floor> ltFloor)
        {
            if(_isOpen)
            {
                _timer.Stop();
                _floorDoor.Close();
                _isOpen = false;
            }
            if (_callControl.CallIsFull())
            {
                _levelSensor = new LevelSensor();
                IObserver motorObserver = new Motor(_txtElevator);
                IObserver cabinObserver = new CabinDoor(_txtElevator);
                IObserver floorObserver = new FloorDoor(_txtElevator);
                _levelSensor.stateChange += new Notify(motorObserver.Notify);
                _levelSensor.stateChange += new Notify(cabinObserver.Notify);
                _levelSensor.stateChange += new Notify(floorObserver.Notify);

                string direction = _callControl.UpdateCallAndDirection(_currentDirection, _currentFloor);
                if(_currentDirection!=direction)
                {
                    _currentDirection = direction;
                    UpdateCurrentHeight(ltFloor);
                }
                if (_currentDirection == Direction.up.ToString())
                {
                    _currentHeight = _motor.MotorUp(_currentHeight);
                }
                if (_currentDirection == Direction.down.ToString())
                {
                    _currentHeight = _motor.MotorDown(_currentHeight);
                }
                if(_cabinSensor.IsOnTheNextFloor(ltFloor,_currentFloor,_currentHeight,_currentDirection))
                {
                    UpdateCurrentFloor();
                    UpdateCurrentHeight(ltFloor);
                    _floorDisplay.ShowFloor(_currentFloor);
                    _cabinDisplay.ShowFloor(_currentFloor);
                    _cabinPanel.TurnOffCabinButtonForFloor(_currentFloor.ToString());
                   _isOpen= _callControl.FloorMakeCall(_currentFloor, _currentDirection, _levelSensor);
                   
                }     
            }
        }

        private void UpdateCurrentFloor()
        {
            if (_currentDirection == Direction.up.ToString())
            {
                _currentFloor = _currentFloor + 1;
            }
            if (_currentDirection == Direction.down.ToString())
            {
                _currentFloor = _currentFloor - 1;
            }

        }
        private void UpdateCurrentHeight(List<Floor> ltFloor)
        {
            if (_currentDirection == Direction.up.ToString())
            {
                _currentHeight = 0;
            }
            if (_currentDirection == Direction.down.ToString())
            {
                foreach (var item in ltFloor)
                {
                    if (item.Number == _currentFloor)
                    {
                        _currentHeight = item.Height;
                    }
                }
            }

        }
        
    }
}
