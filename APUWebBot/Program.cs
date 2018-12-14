using System;
using System.Text;
using System.IO;

namespace APUWebBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var apuBot = new ApuBot();

            //initiate the CSV file which works like a database
            var csv = new StringBuilder();

            const string delimiter = "|";

            foreach (var item in apuBot.AcademicEventList())
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

    }
}
