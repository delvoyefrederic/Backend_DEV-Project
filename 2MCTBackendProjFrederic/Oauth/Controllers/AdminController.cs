using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.BackendDTO;
using Oauth.Data;
using Oauth.Models;
using Oauth.Models.ViewModel;

namespace Oauth.Controllers
{
    [Authorize(Roles = "Admin")]
    [AutoValidateAntiforgeryToken]
    public class AdminController : Controller
    {
        readonly ApplicationDbContext _applicationDbContext;
        readonly usersController _usersController;
        readonly IFestivalRepo _festivalRepo;
        readonly IUserRepo _userRepo;
        public AdminController(IFestivalRepo festivalRepo, ApplicationDbContext applicationDbContext, IUserRepo userRepo, usersController usersController)
        {
            _festivalRepo = festivalRepo;
            _userRepo = userRepo;
            _applicationDbContext = applicationDbContext;
            _usersController = usersController;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddnewRole()
        {
            return View("AddNewRole");
        }

        [HttpPost]
        public async Task<IActionResult> AddNewRolePost(UserRolesDTO userRolesDTO)
        {
            await _usersController.AddNewRole(userRolesDTO);
            var model = await _userRepo.GetUsers();
            return View("UsersList", model);
        }

        [HttpGet]
        [ActionName("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRole(string Email)
        {
            try
            {
                if(Email != null)
                {
                    AspNetUserDTO currentUser = new AspNetUserDTO()
                    {
                        Email = Email
                    };
                    var Getuser = await _userRepo.GetUser(currentUser);
                    var GetRoleUserid = await _userRepo.GetUserRoleId(Getuser);
                    var GetUserInfo = await _userRepo.GetUserInfo(currentUser);
                    string selected;
                    if(GetRoleUserid == null)
                    {
                        selected = "None";
                    }
                    else
                    {
                        var result = await _userRepo.GetUserRoleName(GetRoleUserid);
                        selected = result.RoleName;
                    }

                    var GetRoles = await _userRepo.GetRoles();
                    ViewUsersRole viewUsersRole = new ViewUsersRole()
                    {
                        BusNumber = GetUserInfo.BusNumber,
                        Email = GetUserInfo.Email,
                        FirstName = GetUserInfo.FirstName,
                        LastName = GetUserInfo.LastName,
                        PhoneNumber = GetUserInfo.PhoneNumber,
                        PostCode = GetUserInfo.PostCode,
                        Street = GetUserInfo.Street,
                        StreetNumber = GetUserInfo.StreetNumber,
                        UserName = GetUserInfo.UserName,
                        userRoles = GetRoles,
                        SelectedRole = selected
                    };
                    //await _usersController.UpdateUserRole(CurrentUserDTO);
                    //var model = _userRepo.GetUsers();
                    return View(viewUsersRole);
                }
                else
                {
                    return BadRequest();
                }


            }catch(Exception ex)
            {
                return BadRequest();
            }

            
            



        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserRolePost(ViewUsersRole viewUsersRole)
        {
            AspNetUserDTO updateuser = new AspNetUserDTO()
            {
                Email = viewUsersRole.Email,
                userRoles = new UserRolesDTO()
                {
                    RoleName = viewUsersRole.SelectedRole
                }
            };

            await _usersController.UpdateUserRole(updateuser);
            return RedirectToAction("UsersList");
        }

        [HttpGet]
        public async Task<IActionResult> FestivalList()
        {
            var model = await _festivalRepo.GetFestivals();
            return View("FestivalList",model);
        }
        
        [HttpGet]
        public async Task<IActionResult> UsersList()
        {
            var model = await _userRepo.GetUsers();
            return View("UsersList",model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}