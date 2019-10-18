using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ElevatorV2.Service
{
    public class FloorButton : IFloorButton
    {
        Button _button;
        string _name;
        public string Name
        {
            get { return _name; }
            set { value = _name; }
        }
        public Button _Button
        {
            get { return _button; }
            set { value = _button; }
        }
        public FloorButton(string name, Button button)
        {
            _button = button;
            _name = name;
        }
        public FloorButton()
        {
        }

        public void HideButton(Button button)
        {
            button.Visibility = Visibility.Hidden;
        }

        public void ShowButton(Button button)
        {
            button.Visibility = Visibility.Visible;
        }

    }
}
