using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHours
{
     abstract class FormatToDA
    {

        public FormatToDA()
        {

        }

        public static string GetDayOfWeekInDA(string dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case "Sunday":
                    return "Søndag";
                case "Monday":
                    return "Mandag";
                case "Tuesday":
                    return "Tirsdag";
                case "Wednesday":
                    return "Onsdag";
                case "Thursday":
                    return "Torsdag";
                case "Friday":
                    return "Fredag";
                case "Saturday":
                    return "Lørdag";
                default:
                    break;
            }

            return "";
        }
    }
}
