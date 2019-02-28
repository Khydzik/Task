using LearningProject.Data;
using LearningProject.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningProject.Application
{
    public class UserService:IUserService
    {
        private const string Defaultrole = "user";
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;

        public UserService(IRepository<User> userRepository, IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public async Task<User> GetUser(string username)
        {
            return await _userRepository.Query().Include(r => r.Role).FirstOrDefaultAsync(user => user.UserName == username);
        }        

        public async Task<List<User>> GetUsersItem(int skip, int take)
        {
            return await _userRepository.Query().Include(r => r.Role).Skip(skip-1).Take(take).ToListAsync();
        }

        public async Task<User> CreateUser(string username, string password)
        {
            User user = await _userRepository.GetAsync(u => u.UserName == username);

            if (user != null)
            {
                throw new Exception("Such user exist!");
            }

            Role role = await _roleRepository.GetAsync(r => r.Name == Defaultrole);

            var newuser = new User
            {
                UserName = username,
                Password = password
            };

            newuser.Role = role;

            return await _userRepository.InsertAsync(newuser);
        }

        public async Task<Role> ChangeUserRole(int userId, int roleId)
        {
            User user = await _userRepository.GetAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var role = await _roleRepository.GetAsync(r => r.Id == roleId);

            if (role == null)
            {
                throw new Exception("Role not found.");
            }

            user.RoleId = roleId;

            await _userRepository.UpdateAsync(user);

            return role;
        }
    }
}
