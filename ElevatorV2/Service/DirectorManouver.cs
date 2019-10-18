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
        Direction _currentDirection=Direction.none;
        int _elevatorCurrentFloor = 0;
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

        public void RegisterCall(int numberFloor, Direction typeRequest)
        {
            EnsureCurrentDirection(typeRequest);
            _callControl.RegisterRequest(numberFloor, typeRequest);
            _txtElevator.AppendText($"Order accepted to go to floor{numberFloor}\r\n");

        }

        private void EnsureCurrentDirection(Direction typeRequest)
        {
            if (_currentDirection == Direction.none)
            {
                _currentDirection = typeRequest;
            }
        }

        public void ExecuteCalls(List<Floor> ltFloor)
        {
            if(_isOpen)
            {
                _timer.Stop();
                _floorDoor.Close();
                _isOpen = false;
            }
            if (_callControl.HasAnyRequest())
            {
                _levelSensor = new LevelSensor();
                IObserver motorObserver = new Motor(_txtElevator);
                IObserver cabinObserver = new CabinDoor(_txtElevator);
                IObserver floorObserver = new FloorDoor(_txtElevator);
                _levelSensor.stateChange += new Notify(motorObserver.Notify);
                _levelSensor.stateChange += new Notify(cabinObserver.Notify);
                _levelSensor.stateChange += new Notify(floorObserver.Notify);

                Direction direction = _callControl.UpdateRequestAndDirection(_currentDirection, _elevatorCurrentFloor);
                if(_currentDirection!=direction)
                {
                    _currentDirection = direction;
                    UpdateCurrentHeight(ltFloor);
                }
                if (_currentDirection == Direction.up)
                {
                    _currentHeight = _motor.MotorUp(_currentHeight);
                }
                if (_currentDirection == Direction.down)
                {
                    _currentHeight = _motor.MotorDown(_currentHeight);
                }
                if(_cabinSensor.IsOnTheNextFloor(ltFloor,_elevatorCurrentFloor,_currentHeight,_currentDirection))
                {
                    UpdateCurrentFloor();
                    UpdateCurrentHeight(ltFloor);
                    _floorDisplay.ShowFloor(_elevatorCurrentFloor);
                    _cabinDisplay.ShowFloor(_elevatorCurrentFloor);
                    _cabinPanel.TurnOffCabinButtonForFloor(_elevatorCurrentFloor.ToString());
                   _isOpen= _callControl.FloorMakeCall(_elevatorCurrentFloor, _currentDirection, _levelSensor);
                   
                }     
            }
        }

        private void UpdateCurrentFloor()
        {
            if (_currentDirection == Direction.up)
            {
                _elevatorCurrentFloor = _elevatorCurrentFloor + 1;
            }
            if (_currentDirection == Direction.down)
            {
                _elevatorCurrentFloor = _elevatorCurrentFloor - 1;
            }

        }
        private void UpdateCurrentHeight(List<Floor> ltFloor)
        {
            if (_currentDirection == Direction.up)
            {
                _currentHeight = 0;
            }
            if (_currentDirection == Direction.down)
            {
                foreach (var item in ltFloor)
                {
                    if (item.Number == _elevatorCurrentFloor)
                    {
                        _currentHeight = item.Height;
                    }
                }
            }

        }
        
    }
}
