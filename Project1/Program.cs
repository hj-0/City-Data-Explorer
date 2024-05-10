using System;
using System.IO;
using System.Collections.Generic;
using Project1.Classes;

namespace Project1
{
    /**
      * Class Name: Program.cs		
      * Purpose: parses a specific data file according to user's selection and lists a bunch of different queries which a user
      * can select to display or calculate with given data
      * Coder: Haris	
      * Date: Feb 23rd, 2022
      */
    class Program
    {
        private const string filePath = @"..\..\..\Data";
        static void Main(string[] args)
        {
            Console.WriteLine("---------------------");
            Console.WriteLine("Program functionality");
            Console.WriteLine("---------------------");
            Console.WriteLine("\n- To exit program, enter `exit` at any prompt.");
            Console.WriteLine("- To start program from the beginning enter 'restart' at any prompt");
            Console.WriteLine("- You will be presented with a numbered list of options. Please enter a value, when prompted, to a corresponding file name, file type or data querying routine.");


            Statistics statistics = null;
            FileInfo[] files = null;
            bool resetCatalogue = true;
            bool isProgramDone = false;

            while (!isProgramDone)
            {
                int fileSelection = 0;
                if (resetCatalogue)
                {
                    Console.WriteLine("\nFetching list of available file names to be processed and queried");
                    files = new DirectoryInfo(filePath).GetFiles();
                    int counter = 1;
                    Console.WriteLine();
                    foreach(var file in files)
                    {
                        Console.WriteLine($"{counter}) {file.Name}");
                        counter++;
                    }
                    bool selectedFileType = false;

                    while (!selectedFileType)
                    {
                        Console.Write("\nSelect an option from the list above (e.g 1, 2): ");
                        string userInput = Console.ReadLine();

                        if (userInput.Equals("reset"))
                        {
                            continue;
                        }

                        if (userInput.Equals("exit"))
                        {
                            Environment.Exit(0);
                        }

                        try
                        {
                            fileSelection = int.Parse(userInput); 
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                            continue;
                        }
                        if(fileSelection < 1 || fileSelection > 3)
                        {
                            Console.WriteLine("Error: file selection value is out of range.");
                        }
                        else
                        {
                            selectedFileType = true;
                        }
                    }
                    Console.WriteLine($"\nA city catalogue has now been populated from the {files[fileSelection - 1].Name} file.");
                    Console.WriteLine($"\nFetching list of available data querying routines that can be run on the {files[fileSelection - 1].Name} file.");
                    statistics = new Statistics(files[fileSelection - 1].FullName, files[fileSelection - 1].Extension);
                    resetCatalogue = false;
                }
                bool displayQueries = true;
                int querySelection;
                while (displayQueries)
                {
                    Console.WriteLine("\n1) Display City Information");
                    Console.WriteLine("2) Display Province Cities");
                    Console.WriteLine("3) Calculate Province Population");
                    Console.WriteLine("4) Match Cities Population");
                    Console.WriteLine("5) Distance Between Cities");
                    Console.WriteLine("6) Display City With Largest Population");
                    Console.WriteLine("7) Display City With Smallest Population");
                    Console.WriteLine("8) Show City On Map");
                    Console.WriteLine("9) Display Provinces Ranked By Number Of Cities");
                    Console.WriteLine("10) Display Provinces Ranked By Number Of Population");
                    Console.WriteLine("11) Display Capital of Any Province");
                    Console.WriteLine("Enter 'reset' to start the program from the beginning or 'exit' to quit the program.");
                    Console.Write($"\nSelect a data query routine from the list above for the {files[fileSelection - 1].Name} file (e.g. 1, 2): ");
                    string userInput = Console.ReadLine();

                    if (userInput.Equals("reset"))
                    {
                        resetCatalogue = true;
                        break;
                    }

                    if (userInput.Equals("exit"))
                    {
                        isProgramDone = true;
                        break;
                    }

                    try
                    {
                        querySelection = int.Parse(userInput);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        continue;
                    }
                    if (querySelection == 1)
                    {
                        bool isQueryDone = false;
                        while (!isQueryDone)
                        {
                            Console.Write("\nEnter city name to display its information: ");
                            string cityInput = Console.ReadLine();

                            if (cityInput.Equals("reset"))
                            {
                                displayQueries = false;
                                resetCatalogue = true;
                                break;
                            }

                            if (cityInput.Equals("exit"))
                            {
                                displayQueries = false;
                                isProgramDone = true;
                                break;
                            }

                            try
                            {
                                CityInfo cityInfo = statistics.DisplayCityInformation(cityInput);
                                Console.WriteLine($"\nCity Name: {cityInfo.CityName}");
                                Console.WriteLine($"Province: {cityInfo.Province}");
                                string formattedPopulation = string.Format("{0:n0}", cityInfo.Population);
                                Console.WriteLine($"Population: {formattedPopulation}");
                                Console.WriteLine($"Location: Latitude :- {cityInfo.latitude}, Longitude :- {cityInfo.longitude}");
                                isQueryDone = true;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                                continue;
                            }

                        }
                    }
                    if(querySelection == 2)
                    {
                        bool isQueryDone = false;
                        while (!isQueryDone)
                        {
                            Console.Write("\nEnter province name to display a list of cities: ");
                            string provinceInput = Console.ReadLine();

                            if (provinceInput.Equals("reset"))
                            {
                                displayQueries = false;
                                resetCatalogue = true;
                                break;
                            }

                            if (provinceInput.Equals("exit"))
                            {
                                displayQueries = false;
                                isProgramDone = true;
                                break;
                            }

                            if (provinceInput.Equals("Quebec"))
                            {
                                provinceInput = "Québec";
                            }

                            try
                            {
                                List<CityInfo> cityInfo = statistics.DisplayProvinceCities(provinceInput);
                                if (cityInfo.Count > 1)
                                {
                                    Console.WriteLine($"\nThese are the list of cities in {provinceInput} province: ");
                                    Console.WriteLine();
                                    foreach (var city in cityInfo)
                                    {
                                        Console.WriteLine($"- {city.CityName}");
                                    }
                                    isQueryDone = true;
                                }
                                else
                                {
                                    Console.WriteLine($"Error: '{provinceInput}' province doesn't exist in the list");
                                }

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                                continue;
                            }

                        }
                    }
                    if(querySelection == 3)
                    {
                        bool isQueryDone = false;
                        while (!isQueryDone)
                        {
                            Console.Write("\nEnter a province name to display its population: ");
                            string provinceInput = Console.ReadLine();

                            if (provinceInput.Equals("reset"))
                            {
                                displayQueries = false;
                                resetCatalogue = true;
                                break;
                            }

                            if (provinceInput.Equals("exit"))
                            {
                                displayQueries = false;
                                isProgramDone = true;
                                break;
                            }

                            if (provinceInput.Equals("Quebec"))
                            {
                                provinceInput = "Québec";
                            }

                            try
                            {
                                int totalPopulaton = statistics.DisplayProvincePopulation(provinceInput);
                                if(totalPopulaton != 0)
                                {
                                    string formattedTotalPopulaton = string.Format("{0:n0}", totalPopulaton);
                                    Console.WriteLine($"\nTotal population of {provinceInput} is {formattedTotalPopulaton} people.");
                                    isQueryDone = true;
                                    
                                }
                                else
                                {
                                    Console.WriteLine($"Error: '{provinceInput}' province doesn't exist in the list");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                                continue;
                            }
                        }
                    }
                    if(querySelection == 4)
                    {
                        bool isQueryDone = false;
                        while (!isQueryDone)
                        {
                            Console.Write("\nEnter two city names, separated by a comma, to see which city has the larger population (eg. London, Toronto): ");
                            string cityInput = Console.ReadLine();

                            if (cityInput.Equals("reset"))
                            {
                                displayQueries = false;
                                resetCatalogue = true;
                                break;
                            }

                            if (cityInput.Equals("exit"))
                            {
                                displayQueries = false;
                                isProgramDone = true;
                                break;
                            }

                            try
                            {
                                string[] cities = cityInput.Split(',');
                                for(int i = 0; i < cities.Length; i++)
                                {
                                    cities[i] = cities[i].Trim(' ');
                                }

                                if(cities.Length == 2)
                                {
                                    (CityInfo, CityInfo, int) compareCitiesPop = statistics.CompareCitiesPopulation(cities[0], cities[1]);
                                    string formattedPopulationNo = string.Format("{0:n0}", compareCitiesPop.Item3);
                                    Console.WriteLine($"\n{compareCitiesPop.Item1.CityName} has a larger population than {compareCitiesPop.Item2.CityName} with a population of {formattedPopulationNo} people.");
                                    isQueryDone = true;
                                }
                                else
                                {
                                    Console.WriteLine("Error: Please enter only 2 cities separated by a comma and try again.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                                continue;
                            }
                        }
                    }
                    if(querySelection == 5)
                    {
                        bool isQueryDone = false;
                        while (!isQueryDone)
                        {
                            Console.Write("\nEnter two city names, separated by a comma, to see the distance between cities (eg. London, Toronto): ");
                            string cityInput = Console.ReadLine();

                            if (cityInput.Equals("reset"))
                            {
                                displayQueries = false;
                                resetCatalogue = true;
                                break;
                            }

                            if (cityInput.Equals("exit"))
                            {
                                displayQueries = false;
                                isProgramDone = true;
                                break;
                            }

                            try
                            {
                                string[] cities = cityInput.Split(',');
                                for (int i = 0; i < cities.Length; i++)
                                {
                                    cities[i] = cities[i].Trim(' ');
                                }

                                if (cities.Length == 2)
                                {
                                    double totalDistance = statistics.CalculateDistanceBetweenCities(cities[0], cities[1]);
                                    totalDistance = totalDistance / 1000;
                                    string formattedDistance = string.Format("{0:n0}", totalDistance);
                                    Console.WriteLine($"\nThe distance between from {cities[0]} to {cities[1]} is {formattedDistance} kilometers");
                                    isQueryDone = true;
                                }
                                else
                                {
                                    Console.WriteLine("Error: Please enter only 2 cities separated by a comma and try again.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                                continue;
                            }
                        }
                    }
                    if(querySelection == 6)
                    {
                        bool isQueryDone = false;
                        while (!isQueryDone)
                        {
                            Console.Write("\nEnter province name to display a city with largest population: ");
                            string provinceInput = Console.ReadLine();

                            if (provinceInput.Equals("reset"))
                            {
                                displayQueries = false;
                                resetCatalogue = true;
                                break;
                            }

                            if (provinceInput.Equals("exit"))
                            {
                                displayQueries = false;
                                isProgramDone = true;
                                break;
                            }

                            if (provinceInput.Equals("Quebec"))
                            {
                                provinceInput = "Québec";
                            }

                            try
                            {
                                CityInfo cityInfo = statistics.DisplayLargestPopulationCity(provinceInput);
                                Console.WriteLine($"\nCity with largest population in {provinceInput} is {cityInfo.CityName}");
                                isQueryDone = true;

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                                continue;
                            }
                        }
                    }
                    if (querySelection == 7)
                    {
                        bool isQueryDone = false;
                        while (!isQueryDone)
                        {
                            Console.Write("\nEnter province name to display a city with smallest population: ");
                            string provinceInput = Console.ReadLine();

                            if (provinceInput.Equals("reset"))
                            {
                                displayQueries = false;
                                resetCatalogue = true;
                                break;
                            }

                            if (provinceInput.Equals("exit"))
                            {
                                displayQueries = false;
                                isProgramDone = true;
                                break;
                            }

                            if (provinceInput.Equals("Quebec"))
                            {
                                provinceInput = "Québec";
                            }

                            try
                            {
                                CityInfo cityInfo = statistics.DisplaySmallestPopulationCity(provinceInput);
                                Console.WriteLine($"\nCity with smallest population in {provinceInput} is {cityInfo.CityName}");
                                isQueryDone = true;

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                                continue;
                            }
                        }
                    }
                    if (querySelection == 8)
                    {
                        bool isQueryDone = false;
                        while (!isQueryDone)
                        {
                            Console.Write("\nEnter city name with province, separated by a comma, to display a city on map (eg. London, Ontario): ");
                            string cityProvInput = Console.ReadLine();

                            if (cityProvInput.Equals("reset"))
                            {
                                displayQueries = false;
                                resetCatalogue = true;
                                break;
                            }

                            if (cityProvInput.Equals("exit"))
                            {
                                displayQueries = false;
                                isProgramDone = true;
                                break;
                            }

                            try
                            {
                                string[] cityProv = cityProvInput.Split(',');
                                for (int i = 0; i < cityProv.Length; i++)
                                {
                                    cityProv[i] = cityProv[i].Trim(' ');
                                }

                                if (cityProv[1].Equals("Quebec"))
                                {
                                    cityProv[1] = "Québec";
                                }

                                if (cityProv.Length == 2)
                                {
                                    statistics.ShowCityOnMap(cityProv[0], cityProv[1]);
                                    isQueryDone = true;
                                }
                                else
                                {
                                    Console.WriteLine("Error: Please enter only 1 city with 1 province separated by a comma and try again.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                                continue;
                            }
                        }
                    }
                    if(querySelection == 9)
                    {
                        bool isQueryDone = false;
                        while (!isQueryDone)
                        {
                            try
                            {
                                Console.WriteLine("\nProvinces ranked by cities (Ascending order):");
                                SortedDictionary<int, string> randkedProvinces = statistics.RankProvincesByCities();

                                int counter = 1;
                                Console.WriteLine();
                                foreach(var province in randkedProvinces)
                                {
                                    Console.WriteLine($"{counter}. {province.Value} ({province.Key.ToString()})");
                                    counter++;
                                }
                                isQueryDone = true;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                                continue;
                            }
                        }
                    }
                    if (querySelection == 10)
                    {
                        bool isQueryDone = false;
                        while (!isQueryDone)
                        {
                            try
                            {
                                Console.WriteLine("\nProvinces ranked by population (Ascending order):");
                                SortedDictionary<int, string> randkedProvinces = statistics.RankProvincesByPopulation();

                                int counter = 1;
                                Console.WriteLine();
                                foreach (var province in randkedProvinces)
                                {
                                    Console.WriteLine($"{counter}. {province.Value} ({province.Key.ToString("n0")})");
                                    counter++;
                                }
                                isQueryDone = true;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                                continue;
                            }
                        }
                    }
                    if(querySelection == 11)
                    {
                        bool isQueryDone = false;
                        while (!isQueryDone)
                        {
                            Console.Write("\nEnter province name to display its capital: ");
                            string provinceInput = Console.ReadLine();

                            if (provinceInput.Equals("reset"))
                            {
                                displayQueries = false;
                                resetCatalogue = true;
                                break;
                            }

                            if (provinceInput.Equals("exit"))
                            {
                                displayQueries = false;
                                isProgramDone = true;
                                break;
                            }

                            if (provinceInput.Equals("Quebec"))
                            {
                                provinceInput = "Québec";
                            }

                            try
                            {
                                CityInfo cityInfo = statistics.GetCapital(provinceInput);
                                Console.WriteLine($"\nThe Capital of {provinceInput} is {cityInfo.CityName}");
                                isQueryDone = true;

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                                continue;
                            }
                        }
                    }
                    if (querySelection < 1 || querySelection > 11)
                    {
                        Console.WriteLine("\nError: query selection value is out of range.");
                    }
                }
            }
        }
    }
}
