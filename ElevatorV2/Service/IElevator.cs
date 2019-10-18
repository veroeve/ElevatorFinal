using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElevatorV2.Service
{
    public interface IElevator
    {
        void RegisterCall(int numberFloor, string typeCall);
        void ExecuteCalls();
        void CreateFloor(int number, int height, string typeFloor);
        void ChangeButtonFor(int floor);

    }
}
