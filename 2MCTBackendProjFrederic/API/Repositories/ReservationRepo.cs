using API.Data;
using Microsoft.EntityFrameworkCore;
using Models.BackendDTO;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class ReservationRepo : IReservationRepo
    {
        readonly BackendProjContext _backedProContext;
        public ReservationRepo(BackendProjContext backendProjContext)
        {
            _backedProContext = backendProjContext;
        }

        //public async Task<List<TblReservationDTO>> GetReservationsUser(res)
        public async Task<List<TblReservationDTO>> GetReservations()
        {
            var GetReservationsResult = _backedProContext.TblReservation.Select(y => new TblReservationDTO()
            {
                MusicEvement = new TblFestivalsDTO()
                {
                    MusicEvenementName = y.MusicEvement.MusicEvenementName,
                    Price = y.Price.Price,
                    Name = y.Price.TypeNavigation.Name

                    
                },
                Price = new TblPriceDTO()
                {
                    Price = y.Price.Price,
                    Name = y.Price.TypeNavigation.Name
                },
                Seat = new TblSeatDTO()
                {
                    SeatNumber = y.Seat.SeatNumber
                },
                User = new AspNetUserDTO()
                {
                    Email = y.User.Email
                }
            }).ToList();
            if(GetReservationsResult != null)
            {
                return GetReservationsResult;
            }
            else
            {
                return null;
            }

        }

        public async Task AddnewReservatie(TblReservation tblReservation)
        {
            try
            {
                if (tblReservation != null)
                {
                    _backedProContext.TblReservation.Add(tblReservation);
                    await _backedProContext.SaveChangesAsync();
                }
            }catch(Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<List<TblReservationDTO>> GetReservationsOfUser(AspNetUsers user) {
            try
            {
                var GetReservationsResult = _backedProContext.TblReservation.Where(y => y.Userid == user.Id).Select(c => new TblReservationDTO()
                {
                    MusicEvement = new TblFestivalsDTO()
                    {
                        MusicEvenementName = c.MusicEvement.MusicEvenementName,
                        Price = c.Price.Price,
                        Name = c.Price.TypeNavigation.Name


                    },
                    Price = new TblPriceDTO()
                    {
                        Price = c.Price.Price,
                        Name = c.Price.TypeNavigation.Name
                    },
                    Seat = new TblSeatDTO()
                    {
                        SeatNumber = c.Seat.SeatNumber
                    }
                }).ToList();

                if(GetReservationsResult != null)
                {
                    return await Task.FromResult(GetReservationsResult);
                }
                else
                {
                    return null;
                }
            }catch(Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

    }
}
