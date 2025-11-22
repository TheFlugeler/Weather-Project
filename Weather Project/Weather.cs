using System.Text;
using System;

namespace Weather_Project;

static class Weather
{
    public static async Task GetForeCast(string latlon)
    {
        throw NotImplementedException("MeteoMatic Web API has retracted their free plan and so this project has been deprecated");
        //string today = DateTime.Today.ToString("s");
        //HttpClient client = new();
//
        //Uri uri = new($"https://api.meteomatics.com/{today}ZP1D:PT1H/weather_symbol_1h:idx,t_2m:C,precip_1h:mm,uv:idx,wind_speed_10m:ms,wind_dir_10m:d/{latlon}/csv");
//
        //string auth = "Auth details (not shown for security)";
        //var base64auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(auth));
//
        //HttpRequestMessage message = new(HttpMethod.Get, uri);
        //message.Headers.Authorization = new("Basic", base64auth);
//
        //var response = await client.SendAsync(message);
        //response.EnsureSuccessStatusCode();
        //string responseBody = await response.Content.ReadAsStringAsync();
//
        //StreamWriter sw = new("weather.csv",false);
        //sw.Write(responseBody);
        //sw.Close();
        //client.Dispose();
    }

    public static List<List<string>> GetCSV()
    {
        List<string> WeatherCode = new();
        List<string> Temp = new();
        List<string> Rain = new();
        List<string> UV = new();
        List<string> Speed = new();
        List<string> Dir = new();

        StreamReader reader = new("weather.csv");
        reader.ReadLine();
        for (int i = 0; i < 24; i++)
        {
            string[] hour = reader.ReadLine().Split(';');
            WeatherCode.Add(hour[1]);
            Temp.Add(hour[2]);
            Rain.Add(hour[3]);
            UV.Add(hour[4]);
            Speed.Add(hour[5]);
            Dir.Add(hour[6]);
        }
        List<List<string>> output = new()
        {
            WeatherCode, Temp, Rain, UV, Speed, Dir
        };
        reader.Close();
        return output;
    }
}
