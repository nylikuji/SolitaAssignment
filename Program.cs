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
        public static void CSVintoDB(string path)
        {
            using(var reader = new StreamReader(path))
            {
                reader.ReadLine(); //skip first line of CSV file
                List<BikeJourney> Journeys = new List<BikeJourney>();
                int i = 0;
                int set = 1; //used to write to console how many sets of journeys added to database
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    BikeJourney Journey = new BikeJourney(values);
                    /*Journey.departuretime = values[0];
                    Journey.returntime = values[1];
                    Journey.departurestationid = values[2];
                    Journey.departurestationname = values[3];
                    Journey.returnstationid = values[4];
                    Journey.returnstationname = values[5];
                    Journey.covereddistance = values[6];
                    Journey.duration = values[7];*/
                    if(i < 1000)
                    {
                        Journeys.Add(Journey);
                        i++;
                    }
                    else
                    {
                        DBobject.InsertBikeJourneys(Journeys);
                        Journeys.Clear();
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
                    Console.WriteLine($"Write 'read' to read contents from {filepath} and write them to the database.");
                    Console.WriteLine("Press enter to continue.");
                    string answer = Console.ReadLine();
                    if(answer == "read")
                    {
                        CSVintoDB(filepath);
                    }
                    else if(answer == "erase")
                    {
                        DBobject.ClearDataBase();
                    }
                    //Console.WriteLine(filepath);
                }
            }
        }
        public static void Main(string[] args)
        {
            //DBobject.Initsql();
            //readCSVs();
            DBobject.AddIndexes();

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
