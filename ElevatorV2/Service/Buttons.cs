using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Services.Enums;
using System.Windows;
using System.Windows.Media;

namespace ElevatorV2.Service
{    
    public class Buttons : IButton
    {
        List<CabinButton> ltCabinButton= new List<CabinButton>();
        List<FloorButton> ltFloorButton=new List<FloorButton>();

        public void AddFloorButtons(string name,Button button)
        {
            FloorButton _floorButton = new FloorButton(name, button);
            ltFloorButton.Add(_floorButton);
        }
        public void AddCabinButtons(string name, Button button)
        {
            CabinButton _CabinButton = new CabinButton(name, button);
            ltCabinButton.Add(_CabinButton);
        }
        public void Change(Button button)
        {
            var bc = new BrushConverter();
           button.Background = (Brush)bc.ConvertFrom("#FFDDDDDD");
        }

        public void Hide(Button button)
        {
            button.Visibility = Visibility.Hidden;
        }
        public void Show(Button button)
        {
            button.Visibility = Visibility.Visible;
        }

        public List<FloorButton> GetFloorButons()
        {
            return ltFloorButton;
        }

        public List<CabinButton> GetCabinButons()
        {
            return ltCabinButton;
        }
    }
    public class CabinButton
    {
        Button _button;
        string _name;
        public string Name
        {
            get { return _name; }
            set { value = _name; }
        }
        public Button Butons
        {
            get { return _button; }
            set { value = _button; }
        }
        public CabinButton(string name, Button button)
        {
            _button = button;
            _name = name;
        }
    }
    public class FloorButton
    {
        Button _button;
        string _name;
        public string Name
        {
            get { return _name; }
            set { value = _name; }
        }
        public Button Butons
        {
            get { return _button; }
            set { value = _button; }
        }
        public FloorButton(string name, Button button)
        {
            _button = button;
            _name = name;
        }
    }
}
