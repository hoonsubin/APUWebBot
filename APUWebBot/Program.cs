using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace APUWebBot
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadTimeTableDemo();
        }

        /// <summary>
        /// This demo will get the values from the academic calendar online, and convert that into a list
        /// </summary>
        static void AcademicCalendarDemo()
        {
            //initiate the CSV file which works like a database
            var csv = new StringBuilder();

            const string delimiter = "|";

            foreach (var item in ApuBot.AcademicEventList())
            {
                //combine all the properties in the object to a single line
                string row = item.StartDateTime + delimiter + item.DayOfWeek + delimiter + item.EventName;

                Console.WriteLine(row);

                csv.AppendLine(row);
            }
            //file path for the output
            string filePath = @"output-calendar.csv";

            //output the csv file
            File.WriteAllText(filePath, csv.ToString());
        }

        static void ReadTimeTableDemo()
        {
            const string delimiter = "|";
            //this only holds all the cell texts in the xlsx file
            //var lecutreList = new List<string>();
            var csv = new StringBuilder();
            //string filePath = @"output-timetable.csv";

            try
            {

                //loop through the links that has the xlsx file in the course timetable website
                foreach (var i in ApuBot.LecturesList())
                {
                    //csv.AppendLine(i);
                    Console.WriteLine(i.Term + delimiter + i.DayOfWeek + delimiter + i.SubjectNameEN + delimiter + i.SubjectId + delimiter + i.Semester + delimiter + i.Curriculum);
                }

                //ApuBot.DebugLectureItem();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


        }

    }
}
