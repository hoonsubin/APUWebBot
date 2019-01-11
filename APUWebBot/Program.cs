using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using APUWebBot.Models;

namespace APUWebBot
{
    class Program
    {

        static void Main(string[] args)
        {
            //ReadTimeTableDemo();
            while (true)
            {
                Console.WriteLine("Type a search term (type exit to terminate): ");
                string query = Console.ReadLine();
                if (query.ToLower().Contains("exit"))
                {
                    break;
                }

                SreachLectureDemo(query);
            }

        }

        /// <summary>
        /// This demo will get the values from the academic calendar online, and convert that into a list
        /// </summary>
        static void AcademicCalendarDemo()
        {
            //initiate the CSV file which works like a database
            var csv = new StringBuilder();

            foreach (var item in ApuBot.AcademicEventList())
            {
                //combine all the properties in the object to a single line
                string row = item.StartDateTime + ApuBot.delimiter + item.DayOfWeek + ApuBot.delimiter + item.EventName;

                Console.WriteLine(row);

                csv.AppendLine(row);
            }
            //file path for the output
            string filePath = @"output-calendar.csv";

            //output the csv file
            File.WriteAllText(filePath, csv.ToString());
        }

        /// <summary>
        /// This demo will show how the bot gets the lectures, puts them into a list, and prints them
        /// </summary>
        static void ReadTimeTableDemo()
        {

            try
            {
                //loop through the links that has the xlsx file in the course timetable website
                foreach (var i in ApuBot.LecturesList())
                {
                    //csv.AppendLine(i);
                    Console.WriteLine(i.Term + ApuBot.delimiter + i.DayOfWeek + ApuBot.delimiter + i.SubjectNameEN + ApuBot.delimiter + i.SubjectId + ApuBot.delimiter + i.Semester + ApuBot.delimiter + i.Curriculum);
                }

                Console.WriteLine("There are " + ApuBot.LecturesList().Count + " items in the list");

                //ApuBot.DebugLectureItem();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static void SreachLectureDemo(string query)
        {
            var database = ApuBot.LecturesList();

            var searchResults = new List<LectureItem>();

            Console.WriteLine("Searching...");

            foreach(var i in database)
            {
                foreach(var tag in i.SearchTags)
                {
                    if (tag.Contains(query.ToLower()))
                    {
                        searchResults.Add(i);
                    }
                }
            }

            if (searchResults.Count > 0)
            {
                foreach(var res in searchResults)
                {
                    Console.WriteLine(res.SubjectNameEN);
                }
                Console.WriteLine("Found " + searchResults.Count + " items");
                Console.WriteLine("============================");
            }
            else
            {
                Console.WriteLine("No results found");
            }

        }


    }
}
