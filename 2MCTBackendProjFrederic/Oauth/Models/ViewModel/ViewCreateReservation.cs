using Models.BackendDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oauth.Models.ViewModel
{
    public class ViewCreateReservation
    {
        public List<TblFestivalsDTO> Festivals { get; set; }
        public string SelectedFestival { get; set; }
        public string SelectedPriceType { get; set; }
    }
}
