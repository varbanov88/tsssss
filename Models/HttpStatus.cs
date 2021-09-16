using System.ComponentModel;

namespace BackendIntegrator.Models
{
    public enum HttpStatus
    {
        success = 200,
        unauthorised = 401,
        [Description("unexpected error")] unexpectedError = 500,
        offline = 503,
    }
}
