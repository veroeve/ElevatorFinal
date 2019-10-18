using Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorV2.Service
{
    interface ICabinSensor
    {
        bool IsOnTheNextFloor(List<Floor> ltFloor, int currentFloor, int currentHeight, Direction elevatorDirection);
    }
}