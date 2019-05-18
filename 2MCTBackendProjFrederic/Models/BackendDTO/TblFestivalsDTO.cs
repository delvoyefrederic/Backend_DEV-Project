using System;
using System.Collections.Generic;
using System.Text;

namespace Models.BackendDTO
{
    public class TblFestivalsDTO
    {
        public string MusicEvenementName { get; set; }
        public int MaxSeatNumbers { get; set; }
        public TblPriceDTO Price { get; set; }
        public List<TblPriceDTO> tblPriceDTOs { get; set; }
    }
}
