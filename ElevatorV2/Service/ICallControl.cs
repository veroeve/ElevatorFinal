using Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorV2.Service
{
    interface ICallControl
    {
        void RegisterRequest(int nameFloor, Direction typeRequest);
        bool HasAnyRequest();
        bool FloorMakeCall(int currentFloor, Direction elevatorDirection, ILevelSensor level);
        Direction UpdateRequestAndDirection(Direction elevatorDirection, int currentFloor);
        
    }
}
