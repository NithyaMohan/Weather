using System.Configuration;

namespace WeatherForecast.Controllers
{
    public class ConfigurationValues
    {
        public static string APIKEY
        {
            get
            {
                return ConfigurationManager.AppSettings["apikey"];
            }
        }
    }
}