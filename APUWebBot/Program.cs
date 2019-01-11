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
            string filePath = @"output.csv";

            //output the csv file
            File.WriteAllText(filePath, csv.ToString());
        }

        static void ReadTimeTableDemo()
        {
            //this only holds all the cell texts in the xlsx file
            var lecutreList = new List<string>();
            try
            {
                //loop through the links that has the xlsx file
                foreach (var i in ApuBot.GetTimetableAsMemStream(ApuBot.GetLinksFromMainPage("03")[0]))
                {
                    //loop through all the raw strings in the xlsx cell
                    foreach (var n in ApuBot.ReadRawXlsxFileStream(i))
                    {
                        lecutreList.Add(n);
                    }
                }

                foreach(var i in lecutreList)
                {
                    Console.WriteLine(i);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


        }

    }
}
