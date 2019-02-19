using LearningProject.Data;
using LearningProject.Data.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LearningProject.Application
{
    public class UserService:IUserService
    {
        private readonly IHostingEnvironment _appHosting;
        private readonly IUserDataService userDataService;

        public UserService(IUserDataService dataService, IHostingEnvironment app)
        {
            userDataService = dataService;
            _appHosting = app;
        }

        public UserService(IUserDataService dataService)
        {
            userDataService = dataService;
        }


        public async Task<User> GetUser(string username, string password)
        {
            return await userDataService.GetUserLogin(data);
        }

        public ClaimsIdentity GetIdentity(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Authorization", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }

        public async Task<List<User>> GetUsersItem(PaginationModel paginationModel)
        {
            var users = await userDataService.GetListUsers(paginationModel);
            return users;
        }

        public async Task<User> IsRegistration(string username, string password)
        {
            var register = await userDataService.IsRegistrationData(registerUser);

            return register;
        }

        public async Task<Role> ChangeUserRole(int userId, int roleId)
        {
            var user = await userDataService.GetUser(userId);

            if (user == null)
            {
                throw new NullReferenceException("User not found.");
            }

            var role = userDataService.GetRole(roleId);

            if (role == null)
            {
                throw new NullReferenceException("Role not found.");
            }

            await userDataService.SaveRole(user, role);

            return role;
        }
    }
}
