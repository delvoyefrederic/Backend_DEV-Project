using System;
using System.Collections.Generic;
using System.Text;

namespace Models.BackendDTO
{
    public class TblReservationDTO
    {
        public virtual TblFestivalsDTO MusicEvement { get; set; }
        public virtual TblPriceDTO Price { get; set; }
        public virtual AspNetUserDTO User { get; set; }
        public virtual TblSeatDTO Seat { get; set; }

    }
}
