using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_Project;

static class LocationHelper
{
    private const string latLonPath = "Resources/worldcities.csv";
    private static List<List<string[]>> CitiesByCountry = new();

    public static void CreateLists()
    {
        string[] file = File.ReadAllLines(latLonPath);
        List<string[]> allCities = new();
        foreach (string city in file) allCities.Add(city.Split(','));

        string currentCountry = allCities[0][0];
        List<string[]> country = new();
        foreach (string[] city in allCities)
        {
            if (city[0] == currentCountry) country.Add(city);
            else
            {
                CitiesByCountry.Add( new List<string[]>(country));
                country.Clear();
                country.Add(city);
                currentCountry = city[0];
            }
        }
    }

    public static string GetLatLon(int countryIndex, int cityIndex)
    {
        try
        {
            string lat = CitiesByCountry[countryIndex][cityIndex][2];
            string lon = CitiesByCountry[countryIndex][cityIndex][3];
            return $"{lat},{lon}";
        }catch (Exception ex)
        {
            MessageBox.Show("Index out of bounds of lists", "ERROR");
            MessageBox.Show(ex.ToString());
            return null;
        }
    }

    public static string[] GetCountries()
    {
        List<string> countries = new();
        foreach (List<string[]> country in  CitiesByCountry) countries.Add(country[0][0]);
        return countries.ToArray();
    }

    public static string[] GetCities(int countryIndex)
    {
        List<string> cityNames = new();
        List<string[]> cities = CitiesByCountry[countryIndex];
        foreach (string[] city in cities) cityNames.Add(city[1]);
        return cityNames.ToArray();
    }
}
