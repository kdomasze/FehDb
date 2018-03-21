using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Models.Resource.UserModel
{
    public class User : BaseResource
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
