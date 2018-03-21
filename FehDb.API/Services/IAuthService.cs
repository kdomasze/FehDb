using FehDb.API.Models.Entity.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Services
{
    public interface IAuthService
    {
        Task<User> CheckIfValidAccount(string name, string password);
        Task<bool> CreateAccount(string name, string password);
    }
}
