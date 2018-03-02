using System;
using System.Collections.Generic;
using System.Text;
using WorkHours.Models;

namespace WorkHours
{
    public class GlobalVariables
    {
        private static GlobalVariables instance;

        public String ChosenCompany { get; set; }
        public LønPeriode ValgteLønPeriode { get; set; }

        public int LønPeriode_GårFraDag { get; set; }
        public int LønPeriode_GårTilDag { get; set; }

        public GlobalVariables()
        {
           





        }

        public static GlobalVariables Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GlobalVariables();
                }
                return instance;
            }
        }
    }
}
