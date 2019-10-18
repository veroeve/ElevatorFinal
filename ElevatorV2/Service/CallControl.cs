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
        List<Request> _toGoUpRequests = new List<Request>();
        List<Request> _ltOrderCabin = new List<Request>();

        public List<Request> LtDownCall
        {
            get { return _ltDownCall; }
            set { value = _ltDownCall; }
        }
        public List<Request> LtUploadCall
        {
            get { return _toGoUpRequests; }
            set { value = _toGoUpRequests; }
        }
        public List<Request> LtOrderCabin
        {
            get { return _ltOrderCabin; }
            set { value = _ltOrderCabin; }
        }


        public void RegisterRequest(int nameFloor, Direction typeRequest)
        {
            if (typeRequest == Direction.up)
            {
                _toGoUpRequests.Add(new Request(nameFloor, typeRequest));
                _toGoUpRequests.OrderBy(o => o.NumberFloor).ToList();
            }
            else if (typeRequest == Direction.down)
            {
                _ltDownCall.Add(new Request(nameFloor, typeRequest));
                _ltDownCall.OrderByDescending(o => o.NumberFloor).ToList();
            }
            else if (typeRequest == Direction.none)
            {
                _ltOrderCabin.Add(new Request(nameFloor, typeRequest));
                _ltOrderCabin.OrderBy(o => o.NumberFloor).ToList();
            }
        }
        public bool HasAnyRequest()
        {
            if (_ltDownCall.Count > 0)
            {
                return true;
            }
            if (_toGoUpRequests.Count > 0)
            {
                return true;
            }
            if (_ltOrderCabin.Count > 0)
            {
                return true;
            }

            return false;
        }

        // Do: Check if floor make a call to elevator
        public bool FloorMakeCall(int currentFloor, Direction elevatorDirection, ILevelSensor level)
        {
            List<Request> _ltTemporal = GetRelatedRequestList(elevatorDirection);
            bool exist = IsThereAnyCallForCurrentFloor(currentFloor, elevatorDirection, _ltTemporal);

            if (!exist)
            {
                 exist = IsThereAnyCallForCurrentFloor(currentFloor, elevatorDirection, _ltOrderCabin);
            }

            if (exist)
            {
                RemoveCallFromRequestLists(currentFloor, elevatorDirection);
                level.Change(currentFloor.ToString());
            }
            return exist;
        }

        private void RemoveCallFromRequestLists(int currentFloor, Direction elevatorDirection)
        {
            if (elevatorDirection == Direction.up)
            {
                RemoveUpCall(currentFloor);
            }
            else if (elevatorDirection == Direction.down)
            {
                RemoveDownCall(currentFloor);
            }
            RemoveCall(currentFloor);
        }

        private bool IsThereAnyCallForCurrentFloor(int currentFloor, Direction elevatorDirection, List<Request> _ltTemporal)
        {
            foreach (var item in _ltTemporal)
            {
                if (IsOnTheFloor(item.NumberFloor, currentFloor))
                {
                    return true;
                }
            }

            return false;
        }

        private List<Request> GetRelatedRequestList(Direction elevatorDirection)
        {
            if (elevatorDirection == Direction.up)
            {
                return _toGoUpRequests;
            }

            return _ltDownCall;
        }

        public bool IsOnTheFloor(int numberFloor, int currentFloor)
        {
            return numberFloor == currentFloor;
        }



        // Do: Update and order lists of calls and update direction of elevator
        public Direction UpdateRequestAndDirection(Direction elevatorDirection, int currentFloor)
        {
            if (elevatorDirection == Direction.up)
            {
                updateUpCall(currentFloor);
                if (_toGoUpRequests.Count == 0)
                {
                    elevatorDirection = updateDirectionElevator(currentFloor, elevatorDirection);
                }


            }
            if (elevatorDirection == Direction.down)
            {
                updateDownCall(currentFloor);
                if (_ltDownCall.Count == 0)
                {
                    elevatorDirection = updateDirectionElevator(currentFloor, elevatorDirection);
                }

            }

            return elevatorDirection;
        }
        public void updateUpCall(int currentFloor)
        {           
            for (int i = 0; i < _toGoUpRequests.Count; i++)
            {
                if (_toGoUpRequests[i].NumberFloor < currentFloor)
                {
                    _ltDownCall.Add(_toGoUpRequests[i]);
                    _ltDownCall.OrderByDescending(o => o.NumberFloor).ToList();
                    _toGoUpRequests.RemoveAt(i);
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
                    _toGoUpRequests.Add(_ltDownCall[i]);
                    _toGoUpRequests.OrderBy(o => o.NumberFloor).ToList();
                    _ltDownCall.RemoveAt(i);
                    i--;
                }

            }
        }
        public Direction updateDirectionElevator(int currentFloor, Direction elevatorDirection)
        {
            Direction updateDirection = elevatorDirection;

            if (_ltOrderCabin.Count > 0)
            {
                if (_ltOrderCabin.First().NumberFloor > currentFloor)
                {
                    updateDirection = Direction.up;
                }
                else
                {
                    updateDirection = Direction.down;
                }
            }
            else
            {
                if (elevatorDirection == Direction.up)
                {
                    if (_ltDownCall.Count() > 0)
                    {
                        updateDirection = Direction.down;
                    }
                }
                if (elevatorDirection == Direction.down)
                {
                    if (_toGoUpRequests.Count() > 0)
                    {
                        updateDirection = Direction.up;
                    }
                }

            }

            return updateDirection;
        }


        // Do: Remove calls that was attendant
        public void RemoveUpCall(int currentFloor)
        {
            for (int j = 0; j < _toGoUpRequests.Count; j++)
            {
                if (_toGoUpRequests[j].NumberFloor == currentFloor)
                {
                    _toGoUpRequests.RemoveAt(j);
                    j--;
                }
            }
        }
        public void RemoveDownCall(int currentFloor)
        {
            _ltDownCall.RemoveAll(r => r.NumberFloor == currentFloor);
            /*
            for (int j = 0; j < _ltDownCall.Count; j++)
            {
                if (_ltDownCall[j].NumberFloor == currentFloor)
                {
                    _ltDownCall.RemoveAt(j);
                    j--;
                }
            }*/
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
