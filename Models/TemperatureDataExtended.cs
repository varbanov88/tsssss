namespace BackendIntegrator.Models
{
    public class TemperatureDataExtended : TemperatureData
    {
        public HttpStatus HttpStatusCode => HttpStatus.success;

        public bool Success { get; set; }

        public ErrorCode Error { get; set; }
    }
}
