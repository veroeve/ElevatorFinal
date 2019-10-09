using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElevatorV2.Service
{
    public interface IButton
    {
        void Hide(Button button);
        void Show(Button button);
        void Change(Button button);
        void AddFloorButtons(string name, Button button);
        void AddCabinButtons(string name, Button button);
        List<FloorButton> GetFloorButons();
        List<CabinButton> GetCabinButons();

    }
}
