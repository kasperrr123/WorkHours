using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHours
{
    public sealed class SalarySettings
    {
        private static SalarySettings instance;


        public String BasisTimeLøn { get; set; }
        public AftenTillægObj AftenTillæg { get; set; }
        public LørdagsTillægObj LørdagsTillæg { get; set; }
        public SøndagsTillægObj SøndagsTillæg { get; set; }
        public Object[] AndreTillægs { get; set; }


        private SalarySettings()
        {

        }

        public static SalarySettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SalarySettings();
                }
                return instance;
            }
        }



        public struct AftenTillægObj
        {
            public TimeSpan Time { get; set; }
            public String Løn { get; set; }

            public AftenTillægObj(TimeSpan Time, String Løn)
            {
                this.Time = Time;
                this.Løn = Løn;
            }

            override public string ToString()
            {
                return "Aften tillæg: " + "\n" + "Fra klokken: " + Time + "\n" + "Tillæg i kr: " + Løn;
            }

        }
        public struct LørdagsTillægObj
        {
            public TimeSpan Time { get; set; }
            public String Løn { get; set; }

            public LørdagsTillægObj(TimeSpan Time, String Løn)
            {
                this.Time = Time;
                this.Løn = Løn;
            }

            override public string ToString()
            {
                return "Lørdags tillæg: " + "\n" + "Fra klokken: " + Time + "\n" + "Tillæg i kr: " + Løn;
            }

        }
        public struct SøndagsTillægObj
        {
            private string heleDagenString;

            public TimeSpan Time { get; set; }
            public String Løn { get; set; }
            public bool HeleDagen { get; set; }

            public SøndagsTillægObj(TimeSpan Time, String Løn, bool HeleDagen)
            {
                this.heleDagenString = "";
                this.Time = Time;
                this.Løn = Løn;
                this.HeleDagen = HeleDagen;
            }
            override public string ToString()
            {

                if (HeleDagen == true)
                {
                    heleDagenString = "Ja";
                }
                else
                {
                    heleDagenString = "Nej";
                }
                return "Søndags tillæg: " + "\n" + "Fra klokken: " + Time + "\n" + "Tillæg i kr: " + Løn + "\n" + "Får du tillæg hele dagen: " + heleDagenString;
            }
        }
    }
}
