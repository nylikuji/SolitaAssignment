My pre-assignment for Solita 2022 Dev Academy Fall

This project was made in Visual Studio 2022. 

To run this program, create an SQLite database in the directory named "database.db".

Then execute the following SQL commands to create the SQL tables required.

---------------------------------------------------
#### Table for Bike Journeys
CREATE TABLE "bikejourneys" (
	"departuretime"	TEXT,
	"returntime"	TEXT,
	"departurestationid"	INTEGER,
	"departurestationname"	TEXT,
	"returnstationid"	INTEGER,
	"returnstationname"	TEXT,
	"covereddistance"	INTEGER,
	"duration"	INTEGER
)

---------------------------------------------------
#### Table for Bike Stations
CREATE TABLE "bikestations" (
	"fid"	INTEGER,
	"id"	INTEGER,
	"nimi"	TEXT,
	"namn"	TEXT,
	"name"	TEXT,
	"osoite"	TEXT,
	"adress"	TEXT,
	"kaupunki"	TEXT,
	"stad"	TEXT,
	"operator"	TEXT,
	"capacity"	INTEGER,
	"x"	TEXT,
	"y"	TEXT
)

---------------------------------------------------
I also suggest you create indices for every column in bikejourneys.
A function called AddIndexes() in DataBaseObject can do this for you.
If you uncomment line 125 of Program.cs, it will add indices after prompting the user about the files.

#### The program

When you run the program, it will prompt you on the command line to read the .csv files in the program's directory.
It will then process the files' contents and add them to the SQLite database tables accordingly.

You can find the required .csv files from:
https://github.com/solita/dev-academy-2022-fall-exercise

After prompting the user about the files, it will host a http site in which you can view the database contents.

#### The Code

Program.cs Includes code for reading the CSV files, processing them and calling required database related functions.

Controllers folder includes Controllers which handle http GET API calls.
They also make sure the GET requests are valid.

The wwwroot folder includes files for the frontend http site.

DatabaseObject.cs is responsible for all SQL queries and nonqueries.
