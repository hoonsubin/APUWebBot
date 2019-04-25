using System;
using APUWebBot.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System.IO;
using Newtonsoft.Json;

namespace APUWebBot
{
    public class DataStore
    {
        static SQLiteAsyncConnection _database;

        public DataStore()
        {
            _database = CreateAsyncConnection();

            //create database table
            Console.WriteLine("[DataStore]Creating a new table in the database");
            _database.CreateTableAsync<Lecture>().Wait();
            _database.CreateTableAsync<TimetableCell>().Wait();

        }
        public static string DatabaseFilePath
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db3");
            }
        }

        public static SQLiteAsyncConnection CreateAsyncConnection()
        {
            return new SQLiteAsyncConnection(DatabaseFilePath);
        }

        //get all the lectures in the database, it will return the result as a list
        public Task<List<AcademicEvent>> GetAcademicEventsAsync()
        {
            return _database.GetAllWithChildrenAsync<AcademicEvent>();
        }

        //get all the lectures in the database, it will return the result as a list
        public Task SaveAcademicEventsAsync(AcademicEvent acaEvent)
        {
            return _database.InsertOrReplaceAsync(acaEvent);
        }

        #region Lecture Database
        //get all the lectures in the database, it will return the result as a list
        public Task<List<Lecture>> GetAllLecturesAsync()
        {
            Console.WriteLine("[DataStore]Getting lectures from database");
            //return _database.Table<Lecture>().ToListAsync();
            return _database.GetAllWithChildrenAsync<Lecture>();
        }

        public Task SaveAllLecturesAsync(List<Lecture> lectures)
        {
            //Console.WriteLine("[DataStore]Saving all the lectures from the list");
            return _database.InsertOrReplaceAllWithChildrenAsync(lectures, true);
        }

        //save the given item to the database
        public Task SaveLectureAsync(Lecture lecture)
        {
            Console.WriteLine("[DataStore]Inserting new item " + lecture.SubjectNameEN + " by " + lecture.InstructorEN);
            return _database.InsertOrReplaceWithChildrenAsync(lecture, recursive: true);
        }

        //save the given timetable to the database
        public Task SaveTimetableCellAsync(TimetableCell cell)
        {
            Console.WriteLine("[DataStore]Inserting new item " + cell.Period + " - " + cell.DayOfWeek);
            return _database.InsertOrReplaceWithChildrenAsync(cell, recursive: true);
        }

        //delete the given item from the database
        public Task DeleteLectureAsync(Lecture lecture)
        {
            if (lecture.TimetableCells.Count > 0)
            {
                Console.WriteLine("[DataStore]Deleting timecell...");
                _database.DeleteAllAsync(lecture.TimetableCells, true);
            }
            Console.WriteLine("[DataStore]Deleting item " + lecture.SubjectNameEN + " ID: " + lecture.Id);
            return _database.DeleteAsync(lecture, true);
        }

        public Task DeleteTimetableCellAsync(TimetableCell cell)
        {

            return _database.DeleteAsync(cell, true);
        }

        #endregion

        /// <summary>
        /// Serializes the input Lecture object to JSON in string.
        /// </summary>
        /// <returns>Serialized json string.</returns>
        /// <param name="lecture">Lecture.</param>
        public string SerializeToJson(object lecture)
        {
            return JsonConvert.SerializeObject(lecture, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }

        /// <summary>
        /// Deserializes from json to TimetableCell List.
        /// </summary>
        /// <returns>TimetableCell List.</returns>
        /// <param name="json">Json.</param>
        public List<TimetableCell> DeserializeTimetableListFromJson(string json)
        {
            return JsonConvert.DeserializeObject<List<TimetableCell>>(json);
        }

        /// <summary>
        /// Deserializes from json to Lecture object.
        /// </summary>
        /// <returns>Lecture object.</returns>
        /// <param name="json">Json.</param>
        public List<Lecture> DeserializeLectureListFromJson(string json)
        {
            return JsonConvert.DeserializeObject<List<Lecture>>(json);
        }
    }
}
