namespace SolitaAssignment.Models
{
    public class BikeStation
    {
        public string? fid;           
        public string? id;
        public string? nimi;
        public string? namn;
        public string? name;
        public string? osoite;
        public string? adress;
        public string? kaupunki;
        public string? stad;
        public string? opegator; //can't call it operator
        public string? capacity;
        public string? x;
        public string? y;
        public BikeStation(string[] props)
        {
            for (int i = 0; i < props.Length; i++)
            {
                props[i] = props[i].Replace(",", "");//erasing every "," because they would mess with the SQL command in InsertBikeStations()
                props[i] = props[i].Replace("'", "");
            }

            fid = props[0];
            id = props[1];
            nimi = props[2];
            namn = props[3];
            name = props[4];
            osoite = props[5];
            adress = props[6];
            kaupunki = props[7];
            stad = props[8];
            opegator = props[9];
            capacity = props[10];
            x = props[11];
            y = props[12];
        }
        public BikeStation()
        {

        }
    }
}
