﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElevatorV2.Service
{
    public interface IPanelButton
    {
        void CreateButton(string name, Button button);
    }
}
