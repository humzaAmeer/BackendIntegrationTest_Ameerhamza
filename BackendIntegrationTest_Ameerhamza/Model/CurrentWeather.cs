namespace BackendIntegrationTest_Ameerhamza.Model
{
    public class CurrentWeather
    {

            public float Temp_C { get; set; }
            public Condition Condition { get; set; }
        
    }

    public class WeatherResponse
    {
        public CurrentWeather Current { get; set; }
    }

    public class Condition
    {
        public string Text { get; set; }
    }
}
