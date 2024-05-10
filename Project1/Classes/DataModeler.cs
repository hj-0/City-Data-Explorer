using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Project1.Classes
{
    public class DataModeler
    {
        public static Dictionary<string, CityInfo> cityCatalogue;
        private delegate void fileDelegate(string file);

        /*Method Name: ParseFile
         *Purpose: parses a specific file according to user's selection
         *Accepts: two strings
         *Returns: Dictionary<string, CityInfo>
         */
        public static Dictionary <string, CityInfo> ParseFile(string file, string fileType)
        {
            fileDelegate parseHandler = null;

            switch (fileType.ToLower())
            {
                case ".xml":
                    parseHandler += ParseXML;
                    break;
                case ".json":
                    parseHandler += ParseJSON;
                    break;
                case ".csv":
                    parseHandler += ParseCSV;
                    break;
                default:
                    break;
            }

            cityCatalogue = new Dictionary<string, CityInfo>();
            parseHandler(file);

            return cityCatalogue;
        }
        /*Method Name: ParseXML
         *Purpose: parses an xml file and stores the values in the dictionary object
         *Accepts: string
         *Returns: void
         */
        private static void ParseXML(string file)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(file);
                foreach(XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    int cityID = int.Parse(node.ChildNodes[8]?.InnerText);
                    string cityName = node.ChildNodes[0]?.InnerText;
                    string cityAscii = node.ChildNodes[1]?.InnerText;
                    int population = int.Parse(node.ChildNodes[7]?.InnerText);
                    string province = node.ChildNodes[5]?.InnerText;
                    double latitude = Double.Parse(node.ChildNodes[2]?.InnerText);
                    double longitude = Double.Parse(node.ChildNodes[3]?.InnerText);
                    string capital = node.ChildNodes[6]?.InnerText;

                    CityInfo cityInfo = new CityInfo(cityID, cityName, cityAscii, population, province, latitude, longitude, capital);

                    string key = $"{cityName}";
                    if (!cityCatalogue.ContainsKey(key) && cityName != string.Empty)
                    {
                        cityCatalogue.Add(key, cityInfo);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /*Method Name: ParseJSON
         *Purpose: parses a json file and stores the items in the dictionary object
         *Accepts: string
         *Returns: void
         */
        private static void ParseJSON(string file)
        {
            try
            {
                string rawJSON = File.ReadAllText(file);
                JArray jArray = JsonConvert.DeserializeObject<JArray>(rawJSON);

                foreach (var item in jArray.Children())
                {
                    int cityID = item.Value<int>("id");
                    string cityName = item.Value<string>("city");
                    string cityAscii = item.Value<string>("city_ascii");
                    int population = item.Value<int>("population");
                    string province = item.Value<string>("admin_name");
                    double latitude = item.Value<double>("lat");
                    double longitude = item.Value<double>("lng");
                    string capital = item.Value<string>("capital");

                    CityInfo cityInfo = new CityInfo(cityID, cityName, cityAscii, population, province, latitude, longitude, capital);

                    string key = $"{cityName}";
                    if (!cityCatalogue.ContainsKey(key) && cityName != string.Empty)
                    {
                        cityCatalogue.Add(key, cityInfo);
                    }
                }     
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /*Method Name: ParseCSV
         *Purpose: parses a csv file and stores the data in the dictionary object
         *Accepts: string
         *Returns: void
         */
        private static void ParseCSV(string file)
        {
            try
            {
                List<string> rawCSV = new List<string>(File.ReadAllLines(file));
                rawCSV.RemoveAt(0);

                foreach (var data in rawCSV)
                {
                    List<string> cityData = new List<string>(data.Split(','));

                    int cityID = int.Parse(cityData[8]);
                    string cityName = cityData[0];
                    string cityAscii = cityData[1];
                    int population = int.Parse(cityData[7]);
                    string province = cityData[5];
                    double latitude = Double.Parse(cityData[2]);
                    double longitude = Double.Parse(cityData[3]);
                    string capital = cityData[6];

                    CityInfo cityInfo = new CityInfo(cityID, cityName, cityAscii, population, province, latitude, longitude, capital);

                    string key = $"{cityName}";
                    if (!cityCatalogue.ContainsKey(key) && cityName != string.Empty)
                    {
                        cityCatalogue.Add(key, cityInfo);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
