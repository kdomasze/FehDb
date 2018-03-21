using FehDb.API.Extensions;
using FehDb.API.Models.Binding;
using FehDb.API.Models.Entity.UserModel;
using FehDb.API.Models.Entity.WeaponModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FehDb.API.Buisness
{
    public class AuthBusinessLogic
    {
        public static bool CheckIfValidPassword(User user, string password, IConfiguration _configuration)
        {
            var saltedHash = GenerateSaltedHash(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(user.Username + _configuration["Auth:Secret"]));

            if (!CompareByteArrays(saltedHash, user.PasswordHash)) return false;

            return true;
        }

        public static byte[] GetHash(string username, string password, IConfiguration _configuration)
        {
            return GenerateSaltedHash(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(username + _configuration["Auth:Secret"]));
        }

        public static bool CheckWaitPeriod(User user)
        {
            var currentTime = DateTime.Now;
            float waitTimePerFailedLogin = 5;

            float waitTime = waitTimePerFailedLogin * user.LoginAttempts;
            var secondsSinceLastLogin = (currentTime - user.LastLoginAttempt).Seconds;

            if (secondsSinceLastLogin > waitTime) return true;
            return false;
        }

        private static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] plainTextWithSaltBytes = new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }

            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }

        private static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
