using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSSAgregator.Shared.Common;

namespace RSSAgregator.Client.Tests
{
    [TestClass]
    public class WebApiServiceTest
    {
        [TestMethod]
        public void TestAddCategory()
        {
            var service = new WebApiServiceManager();

            var result = service.AddCategoryAsync(3, "new category").Result;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestGetCategories()
        {
            var service = new WebApiServiceManager();

            var result = service.GetCategoriesAsync(3).Result;

            Assert.IsNotNull(result);
        }
    }
}
