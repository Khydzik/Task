using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;
using Moq;
using LearningProject.Application;
using LearningProject.Data.Models;
using LearningProject.Web.Controllers;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using LearningProject.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WebApi.Test
{
    [ExcludeFromCodeCoverage]
    public class LearningProjectApplicationUserServiceTest
    {
        [Fact]
        public async Task GetUserCheckIsNullUser_Test()
        {
            var user = new LoginModel { UserName = "qwer", Password = "qwer1" };
            var mock = new Mock<IUserDataService>();
            User userNull = null;
            mock.Setup(repo => repo.GetUserLogin(user)).Returns(async () => { return userNull; });

            var userService = new UserService(mock.Object);

            Exception ex = await Assert.ThrowsAsync<NullReferenceException>(() => userService.GetUser(user));

            Assert.Equal("Invalid username or password", ex.Message);
        }

        [Fact]
        public void RegistrationCheckReturnTrue_Test()
        {
            var registrationModel = new Register { UserName = "volodya", Password = "volodya1", ConfirmPassword = "volodya1" };
            var mock = new Mock<IUserDataService>();

            mock.Setup(repo => repo.IsRegistrationData(registrationModel)).Returns(async() => { return true; });

            var userService = new UserService(mock.Object);

            var result = userService.IsRegistration(registrationModel);

            Assert.True(result.Result);
        }

        [Fact]
        public void GetUserCheckReturnUser_Test()
        {
            var user = new LoginModel { UserName = "admin", Password = "admin1" };
            var mock = new Mock<IUserDataService>();
            mock.Setup(repo => repo.GetUserLogin(user)).Returns(GetTestUser());
            var userService = new UserService(mock.Object);

            var result = userService.GetUser(user);

            Assert.IsType<User>(result.Result);
        }
      
        [Fact]
        public void GetUserCheckReturnUsers_Test()
        {
            var paginationModel = new PaginationModel { CurrentPage = 1, PerPage = 3 };

            var mock = new Mock<IUserDataService>();

            mock.Setup(repo => repo.GetListUsers(paginationModel)).Returns(GetUsers());

            var userService = new UserService(mock.Object);

            var result = userService.GetUsersItem(paginationModel);

            Assert.Equal(3, result.Result.Count);
        }

        [Fact]
        public void GetIdentityCheckReturnClaims_Test()
        {
            var userModel = new User { Id = 3, UserName = "admin", Password = "admin1", RoleId = 1, Role = new Role { Name = "admin" } };

            var mock = new Mock<IUserDataService>();

            var userService = new UserService(mock.Object);

            var result = userService.GetIdentity(userModel);

            Assert.IsType<ClaimsIdentity>(result);
        }

        [Fact]
        public void EditUserCheckReturnRole_Test()
        {
            var editModel = new EditModel { Id = 3, Role = new Role { Name = "user" } };
            User user = new User { Id = 3, UserName = "admin", Password = "admin1", RoleId = 1, Role = new Role { Name = "admin" }};
            var mock = new Mock<IUserDataService>();

            mock.Setup(repo => repo.SaveRole(user, editModel)).Returns(async () =>
            {
                user.Role.Name = editModel.Role.Name;
                return user.Role;
            });

            mock.Setup(repo => repo.GetUser(editModel.Id)).Returns(async()=> { return user; });

            var userService = new UserService(mock.Object);

            var result = userService.EditUser(editModel);

            Assert.Equal("user", result.Result);
        }

        [Fact]
        public async Task EditUserCheckIsNullUser_Test()
        {
            var editModel = new EditModel { Id = 10, Role = new Role { Name = "user" } };
            var mock = new Mock<IUserDataService>();
            User user = null;
            mock.Setup(repo => repo.GetUser(editModel.Id)).Returns(async ()=> { return user; });

            var userService = new UserService(mock.Object);

            Exception ex = await Assert.ThrowsAsync<NullReferenceException>(() => userService.EditUser(editModel));

            Assert.Equal("Not exist user", ex.Message);
        }
        
        private async Task<List<User>> GetUsers()
        {
            var users = new List<User>
            {
                new User { Id=1, UserName="iVova", Password ="Apple1", RoleId = 2, Role = new Role{Name = "user" } },
                new User { Id=2, UserName="Shasa", Password="shasha1", RoleId=2, Role = new Role{Name = "user" }},
                new User { Id =3, UserName = "admin", Password = "admin1", RoleId = 1,Role = new Role{Name = "admin" }  }
            };
            
            return users;
        }

        private async Task<User> GetTestUser()
        {
            var users = new User { Id = 1, UserName = "iVova", Password = "Apple1", RoleId = 2, Role = new Role { Name = "user" } };
           
            return users;
        }
    }
}
