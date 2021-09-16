using System.ComponentModel;

namespace BackendIntegrator.Models
{
    public enum ErrorCode
    { 
        success = 0,
        unauthorized = 1,
        [Description("unexpected error")] unexpectedError = 2,
        offline = 3,

    }
}
