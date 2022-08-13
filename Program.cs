using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Linq;
using SolitaAssignment.Models;
using SolitaAssignment.Controllers;

namespace SolitaAssignment
{
    public class SolitaAssignment
    {
        public static DatabaseObject DBobject = new DatabaseObject();
        public static string EraseQuotes(string line)
        {
            string newline = "";
            bool withinQuote = false;
            foreach(char c in line)
            {
                if (c == '"' && !withinQuote)
                    withinQuote = true;
                else if (c == '"' && withinQuote)
                    withinQuote = false;

                if (withinQuote && c != ',' && c != '"')
                    newline += c;

                if(!withinQuote && c != '"')
                    newline += c;

            }
            //some entries in the CSV files are quoted because they contain commas.
            //trying to add such entries into the database turned out messy, so I wrote this loop which erases quotes and commas that are within quotes.

            return newline;
        }
        public static void CSVintoDB(string path, bool isJourneyFile)
        {
            int lineCount = File.ReadLines(path).Count();

            using(var reader = new StreamReader(path))
            {
                reader.ReadLine(); //skip first line of CSV file

                List<BikeJourney> Journeys = new List<BikeJourney>();
                List<BikeStation> Stations = new List<BikeStation>();

                int i = 0;
                int linePosition = 2;
                int set = 1; //used to write to console how many sets of journeys added to database

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (line.Contains("\""))
                    {
                        //Console.WriteLine();
                        //Console.WriteLine(line);
                        line = EraseQuotes(line);
                        //Console.WriteLine(line);
                    }

                    var values = line.Split(',');

                    BikeJourney Journey = new BikeJourney();
                    BikeStation Station = new BikeStation();

                    if (isJourneyFile)
                        Journey = new BikeJourney(values);
                    else
                        Station = new BikeStation(values);
                    
                    if(i < 1000 && linePosition < lineCount)
                    {
                        if(isJourneyFile)
                            Journeys.Add(Journey);
                        else 
                            Stations.Add(Station);

                        i++;
                        linePosition++;
                    }
                    else
                    {
                        if (isJourneyFile)
                            DBobject.InsertBikeJourneys(Journeys);
                        else 
                            DBobject.InsertBikeStations(Stations);

                        Journeys.Clear();
                        Stations.Clear();
                        i = 0;
                        Console.WriteLine("Set " + set + " added to database from " + path);
                        set++;
                    }
                }
            }
        }
        public static void readCSVs()
        {
            string[] filepaths = Directory.GetFiles("./");
            foreach(string filepath in filepaths)
            {
                if (filepath.EndsWith(".csv"))
                {
                    Console.WriteLine("Write 'erase' to clear database contents.");
                    Console.WriteLine($"Write 'journey' to read contents from {filepath} and write them to the bikejourneys table.");
                    Console.WriteLine($"Write 'station' to read contents from {filepath} and write them to the bikestations table.");
                    Console.WriteLine("Press enter to continue.");

                    string answer = Console.ReadLine();
                    if(answer == "journey")
                    {
                        CSVintoDB(filepath, true);
                    }
                    if(answer == "station")
                    {
                        CSVintoDB(filepath, false);
                    }
                    else if(answer == "erase")
                    {
                        DBobject.ClearDataBase();
                    }
                }
            }
            //DBobject.AddIndexes();
        }
        public static void Main(string[] args)
        {
            readCSVs();

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddMvc();
            var app = builder.Build();


            //app.MapGet("/", () => "Hello World!");
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.MapControllers();

            app.Run();
        }
    }
}
