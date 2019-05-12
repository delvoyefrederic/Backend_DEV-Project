using System;
using System.Collections.Generic;
using System.Text;

namespace Models.BackendDTO
{
    public class TblFestivalsDTO:TblPriceDTO
    {
        public string MusicEvenementName { get; set; }
        public int MaxSeatNumbers { get; set; }
    }
}
