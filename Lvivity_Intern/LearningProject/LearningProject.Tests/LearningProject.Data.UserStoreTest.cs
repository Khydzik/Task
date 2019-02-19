using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;
using LearningProject.Data.Models;
using LearningProject.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LearningProject.Tests
{
    [ExcludeFromCodeCoverage]
    public class LearningProjectDataUserStoreTest
    {
        private readonly UserDataStore _userDataStore;
        
        public LearningProjectDataUserStoreTest()
        {
            _userDataStore = DataStoreConfig();
        }

        [Fact]
        public void GetRoleCheckReturnRole_Test()
        {
            string role = "user";

            var result = _userDataStore.GetRole(role);
            Assert.Equal("user", result.Result.Name);
        }

        [Fact]
        public void GetUserCheckReturnUser_Test()
        {
            int Id = 3;
            var result = _userDataStore.GetUser(Id);

            Assert.IsType<User>(result.Result);
        }

        [Fact]
        public void GetUserCheckReturnUserNull_Test()
        {
            int Id = 10;
           
            var result = _userDataStore.GetUser(Id);

            Assert.Null(result.Result);
        }

        [Fact]
        public void GetUserLoginCheckReturnUser_Test()
        {
            LoginModel model = new LoginModel { UserName = "admin", Password = "admin1" };

            var result = _userDataStore.GetUserLogin(model);

            Assert.IsType<User>(result.Result);
        }

        [Fact]
        public void GetUserLoginCheckReturnNull_Test()
        {
            LoginModel model = new LoginModel { UserName = "user", Password = "user1" };

            var result = _userDataStore.GetUserLogin(model);

            Assert.Null(result.Result);
        }

        [Fact]
        public void GetListUser_Test()
        {
            var paginationModel = new PaginationModel { CurrentPage = 1, PerPage = 2 };

            var result = _userDataStore.GetListUsers(paginationModel);

            Assert.Equal(2, result.Result.Count);
        }

        

        [Fact]
        public async Task RegistrationCheckUserNotNull_Test()
        {
            var registrationModel = new Register { UserName = "volodya", Password = "volodya1", ConfirmPassword = "volodya1" };

            Exception ex = await Assert.ThrowsAsync<Exception>(() => _userDataStore.IsRegistrationData(registrationModel));

            Assert.Equal("Such user exist", ex.Message);
        }

        [Fact]
        public void RegistrationCheckReturnTrue_Test()
        {
            var registrationModel = new Register { UserName = "ivan", Password = "ivan1", ConfirmPassword = "ivan1" };

            var result = _userDataStore.IsRegistrationData(registrationModel);

            Assert.True(result.Result);
        }        

        private UserDataStore DataStoreConfig()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "ReturnRole_From_Database")
                .Options;

            var context = new DataBaseContext(options);

            var userDataStore = new UserDataStore(context);

            return userDataStore;
        }

    }
}
