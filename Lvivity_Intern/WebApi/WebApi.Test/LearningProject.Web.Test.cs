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
using System.Threading.Tasks;

namespace WebApi.Test
{
    [ExcludeFromCodeCoverage]
    public class LearningProjectWebTest
    {
        [Fact]
        public void AuthorizationCheckIsValidData_Test()
        {
            var userIdent = new User { Id = 3, UserName = "admin", Password = "admin1", RoleId = 1, Role = new Role { Name = "admin" } };
            var user = new LoginModel { UserName = "admin", Password = "admin1" };
            var mock = new Mock<IUserService>();

            mock.Setup(repo => repo.GetIdentity(userIdent)).Returns(GetUserIdentity());

            mock.Setup(repo => repo.GetUser(user)).Returns(async () => { return userIdent; });

            var securityController = new SecurityController(mock.Object);

            var result = securityController.Authorization(user);

            Assert.Equal("admin", result.Result.Username);
            Assert.Equal("admin", result.Result.Role.Name);
        }

        [Fact]
        public void EditCheckReturnTrue_Test()
        {
            User user = new User { Id = 2, UserName = "iVova", Password = "Apple1", RoleId = 2, Role = new Role { Name = "user" } };
            var editModel = new EditModel { Id = 2, Role = new Role { Name = "user" } };
            var mock = new Mock<IUserService>();

            mock.Setup(repo => repo.EditUser(editModel)).Returns(async() => { return user.Role.Name; });

            var editController = new EditController(mock.Object);

            var result = editController.Edit(editModel);

            Assert.Equal("user", result.Result);
        }

        [Fact]
        public void RegistrationCheckReturnTrue_Test()
        {
            var registrationModel = new Register { UserName = "volodya", Password = "volodya1", ConfirmPassword = "volodya1" };
            var mock = new Mock<IUserService>();

            mock.Setup(repo => repo.IsRegistration(registrationModel)).Returns(async() => { return true; });

            var registerController = new RegisterController(mock.Object);

            var result = registerController.Registration(registrationModel);

            Assert.True(result.Result);
        }

        [Fact]
        public void CreatePostCheckReturnTrue_Test()
        {
            var fileMock = new Mock<IFormFile>();

            var createPostModel = new CreatePostModel
            {
                Title = "Luxury interior in the design of apartments and houses",
                ShortDescription = "Designing premium interiors is a rejection of typical planning.",
                Image = fileMock.Object
            };

            var mock = new Mock<IPostService>();

            mock.Setup(repo => repo.IsCreateNewPost(createPostModel)).Returns(async() => { return true; });

            var createNewPostController = new CreatePostController(mock.Object);

            var result = createNewPostController.CreatePost(createPostModel);

            Assert.True(result.Result);
        }

        [Fact]
        public void PostsCheckReturnPosts_Test()
        {
            var paginationModel = new PaginationModel { CurrentPage = 1, PerPage = 1 };
            var mock = new Mock<IPostService>();

            mock.Setup(repo => repo.GetPosts(paginationModel)).Returns(GetTestPosts());

            var postsController = new PostController(mock.Object);

            var result = postsController.GetPost(paginationModel);

            Assert.Equal(2, result.Result.Count);
        }

        [Fact]
        public void UsersCheckRetunUsers_Test()
        {
            var paginationModel = new PaginationModel { CurrentPage = 1, PerPage = 3 };
            var mock = new Mock<IUserService>();

            mock.Setup(repo => repo.GetUsersItem(paginationModel)).Returns(GetTestUsers());

            var userController = new UserController(mock.Object);

            var result = userController.GetUsers(paginationModel);

            Assert.Equal(3, result.Result.Count);
        }

        private ClaimsIdentity GetUserIdentity()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, "admin"),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin")

            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Authorization", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        private async Task<List<Post>> GetTestPosts()
        {
            var posts = new List<Post>
            {
                new Post { Id=1, Title="Luxury interior in the design of apartments and houses", ShortDescription ="Designing premium interiors is a rejection of typical planning, finishing and decoration decisions " +
                "in favor of a unique environment that best meets the needs of the customer.", ImageUrl = "i.jpeg"},
                new Post { Id=2, Title="Luxury interior in the design of apartments and houses", ShortDescription ="Designing premium interiors is a rejection of typical planning, finishing and decoration decisions " +
                "in favor of a unique environment that best meets the needs of the customer.", ImageUrl = "i.jpeg"}

            };
            return posts;
        }

        private async Task<List<User>> GetTestUsers()
        {
            var users = new List<User>
            {
                new User { Id=1, UserName="iVova", Password ="Apple1", RoleId = 2, Role = new Role{Name = "user" } },
                new User { Id=2, UserName="Shasa", Password="shasha1", RoleId=2, Role = new Role{Name = "user" }},
                new User { Id =3, UserName = "admin", Password = "admin1", RoleId = 1,Role = new Role{Name = "admin" }  }
        };
            return users;
        }
    }
}
