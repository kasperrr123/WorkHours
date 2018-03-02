using System;
using System.Collections.Generic;
using System.Text;

namespace WorkHours.CreateNewWorkPlace
{
    public sealed class CreateNewWorkPlaceObj
    {
        private static CreateNewWorkPlaceObj instance;


        public String BasisTimeLøn { get; set; }
        public int LønPeriode_FraDato { get; set; }
        public int LønPeriode_TilDato { get; set; }
        public AftenTillægObj AftenTillæg { get; set; }
        public LørdagsTillægObj LørdagsTillæg { get; set; }
        public SøndagsTillægObj SøndagsTillæg { get; set; }
        public Object[] AndreTillægs { get; set; }


        private CreateNewWorkPlaceObj()
        {

        }

        public static CreateNewWorkPlaceObj Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CreateNewWorkPlaceObj();
                }
                return instance;
            }
        }

        public string CompanyName { get; internal set; }

        public static CreateNewWorkPlaceObj NewMonthObj()
        {
            instance = new CreateNewWorkPlaceObj();
            return instance;
        }

        public struct AftenTillægObj
        {
            public TimeSpan Time { get; set; }
            public String Løn { get; set; }
            public String Day { get; set; }

            public AftenTillægObj(TimeSpan Time, String Løn)
            {
                this.Day = "Aften";
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
            public String Day { get; set; }

            public LørdagsTillægObj(TimeSpan Time, String Løn)
            {
                this.Day = "Lørdag";

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
            public String Day { get; set; }

            public SøndagsTillægObj(TimeSpan Time, String Løn, bool HeleDagen)
            {
                this.Day = "Søndag";
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
