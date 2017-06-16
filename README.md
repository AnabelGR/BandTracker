# Band Tracker

#### An Epicodus Friday independent project in CSharp and SQL database building, 06.16.17

#### **By Anabel Ramirez**

## Description

This web application will allow a salon to book bands with a venue.

|Behavior| Input (User Action/Selection) |Description|
|---|:---:|:---:|
|Add a band. |Add band: Faith No More|An add function. |
|Find a band. |Find band: Faith No More|A find function. |
|Search for a band. |Search band: Faith No More|A search function. |
|Delete a band. |Delete band: Faith No More|A delete function. |
|Update a band's name. |Find band: Faith No More|An update function. |
|Add a venue. |Add venue: Fillmore Theater|An add function. |
|Find a venue. |Find venue: Fillmore Theater|A find function. |
|Search for a venue. |Search venue: Fillmore Theater|A search function. |
|Delete a venue. |Delete venue: Fillmore Theater|A delete function. |
|Update a venue's name. |Find venue: Fillmore Theater, update to Roseland Theater|An update function. |
|View all venues. |venues: Fillmore Theater, Roseland Theater, Revolution Hall|View the full list of venues in the database. |
|View a venue's bands. |venue: Fillmore Theater |View the full list of venues in the database. |
|Link a venue to a band. |venue: Fillmore Theater, band: Faith No More|A one to one database relationship. |
|Link a venue to several bands. |venue: Fillmore Theater, bands: Faith No More, Harvey|A one to many database relationship. |

## Setup/Installation Requirements

Must have current version of .Net and Mono installed. Will require database file to work correctly, see download instructions below.

Copy all files and folders to your desktop or {git clone} the project using this link https://github.com/AnabelGR/BandTracker.git.

To recreate the databases using SQLCMD in powershell on a windows operating system type:
* > CREATE DATABASE band_tracker; > GO > USE band_tracker; > GO > CREATE TABLE bands (id INT IDENTITY(1,1), name VARCHAR(255)); > CREATE TABLE venues (id INT IDENTITY(1,1), name VARCHAR(255)); > CREATE TABLE venues_bands (id INT IDENTITY(1,1), band_id INT, venue_id INT); > GO.

Navigate to the folder in your Windows powershell and run {dnu restore} to compile the file then run {dnx kestrel} to start the web server. In your web browser address bar, navigate to {//localhost:5004} to get to the home page.

## Known Bugs

* Having homemodule.cs routing issues with bands to concerts link.

## Support and contact details

If you have any issues or have questions, ideas, concerns, or contributions please contact the contributor through Github.

## Technologies Used

* C#
* Nancy
* Razor
* xUnit
* JSON
* HTML
* CSS
* Bootstrap

### License
This software is licensed under the MIT license.

Copyright (c) 2017 **Anabel Ramirez**
