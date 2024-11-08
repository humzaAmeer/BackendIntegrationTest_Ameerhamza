using BackendIntegrationTest_Ameerhamza.Model;
using Newtonsoft.Json;

namespace BackendIntegrationTest_Ameerhamza.Services.WeatherService
{
    public class WeatherService : IWeatherService
    {
        
    private static readonly string apiKey = "45324673c0324eeca3b104108240611";
    private static readonly string baseUrl = "http://api.weatherapi.com/v1/current.json";

        public async Task<Condition> GetCurrentWeather(string location)
        {
            using (var client = new HttpClient())
            {
                var url = $"{baseUrl}?key={apiKey}&q={location}&aqi=no";
                var response = await client.GetStringAsync(url);
                try
                {
                var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);
                return weatherResponse.Current.Condition;
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }
    }

}
