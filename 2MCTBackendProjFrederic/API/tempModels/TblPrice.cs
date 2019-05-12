using System;
using System.Collections.Generic;

namespace API.NewModels
{
    public partial class TblPrice
    {
        public TblPrice()
        {
            TblReservation = new HashSet<TblReservation>();
        }

        public Guid Priceid { get; set; }
        public decimal Price { get; set; }
        public Guid Type { get; set; }
        public Guid MusicEvenementId { get; set; }

        public virtual TblFestivals MusicEvenement { get; set; }
        public virtual TblTypeCategory TypeNavigation { get; set; }
        public virtual ICollection<TblReservation> TblReservation { get; set; }
    }
}
