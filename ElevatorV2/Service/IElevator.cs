using Services.Enums;
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
        void RegisterRequest(int numberFloor, Direction typeRequest);
        void ExecuteCalls();
        void CreateFloor(int number, int height, TypeFloor typeFloor);
        void ChangeButtonFor(int floor);

    }
}
