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

namespace FehDb.API.Business
{
    public class AuthBusinessLogic
    {
        public static bool CheckIfValidPassword(User user, string password, IConfiguration _configuration)
        {
            var saltedHash = GetHash(user.Username, password, _configuration["Auth:Secret"]);

            var hashString = Encoding.Unicode.GetString(saltedHash);

            if (!CompareByteArrays(saltedHash, user.PasswordHash)) return false;

            return true;
        }

        public static byte[] GetHash(string username, string password, string secret)
        {
            return GenerateSaltedHash(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(username + secret));
        }

        public static byte[] GetHash(string username, string password, IConfiguration _configuration)
        {
            var secret = _configuration["Auth:Secret"];

            return GetHash(username, password, secret);
        }

        public static bool CheckWaitPeriod(User user, IConfiguration _configuration)
        {
            if (user.LoginAttempts == 0) return true;

            var currentTime = DateTime.Now;
            float waitTimePerFailedLogin = float.Parse(_configuration["Auth:WaitTime"]);

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

        public static bool CompareByteArrays(byte[] array1, byte[] array2)
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
