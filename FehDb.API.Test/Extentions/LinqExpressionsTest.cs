using FehDb.API.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FehDb.API.Test.Extentions
{
    [TestClass]
    public class LinqExpressionsTest
    {
        [TestMethod]
        public void WhereIfTest()
        {
            List<int> testDataList = new List<int>()
            {
                1, 2, 3, 4, 5
            };

            var testData = testDataList.AsQueryable();

            var trueTestNumbers = testData.WhereIf(true, x => x % 2 == 0);
            var falseTestNumbers = testData.WhereIf(false, x => x % 2 == 0);

            Assert.AreEqual(trueTestNumbers.Count(), 2);
            Assert.AreEqual(falseTestNumbers.Count(), 5);
        }
    }
}
