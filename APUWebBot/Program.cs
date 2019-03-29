using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using APUWebBot.Models;
using System.Threading.Tasks;
using SQLite;

namespace APUWebBot
{
    class Program
    {

       public static void Main(string[] args)
       {

            try
            {
                //GetLecturesDemo();
                DatabaseDemoAsync().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: \n" + ex);
            }

        }

        #region Demo Methods

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
        static void GetLecturesDemo()
        {
            //initiate the CSV file which works like a database
            var csv = new StringBuilder();

            var lectureList = ApuBot.LecturesList();

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

        static void SreachLectureDemo()
        {
            Console.WriteLine("Starting up the database...");
            //the list that the engine should look to
            var lectureList = ApuBot.LecturesList();

            while (true)
            {
                Console.WriteLine("There are " + lectureList.Count + " lectures in the database");
                Console.WriteLine("Type a search term (type exit to terminate): ");
                string query = Console.ReadLine().ToLower();

                if (query.Contains("exit"))
                {
                    Console.WriteLine("Exiting...");
                    break;
                }

                Console.WriteLine("Searching...");

                //search the given database with the given query
                var searchResults = SearchEngine.SearchLecture(query, lectureList);

                //show the search results
                if (SearchEngine.ResultCount > 0)
                {
                    foreach (var res in searchResults)
                    {
                        string outputRow = res.Term + ApuBot.delimiter
                            + res.SubjectNameEN + ApuBot.delimiter
                            + res.Classroom + ApuBot.delimiter
                            + res.InstructorEN + ApuBot.delimiter
                            + res.Grade + ApuBot.delimiter;

                        foreach (var n in res.TimetableCells)
                        {
                            outputRow += n.DayOfWeek + "-" + n.Period
                                + $"[{n.Column} - {n.Row}]" + ApuBot.delimiter;
                        }

                        Console.WriteLine(outputRow);
                    }
                    Console.WriteLine("Took " + SearchEngine.LastSearchTime + " ms");
                    Console.WriteLine("Found " + SearchEngine.ResultCount + " items");
                    Console.WriteLine("============================");
                }
                else
                {
                    Console.WriteLine("No results found");
                    Console.WriteLine("============================");
                }

            }
        }

        #region Database implementation
        static DataStore database;

        static DataStore Database
        {
            get
            {
                if (database == null)
                {
                    database = new DataStore();
                }

                return database;
            }
        }
        #endregion

        static async Task DatabaseDemoAsync()
        {

            while (true)
            {
                Console.WriteLine("============================");
                Console.WriteLine("Please choose your option (type the number)");
                Console.WriteLine("1.Update or show the contents of the database");
                Console.WriteLine("2.Empty the database");
                Console.WriteLine("3.Exit");

                string option = Console.ReadLine();

                if (option == "1")
                {
                    await GetDatabaseContentAsync();
                }
                else if (option == "2")
                {
                    await CleanDatabaseAsync();
                }
                else if (option == "3")
                {
                    Console.WriteLine("Quitting...");
                    break;
                }
                else
                {
                    Console.WriteLine("Input a valid option");
                }
            }



        }

        static async Task CleanDatabaseAsync()
        {
            var listFromDb = await Database.GetLecturesAsync();

            if (listFromDb.Count <= 0)
            {
                Console.WriteLine("The database is already empty");
            }
            else
            {
                Console.WriteLine("Emptying the database");
                foreach (var i in listFromDb)
                {
                    await Database.DeleteLectureAsync(i);
                }

                if (listFromDb.Count <= 0)
                {
                    Console.WriteLine("Database is now empty");
                }
            }

        }

        static async Task GetDatabaseContentAsync()
        {
            var listFromDb = await Database.GetLecturesAsync();

            if (listFromDb.Count <= 0)
            {
                Console.WriteLine("The database is empty, will get new lectures");
                var newLectures = ApuBot.LecturesList();

                await Database.SaveAllLecturesAsync(newLectures);
                Console.WriteLine("Finished saving all the lectures!");
            }
            else
            {
                Console.WriteLine($"Found {listFromDb.Count} items from an existing database, listing all of them...");
                var currentLectures = await Database.GetLecturesAsync();
                foreach (var i in currentLectures)
                {
                    string row = i.Term + ApuBot.delimiter
                        + i.SubjectNameEN + ApuBot.delimiter
                    + i.Semester + ApuBot.delimiter
                        + i.Curriculum + ApuBot.delimiter
                    + i.BuildingFloor + ApuBot.delimiter
                        + i.Classroom + ApuBot.delimiter
                    + i.InstructorEN + ApuBot.delimiter
                        + i.Grade + ApuBot.delimiter;

                    if (i.TimetableCells != null)
                    {
                        foreach (var n in i.TimetableCells)
                        {
                            row += n.DayOfWeek + "-" + n.Period
                                + $"[{n.Column} - {n.Row}]" + ApuBot.delimiter;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No timetable cell");
                    }


                    Console.WriteLine(row);
                }
            }
        }
        #endregion
    }
}
