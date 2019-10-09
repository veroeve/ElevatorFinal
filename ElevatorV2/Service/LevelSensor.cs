using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorV2.Service
{
    public delegate void Notify(string state);    
    class LevelSensor:ILevelSensor
    {
        private string _state;       
      
        public event Notify stateChange;
        public void Change(string state)
        {
            _state = state;
            if (stateChange != null)
            {
                stateChange.Invoke(_state);
            }

        }

        
    }
}
