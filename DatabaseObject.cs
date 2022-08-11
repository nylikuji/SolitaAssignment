using System.Data.SQLite;
using SolitaAssignment.Models;

namespace SolitaAssignment
{
    public class DatabaseObject
    {
        SQLiteConnection? connection;

        public void Initsql()
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
    }
}
