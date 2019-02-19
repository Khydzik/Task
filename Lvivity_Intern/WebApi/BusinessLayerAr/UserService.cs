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


        public async Task<User> GetUser(LoginModel data)
        {
            User user = await userDataService.GetUserLogin(data);

            if (user == null)
            {
                throw new NullReferenceException("Invalid username or password");
            }

            return user;
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

        public async Task<bool> IsRegistration(Register registerUser)
        {
            var register = await userDataService.IsRegistrationData(registerUser);

            return register;
        }

        public async Task<string> EditUser(EditModel model)
        {
            User user = await userDataService.GetUser(model.Id);

            if (user != null)
            {
                Role role = await userDataService.SaveRole(user, model);

                return role.Name;
            }
            else
            {
                throw new NullReferenceException("Not exist user");
            }
        }
    }
}
