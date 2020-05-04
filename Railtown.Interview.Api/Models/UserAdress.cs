
namespace Railtown.Interview.Api.Models
{
    public class UserAdress
    {
        public string Street { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public GeoLocation Geo { get; set; }
    }
}
