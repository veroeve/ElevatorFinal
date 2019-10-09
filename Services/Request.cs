using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    class Request
    {
        int _numberFloor;
        string _typeRequest;
        public Request(int numberFloor, string typeRequest)
        {
            _numberFloor = numberFloor;
            _typeRequest = typeRequest;
        }
    }
}
