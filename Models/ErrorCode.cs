using System.Xml.Serialization;

namespace BackendIntegrator.Models
{
    public enum ErrorCode
    {

        [XmlEnum("0")] success = 0,
        [XmlEnum("1")] unauthorized = 1,
        [XmlEnum("2")] unexpectedError = 2,
        [XmlEnum("3")] offline = 3
    }
}
