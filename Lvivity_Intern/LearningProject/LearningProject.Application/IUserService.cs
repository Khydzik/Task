using LearningProject.Data.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LearningProject.Application
{
    public interface IUserService
    {
        Task<User> GetUser(string username, string password);
        Task<User> CreateUser(string username, string password);
        Task<Role> ChangeUserRole(int userId, int roleId);
        Task<List<User>> GetUsersItem(PaginationModel paginationModel);
        ClaimsIdentity GetIdentity(User user);
    }
}
