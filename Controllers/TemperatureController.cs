using BackendIntegrator.Attributes;
using BackendIntegrator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BackendIntegrator.Controllers
{
    [ApiController]
    [HMACAuthentication]
    [Route("[controller]")]
    public class TemperatureController : ControllerBase
    {
        private readonly Random _rnd = new Random();
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public TemperatureController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _client = clientFactory.CreateClient("OpenWeatherApi");
            _configuration = configuration;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<TemperatureData>> TodayJson()
        {
            var result = await GetTemperatureDataAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [Produces("application/xml")]
        [HttpGet("[action]")]
        public async Task<ActionResult<TemperatureDataExtended>> TodayXml()
        {
            var data = await GetTemperatureDataAsync();
            var values = Enum.GetValues(typeof(ErrorCode));
            var randomStatus = (ErrorCode)values.GetValue(_rnd.Next(values.Length));
            return Ok(new TemperatureDataExtended
            {
                Pressure = data != null ? data.Pressure : 0,
                TemperatureInC = data != null ? data.TemperatureInC : 0,
                Error = data != null ? randomStatus : ErrorCode.unexpectedError,
                Success = true
            });
        }

        private async Task<TemperatureData> GetTemperatureDataAsync()
        {
            TemperatureData result = null; ;
            var key = _configuration.GetValue<string>("OpenWeatherKey");
            var endpoint = $"?q=Varna&appid={key}";
            var response = await _client.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                var respnseBody = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<WeatherResponse>(respnseBody);
                result = new TemperatureData
                {
                    Pressure = responseData.Main.Pressure,
                    TemperatureInC = responseData.Main.Temp / 32
                };
            }

            return result;
        }
    }
}
