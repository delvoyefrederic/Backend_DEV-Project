using API.Data;
using API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http2.HPack;
using Models.BackendDTO;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [AutoValidateAntiforgeryToken]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly BackendProjContext _backendProjContext;

        public usersController(IUserRepo userRepo, BackendProjContext backendProjContext)
        {
            _backendProjContext = backendProjContext;
            _userRepo = userRepo;
        }

        private AspNetUserDTO userDTOcorrect = new AspNetUserDTO()
        {
            Email = "test1@example",
            PasswordHash = "Azerty01@"
        };

        /*private AspNetUserDTO userDTOfalse = new AspNetUserDTO()
        {
            Email = "tes@example",
            PasswordHash = "Azerty01@"
        };

        private AspNetUserDTO userDTOfalse2 = new AspNetUserDTO()
        {
            Email = "tes@example",
            PasswordHash = "Azerty01"
        };*/
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<Task<List<AspNetUserDTO>>> GetUsers()
        {
            var userresult = _userRepo.GetUsers();
            return userresult;
        }

        // GET api/values
        [HttpPost ("login")]
        public async Task<ActionResult<AspNetUserDTO>> Getuser([FromBody]AspNetUserDTO user)
        {

            try
            {
                /*AspNetUserDTO userDTO = new AspNetUserDTO();
                userDTO.Email = user[0];
                userDTO.PasswordHash = user[1];*/
                var result = await _userRepo.GetUserAuth(user);
                var resultest = result;
                return resultest;
            }catch(Exception ex)
            {
                return StatusCode(400);
            }

        }

        // POST api/values
        //Reigster new user
        [HttpPost("register")]
        public async Task<ActionResult> PostUser([FromBody] AspNetUserDTO newUser)
        {
            try
            {

                var GetRoleId = _userRepo.GetRole("Member");
                var hashedpassword = new PasswordHasher<IdentityUser>();
                var Password = new IdentityUser();
                var resultpass = hashedpassword.HashPassword(Password, newUser.PasswordHash);

                Guid NewuserId = Guid.NewGuid();
                AspNetUsers User = new AspNetUsers() {
                    Email = newUser.Email,
                    BusNumber = newUser.BusNumber,
                    FirstName = newUser.FirstName,
                    Id = Password.Id,
                    LastName = newUser.LastName,
                    NormalizedEmail = newUser.Email.ToUpper(),
                    NormalizedUserName = newUser.Email.ToUpper(),
                    PasswordHash = resultpass,
                    ConcurrencyStamp = Password.ConcurrencyStamp,
                    PhoneNumber = newUser.PhoneNumber,
                    PostCode = newUser.PostCode,
                    Street = newUser.Street,
                    StreetNumber = newUser.StreetNumber,
                    SecurityStamp = null,
                    UserName = newUser.Email
                };


                AspNetUserRoles AddRoleToUser = new AspNetUserRoles()
                {
                    RoleId = GetRoleId.Result.Id,
                    UserId = Password.Id
                };
                await _userRepo.AddnewUser(User, AddRoleToUser);
                return new OkObjectResult(200);

            }catch(Exception ex)
            {
                return new StatusCodeResult(400);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateUserRole")]
        public async Task<ActionResult> UpdateUserRole([FromBody] AspNetUserDTO aspNetUserDTO)
        {
            try
            {
                if(aspNetUserDTO.userRoles.RoleName != null && aspNetUserDTO.Email != null)
                {
                    var GetuserId = _userRepo.GetUser(aspNetUserDTO);
                    var GetUserRoleId = _userRepo.GetRole(aspNetUserDTO.userRoles.RoleName);
                    AspNetUserRoles NewRole = new AspNetUserRoles()
                    {
                        UserId = GetuserId.Result.Id,
                        RoleId = GetUserRoleId.Result.Id
                    };

                    await _userRepo.UpdateUserRole(NewRole);
                    return new StatusCodeResult(200);
                }
                else
                {
                    return new StatusCodeResult(400);
                }
            }catch(Exception ex)
            {
                return new StatusCodeResult(400);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddNewRole")]
        public async Task<ActionResult> AddNewRole([FromBody] UserRolesDTO userRolesDTO)
        {
            try
            {
                if (userRolesDTO != null)
                {
                    AspNetRoles aspNetRole = new AspNetRoles()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = userRolesDTO.RoleName,
                        NormalizedName = userRolesDTO.RoleName.ToUpper()
                    };
                    await _userRepo.AddnewRole(aspNetRole);
                    return new StatusCodeResult(200);
                }
                else
                {
                    return new StatusCodeResult(400);
                }
            }catch(Exception ex)
            {
                return new StatusCodeResult(400);
            }

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
