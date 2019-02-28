using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;
using Moq;
using LearningProject.Application;
using LearningProject.Data.Models;
using System.Threading.Tasks;
using LearningProject.Data;
using System.Linq;

namespace LearningProject.Tests
{
    [ExcludeFromCodeCoverage]
    public class LearningProjectApplicationUserServiceTest
    {
        //[Fact]
        //public async Task GetUserCheckReturnUser_Test()
        //{
        //    string username = "KhYDZik";
        //    var mock1 = new Mock<IRepository<User>>();
        //    var mock2 = new Mock<IRepository<Role>>();

        //    List<User> users = new List<User>
        //    {
        //        new User{Id = 3, UserName = "KhYDZik", Role = new Role{ Id = 2, Name = "user" }, RoleId = 2 }
        //    };

        //    IQueryable<User> queryable = users.AsQueryable();
        //    mock1.Setup(repo => repo.Query()).Returns(() => { return queryable; });

        //    var userService = new UserService(mock1.Object, mock2.Object);

        //    var result = await userService.GetUser(username);

        //    Assert.IsType<User>(result);
        //}

        [Fact]
        public async Task CreateUserCheckReturnUserExist_Test()
        {
            var mock1 = new Mock<IRepository<User>>();
            var mock2 = new Mock<IRepository<Role>>();
            User user = new User
            {
                UserName = "volodya",
                Password = "volodya1",
                Id = 3,
                RoleId = 1,
                Role = new Role { Id = 1, Name = "admin" }
            };

            mock1.Setup(repo => repo.GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>())).Returns(async() => { return user; });

            var userService = new UserService(mock1.Object, mock2.Object);

            Exception ex = await Assert.ThrowsAsync<Exception>(() => userService.CreateUser(user.UserName, user.Password));

            Assert.Equal("Such user exist!", ex.Message);
        }

        [Fact]
        public async Task CreateUserCheckReturnUser_Test()
        {
            User inputUser = new User { UserName = "volodya", Password = "volodya1" };
            var mock1 = new Mock<IRepository<User>>();
            var mock2 = new Mock<IRepository<Role>>();
            Role DefaultRole = new Role { Name = "user" };
            User user = null;

            mock1.Setup(repo => repo.InsertAsync(It.IsAny<User>())).Returns(async () => { return inputUser; });
            mock2.Setup(repo => repo.GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Role, bool>>>())).Returns(async () => { return DefaultRole; });
            mock1.Setup(repo => repo.GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>())).Returns( async () => { return user; });

            var userService = new UserService(mock1.Object, mock2.Object);

            var result = await userService.CreateUser(inputUser.UserName, inputUser.Password);

            Assert.IsType<User>(result);
        }

        //[Fact]
        //public void GetUserCheckReturnUsersItem_Test()
        //{
        //    var mock1 = new Mock<IRepository<User>>();
        //    var mock2 = new Mock<IRepository<Role>>();
        //    int Skip = 1;
        //    int Take = 2;
            
        //    mock1.Setup(repo => repo.Query()).Returns(GetUsers().AsQueryable);

        //    var userService = new UserService(mock1.Object, mock2.Object);

        //    var result = userService.GetUsersItem(Skip, Take);

        //    Assert.Equal(3, result.Result.Count);
        //}

        [Fact]
        public void ChangeUserRoleCheckReturnRole_Test()
        {
            var mock1 = new Mock<IRepository<User>>();
            var mock2 = new Mock<IRepository<Role>>();
            User user = new User
            {
                UserName = "volodya",
                Password = "volodya1",
                Id = 3,
                RoleId = 1,
                Role = new Role { Id = 1, Name = "admin" }
            };

            Role role = new Role { Id = 2, Name = "user" };
           
            mock1.Setup(repo => repo.Update(user)).Returns(() => { return user; });
            mock2.Setup(repo => repo.GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Role, bool>>>())).Returns(async() => { return role; });
            mock1.Setup(repo => repo.GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>())).Returns(async () => { return user; });  

            var userService = new UserService(mock1.Object, mock2.Object);

            var result = userService.ChangeUserRole(user.Id, role.Id);

            Assert.IsType<Role>(result.Result);
        }

        [Fact]
        public async Task ChangeUserRoleCheckIsNullUser_Test()
        {
            int userId = 90;
            int roleId = 2;
            var mock1 = new Mock<IRepository<User>>();
            var mock2 = new Mock<IRepository<Role>>();
            User user = null;

            mock1.Setup(repo => repo.GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>())).Returns(async () => { return user; });           

            var userService = new UserService(mock1.Object,mock2.Object);

            Exception ex = await Assert.ThrowsAsync<Exception>(() => userService.ChangeUserRole(userId, roleId));

            Assert.Equal("User not found.", ex.Message);
        }

        [Fact]
        public async Task ChangeUserRoleCheckIsNullRole_Test()
        {
            int roleId = 3;
            var mock1 = new Mock<IRepository<User>>();
            var mock2 = new Mock<IRepository<Role>>();

            User user = new User { Id = 3, UserName = "volodya", Password = "volodya1" };
            
            Role role = null;

            mock2.Setup(repo => repo.GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Role, bool>>>())).
                Returns(async() => { return role; });
            mock1.Setup(repo => repo.GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>())).Returns(async ()=> { return user; });

            var userService = new UserService(mock1.Object, mock2.Object);

            Exception ex = await Assert.ThrowsAsync<Exception>(() => userService.ChangeUserRole(user.Id, roleId));

            Assert.Equal("Role not found.", ex.Message);
        }

        private List<User> GetUsers()
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
