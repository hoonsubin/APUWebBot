using System;
using System.Collections.Generic;

namespace APUWebBot.Models
{
    //this lecture item class holds all the lecture information
    public class LectureItem
    {
        public LectureItem()
        {
        }

        public int Id { get; set; }

        //the duration and start of lecture (semester course, Q1, Q2)
        public string Term { get; set; }

        public string DayOfWeek { get; set; }

        public string Period { get; set; }

        public string Classroom { get; set; }

        public string BuildingFloor { get; set; }

        public string SubjectId { get; set; }

        public string SubjectNameJP { get; set; }

        public string SubjectNameEN { get; set; }

        public string InstructorJP { get; set; }

        public string InstructorEN { get; set; }

        public string Language { get; set; }

        public string Grade { get; set; }

        public string Field { get; set; }
        //which field in APS
        public string APS { get; set; }
        //which field in APM
        public string APM { get; set; }

        //add public SyllabusItem Syllabus { get; set; }
        //what semester the lecture is (ex: 2018 Fall)
        public string Semester { get; set; }

        //the tags are used for searching, and will be dynamically generated
        public List<string> Tags { get; set; }

        public string Curriculum { get; set; }
    }
}
