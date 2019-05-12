using API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.BackendDTO;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly BackendProjContext _databaseContext;
        public UserRepo(BackendProjContext backendProjContext)
        {
            _databaseContext = backendProjContext;
        }

        public async Task<List<AspNetUserDTO>> GetUsers()
        {
            try{
                var UsersGetInfo = _databaseContext.AspNetUsers.Include(y => y.AspNetUserRoles).Select(a => new AspNetUserDTO()
                {
                    UserName = a.UserName,
                    Email = a.Email,
                    BusNumber = a.BusNumber,
                    PhoneNumber = a.PhoneNumber,
                    PostCode = a.PostCode,
                    Street = a.Street,
                    StreetNumber = a.StreetNumber,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    userRoles = new UserRolesDTO()
                    {
                        RoleName = a.AspNetUserRoles.Where(e => e.UserId == a.Id).Select(z => z.Role.Name).FirstOrDefault()

                    }
                }).ToList();


                if(UsersGetInfo != null)
                {
                    return await Task.FromResult(UsersGetInfo);
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

        public async Task<AspNetUserDTO>GetUserInfo(AspNetUserDTO aspNetUserDTO)
        {
            try{
                if(aspNetUserDTO.Email != null)
                {
                    var userResult = _databaseContext.AspNetUsers.Where(a => a.Email == aspNetUserDTO.Email).Select(a => new AspNetUserDTO()
                    {
                        BusNumber = a.BusNumber,
                        Email = a.Email,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        PhoneNumber = a.PhoneNumber,
                        PostCode = a.PostCode,
                        Street = a.Street,
                        StreetNumber = a.StreetNumber,
                        UserName = a.Email
                    }).FirstOrDefault();
                    return await Task.FromResult(userResult);
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

        public async Task<AspNetUsers> GetUser(AspNetUserDTO aspNETUserDTO)
        {
            try
            {
                var UserGetInfo = _databaseContext.AspNetUsers.Where(a => a.Email == aspNETUserDTO.Email).Select(a => new AspNetUsers()
                {
                    Id = a.Id
                }).FirstOrDefault();

                if (UserGetInfo != null)
                {
                    return UserGetInfo;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<AspNetUserDTO> GetUserAuth(AspNetUserDTO aspNETUserDTO)
        {
            try
            {
                var UserGetInfo = _databaseContext.AspNetUsers.Include(y => y.TblReservation).ThenInclude(x => x.MusicEvement).Include(y => y.TblReservation).ThenInclude(b => b.Price).Include(y => y.TblReservation).Where(a => a.Email == aspNETUserDTO.Email).Select(a => new AspNetUserDTO()
                {
                    UserName = a.UserName,
                    Email = a.Email,
                    BusNumber = a.BusNumber,
                    PhoneNumber = a.PhoneNumber,
                    PostCode = a.PostCode,
                    Street = a.Street,
                    StreetNumber = a.StreetNumber,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    PasswordHash = a.PasswordHash,
                    Reservaties = a.TblReservation.Select(ab => new TblReservationDTO()
                    {
                        MusicEvement = new TblFestivalsDTO()
                        {
                            MusicEvenementName = ab.MusicEvement.MusicEvenementName
                        },
                        Price = new TblPriceDTO()
                        {
                            
                            Name = ab.Price.TypeNavigation.Name,
                            Price = ab.Price.Price
                        }
                    })
                })
                    .FirstOrDefault();
                if(UserGetInfo != null)
                {
                    var PWHasher = new PasswordHasher<IdentityUser>();
                    var Password = new IdentityUser()
                    {
                        PasswordHash = aspNETUserDTO.PasswordHash
                    };
                    bool pwd = PWHasher.VerifyHashedPassword(Password, UserGetInfo.PasswordHash, Password.PasswordHash) == PasswordVerificationResult.Success;


                    if (pwd)
                    {
                        Console.WriteLine("correct");
                        return await Task.FromResult(UserGetInfo);

                    }
                    else
                    {
                        Console.WriteLine("fout");
                        return null;
                    }
                    
                }
                else
                {
                    Console.WriteLine("lol");
                    return null;
                }

                //var postReponse = await string.Format("")
            }catch(Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task AddnewUser(AspNetUsers aspNetUsers, AspNetUserRoles AddRoleToUser)
        {
            try
            {
                if(aspNetUsers != null && AddRoleToUser != null)
                {
                    _databaseContext.AspNetUsers.Add(aspNetUsers);
                    await _databaseContext.SaveChangesAsync();

                    _databaseContext.AspNetUserRoles.Add(AddRoleToUser);
                    await _databaseContext.SaveChangesAsync();
                }
            }catch(Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<AspNetRoles> GetRole(string role)
        {
            try
            {
                if(role != null)
                {
                    var RoleResult = _databaseContext.AspNetRoles.Where(y => y.Name == role).Select(x => new AspNetRoles()
                    {
                        Id = x.Id

                    }).FirstOrDefault();
                    return await Task.FromResult(RoleResult);
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

        public async Task UpdateUserRole( AspNetUserRoles NewRoleToUser)
        {
            try
            {
                if(NewRoleToUser.RoleId != null && NewRoleToUser.UserId != null)
                {
                    try
                    {
                        var checkuserifexcist = _databaseContext.AspNetUserRoles.Where(y => y.UserId == NewRoleToUser.UserId).Select(c => new AspNetUserRoles() {
                            RoleId = c.RoleId,
                            UserId = c.UserId
                        }).FirstOrDefault();
                        if (checkuserifexcist != null)
                        {
                            _databaseContext.AspNetUserRoles.Remove(checkuserifexcist);
                            await _databaseContext.SaveChangesAsync();
                            _databaseContext.AspNetUserRoles.Add(NewRoleToUser);
                        }
                        else
                        {
                            _databaseContext.AspNetUserRoles.Add(NewRoleToUser);
                        }
                        await _databaseContext.SaveChangesAsync();
                    }catch(Exception ex)
                    {
                        throw new ArgumentException(ex.Message);
                    }

                }

            }catch(Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task AddnewRole(AspNetRoles aspNetRoles)
        {
            try
            {
                if(aspNetRoles != null)
                {
                    _databaseContext.AspNetRoles.Add(aspNetRoles);
                    await _databaseContext.SaveChangesAsync();
                }
            }catch(Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<List<UserRolesDTO>> GetRoles()
        {
            try
            {
                var GetRolesResult = _databaseContext.AspNetRoles.Select(a => new UserRolesDTO() {
                    RoleName = a.Name
                }).ToList();

                return await Task.FromResult(GetRolesResult);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<AspNetUserRoles> GetUserRoleId(AspNetUsers aspNetUsers)
        {
            try
            {
                var GetCurrentRoleUser = _databaseContext.AspNetUserRoles.Where(z => z.UserId == aspNetUsers.Id).Select(d => new AspNetUserRoles() {
                    RoleId = d.RoleId
                    }).FirstOrDefault();

                return await Task.FromResult(GetCurrentRoleUser);



            }catch(Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<UserRolesDTO> GetUserRoleName(AspNetUserRoles aspNetUserRoles)
        {
            try
            {
                var GetRoleName = _databaseContext.AspNetRoles.Where(z => z.Id == aspNetUserRoles.RoleId).Select(d => new UserRolesDTO()
                {
                    RoleName = d.Name
                }).FirstOrDefault();

                return await Task.FromResult(GetRoleName);
            }catch(Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }


        }
    }
}
