﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Global_Classes
{
    public class clsFormat
    {
        public static string DateToShort(DateTime date)
        {
            return date.ToString("dd/MMM/yyyy");
        }
    }
}
