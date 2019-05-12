using System;
using System.Collections.Generic;
using System.Text;

namespace Models.BackendDTO
{
    public class TblSeatDTO
    {
        public int SeatNumber { get; set; } = 0;

        public virtual TblReservationDTO Reservation { get; set; }
    }
}
