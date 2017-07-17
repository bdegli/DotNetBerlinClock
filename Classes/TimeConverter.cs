using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        string MaskON = "RRRR";
        string MaskOff = "OOOOOOOOOOO";
        string MaskMinutes = "YYRYYRYYRYY";
        string MinutesON = "YYYY";

        public string convertTime(string aTime)
        {
            StringBuilder sb = new StringBuilder("");
            Boolean valid = true;
            int Hour=-1,Minute=-1,Second=-1;
            int parts;
            int remainder;

            try
            {
                Hour = Convert.ToInt16(aTime.Substring(0, 2));
                Minute = Convert.ToInt16(aTime.Substring(3, 2));
                Second = Convert.ToInt16(aTime.Substring(6, 2));
            }
            catch
            {
                valid = false;
            }
                                 
            // is time Valid
            if ((Hour < 0) || (Hour > 24)) { valid = false; };
            if ((Minute < 0) || (Minute > 59)) { valid = false; };
            if ((Second < 0) || (Second > 59)) { valid = false; };

            if (valid == false) { throw new System.ArgumentException("Time is not valid"); };
            

            // line 1 shows if the second is even or odd
            string line = "";
            
            if ((Second % 2) == 0) { line = "Y"; } else { line = "O"; };
            sb.AppendLine(line);

            // line 2 : 1 segment = 5 hours
            parts = Hour / 5;
            line = MaskON.Substring(0, parts) + MaskOff.Substring(0, 4 - parts);
            sb.AppendLine(line);

            // line 3 is the remainder of line 2
            remainder = Hour % 5;
            line = MaskON.Substring(0, remainder) + MaskOff.Substring(0, 4 - remainder);
            sb.AppendLine(line);

            // line 4 : 1 segment = 5 Minutes
            parts = Minute / 5;
            line = MaskMinutes.Substring(0, parts) + MaskOff.Substring(0, 11 - parts);
            sb.AppendLine(line);

            // line 5 : 1 segment = 1 Minutes remainder of line 4
            remainder = Minute % 5;
            line = MinutesON.Substring(0, remainder) + MaskOff.Substring(0, 4 - remainder);
            sb.Append(line);           
                       
            return sb.ToString();
        }

       
    }
}
