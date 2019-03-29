using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Threading.Tasks;
using System.Linq;
using SQLite;

namespace APUWebBot.Models
{
    [Table("AcademicEvents")]
    //this item class shows the calendar items
    public class AcademicEvent : IEquatable<AcademicEvent>
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        //name of the event, which should be descriptive
        public string EventName { get; set; }
        //the day of the week in full (ex: Friday, instead of Fri)
        public string DayOfWeek { get { return DateTime.ParseExact(StartDateTime, "yyyy/MM/dd", null).DayOfWeek.ToString(); } }
        //the start day of the event formatted as yyyy/MM/dd (ex: 1992/02/21)
        public string StartDateTime { get; set; }

        //GroupDate is formatted as yyyy/MM, this is only used for list grouping reasons
        public string GroupDate { get { return StartDateTime.Remove(7); } }

        //by default the event will happen all day long so it'll be same as the starting date
        public string EndDateTime { get { return StartDateTime; } }

        //a bool for checking if the event has passed the current date or not
        public bool Done
        {
            get
            {
                //this part is used to check if the event has past current date
                DateTime date = DateTime.ParseExact(EndDateTime, "yyyy/MM/dd", null);

                //if the event is earlier than today, market it as done
                return DateTime.Compare(date, DateTime.Now) < 0;
            }
        }

        //check if the name of the event and the starting date of the event is the same
        public bool Equals(AcademicEvent other)
        {
            if (other is null)
                return false;
            return EventName == other.EventName && StartDateTime == other.StartDateTime;
        }

        public override bool Equals(object obj) => Equals(obj as AcademicEvent);
        public override int GetHashCode() => (EventName, StartDateTime).GetHashCode();
    }
    //this model is used for grouping the events by their year and month
    public class AcademicEventGroup : ObservableCollection<AcademicEvent>
    {
        //long display name for the group, this will be yyyy/MM
        public string Heading { get; private set; }

        public string JumpList
        {
            get
            {
                return Heading.Remove(0, 5);
            }
        }

        //set the heading of the group, this is used to make new groups
        public AcademicEventGroup(string heading)
        {
            Heading = heading;
        }

    }
}
