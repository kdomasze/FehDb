using FehDb.API.Buisness;
using FehDb.API.Contexts;
using FehDb.API.Models.Entity.UserModel;
using FehDb.API.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FehDb.API.Services
{
    public class AuthService : IAuthService
    {
        private UserRepository _userRepository;
        private IConfiguration _configuration;

        public AuthService(FehContext context, IConfiguration Configuration)
        {
            _userRepository = new UserRepository(context);
            _configuration = Configuration;
        }

        public async Task<User> CheckIfValidAccount(string name, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(name);

            if (!AuthBusinessLogic.CheckWaitPeriod(user))
            {
                throw new InvalidOperationException("User must wait before attempting to login.");
            }

            if (user == null) throw new Exception("Invalid username or password.");

            if (!AuthBusinessLogic.CheckIfValidPassword(user, password, _configuration))
            {
                await _userRepository.MarkFailedLogin(user);
                throw new Exception("Invalid username or password.");
            }

            await _userRepository.MarkSuccessfulLogin(user);
            return user;
        }

        public async Task<bool> CreateAccount(string name, string password)
        {
            var user = new User()
            {
                Username = name,
                UserRole = UserRole.Admin,
                PasswordHash = AuthBusinessLogic.GetHash(name, password, _configuration)
            };

            await _userRepository.Insert(user);
            await _userRepository.SaveChanges();

            return true;
        }
    }
}
