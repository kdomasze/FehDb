using FehDb.API.Business;
using FehDb.API.Models.Entity.UserModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FehDb.API.Test.Business
{
    [TestClass]
    public class AuthBusinessLogicTest
    {
        [TestMethod]
        public void CheckIfValidPasswordTest()
        {
            Mock<IConfigurationSection> configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(x => x["Secret"]).Returns("0");

            Mock<IConfiguration> configuration = new Mock<IConfiguration>();
            configuration.Setup(x => x["Auth:Secret"]).Returns(configurationSection.Object["Secret"]);
            
            var user = new User
            {
                Username = "1",
                PasswordHash = Encoding.Unicode.GetBytes("鷒灓㰝麅¹쫁⾎䌩频옶儉䗻ꆊ")
            };

            var password = "2";

            var result = AuthBusinessLogic.CheckIfValidPassword(user, password, configuration.Object);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetHashTest()
        {
            var user = new User
            {
                Username = "1"
            };

            var password = "2";

            var result = AuthBusinessLogic.GetHash(user.Username, password, "0");

            Assert.AreEqual("鷒灓㰝麅¹쫁⾎䌩频옶儉䗻ꆊ", Encoding.Unicode.GetString(result));
        }

        [TestMethod]
        public void GetHashConfigTest()
        {
            Mock<IConfigurationSection> configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(x => x["Secret"]).Returns("0");

            Mock<IConfiguration> configuration = new Mock<IConfiguration>();
            configuration.Setup(x => x["Auth:Secret"]).Returns(configurationSection.Object["Secret"]);
            
            var user = new User
            {
                Username = "1"
            };

            var password = "2";

            var result = AuthBusinessLogic.GetHash(user.Username, password, configuration.Object);

            Assert.AreEqual("鷒灓㰝麅¹쫁⾎䌩频옶儉䗻ꆊ", Encoding.Unicode.GetString(result));
        }

        [TestMethod]
        public void CompareByteArraysTest()
        {
            var testString = "123456789";
            var testBytes = Encoding.ASCII.GetBytes(testString);

            var badTestBytes = Encoding.ASCII.GetBytes(testString + "0");

            Assert.IsTrue(AuthBusinessLogic.CompareByteArrays(testBytes, testBytes));
            Assert.IsFalse(AuthBusinessLogic.CompareByteArrays(testBytes, badTestBytes));
            Assert.IsFalse(AuthBusinessLogic.CompareByteArrays(badTestBytes, testBytes));
        }

        [TestMethod]
        public void CheckWaitPeriodTest()
        {
            Mock<IConfigurationSection> configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(x => x["WaitTime"]).Returns("5");

            Mock<IConfiguration> configuration = new Mock<IConfiguration>();
            configuration.Setup(x => x["Auth:WaitTime"]).Returns(configurationSection.Object["WaitTime"]);

            var userNoFails = new User
            {
                LoginAttempts = 0
            };

            var user1FailWaitTimeGood = new User
            {
                LoginAttempts = 1,
                LastLoginAttempt = DateTime.MinValue
            };

            var user1FailWaitTimeBad = new User
            {
                LoginAttempts = 1,
                LastLoginAttempt = DateTime.Now
            };

            Assert.IsTrue(AuthBusinessLogic.CheckWaitPeriod(userNoFails, configuration.Object));
            Assert.IsTrue(AuthBusinessLogic.CheckWaitPeriod(user1FailWaitTimeGood, configuration.Object));
            Assert.IsFalse(AuthBusinessLogic.CheckWaitPeriod(user1FailWaitTimeBad, configuration.Object));
        }
    }
}
