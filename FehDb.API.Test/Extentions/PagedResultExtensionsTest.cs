using FehDb.API.Extensions;
using FehDb.API.Models.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FehDb.API.Models;
using Moq;
using FehDb.API.Contexts;
using Microsoft.EntityFrameworkCore;
using FehDb.API.Models.Entity.WeaponModel;
using FehDb.API.Test.Helpers;
using FehDb.API.Models.Resource.WeaponModel;
using AutoMapper;

namespace FehDb.API.Test.Extentions
{
    [TestClass]
    public class PagedResultExtensionsTest
    {
        [TestMethod]
        public async Task GetPagedTest()
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

            var entity = mockContext.Object.Set<Weapon>();

            PagedResult<Weapon> pagedTestData = await entity.GetPaged(0, 2);

            Assert.AreEqual(pagedTestData.Results.Count, 2, "1");
            Assert.IsNotNull(pagedTestData.Results.SingleOrDefault(x => x.ID == 1), "2");
            Assert.IsNotNull(pagedTestData.Results.SingleOrDefault(x => x.ID == 2), "3");
            Assert.IsNull(pagedTestData.Results.SingleOrDefault(x => x.ID == 3), "4");
        }
    }
}
