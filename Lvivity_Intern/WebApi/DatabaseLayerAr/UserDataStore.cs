using LearningProject.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningProject.Data
{
    public class UserDataStore:IUserDataService
    {
        private readonly DataBaseContext _context;

        public UserDataStore(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<Role> GetRole(string role)
        {
            Role roleName = await _context.Roles.FirstOrDefaultAsync(r => r.Name == role);
            return roleName;
        }

        public async Task<User> GetUser(int id)
        {
            User user = await _context.Users.FirstOrDefaultAsync(r => r.Id == id);
            return user;
        }

        public async Task<User> GetUserLogin(LoginModel data)
        {
            User user = await _context.Users.Include(u =>
            u.Role).FirstOrDefaultAsync(x =>
            x.UserName == data.UserName && x.Password == data.Password);

            return user;
        }

        public async Task<List<User>> GetListUsers(PaginationModel model)
        {
            var users = await _context.Users.Include(x => x.Role.Name).Select(user => new User
            {
                Id = user.Id,
                UserName = user.UserName,
                Role = user.Role
            })
                .Skip((model.CurrentPage - 1) * model.PerPage)
                .Take(model.PerPage)
                .ToListAsync();

            return users;
        }

        public async Task<Role> SaveRole(User userRole, EditModel editRole)
        {
            userRole.Role = editRole.Role;
            await _context.SaveChangesAsync();
            return userRole.Role;
        }

        public async Task<bool> IsRegistrationData(Register registerUser)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == registerUser.UserName);

            if (user != null)
            {
                throw new Exception("Such user exist");
            }

            Role role = await _context.Roles.FirstOrDefaultAsync(t => t.Name == "user");

            user = new User
            {
                UserName = registerUser.UserName,
                Password = registerUser.Password
            };

            user.Role = role;

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
