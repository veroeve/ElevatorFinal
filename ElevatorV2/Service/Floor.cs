using Services.Enums;
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
        TypeFloor _typeFloor;
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
        public TypeFloor TypeFloor
        {
            get { return _typeFloor; }
            set { value = _typeFloor; }
        }

        public Floor(int number=0, int height=0, TypeFloor typeFloor=TypeFloor.floorboth)
        {
            _number = number;
            _height = height;
            _typeFloor = typeFloor;

        }
        public Floor()
        {

        }
        public int sendNumber(int height)
        {
            return Number;
        }
        
    }
}
