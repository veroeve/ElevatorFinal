using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorV2.Service
{
    public class Floor : IFloor
    {
        int _number;
        int _height;
        string _typeFloor;
        public int Number
        {
            get { return _number; }
            set { value = _number; }
        }
        public int Height
        {
            get { return _height; }
            set { value = _height; }
        }
        public string TypeFloor
        {
            get { return _typeFloor; }
            set { value = _typeFloor; }
        }

        public Floor(int number, int height,string typeFloor)
        {
            _number = number;
            _height = height;
            _typeFloor = typeFloor;
        }
        public int sendNumber(int height)
        {
            return Number;
        }
        
    }
}
