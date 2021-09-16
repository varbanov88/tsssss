using System.Threading.Tasks;

namespace BackendIntegrator.Services
{
    public interface IGetSourceApiData
    {
        Task<string> GetDataFromApi();
    }
}
