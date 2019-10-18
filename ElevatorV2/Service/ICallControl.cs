using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorV2.Service
{
    interface ICallControl
    {
        void RegisterCall(int nextFloor, string direction);
        bool CallIsFull();
        bool FloorMakeCall(int currentFloor,string direction, ILevelSensor level);
        string UpdateCallAndDirection(string currentDirection, int currentFloor);


    }
}
