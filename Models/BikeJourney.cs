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
            departuretime = props[0];
            returntime = props[1];
            departurestationid = props[2];
            departurestationname = props[3];
            returnstationid = props[4];
            returnstationname = props[5];
            covereddistance = props[6];
            duration = props[7];
        }
    }
}
