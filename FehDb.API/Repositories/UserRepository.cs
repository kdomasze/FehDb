using FehDb.API.Buisness;
using FehDb.API.Contexts;
using FehDb.API.Extensions;
using FehDb.API.Models;
using FehDb.API.Models.Binding;
using FehDb.API.Models.Entity.UserModel;
using FehDb.API.Models.Entity.WeaponModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(FehContext context) : base(context) {}

        public async Task<IList<User>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<User> GetByUsernameAsync(string name)
        {
            return await _entities.SingleOrDefaultAsync(s => s.Username == name);
        }

        public async Task MarkFailedLogin(User user)
        {
            user.LoginAttempts++;
            user.LastLoginAttempt = DateTime.Now;

            _entities.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task MarkSuccessfulLogin(User user)
        {
            user.LoginAttempts = 0;
            user.LastLoginAttempt = DateTime.Now;

            _entities.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
