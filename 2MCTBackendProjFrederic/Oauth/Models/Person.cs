using Microsoft.AspNetCore.Identity;

namespace Oauth.Models
{
    public class Person : IdentityUser
    {
        public string FirstName { get; set; } // FirstName
        public string LastName { get; set; } // LastName
        public string Street { get; set; }
        public string PostCode { get; set; } //PostCode
        public string BusNumber { get; set; }
        public string StreetNumber { get; set; }

    }
}
