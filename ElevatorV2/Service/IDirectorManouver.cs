using Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElevatorV2.Service
{
    public interface IDirectorManouver
    {
        void RegisterCall(int numberFloor, Direction typeRequest);
        void ExecuteCalls(List<Floor> lisFloor);
    }
}
