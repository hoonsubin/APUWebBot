using System;
using System.Collections.Generic;

namespace APUWebBot.Models
{
    public class TimetableCell : IEquatable<Lecture>
    {
        public TimetableCell()
        {
        }
        public string SubjectNameEN { get; set; }

        public string Semester { get; set; }

        public string Curriculum { get; set; }

        public int Row;

        public int Column;

        public string DayOfWeek;

        public string Period;

        public string ClassStartTime;

        public string ClassEndTime
        {
            get
            {
                //return the StartTime value, but added 1 hour and 35 minutes to it
                return ClassStartTime.Contains("T.B.A.") ? "T.B.A." : DateTime.ParseExact(ClassStartTime, "HH:mm", null).AddHours(1).AddMinutes(35).ToString("HH:mm");
            }
        }

        public static TimetableCell Parse(string dayOfWeek, string period, string classStartTime, string subjectName, string semester, string curriculum)
        {
            //convert the DayOfWeek value to a number, missing value is 99
            var dayOfWeekToInt = new Dictionary<string, int>
            {
                {"Monday", 1},
                {"Tuesday", 2},
                {"Wednesday", 3},
                {"Thursday", 4},
                {"Friday", 5},
                {"T.B.A.", 99}
            };

            int col = dayOfWeekToInt[dayOfWeek];

            //convert the first char of the Period attribute to int, missing value is 99
            int row = (int)char.GetNumericValue(period[0]);

            //the timetable row max number is 6, anything above 6 will be considered missing
            if (row == -1)
            {
                row = 99;
            }

            var timetableCell = new TimetableCell
            {
                Row = row,
                Column = col,
                DayOfWeek = dayOfWeek,
                Period = period,
                ClassStartTime = classStartTime,
                SubjectNameEN = subjectName,
                Semester = semester,
                Curriculum = curriculum
            };

            return timetableCell;
        }

        //check if the subject name, lecture semester and curriculum is the same
        public bool Equals(Lecture other)
        {
            if (other is null)
                return false;
            return SubjectNameEN == other.SubjectNameEN && Semester == other.Semester && Curriculum == other.Curriculum;
        }

        //override the object comparison logic with the one above
        public override bool Equals(object obj) => Equals(obj as Lecture);

        public override int GetHashCode() => (SubjectNameEN, Semester, Curriculum).GetHashCode();

    }
}
