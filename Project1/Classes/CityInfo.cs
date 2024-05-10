namespace Project1.Classes
{
    public class CityInfo
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public string CityAscii { get; set; }
        public int Population { get; set; }
        public string Province { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string Capital { get; set; }
        public CityInfo(int CityID, string CityName, string CityAscii, int Population, string Province, double latitude, double longitude, string Capital)
        {
            this.CityID = CityID;
            this.CityName = CityName;
            this.CityAscii = CityAscii;
            this.Population = Population;
            this.Province = Province;
            this.latitude = latitude;
            this.longitude = longitude;
            this.Capital = Capital;
        }
        public string GetProvince()
        {
            return Province;
        }
        public int GetPopulation()
        {
            return Population;
        }
        public string GetLocation()
        {
            return $"latitude: {latitude}, longitude {longitude}";
        }
    }
}
