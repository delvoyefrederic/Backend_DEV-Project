using System.Collections.Generic;
using System.Threading.Tasks;
using Models.BackendDTO;
using Models.Models;

namespace API.Repositories
{
    public interface IUserRepo
    {
        Task AddnewRole(AspNetRoles aspNetRoles);
        Task AddnewUser(AspNetUsers aspNetUsers, AspNetUserRoles AddRoleToUser);
        Task<AspNetRoles> GetRole(string role);
        Task<List<UserRolesDTO>> GetRoles();
        Task<AspNetUsers> GetUser(AspNetUserDTO aspNETUserDTO);
        Task<AspNetUserDTO> GetUserAuth(AspNetUserDTO aspNETUserDTO);
        Task<AspNetUserDTO> GetUserInfo(AspNetUserDTO aspNetUserDTO);
        Task<AspNetUserRoles> GetUserRoleId(AspNetUsers aspNetUsers);
        Task<UserRolesDTO> GetUserRoleName(AspNetUserRoles aspNetUserRoles);
        Task<List<AspNetUserDTO>> GetUsers();
        Task UpdateUserRole(AspNetUserRoles NewRoleToUser);
    }
}