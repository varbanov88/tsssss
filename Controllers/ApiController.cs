using BackendIntegrator.Attributes;
using BackendIntegrator.Models;
using BackendIntegrator.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BackendIntegrator.Controllers
{
    [ApiController]
    [HMACAuthentication]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly IGetSourceApiData _getSourceApiData;

        public ApiController(IGetSourceApiData getSourceApiData)
        {
            _getSourceApiData = getSourceApiData;
        }

        [HttpGet]
        public async Task<string> GetWeatherInfo()
        {
            
            return await _getSourceApiData.GetDataFromApi();
        }
    }
}
