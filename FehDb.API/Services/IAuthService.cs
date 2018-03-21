using FehDb.API.Models.Entity.UserModel;
using FehDb.API.Models.Resource.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Services
{
    public interface IAuthService
    {
        JWTToken GenerateJwtToken(User userAccount);
        Task<User> CheckIfValidAccount(UserResource userEntry);
        Task CreateAccount(UserResource userEntry);
        Task ChangePassword(UserPasswordChangeResource userEntry);
    }
}
