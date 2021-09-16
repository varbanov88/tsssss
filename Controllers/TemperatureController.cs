using BackendIntegrator.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BackendIntegrator.Controllers
{
    [ApiController]
    //[HmacAuthentication]
    [Route("[controller]")]
    public class TemperatureController : ControllerBase
    {
        private readonly Random _rnd = new Random();

        [HttpGet("[action]")]
        public ActionResult<TemperatureData> TodayJson()
        {
            var result = GetTemperatureData();
            return Ok(result);
        }

        [HttpGet("[action]")]
        public ActionResult<TemperatureDataExtended> TodayXml()
        {
            var data = GetTemperatureData();
            var values = Enum.GetValues(typeof(ErrorCode));
            var randomStatus = (ErrorCode)values.GetValue(_rnd.Next(values.Length));
            return new TemperatureDataExtended
            {
                Pressure = data.Pressure,
                TemperatureInC = data.TemperatureInC,
                Error = randomStatus,
                Success = true
            };
        }

        private TemperatureData GetTemperatureData()
        {
            var temperatureInC = _rnd.Next(-20, 55);
            var pressure = _rnd.Next(1001, 1084);

            return new TemperatureData
            {
                Pressure = pressure,
                TemperatureInC = temperatureInC
            };
        }
    }
}
