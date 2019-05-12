using System;
using System.Collections.Generic;

namespace API.NewModels
{
    public partial class TblSeat
    {
        public TblSeat()
        {
            TblReservation = new HashSet<TblReservation>();
        }

        public Guid SeatId { get; set; }
        public int? SeatNumber { get; set; }

        public virtual ICollection<TblReservation> TblReservation { get; set; }
    }
}
