using System;
using System.Collections.Generic;

namespace API.NewModels
{
    public partial class TblReservation
    {
        public Guid ReservationId { get; set; }
        public Guid MusicEvementId { get; set; }
        public Guid PriceId { get; set; }
        public string Userid { get; set; }
        public Guid? SeatId { get; set; }

        public virtual TblFestivals MusicEvement { get; set; }
        public virtual TblPrice Price { get; set; }
        public virtual TblSeat Seat { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
