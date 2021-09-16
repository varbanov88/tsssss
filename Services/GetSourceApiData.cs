using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace BackendIntegrator.Services
{
    public class GetSourceApiData : IGetSourceApiData
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public GetSourceApiData(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _client = clientFactory.CreateClient("BackEndIntegratorApi");
            _configuration = configuration;
        }

        public async Task<string> GetDataFromApi()
        {
            var primaryEndpoint = _configuration.GetValue<string>("PrimaryDataEndpoint");
            var stringResponse = string.Empty;

            var response = await _client.GetAsync(primaryEndpoint);
            if (response.IsSuccessStatusCode)
            {
                stringResponse = await response.Content.ReadAsStringAsync();
                return stringResponse;
            }

            var secondaryEndpoint = _configuration.GetValue<string>("SecondaryDataEndpoint");
            response = await _client.GetAsync(secondaryEndpoint);
            if (response.IsSuccessStatusCode)
            {
                stringResponse = await response.Content.ReadAsStringAsync();
                return stringResponse;
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }
    }
}
