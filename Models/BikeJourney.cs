namespace SolitaAssignment.Models
{
    public class BikeJourney
    {
        public string? departuretime;
        public string? returntime;
        public string? departurestationid;
        public string? departurestationname;
        public string? returnstationid;
        public string? returnstationname;
        public string? covereddistance;
        public string? duration;
        public BikeJourney(string[] props)
        {
            foreach(string prop in props)
            {
                prop.Replace(",", ""); //erasing every "," because they would mess with the SQL command in InsertBikeJourneys()
            }

            departuretime = props[0];
            returntime = props[1];
            departurestationid = props[2];
            departurestationname = props[3];
            returnstationid = props[4];
            returnstationname = props[5];
            covereddistance = props[6];
            duration = props[7];
        }
        public bool Validate()
        {
            bool validated = true;

            if (!int.TryParse(departurestationid, out int a))
                validated = false;

            if (!int.TryParse(returnstationid, out int b))
                validated = false;
            
            if (int.TryParse(covereddistance, out int c))
            {
                if(c < 10)
                    validated = false;
            }
            else
                validated = false;

            if (int.TryParse(duration, out int d))
            {
                if (d < 10)
                    validated = false;
            }
            else
                validated = false;

            return validated;
        }
    }
}
