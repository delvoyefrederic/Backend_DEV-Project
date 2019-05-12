using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class TblTypeCategory
    {
        public TblTypeCategory()
        {
            TblPrice = new HashSet<TblPrice>();
        }

        public Guid Type { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TblPrice> TblPrice { get; set; }
    }
}
