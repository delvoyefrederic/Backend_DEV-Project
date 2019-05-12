using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class TblFestivals
    {
        public TblFestivals()
        {
            TblPrice = new HashSet<TblPrice>();
            TblReservation = new HashSet<TblReservation>();
        }

        public Guid MusicEvenementId { get; set; }
        public string MusicEvenementName { get; set; }
        public Guid PriceListid { get; set; }
        public int MaxSeatNumbers { get; set; }

        public virtual ICollection<TblPrice> TblPrice { get; set; }
        public virtual ICollection<TblReservation> TblReservation { get; set; }
    }
}
