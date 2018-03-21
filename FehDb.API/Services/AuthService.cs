using FehDb.API.Buisness;
using FehDb.API.Contexts;
using FehDb.API.Models.Entity.UserModel;
using FehDb.API.Models.Resource.UserModel;
using FehDb.API.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

        public JWTToken GenerateJwtToken(User userAccount)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userAccount.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], //issued by
                _configuration["Jwt:Audience"], //issued for
                claims, //payload
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpireTime"])), // valid for 1/2 hour
                signingCredentials: creds); // signature

            var tokenEncoded = new JwtSecurityTokenHandler().WriteToken(token);

            return new JWTToken()
            {
                Token = tokenEncoded
            };
        }

        public async Task<User> CheckIfValidAccount(UserResource userEntry)
        {
            var user = await _userRepository.GetByUsernameAsync(userEntry.Username);

            if (!AuthBusinessLogic.CheckWaitPeriod(user, _configuration))
            {
                throw new InvalidOperationException("User must wait before attempting to login.");
            }

            if (user == null) throw new Exception("Invalid username or password.");

            if (!AuthBusinessLogic.CheckIfValidPassword(user, userEntry.Password, _configuration))
            {
                _userRepository.MarkFailedLogin(user);
                await _userRepository.SaveChanges();
                throw new Exception("Invalid username or password.");
            }

            _userRepository.MarkSuccessfulLogin(user);
            await _userRepository.SaveChanges();
            return user;
        }

        public async Task CreateAccount(UserResource userEntry)
        {
            var user = new User()
            {
                Username = userEntry.Username,
                UserRole = UserRole.Admin,
                PasswordHash = AuthBusinessLogic.GetHash(userEntry.Username, userEntry.Password, _configuration)
            };

            await _userRepository.Insert(user);
            await _userRepository.SaveChanges();
        }

        public async Task ChangePassword(UserPasswordChangeResource userEntry)
        {
            var user = await _userRepository.GetByUsernameAsync(userEntry.Username);

            if (!AuthBusinessLogic.CheckWaitPeriod(user, _configuration))
            {
                throw new InvalidOperationException("User must wait before attempting to login.");
            }

            if (user == null) throw new Exception("Invalid username or password.");

            if (!AuthBusinessLogic.CheckIfValidPassword(user, userEntry.Password, _configuration))
            {
                _userRepository.MarkFailedLogin(user);
                await _userRepository.SaveChanges();
                throw new Exception("Invalid username or password.");
            }

            user.PasswordHash = AuthBusinessLogic.GetHash(userEntry.Username, userEntry.NewPassword, _configuration);

            await _userRepository.Update(user);
            _userRepository.MarkSuccessfulLogin(user);
            await _userRepository.SaveChanges();
        }
    }
}
