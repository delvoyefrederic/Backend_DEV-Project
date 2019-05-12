using System.Collections.Generic;
using System.Threading.Tasks;
using Models.BackendDTO;
using Models.Models;

namespace API.Repositories
{
    public interface IReservationRepo
    {
        Task AddnewReservatie(TblReservation tblReservation);
        Task<List<TblReservationDTO>> GetReservations();
        Task<List<TblReservationDTO>> GetReservationsOfUser(AspNetUsers user);
    }
}