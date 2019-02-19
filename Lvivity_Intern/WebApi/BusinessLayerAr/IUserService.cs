using LearningProject.Data.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LearningProject.Application
{
    public interface IUserService
    {
        Task<User> GetUser(LoginModel data);
        Task<bool> IsRegistration(Register registerUser);
        Task<string> EditUser(EditModel model);
        Task<List<User>> GetUsersItem(PaginationModel paginationModel);
        ClaimsIdentity GetIdentity(User user);
    }
}
