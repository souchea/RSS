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

        }

        [TestMethod]
        public void TestGetCategories()
        {

        }

        [TestMethod]
        public void TestGetTokenLogin()
        {
            var service = new WebApiServiceManager();

            var result = service.GetTokenLoginAsync("souchet.aurelien@gmail.com", "]");

            result.Wait();

            //Assert.IsTrue(result.Result);
        }
    }
}
