using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.BackendDTO;
using Models.Models;

namespace API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class reservationsController : ControllerBase
    {
        readonly IUserRepo _userRepo;
        readonly IFestivalRepo _festivalRepo;
        readonly IReservationRepo _reservationRepo;
        public reservationsController(IUserRepo userRepo, IFestivalRepo festivalRepo, IReservationRepo reservationRepo)
        {
            _userRepo = userRepo;
            _festivalRepo = festivalRepo;
            _reservationRepo = reservationRepo;
        }

        // GET: api/reservations

        // GET: api/reservations/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        public async Task<List<TblReservationDTO>> Reservations()
        {
            try
            {
                var result = await _reservationRepo.GetReservations();
                return result;
            }catch(Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        // POST: api/reservations
        [HttpPost]
        public async Task<ActionResult> PostReservation([FromBody] TblReservationDTO tblReservationDTO)
        {
            try
            {
                AspNetUserDTO aspNetUserDTO = new AspNetUserDTO();
                aspNetUserDTO.Email = tblReservationDTO.User.Email;

                AspNetUsers aspNetUser = await _userRepo.GetUser(aspNetUserDTO);
                TblFestivalsDTO tblFestivalsDTO = new TblFestivalsDTO();
                tblFestivalsDTO.MusicEvenementName = tblReservationDTO.MusicEvement.MusicEvenementName;

                TblFestivals tblFestivals = new TblFestivals();
                tblFestivals = await _festivalRepo.GetFestivalName(tblFestivalsDTO);

                TblPriceDTO tblPriceDTOGetCat = new TblPriceDTO()
                {
                    Name = tblReservationDTO.Price.Name
                };

                TblPrice tblPriceCat = new TblPrice();
                tblPriceCat = await _festivalRepo.GetFestivalTypeCategory(tblPriceDTOGetCat);

                TblReservation tblTempReservation = new TblReservation();
                tblTempReservation.MusicEvementId = tblFestivals.MusicEvenementId;
                tblTempReservation.Price = new TblPrice()
                {
                    Price = tblReservationDTO.Price.Price,
                    Type = tblPriceCat.Type
                };
                int NumberOfSeat;

                if (tblReservationDTO.Seat != null)
                {
                    NumberOfSeat = tblReservationDTO.Seat.SeatNumber;
                }
                else
                {
                    NumberOfSeat = 0;
                }


                TblPrice tblPrice = new TblPrice();
                tblPrice = await _festivalRepo.GetPrice(tblTempReservation);
                Guid NewSeatId = Guid.NewGuid();
                Guid NewReservationId = Guid.NewGuid();
                TblReservation tblReservation = new TblReservation()
                {
                    ReservationId = NewReservationId,
                    Userid = aspNetUser.Id,
                    MusicEvementId = tblFestivals.MusicEvenementId,
                    PriceId = tblPrice.Priceid,
                    SeatId = NewSeatId,
                    Seat = new TblSeat()
                    {
                        SeatId = NewSeatId,
                        SeatNumber = NumberOfSeat
                    }
                };

                await _reservationRepo.AddnewReservatie(tblReservation);
                return new OkObjectResult(200);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(400);
            }


        }



        // PUT: api/reservations/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
