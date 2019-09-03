using System;
using System.Collections.Generic;
using System.Text;
using WorkHours.Models;

namespace WorkHours.Views
{
   public class PeriodeView
    {

        public LønPeriode Periode { get; set; }
        public string Color { get; set; }
        public string PeriodeAsString { get; set; }


        public PeriodeView(LønPeriode periode, string color)
        {
            this.Color = color;
            this.Periode = periode;
            this.PeriodeAsString = periode.Periode;
        }


    }
}
