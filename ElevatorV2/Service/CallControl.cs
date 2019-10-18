using Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorV2.Service
{
    class CallControl : ICallControl
    {
        List<Request> _ltDownCall = new List<Request>();
        List<Request> _ltUploadCall = new List<Request>();
        List<Request> _ltOrderCabin = new List<Request>();

        public List<Request> LtDownCall
        {
            get { return _ltDownCall; }
            set { value = _ltDownCall; }
        }
        public List<Request> LtUploadCall
        {
            get { return _ltUploadCall; }
            set { value = _ltUploadCall; }
        }
        public List<Request> LtOrderCabin
        {
            get { return _ltOrderCabin; }
            set { value = _ltOrderCabin; }
        }


        public void RegisterCall(int nextFloor, string direction)
        {
            if (direction == Direction.up.ToString())
            {
                _ltUploadCall.Add(new Request(nextFloor, direction));
                _ltUploadCall.OrderBy(o => o.NumberFloor).ToList();
            }
            if (direction == Direction.down.ToString())
            {
                _ltDownCall.Add(new Request(nextFloor, direction));
                _ltDownCall.OrderByDescending(o => o.NumberFloor).ToList();
            }
            if (direction == string.Empty)
            {
                _ltOrderCabin.Add(new Request(nextFloor, direction));
                _ltOrderCabin.OrderBy(o => o.NumberFloor).ToList();
            }
        }
        public bool CallIsFull()
        {
            if (_ltDownCall.Count > 0)
            {
                return true;
            }
            else if (_ltUploadCall.Count > 0)
            {
                return true;
            }
            else if (_ltOrderCabin.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Do: Check if floor make a call to elevator
        public bool FloorMakeCall(int currentFloor,string ElevatorDirection, ILevelSensor level)
        {
            bool exist = false;
            List<Request> _ltTemporal;
            if (ElevatorDirection == Direction.up.ToString())
            {
                _ltTemporal = _ltUploadCall;
            }
            else
            {
                _ltTemporal = _ltDownCall;
            }
            foreach (var item in _ltTemporal)
            {
                exist = IsOnTheFloor(item.NumberFloor, currentFloor, ElevatorDirection);
            }         

            if (!exist)
            {
                foreach (var item in _ltOrderCabin)
                {
                    exist = IsOnTheFloor(item.NumberFloor, currentFloor, string.Empty);
                }
            }
            if (exist)
            {
                if (ElevatorDirection == Direction.up.ToString())
                {
                    RemoveUpCall(currentFloor);
                }
                if (ElevatorDirection == Direction.down.ToString())
                {
                    RemoveDownCall(currentFloor);
                }
                RemoveCall(currentFloor);
                level.Change(currentFloor.ToString());
            }
            return exist;
        }
        public bool IsOnTheFloor(int numberFloor, int currentFloor,string direction)
        {
            bool exist = false;
            if (numberFloor == currentFloor)
            {
               
                exist = true;
            }

            return exist;
        }



        // Do: Update and order lists of calls and update direction of elevator
        public string UpdateCallAndDirection(string currentDirection, int currentFloor)
        {
            if (currentDirection == Direction.up.ToString())
            {
                updateUpCall(currentFloor);
                if (_ltUploadCall.Count == 0)
                {
                    currentDirection = updateDirectionElevator(currentFloor, currentDirection);
                }


            }
            if (currentDirection == Direction.down.ToString())
            {
                updateDownCall(currentFloor);
                if (_ltDownCall.Count == 0)
                {
                    currentDirection = updateDirectionElevator(currentFloor, currentDirection);
                }

            }

            return currentDirection;
        }
        public void updateUpCall(int currentFloor)
        {           
            for (int i = 0; i < _ltUploadCall.Count; i++)
            {
                if (_ltUploadCall[i].NumberFloor < currentFloor)
                {
                    _ltDownCall.Add(_ltUploadCall[i]);
                    _ltDownCall.OrderByDescending(o => o.NumberFloor).ToList();
                    _ltUploadCall.RemoveAt(i);
                    i--;
                }

            }  
        }
        public void updateDownCall(int currentFloor)
        {
            for (int i = 0; i < _ltDownCall.Count; i++)
            {
                if (_ltDownCall[i].NumberFloor > currentFloor)
                {
                    _ltUploadCall.Add(_ltDownCall[i]);
                    _ltUploadCall.OrderBy(o => o.NumberFloor).ToList();
                    _ltDownCall.RemoveAt(i);
                    i--;
                }

            }
        }
        public string updateDirectionElevator(int currentFloor, string direction)
        {
            string updateDirection = direction;

            if (_ltOrderCabin.Count > 0)
            {
                if (_ltOrderCabin.First().NumberFloor > currentFloor)
                {
                    updateDirection = Direction.up.ToString();
                }
                else
                {
                    updateDirection = Direction.down.ToString();
                }
            }
            else
            {
                if (direction == Direction.up.ToString())
                {
                    if (_ltDownCall.Count() > 0)
                    {
                        updateDirection = Direction.down.ToString();
                    }
                }
                if (direction == Direction.down.ToString())
                {
                    if (_ltUploadCall.Count() > 0)
                    {
                        updateDirection = Direction.up.ToString();
                    }
                }

            }

            return updateDirection;
        }


        // Do: Remove calls that was attendant
        public void RemoveUpCall(int currentFloor)
        {
            for (int j = 0; j < _ltUploadCall.Count; j++)
            {
                if (_ltUploadCall[j].NumberFloor == currentFloor)
                {
                    _ltUploadCall.RemoveAt(j);
                    j--;
                }
            }
        }
        public void RemoveDownCall(int currentFloor)
        {
            for (int j = 0; j < _ltDownCall.Count; j++)
            {
                if (_ltDownCall[j].NumberFloor == currentFloor)
                {
                    _ltDownCall.RemoveAt(j);
                    j--;
                }
            }
        }
        public void RemoveCall(int currentFloor)
        {

            for (int j = 0; j < _ltOrderCabin.Count; j++)
            {
                if (_ltOrderCabin[j].NumberFloor == currentFloor)
                {
                    _ltOrderCabin.RemoveAt(j);
                    j--;
                }
            }
        }


    }
}
