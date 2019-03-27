using System;
using System.Collections.Generic;

namespace APUWebBot.Models
{
    public class Lecture : IEquatable<Lecture>, ILecture
    {
        public Lecture()
        {
            TimetableCells = new List<TimetableCell>();
        }

        public void AddCell(TimetableCell cell)
        {
            TimetableCells.Add(cell);
        }

        int Id { get; set; }

        public string Term { get; set; }

        public string Classroom { get; set; }

        public string BuildingFloor { get; set; }

        public string SubjectId { get; set; }

        public string SubjectNameJP { get; set; }

        public string SubjectNameEN { get; set; }

        public string InstructorJP { get; set; }

        public string InstructorEN { get; set; }

        public string GradeEval { get; set; }

        public string Language { get; set; }

        public string Grade { get; set; }

        public string Field { get; set; }

        public string APS { get; set; }

        public string APM { get; set; }

        public string Semester { get; set; }

        public string Curriculum { get; set; }

        public List<TimetableCell> TimetableCells;

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
