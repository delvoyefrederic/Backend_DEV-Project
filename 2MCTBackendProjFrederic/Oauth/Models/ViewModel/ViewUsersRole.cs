using Models.BackendDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oauth.Models.ViewModel
{
    public class ViewUsersRole
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string PostCode { get; set; }
        public string BusNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SelectedRole { get; set; }
        public List<UserRolesDTO> userRoles { get; set; } = new List<UserRolesDTO>();
    }
}
