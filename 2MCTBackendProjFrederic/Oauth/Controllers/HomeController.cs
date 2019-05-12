using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using API.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.BackendDTO;
using Models.Models;
using Oauth.Models;
using Oauth.Models.ViewModel;

namespace Oauth.Controllers
{
    public class HomeController : Controller
    {
        readonly UserManager<Person> _userManager;
        readonly IFestivalRepo _FestivalRepo;
        readonly IReservationRepo _reservationRepo;
        readonly reservationsController _reservationsController;
        public HomeController(UserManager<Person> userManager, IFestivalRepo festivalRepo, IReservationRepo reservationRepo, reservationsController reservationsController)
        {
            _userManager = userManager;
            _FestivalRepo = festivalRepo;
            _reservationRepo = reservationRepo;
            _reservationsController = reservationsController;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ReservationTicket()
        {
            var GetFestivals = _FestivalRepo.GetFestivals();
            ViewCreateReservation viewCreateReservation = new ViewCreateReservation()
            {
                Festivals = GetFestivals.Result
            };
            return View("ReservationTicket", viewCreateReservation);
        }

        [HttpPost]
        [ActionName("ReservationTicket")]
        public async Task<IActionResult> ReservationTicketPost(ViewCreateReservation viewCreateReservation)
        {
            Person currentuser = await GetCurrentUserAsync();
            TblFestivalsDTO tblFestivalsDTO = new TblFestivalsDTO()
            {
                MusicEvenementName = viewCreateReservation.SelectedFestival
            };
            var GetFestivalId = _FestivalRepo.GetFestivalName(tblFestivalsDTO);

            TblPriceDTO tblPriceDTO = new TblPriceDTO()
            {
                Name = viewCreateReservation.SelectedPriceType
            };
            var GetFestivalTypeId = _FestivalRepo.GetFestivalTypeCategory(tblPriceDTO);

            TblReservation tblReservation = new TblReservation()
            {
                MusicEvementId = GetFestivalId.Result.MusicEvenementId,
                Price = GetFestivalTypeId.Result
                
            };

            var getPrice = await _FestivalRepo.GetPriceofCurrentFestivalType(tblReservation);
            TblReservationDTO tblReservationDTO = new TblReservationDTO()
            {
                MusicEvement = new TblFestivalsDTO()
                {
                    MusicEvenementName = viewCreateReservation.SelectedFestival,
                    Name = viewCreateReservation.SelectedPriceType
                },
                Price = new TblPriceDTO()
                {
                    Name = viewCreateReservation.SelectedPriceType,
                    Price = getPrice.Price
                },
                User = new AspNetUserDTO()
                {
                    Email = currentuser.Email
                }
            };
            await _reservationsController.PostReservation(tblReservationDTO);

            AspNetUsers user = new AspNetUsers()
            {
                Id = currentuser.Id,
                Email = currentuser.Email
            };

            var model = await _reservationRepo.GetReservationsOfUser(user);

            if(model.Count == 0)
            {
                return RedirectToAction("ReservationTicket");
            }
            else
            {
                return View("Reservations", model);
            }


        }

        [HttpGet]
        public async Task<IActionResult> Reservations()
        {
            Person currentuser = await GetCurrentUserAsync();
            AspNetUsers user = new AspNetUsers()
            {
                Id = currentuser.Id,
                Email = currentuser.Email
            };
            var model = await _reservationRepo.GetReservationsOfUser(user);

            if(model.Count == 0)
            {
                return RedirectToAction("ReservationTicket");
            }
            else
            {
                return View("Reservations", model);
            }

        }

        private Task<Person> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
