/*
APUWebBot ( https://github.com/hoonsubin/APUWebBot )
 Copyright (c) 2019 Hoon Kim
 
 Permission is hereby granted, free of charge, to any person obtaining a copy
 of this software and associated documentation files (the "Software"), to deal
 in the Software without restriction, including without limitation the rights
 to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 copies of the Software, and to permit persons to whom the Software is
 furnished to do so, subject to the following conditions:
 
 The above copyright notice and this permission notice shall be included in
 all copies or substantial portions of the Software.
 
 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 THE SOFTWARE.
*/
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace APUWebBot.Models
{

    public class Item: IEquatable<Item>
    {
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
        public bool Equals(Item other)
        {
            if (other is null)
                return false;
            return EventName == other.EventName && StartDateTime == other.StartDateTime;
        }

        public override bool Equals(object obj) => Equals(obj as Item);
        public override int GetHashCode() => (EventName, StartDateTime).GetHashCode();
    }
}
