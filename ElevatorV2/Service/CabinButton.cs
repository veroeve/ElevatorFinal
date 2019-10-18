using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace ElevatorV2.Service
{
    class CabinButton : ICabinButton
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
        public CabinButton()
        {
        }
        public void ChangeColor(Button button)
        {
            var bc = new BrushConverter();
            button.Background = (Brush)bc.ConvertFrom("#FFDDDDDD");
        }
    }
}
