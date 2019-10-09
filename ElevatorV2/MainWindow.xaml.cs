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

namespace ElevatorV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IElevator _elevator;
        public MainWindow()
        {
            InitializeComponent();
            #region Parameter Initialization
            cmbFloor.Items.Add("0");
            cmbFloor.Items.Add("1");
            cmbFloor.Items.Add("2");
            cmbFloor.Items.Add("3");

            txtElevator.AppendText("Start \r\n");
            List<Floor> ltFloor = new List<Floor>();
            var floorA = new Floor(0, 0, TypeFloor.floorup.ToString());
            var floorB = new Floor(1, 3,TypeFloor.floorboth.ToString());
            var floorC = new Floor(2, 6,TypeFloor.floorboth.ToString());
            var floorD = new Floor(3, 9, TypeFloor.floordown.ToString());
            var floorE= new Floor(4, 12, TypeFloor.floorboth.ToString());

            ltFloor.Add(floorA);
            ltFloor.Add(floorB);
            ltFloor.Add(floorC);
            ltFloor.Add(floorD);
            ltFloor.Add(floorE);

            IButton _button = new Buttons();
            _button.AddFloorButtons("up", btnUp);
            _button.AddFloorButtons("down", btnDown);
            _button.AddCabinButtons("0", btn0);
            _button.AddCabinButtons("1", btn1);
            _button.AddCabinButtons("2", btn2);
            _button.AddCabinButtons("3", btn3);
            #endregion

            _elevator = new Elevator(txtElevator, _button, new ControllerManouver(txtElevator, _button, lblDisplayCabin, lblDisplayFloor), ltFloor);

            btnDown.Visibility = Visibility.Hidden;
            lblDisplayCabin.Content = "0";
            lblDisplayFloor.Content = "0";

            DispatcherTimer dispathcer = new DispatcherTimer();
            dispathcer.Interval = new TimeSpan(0, 0, 3);
            dispathcer.Tick += (s, a) =>
            {
                _elevator.Execute();
            };
            dispathcer.Start();

        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int numberFloor = Int32.Parse(cmbFloor.SelectedValue.ToString());
                _elevator.SendRequest(numberFloor, "up");
            }
            catch
            {
                MessageBox.Show($"Select a floor");
            }
        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int numberFloor = Int32.Parse(cmbFloor.SelectedValue.ToString());
                _elevator.SendRequest(numberFloor, "down");
            }
            catch
            {
                MessageBox.Show($"Select a floor");
            }
        }

        private void btn0_Click(object sender, RoutedEventArgs e)
        {
            btn0.Background = Brushes.Red;
            txtElevator.AppendText("Floor request 0 \r\n");
            _elevator.SendRequest(0, "");
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            btn1.Background = Brushes.Red;
            txtElevator.AppendText("Floor request 1 \r\n");
            _elevator.SendRequest(1, "");
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            btn2.Background = Brushes.Red;
            txtElevator.AppendText("Floor request 2 \r\n");
            _elevator.SendRequest(2, "");
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            btn3.Background = Brushes.Red;
            txtElevator.AppendText("Floor request 3 \r\n");
            _elevator.SendRequest(3, "");
        }
    }
}
