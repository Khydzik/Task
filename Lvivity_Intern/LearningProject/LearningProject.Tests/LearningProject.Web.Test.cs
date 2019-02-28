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
using System.IO;
using LearningProject.Web.Token;

namespace LearningProject.Tests
{
    [ExcludeFromCodeCoverage]
    public class LearningProjectWebTest
    {
        [Fact]
        public void AuthorizationCheckIsReturnAuthorizationDto_Test()
        {
            var userIdent = new User { Id = 3, UserName = "admin", Password = "admin1", RoleId = 1, Role = new Role { Name = "admin" } };
            var user = new LoginModel { UserName = "admin", Password = "admin1" };
            string token = "";
            var mock1 = new Mock<IUserService>();
            var mock2 = new Mock<ITokenFormation>();

            mock2.Setup(repo => repo.GetToken(userIdent)).Returns(() => { return token; });
            mock1.Setup(repo => repo.GetUser(user.UserName)).Returns(async () => { return userIdent; });

            var securityController = new SecurityController(mock1.Object,mock2.Object);

            var result = securityController.Authorization(user);

            Assert.IsType<AuthorizationDto>(result.Result);
        }

        [Fact]
        public async Task AuthorizationCheckIsUserNull_Test()
        {
            User userIdent = null;
            var user = new LoginModel { UserName = "admin", Password = "admin1" };
            var mock1 = new Mock<IUserService>();
            var mock2 = new Mock<ITokenFormation>();
            
            mock1.Setup(repo => repo.GetUser(user.UserName)).Returns(async () => { return userIdent; });

            var securityController = new SecurityController(mock1.Object, mock2.Object);

            var result = securityController.Authorization(user);

            Exception ex = await Assert.ThrowsAsync<Exception>(() => securityController.Authorization(user));

            Assert.Equal("Invalid username or password", ex.Message);

        }

        [Fact]
        public async Task ChangeCheckReturnRoleNull_Test()
        {
            Role role = null;
            var editModel = new EditModel { userId = 2, roleId = 3 };
            var mock = new Mock<IUserService>();

            mock.Setup(repo => repo.ChangeUserRole(editModel.userId, editModel.roleId)).Returns( async() => { return role; });

            var editController = new UserEditController(mock.Object);

            Exception ex = await Assert.ThrowsAsync<Exception>(() => editController.ChangeRole(editModel));

            Assert.Equal("Role is not change!", ex.Message);
        }

        [Fact]
        public void ChangeCheckReturnRole_Test()
        {
            User user = new User { Id = 2, UserName = "iVova", Password = "Apple1", RoleId = 2, Role = new Role { Name = "user" } };
            var editModel = new EditModel { userId = 2, roleId = 1 };
            var mock = new Mock<IUserService>();

            mock.Setup(repo => repo.ChangeUserRole(editModel.userId, editModel.roleId)).Returns(async() => { return user.Role; });

            var editController = new UserEditController(mock.Object);

            var result = editController.ChangeRole(editModel);

            Assert.IsType<Role>(result.Result);
        }

        [Fact]
        public async Task CheckCreatePostNull_Test()
        {
            var fileMock = new Mock<IFormFile>();
            var mock = new Mock<IPostService>();
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            var fileName = "i.jpg";            
            writer.Write(fileName);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            
            var createPostModel = new CreatePostModel
            {
                Title = "Luxury interior in the design of apartments and houses",
                ShortDescription = "Designing premium interiors is a rejection of typical planning.",
                Image = fileMock.Object
            };         
           
            Post post = null;

            byte[] imageData = new byte[255];

            mock.Setup(repo => repo.CreatePost(createPostModel.Title, createPostModel.ShortDescription,
                createPostModel.Image.FileName, imageData)).Returns(async () => { return post; });

            var createNewPostController = new CreatePostController(mock.Object);

            Exception ex = await Assert.ThrowsAsync<Exception>(() => createNewPostController.CreatePost(createPostModel));

            Assert.Equal("Post not created!", ex.Message);
        }

        [Fact]
        public void CreatePostCheckReturnPost_Test()
        {
            var fileMock = new Mock<IFormFile>();
            
            var mock = new Mock<IPostService>();
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            var fileName = "i.jpg";
            writer.Write(fileName);
            writer.Flush();
            ms.Position = 0;

            var createPostModel = new CreatePostModel
            {
                Title = "Luxury interior in the design of apartments and houses",
                ShortDescription = "Designing premium interiors is a rejection of typical planning.",
                Image = fileMock.Object                
            };

            var postModel = new Post
            {
                Title = "Luxury interior in the design of apartments and houses",
                ShortDescription = "Designing premium interiors is a rejection of typical planning.",
                ImageUrl = "i.jpg"
            };

            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            mock.Setup(repo => repo.CreatePost(postModel.Title, postModel.ShortDescription, 
                postModel.ImageUrl, It.IsAny<byte[]>())).Returns(async ()=> { return postModel; });

            var createNewPostController = new CreatePostController(mock.Object);

            var result = createNewPostController.CreatePost(createPostModel);

            Assert.IsType<Post>(result.Result);
        }

        private async Task<Post> GetPostTest()
        {
            Post post = new Post
            {
                Id = 9,
                ImageUrl = "1.jpg",
                ShortDescription = "Designing premium interiors is a rejection of typical planning.",
                Title = "Luxury interior in the design of apartments and houses"
            };

            return post;
        }

        [Fact]
        public void RegistrationCheckReturnUser_Test()
        {
            var registrationModel = new Register { UserName = "volodya", Password = "volodya1", ConfirmPassword = "volodya1" };
            var mock = new Mock<IUserService>();
            User user = new User { Id = 1, UserName = "iVova", Password = "Apple1", RoleId = 2, Role = new Role { Name = "user" } };
            mock.Setup(repo => repo.CreateUser(registrationModel.UserName, registrationModel.Password)).Returns(async () => { return user; });

            var registerController = new RegistrationController(mock.Object);

            var result = registerController.Registration(registrationModel);

            Assert.IsType<User>(result.Result);
        }

        [Fact]
        public void PostsCheckReturnPosts_Test()
        {
            var paginationModel = new PaginationModel { Skip = 1, Take = 2 };
            var mock = new Mock<IPostService>();

            mock.Setup(repo => repo.GetPosts(paginationModel.Take, paginationModel.Skip)).Returns(GetTestPosts());

            var postsController = new PostsController(mock.Object);

            var result = postsController.GetPosts(paginationModel);

            Assert.Equal(2, result.Result.Count);
        }

        [Fact]
        public async Task PostsCheckReturnPostsNull_Test()
        {
            var paginationModel = new PaginationModel { Skip = 1, Take = 2 };
            var mock = new Mock<IPostService>();
            mock.Setup(repo => repo.GetPosts(paginationModel.Take, paginationModel.Skip)).Returns(async() => { return null; });

            var postsController = new PostsController(mock.Object);

            Exception ex = await Assert.ThrowsAsync<Exception>(() => postsController.GetPosts(paginationModel));

            Assert.Equal("No posts.", ex.Message);
        }


        [Fact]
        public void UsersCheckRetunUsers_Test()
        {
            var paginationModel = new PaginationModel { Skip = 1, Take = 3 };
            var mock = new Mock<IUserService>();

            mock.Setup(repo => repo.GetUsersItem(paginationModel.Skip, paginationModel.Take)).Returns(GetTestUsers());

            var userController = new UserController(mock.Object);

            var result = userController.GetUsers(paginationModel);

            Assert.Equal(3, result.Result.Count);
        }

        [Fact]
        public async Task UsersCheckReturnUsersNull_Test()
        {
            var paginationModel = new PaginationModel { Skip = 1, Take = 2 };
            var mock = new Mock<IUserService>();

            mock.Setup(repo => repo.GetUsersItem(paginationModel.Take, paginationModel.Skip)).Returns(() => { return null; });

            var userController = new UserController(mock.Object);

            Exception ex = await Assert.ThrowsAsync<Exception>(() => userController.GetUsers(paginationModel));

            Assert.Equal("No users.", ex.Message);
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
