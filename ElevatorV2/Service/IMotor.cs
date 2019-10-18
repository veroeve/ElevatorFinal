using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorV2.Service
{
    interface IMotor
    {
        int MotorUp(int currentheight);
        int MotorDown(int currentheight);
        void Stop(string numberFloor);
    }
}
