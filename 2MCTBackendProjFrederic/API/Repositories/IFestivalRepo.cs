using System.Collections.Generic;
using System.Threading.Tasks;
using Models.BackendDTO;
using Models.Models;

namespace API.Repositories
{
    public interface IFestivalRepo
    {
        Task<TblFestivals> GetFestivalName(TblFestivalsDTO tblFestivalsDTO);
        Task<List<TblFestivalsDTO>> GetFestivals();
        Task<TblPrice> GetFestivalTypeCategory(TblPriceDTO tblPriceDTO);
        Task<TblPrice> GetPrice(TblReservation tblReservation);
        Task<TblPriceDTO> GetPriceofCurrentFestivalType(TblReservation tblReservation);
    }
}