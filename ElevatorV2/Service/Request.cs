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
        string _typeRequest;
        public int NumberFloor
        {
            get { return _numberFloor; }
            set { value = _numberFloor; }
        }
        public string TypeRequest
        {
            get { return _typeRequest; }
            set { value = _typeRequest; }
        }

        public Request(int numberFloor, string typeRequest)
        {
            _numberFloor = numberFloor;
            _typeRequest = typeRequest;
        }
       
    }
}
