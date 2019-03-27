using System;
namespace APUWebBot.Models
{
    public class TimetableCell
    {
        public TimetableCell()
        {
        }

        public int Row { get; set; }

        public int Column { get; set; }

        public string DayOfWeek { get; set; }

        public string Period { get; set; }

        public string ClassStartTime { get; set; }

        public string ClassEndTime
        {
            get
            {
                //return the StartTime value, but added 1 hour and 35 minutes to it
                return ClassStartTime.Contains("T.B.A.") ? "T.B.A." : DateTime.ParseExact(ClassStartTime, "HH:mm", null).AddHours(1).AddMinutes(35).ToString("HH:mm");
            }
        }
    }
}
