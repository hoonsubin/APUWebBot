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
            GetLecturesDemo();
        }

        #region Demo Methods

        /// <summary>
        /// This demo will get the values from the academic calendar online, and convert that into a list
        /// </summary>
        static void AcademicCalendarDemo()
        {
            //initiate the CSV file which works like a database
            var csv = new StringBuilder();
            try
            {
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
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        /// <summary>
        /// This demo will show how the bot gets the lectures, puts them into a list, and prints them
        /// </summary>
        static void GetLecturesDemo()
        {
            //initiate the CSV file which works like a database
            var csv = new StringBuilder();

            var lectureList = ApuBot.LecturesList();

            try
            {
                foreach (var i in lectureList)
                {
                    //lectureList.Add(i);

                    string row = i.Term + ApuBot.delimiter
                        + i.SubjectNameEN + ApuBot.delimiter
                    + i.Semester + ApuBot.delimiter
                        + i.Curriculum + ApuBot.delimiter
                    + i.BuildingFloor + ApuBot.delimiter
                        + i.Classroom + ApuBot.delimiter
                    + i.InstructorEN + ApuBot.delimiter
                        + i.Grade + ApuBot.delimiter;

                    foreach (var n in i.TimetableCells)
                    {
                        row += n.DayOfWeek + "-" + n.Period
                            + $"[{n.Column} - {n.Row}]" + ApuBot.delimiter;
                    }

                    Console.WriteLine(row);

                    csv.AppendLine(row);
                }

                //file path for the output
                string filePath = @"output-lectures.csv";

                //output the csv file
                File.WriteAllText(filePath, csv.ToString());

                Console.WriteLine("There are " + lectureList.Count + " items in the list");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static void SreachLectureDemo()
        {
            Console.WriteLine("Starting up the database...");
            try
            {
                //the list that the engine should look to
                var database = ApuBot.LecturesList();

                while (true)
                {
                    Console.WriteLine("There are " + database.Count + " lectures");
                    Console.WriteLine("Type a search term (type exit to terminate): ");
                    string query = Console.ReadLine();
                    if (query.ToLower().Contains("exit"))
                    {
                        Console.WriteLine("Exiting...");
                        break;
                    }
                    if (query != "")
                    {
                        Console.WriteLine("Searching...");
                        var searchTime = System.Diagnostics.Stopwatch.StartNew();

                        //the results after the filtering
                        var searchResults = new List<ILecture>();

                        //the search algorithm starts here, currently it's just a simple linear filtering
                        //loop through the database
                        foreach (var i in database)
                        {
                            //todo: implement search logic
                        }

                        //show the search results
                        if (searchResults.Count > 0)
                        {
                            foreach (var res in searchResults)
                            {
                                //Console.WriteLine(res.SubjectNameEN + " subject ID: " + res.SubjectId);
                                Console.WriteLine(res.Term + ApuBot.delimiter
                                    + res.SubjectNameEN + ApuBot.delimiter
                                    + res.Classroom + ApuBot.delimiter
                                    + res.InstructorEN + ApuBot.delimiter
                                    + res.Grade);
                            }
                            searchTime.Stop();
                            Console.WriteLine("Took " + searchTime.ElapsedMilliseconds.ToString() + " ms");
                            Console.WriteLine("Found " + searchResults.Count + " items");
                            Console.WriteLine("============================");
                        }
                        else
                        {
                            Console.WriteLine("No results found");
                            Console.WriteLine("============================");
                        }
                    }

                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion
    }
}
