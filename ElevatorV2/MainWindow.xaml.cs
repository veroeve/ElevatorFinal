using ElevatorV2.Service;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Services.Enums;
using System.Configuration;

namespace ElevatorV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IElevator _elevator;
        ITimerSensor _timeSensor= new TimerSensor();
        Dictionary<Direction, Button> _dictionaryFloorButton = new Dictionary<Direction, Button>();
        Dictionary<string, Button> _dictionaryCabinButton = new Dictionary<string, Button>();
        public MainWindow()
        {
            InitializeComponent();
            CreateCabinButton();
            CreateFloorButton();
            txtElevator.AppendText("Start \r\n");

            _elevator = new Elevator(txtElevator, lblDisplayFloor, lblDisplayCabin, _dictionaryFloorButton, _dictionaryCabinButton);
            CreateFloor();

            btnDown.Visibility = Visibility.Hidden;
            lblDisplayCabin.Content = "0";
            lblDisplayFloor.Content = "0";

            _timeSensor.Star(_elevator);
            cmbFloor.SelectionChanged += selection;
           
        }

        private void selection(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFloor.SelectedValue == null)
            {
                MessageBox.Show($"Select a floor");
                return;
            }
            else
            {
                int floor =(int) cmbFloor.SelectedValue;
                _elevator.ChangeButtonFor(floor);
                
            }
        }

        public void CreateFloor()
        {
            var floorQuantity = int.Parse(ConfigurationSettings.AppSettings["floorQuantity"]);
            for (int i = 0; i < floorQuantity; i++)
            {
                string key = "Floor" + i;
                var type = TypeFloor.floorboth;

                // Do: Fill combobox
                cmbFloor.Items.Add(i);
                // Do: Create virtual floors                
                if (i==0)
                {
                    type = TypeFloor.floorup;
                }
                if(i==floorQuantity-1)
                {
                    type = TypeFloor.floordown;
                }
                if (ConfigurationSettings.AppSettings[key] != null)
                {
                    _elevator.CreateFloor(i, Int32.Parse(ConfigurationSettings.AppSettings[key]), type);
                }
            }
        }
        public void CreateFloorButton()
        {
            _dictionaryFloorButton.Add(Direction.up, btnUp);
            _dictionaryFloorButton.Add(Direction.down, btnDown);
        }
        public void CreateCabinButton()
        {
            var floorQuantity = int.Parse(ConfigurationSettings.AppSettings["floorQuantity"]);
            for (int i = 0; i < floorQuantity; i++)
            {
                var btn = new Button();
                btn.Content = "Floor " + i;
                btn.Tag = i;
                btn.Click += btn0_Click;
                btn.Height = 39;
                if (i % 2 == 0)
                {
                    pnlOdd.Children.Add(btn);
                }
                else
                {
                    pnlEven.Children.Add(btn);
                }
                _dictionaryCabinButton.Add(i.ToString(), btn);
            }
           
        }


        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            var buttonClicked = sender as Button;

            if (cmbFloor.SelectedValue == null)
            {
                MessageBox.Show($"Select a floor");
                return;
            }

            var typeRequest = (Direction)Enum.Parse(typeof(Direction), buttonClicked.Tag.ToString());
            var numberFloor = (int)cmbFloor.SelectedValue;
            _elevator.RegisterRequest(numberFloor, typeRequest);
        }
        private void btn0_Click(object sender, RoutedEventArgs e)
        {
            var buttonClicked = sender as Button;
            buttonClicked.Background = Brushes.Red;
            txtElevator.AppendText($"Floor request {buttonClicked.Tag.ToString()} \r\n");
            _elevator.RegisterRequest(int.Parse(buttonClicked.Tag.ToString()), Direction.none);
        }

    }
}
