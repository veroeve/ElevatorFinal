using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    interface IController
    {
        void RegisterRequest(int numberFloor, string typeRequest);
    }
}
