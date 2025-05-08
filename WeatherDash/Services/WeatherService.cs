using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherDash.Models;
using System.Diagnostics;


namespace WeatherDash.Services
{
    internal class WeatherService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public async Task<WeatherModel?> GetWeatherAsync(string city)
        {
            string apiKey = "d9c99dc0f3769956182975d86eda4020";

            string url = $"https://api.openweathermap.org/data/2.5/weather?q={Uri.EscapeDataString(city)}&appid={apiKey}&units=metric";
            try 
            { 
                var response = await _httpClient.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                
                Debug.WriteLine("Odpowiedź z API:");
                Debug.WriteLine(json);

                return JsonSerializer.Deserialize<WeatherModel>(json);
            }
            catch
            {
                return null;
            }
        }
    }    
}
