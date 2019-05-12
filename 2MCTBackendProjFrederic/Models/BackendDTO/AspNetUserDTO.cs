using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Models.BackendDTO
{
    public class AspNetUserDTO
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
        public string PasswordHash { get; set; }
        public IEnumerable<TblReservationDTO> Reservaties { get; set; }
        public UserRolesDTO userRoles { get; set; }

    }
}
