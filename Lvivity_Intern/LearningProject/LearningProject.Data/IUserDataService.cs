using LearningProject.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningProject.Data
{
    public interface IUserDataService
    {
        Task<Role> GetRole(string role);
        Task<User> GetUser(int id);
        Task<User> GetUserLogin(LoginModel data);
        Task<List<User>> GetListUsers(PaginationModel model);
        Task<Role> SaveRole(User userRole, EditModel editRole);
        Task<bool> IsRegistrationData(Register registerUser);
    }
}
