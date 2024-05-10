using System.Collections.Generic;
using System.Linq;
using System.Device.Location;
using System.Diagnostics;

namespace Project1.Classes
{
    public class Statistics
    {
        public Dictionary<string, CityInfo> cityCatalogue = new Dictionary<string, CityInfo>();

        public Statistics(string file, string fileType)
        {
            cityCatalogue = DataModeler.ParseFile(file, fileType);
        }
        /*Method Name: DisplayCityInformation
         *Purpose: displays its city information
         *Accepts: string
         *Returns: CityInfo
         */
        public CityInfo DisplayCityInformation(string cityInfo)
        {
            return cityCatalogue[cityInfo];
        }
        /*Method Name: DisplayLargestPopulationCity
         *Purpose: displays a city with the largest population in a province
         *Accepts: string
         *Returns: CityInfo
         */
        public CityInfo DisplayLargestPopulationCity(string province)
        {
            return cityCatalogue.Where(cityInfo => cityInfo.Value.Province == province).OrderByDescending(cityInfo => cityInfo.Value.Population).First().Value;
        }
        /*Method Name: DisplaySmallestPopulationCity
         *Purpose: displays a city with the smallest population in a province
         *Accepts: string
         *Returns: CityInfo
         */
        public CityInfo DisplaySmallestPopulationCity(string province)
        {
            return cityCatalogue.Where(cityInfo => cityInfo.Value.Province == province).OrderBy(cityInfo => cityInfo.Value.Population).First().Value;
        }
        /*Method Name: CompareCitiesPopulation
         *Purpose: compare and displays which city has the highest population than other city
         *Accepts: two strings
         *Returns: (CityInfo, CityInfo, int) 
         */
        public (CityInfo, CityInfo, int) CompareCitiesPopulation(string cityA, string cityB)
        {
            if(cityCatalogue[cityA].Population > cityCatalogue[cityB].Population)
            {
                return (cityCatalogue[cityA], cityCatalogue[cityB], cityCatalogue[cityA].Population);
            }
            else
            {
                return (cityCatalogue[cityB], cityCatalogue[cityA], cityCatalogue[cityB].Population);
            }
        }
        /*Method Name: ShowCityOnMap
         *Purpose: starts a process which open up a web browser displaying a map pinpointed with a latitude and longitude of a city 
         *Accepts: two strings
         *Returns: void
         */
        public void ShowCityOnMap(string city, string province)
        {
            string key = cityCatalogue.First(cityInfo => cityInfo.Value.CityName == city && cityInfo.Value.Province == province).Key;
            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            string uri = $"https://www.latlong.net/c/?lat={cityCatalogue[key].latitude}&long={cityCatalogue[key].longitude}";
            process.StartInfo.FileName = uri;
            process.Start();
        }
        /*Method Name: CalculateDistanceBetweenCities
         *Purpose: calculate a distance in meters between each cities  
         *Accepts: two strings
         *Returns: double
         */
        public double CalculateDistanceBetweenCities(string cityA, string cityB)
        {
            GeoCoordinate cityACoordinate = new GeoCoordinate(cityCatalogue[cityA].latitude, cityCatalogue[cityA].longitude);
            GeoCoordinate cityBCoordinate = new GeoCoordinate(cityCatalogue[cityB].latitude, cityCatalogue[cityB].longitude) ;

            return cityACoordinate.GetDistanceTo(cityBCoordinate);
        }
        /*Method Name: DisplayProvincePopulation
         *Purpose: displays total population of a province  
         *Accepts: string
         *Returns: int
         */
        public int DisplayProvincePopulation(string province)
        {
            return cityCatalogue.Where(cityInfo => cityInfo.Value.Province == province).Sum(cityInfo => cityInfo.Value.Population);
        }
        /*Method Name: DisplayProvinceCities
         *Purpose: displays a list of cities in a province  
         *Accepts: string
         *Returns: List<CityInfo>
         */
        public List<CityInfo> DisplayProvinceCities(string province)
        {
            return cityCatalogue.Values.Where(cityInfo => cityInfo.Province == province).ToList();
        }
        /*Method Name: RankProvincesByPopulation
         *Purpose: ranks a list of provinces based on population in ascending order  
         *Accepts: nothing
         *Returns: SortedDictionary<int, string>
         */
        public SortedDictionary<int, string> RankProvincesByPopulation()
        {
            SortedDictionary<int, string> sortedDictionary = new SortedDictionary<int, string>();
            List<string> provinceList = new List<string>(cityCatalogue.Select(cityInfo => cityInfo.Value.Province).Distinct().ToList());

            foreach(string province in provinceList)
            {
                int totalPopulationByProvince = DisplayProvincePopulation(province);
                if (sortedDictionary.ContainsKey(totalPopulationByProvince))
                {
                    sortedDictionary.Add(totalPopulationByProvince + 1, province);
                }
                else
                {
                    sortedDictionary.Add(totalPopulationByProvince, province);
                }
            }
            return sortedDictionary;
        }
        /*Method Name: RankProvincesByCities
         *Purpose: ranks a list of provinces based on cities in ascending order  
         *Accepts: nothing
         *Returns: SortedDictionary<int, string>
         */
        public SortedDictionary<int, string> RankProvincesByCities()
        {
            SortedDictionary<int, string> sortedDictionary = new SortedDictionary<int, string>();
            List<string> provinceList = new List<string>(cityCatalogue.Select(cityInfo => cityInfo.Value.Province).Distinct().ToList());

            foreach(string province in provinceList)
            {
                sortedDictionary.Add(cityCatalogue.Count(cityInfo => cityInfo.Value.Province == province), province);
            }
            return sortedDictionary;
        }
        /*Method Name: GetCapital
         *Purpose: gets a capital of a province  
         *Accepts: string
         *Returns: CityInfo
         */
        public CityInfo GetCapital(string province)
        {
            return cityCatalogue.First(cityInfo => cityInfo.Value.Province == province && cityInfo.Value.Capital == "admin").Value;
        }
    }
}
