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

        public void ClearDataBase()
        {
            string order = "DELETE FROM bikejourneys;";
            SQLiteCommand command = new SQLiteCommand(order,connection);
            command.ExecuteNonQuery();
        }
        public void InsertBikeJourneys(List<BikeJourney> Journeys)
        {
            string order = "BEGIN TRANSACTION;";
            int skips = 0;
            foreach(BikeJourney Journey in Journeys)
            {
                if(int.TryParse(Journey.duration, out int dur))
                {
                    if(dur < 10)
                    {
                        continue;
                    }
                }
                if(int.TryParse(Journey.covereddistance,out int dist))
                {
                    if(dist < 10)
                    {
                        continue;
                    }
                }
                order += $"INSERT INTO bikejourneys VALUES ('{Journey.departuretime}','{Journey.returntime}','{Journey.departurestationid}','{Journey.departurestationname}','{Journey.returnstationid}','{Journey.returnstationname}','{Journey.covereddistance}','{Journey.duration}');";
            }
            order += "COMMIT;";
            SQLiteCommand command = new SQLiteCommand(order, connection);
            command.ExecuteNonQuery();
        }
        public List<BikeJourney> GetJourneys(int page, string sortby)
        {
            string list = "";
            string order = $"SELECT * FROM bikejourneys ORDER BY {sortby} ASC LIMIT 10 OFFSET " + page * 10;
            SQLiteCommand command = new SQLiteCommand(order, connection);
            SQLiteDataReader reader = command.ExecuteReader();

            List<BikeJourney> Journeys = new List<BikeJourney>();

            while (reader.Read())
            {
                string[] props = new string[8];
                props[0] = reader.GetString(0);
                props[1] = reader.GetString(1);
                props[2] = reader.GetString(2);
                props[3] = reader.GetString(3);
                props[4] = reader.GetString(4);
                props[5] = reader.GetString(5);
                props[6] = reader.GetString(6);
                props[7] = reader.GetString(7);
                BikeJourney Journey = new BikeJourney(props);
                Journeys.Add(Journey);
            }

            return Journeys;
            //return command.ExecuteNonQuery().ToString();
        }
    }
}
