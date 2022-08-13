using System.Data.SQLite;
using SolitaAssignment.Models;

namespace SolitaAssignment
{
    public class DatabaseObject
    {
        SQLiteConnection? connection;

        public DatabaseObject()
        {
            connection = new SQLiteConnection("Data Source=database.db;Version=3");
            connection.Open();
        }
        
        public void AddIndexes()
        {
            string[] indexes = { "departuretime", "returntime", "departurestationid", "departurestationname", "returnstationid", "returnstationname", "covereddistance", "duration" };
            foreach(string index in indexes)
            {
                string order = $"CREATE INDEX {index}_idx ON bikejourneys ({index})";
                Console.WriteLine($"creating índex {index}_idx");
                SQLiteCommand cmd = new SQLiteCommand(order,connection);
                cmd.ExecuteNonQuery();
            }
        }

        public void ClearDataBase()
        {
            string order = "DELETE FROM bikejourneys; DELETE FROM bikestations;";
            SQLiteCommand command = new SQLiteCommand(order,connection);
            command.ExecuteNonQuery();
        }
        public void InsertBikeJourneys(List<BikeJourney> Journeys)
        {
            string order = "BEGIN TRANSACTION;";
            foreach(BikeJourney Journey in Journeys)
            {
                if (!Journey.Validate())
                {
                    continue;
                }
                order += $"INSERT INTO bikejourneys VALUES ('{Journey.departuretime}','{Journey.returntime}','{Journey.departurestationid}','{Journey.departurestationname}','{Journey.returnstationid}','{Journey.returnstationname}','{Journey.covereddistance}','{Journey.duration}');";
            }
            order += "COMMIT;";
            SQLiteCommand command = new SQLiteCommand(order, connection);
            command.ExecuteNonQuery();
        }
        public void InsertBikeStations(List<BikeStation> Stations)
        {
            string order = "BEGIN TRANSACTION;";
            foreach(BikeStation Station in Stations)
            {
                order += $"\nINSERT INTO bikestations VALUES ('{Station.fid}','{Station.id}','{Station.nimi}','{Station.namn}','{Station.name}','{Station.osoite}','{Station.adress}','{Station.kaupunki}','{Station.stad}','{Station.opegator}','{Station.capacity}','{Station.x}','{Station.y}');";
            }
            order += "COMMIT;";
            SQLiteCommand command = new SQLiteCommand(order, connection);
            command.ExecuteNonQuery();
        }

        public List<BikeJourney> GetJourneys(int page, string sortby)
        {
            string list = "";
            string order = $"SELECT * FROM bikejourneys ORDER BY {sortby} LIMIT 10 OFFSET " + page * 10;
            SQLiteCommand command = new SQLiteCommand(order, connection);
            SQLiteDataReader reader = command.ExecuteReader();

            List<BikeJourney> Journeys = new List<BikeJourney>();

            while (reader.Read())
            {
                string[] props = new string[8];
                props[0] = reader.GetString(0);
                props[1] = reader.GetString(1);
                props[2] = reader.GetInt64(2).ToString();
                props[3] = reader.GetString(3);
                props[4] = reader.GetInt64(4).ToString();
                props[5] = reader.GetString(5);
                props[6] = (reader.GetFloat(6) / 1000).ToString("0.00");

                int seconds = reader.GetInt32(7);
                int minutes = seconds / 60;
                seconds = seconds % 60;
                props[7] = minutes + ":" + seconds;

                BikeJourney Journey = new BikeJourney(props);
                Journeys.Add(Journey);
            }

            return Journeys;
            //return command.ExecuteNonQuery().ToString();
        }

        public List<BikeStation> GetStations(int page, string sortby)
        {
            string list = "";
            string order = $"SELECT * FROM bikestations ORDER BY {sortby} LIMIT 10 OFFSET " + page * 10;
            SQLiteCommand command = new SQLiteCommand(order, connection);
            SQLiteDataReader reader = command.ExecuteReader();

            List<BikeStation> Stations = new List<BikeStation>();

            while (reader.Read())
            {
                string[] props = new string[13];
                props[0] = reader.GetInt64(0).ToString();
                props[1] = reader.GetInt64(1).ToString();
                props[2] = reader.GetString(2);
                props[3] = reader.GetString(3);
                props[4] = reader.GetString(4);
                props[5] = reader.GetString(5);
                props[6] = reader.GetString(6);
                props[7] = reader.GetString(7);
                props[8] = reader.GetString(8);
                props[9] = reader.GetString(9);
                props[10] = reader.GetInt64(10).ToString();
                props[11] = reader.GetString(11);
                props[12] = reader.GetString(12);
                BikeStation Station = new BikeStation(props);
                Stations.Add(Station);
            }

            return Stations;
        }
    }
}
