using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Services
{
    class Controller : IController
    {
        string _state;
        List<Request> ltRequest = new List<Request>();
        

        public void RegisterRequest(int numberFloor, string typeRequest)
        {
            if(ltRequest.Count==0)
            {
                _state = typeRequest;
                Request request = new Services.Request(numberFloor, typeRequest);
                ltRequest.Add(request);
            }
        }
    }
}
