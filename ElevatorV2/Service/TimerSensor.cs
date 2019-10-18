using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ElevatorV2.Service
{
    class TimerSensor : ITimerSensor
    {
        DispatcherTimer dispathcer = new DispatcherTimer();
        public void Star(IElevator elevator)
        {
            
            dispathcer.Interval = new TimeSpan(0, 0, 3);
            dispathcer.Tick += (s, a) =>
            {
                elevator.ExecuteCalls();
            };
            dispathcer.Start();
        }

        public void Stop()
        {
            dispathcer.Stop();
            Thread.Sleep(3000);
            dispathcer.Start();
        }
        
    }
}
