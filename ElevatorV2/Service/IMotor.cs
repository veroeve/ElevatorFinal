using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorV2.Service
{
    interface IMotor
    {
        void MotorUp();
        void MotorDown();
        void Stop();
    }
}
