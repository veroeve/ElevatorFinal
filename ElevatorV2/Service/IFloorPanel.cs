﻿using Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorV2.Service
{
    public interface IFloorPanel:IPanelButton
    {
        void EnableFloorButton(TypeFloor nameButton);
    }
}
