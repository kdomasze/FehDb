using FehDb.API.Contexts;
using FehDb.API.Extensions;
using FehDb.API.Models.Entity.WeaponModel;
using FehDb.API.Repositories;
using FehDb.API.Test.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Test.Extentions
{
    [TestClass]
    public class BaseRepositoryTest
    {
        [TestMethod]
        public void GetAllTest()
        {
            var data = new List<Weapon>()
            {
                new Weapon
                {
                    ID = 1
                },
                new Weapon
                {
                    ID = 2
                },
                new Weapon
                {
                    ID = 3
                },
                new Weapon
                {
                    ID = 4
                },
                new Weapon
                {
                    ID = 5
                }
            };

            var mockContext = DbSetMocking.GetFehContextMock<Weapon, FehContext>(data);

            var repo = new BaseRepository<Weapon>(mockContext.Object);

            var result = repo.GetAll(new Models.Binding.Query(), new Models.Binding.BaseFilter());

            Assert.AreEqual(result.Results.Count, 5);
        }

        [TestMethod]
        public void GetById()
        {
            var data = new List<Weapon>()
            {
                new Weapon
                {
                    ID = 1
                },
                new Weapon
                {
                    ID = 2
                },
                new Weapon
                {
                    ID = 3
                },
                new Weapon
                {
                    ID = 4
                },
                new Weapon
                {
                    ID = 5
                }
            };

            var mockContext = DbSetMocking.GetFehContextMock<Weapon, FehContext>(data);

            var repo = new BaseRepository<Weapon>(mockContext.Object);

            var result = repo.GetById(2);

            Assert.AreEqual(result.ID, 2);
        }

        [TestMethod]
        public void Insert()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Update()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Delete()
        {
            Assert.Fail();
        }
    }
}
