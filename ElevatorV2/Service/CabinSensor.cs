using Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorV2.Service
{
    class CabinSensor : ICabinSensor
    {      
        public bool IsOnTheNextFloor(List<Floor> ltFloor, int currentFloor,int currentHeight, Direction elevatorDirection)
        {
            bool next = false;
            foreach (var item in ltFloor)
            {
                if (item.Number == currentFloor)
                {
                    if (elevatorDirection == Direction.up)
                    {
                        if (currentHeight >= item.Height)
                        {
                            next = true;
                        }
                    }
                    if (elevatorDirection == Direction.down)
                    {
                        if (currentHeight == 0)
                        {
                            next = true;
                        }
                    }

                }
            }
            return next;
        }
    }
}
