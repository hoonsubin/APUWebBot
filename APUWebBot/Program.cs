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

using System;
using System.Text;
using System.IO;

namespace APUWebBot
{
    class Program
    {
        static void Main(string[] args)
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

    }
}
