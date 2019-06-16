
# APUWebBot

## Introduction

This is a simple code that will scrape the content from the [Ristumeikan Asia Pacific University Academic Office homepage](http://en.apu.ac.jp/academic/) that I made as a hobby. I hope this project will be the strating point for other APU improvement projects.
The full application that uses this function can be downloaded from [Google Playstore](https://play.google.com/store/apps/details?id=com.TeamSTEP.APU_Calendar) and the [App Store](https://itunes.apple.com/us/app/apu-calendar/id1448344035?mt=8) and the repository of that project can be hound from [here](https://github.com/hoonsubin/APUCalendar)

Scraped contents currently includes:
 - Academic Events from the Academic Calendar
 - Lectures from the timetable xlsx sheet

## How it works

The APU WebBot project works by using the HTML Agility Pack to get the raw html document from the given homepage, convert it as an object, save it to the database (SQLite).
This project currently looks for two points from the Academic Office homepage; [Timetable Page](http://en.apu.ac.jp/academic/page/content0186.html/), and the [Academic Calendar Page](http://en.apu.ac.jp/academic/page/content0309.html/).
The Academic Calendar scraping is straight forward as it will just check for rows and columns of the tbody element, and assign the data to the AcademicEvent class.
The Timetable scraping is done by first memorystreaming the timetable xlsx file, and parse through the content of the xlsx file by using Office Open XML Nuget package, and assigning the values to the Lecture class.

## Project structure

The codes can be divided into two types; Services, and Models.
Services files also contains Helper codes like *DataStore.cs* which accesses SQLite back-end to save/load and deleting data.
*SearchEngine.cs* is a class that contains several simple search tools. This method get's a string search query, and list of Lecture objects (database to look through) as parameters and will return a list of matching Lecture items from the given list. The core search algorithm uses a simple foreach loop and string comparisons.
*APUBot.cs* contains all the core fuctions of the entire APU WebBot project, such as the web scraping algorithm.
The Models folder contains all the object models that are used for this project such as *AcademicEvent.cs*, *Lecture.cs*, and *TimetableCell.cs*.
