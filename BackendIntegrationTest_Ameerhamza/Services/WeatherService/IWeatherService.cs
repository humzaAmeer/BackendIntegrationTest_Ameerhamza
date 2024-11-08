using BackendIntegrationTest_Ameerhamza.Model;

namespace BackendIntegrationTest_Ameerhamza.Services.WeatherService
{
    public interface IWeatherService
    {
        public Task<Condition> GetCurrentWeather(string location);
    }
}
