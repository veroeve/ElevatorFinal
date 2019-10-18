using Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorV2.Service
{
    class Request
    {
        int _numberFloor;
        Direction _typeRequest;
        public int NumberFloor
        {
            get { return _numberFloor; }
            set { value = _numberFloor; }
        }
        public Direction TypeRequest
        {
            get { return _typeRequest; }
            set { value = _typeRequest; }
        }

        public Request(int numberFloor, Direction typeRequest)
        {
            _numberFloor = numberFloor;
            _typeRequest = typeRequest;
        }
       
    }
}
