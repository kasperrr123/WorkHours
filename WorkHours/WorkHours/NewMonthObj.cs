using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHours
{
    public sealed class NewMonthObj
    {
        private static NewMonthObj instance;

        public String CompanyName { get; set; }
        public String Month { get; set; }

        public String EmployeeName { get; set; }

        private NewMonthObj()
        {

        }


        public static NewMonthObj Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NewMonthObj();
                }
                return instance;
            }
        }
    }
}
