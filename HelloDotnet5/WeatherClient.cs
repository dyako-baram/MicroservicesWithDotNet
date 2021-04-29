using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace HelloDotnet5
{
    public class WeatherClient
    {
        private readonly HttpClient _httpClient;
        private readonly ServiceSettings _serviceSettings;
        public WeatherClient(HttpClient httpClient, IOptions<ServiceSettings> options)
        {
            _httpClient=httpClient;
            _serviceSettings=options.Value;
        }
        public record Weather(string description);
        public record Main(decimal temp);

        public record Forecast(Weather[] weather, Main main,long dt);
        public async Task<Forecast> GetCurrentWeatherAsync(string city){
            return await _httpClient.GetFromJsonAsync<Forecast>($"https://{_serviceSettings.OpenWeatherHost}/data/2.5/weather?q={city}&appid={_serviceSettings.ApiKey}&units=metric");
        }
    }
}