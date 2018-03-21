using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Models.Entity.UserModel
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public UserRole UserRole { get; set; }
        public int LoginAttempts { get; set; }
        public DateTime LastLoginAttempt { get; set; }
    }

    public enum UserRole
    {
        User, Admin
    }
}
