using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WeatherForecast.Models;

namespace WeatherForecast.Controllers
{
    public class ForecastController : Controller
    {
        protected string _apikey;
        // GET: Forecast
        public async Task<ActionResult> FiveDayForecast(string txtCity)
        {
            _apikey = ConfigurationValues.APIKEY;
            HttpResponseMessage httpResponse = null;
            using (var Client = new HttpClient())
            {
                try
                {
                    Client.BaseAddress = new Uri("http://api.openweathermap.org");
                    if (txtCity != null && txtCity != "")
                    {
                        httpResponse = await Client.GetAsync($"/data/2.5/forecast?q={txtCity}&appid={_apikey}&units=metric");
                        //http://api.openweathermap.org/data/2.5/forecast/daily?q=Melbourne,AU&cnt=5&&appid=2e0bfcd75d67a0452c6890c445c51030&units=metric
                    }
                    else
                    {
                        httpResponse = await Client.GetAsync($"/data/2.5/forecast?q=Melbourne,AU&appid={_apikey}&units=metric");

                    }

                    httpResponse.EnsureSuccessStatusCode();
                    var stringResult = await httpResponse.Content.ReadAsStringAsync();
                    ForecastModel rawWeather = new ForecastModel();
                    rawWeather = JsonConvert.DeserializeObject<ForecastModel>(stringResult);
                    return View(rawWeather);
                }
                catch (HttpRequestException httpRequestException)
                {
                    return View($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }

            }       
        }
    }
}