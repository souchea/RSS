using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using RSSAgregator.Server.Models;
using RSSAgregator.Server.Controllers;

namespace RSSAgregator.Server.Tests.Controllers
{
    [TestClass]
    public class OAuthControllerTest
    {

        [TestMethod]
        public void Register()
        {

            var controller = InitController();


            CancellationTokenSource cs = new CancellationTokenSource();
            CancellationToken t = cs.Token;


            // Test 1
            var model = new AccountBindingModel {Email = "tata@titi.com", Password = "Titi123)"};
            var result = controller.Register(model);
            var response = result.Result.ExecuteAsync(t).Result;
            var code = response.StatusCode;
            
            // if user already exists BadRequest else OK
            Assert.IsTrue(HttpStatusCode.OK == code || HttpStatusCode.BadRequest == code);


            // Test 2
             model = new AccountBindingModel { Email = "tata@titi.com", Password = "" };
             result = controller.Register(model);
             response = result.Result.ExecuteAsync(t).Result;
             code = response.StatusCode;

             Assert.AreEqual(HttpStatusCode.BadRequest, code);

             // Test 3
             model = new AccountBindingModel { Email = "", Password = "" };
             result = controller.Register(model);
             response = result.Result.ExecuteAsync(t).Result;
             code = response.StatusCode;

             Assert.AreEqual(HttpStatusCode.BadRequest, code);

             // Test 4
             model = new AccountBindingModel { Email = "", Password = "toto" };
             result = controller.Register(model);
             response = result.Result.ExecuteAsync(t).Result;
             code = response.StatusCode;

             Assert.AreEqual(HttpStatusCode.BadRequest, code);

             // Test 5
             model = new AccountBindingModel {Email = "dd"};
             result = controller.Register(model);
             response = result.Result.ExecuteAsync(t).Result;
             code = response.StatusCode;

             Assert.AreEqual(HttpStatusCode.BadRequest, code);
             var value = response.Content.ReadAsStringAsync().Result;
             Assert.IsTrue(value.Contains("Parameters cannot be null"));

             // Test 6
             model = new AccountBindingModel { Password = "dd" };
             result = controller.Register(model);
             response = result.Result.ExecuteAsync(t).Result;
             code = response.StatusCode;

             Assert.AreEqual(HttpStatusCode.BadRequest, code);
             value = response.Content.ReadAsStringAsync().Result;
             Assert.IsTrue(value.Contains("Parameters cannot be null"));

             // Test 7
             result = controller.Register(null);
             response = result.Result.ExecuteAsync(t).Result;
             code = response.StatusCode;

             Assert.AreEqual(HttpStatusCode.BadRequest, code);
             value = response.Content.ReadAsStringAsync().Result;
             Assert.IsTrue(value.Contains("Parameters cannot be null"));
        }


        [TestMethod]
        public void Delete()
        {

            var controller = InitController();

            CancellationTokenSource cs = new CancellationTokenSource();
            CancellationToken t = cs.Token;


            // Test 1
            var model = new AccountBindingModel { Email = "tata@titi.com", Password = "Titi1334)" };
            var result = controller.Delete(model);
            var response = result.Result.ExecuteAsync(t).Result;
            var code = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.BadRequest, code);

            // Test 2
            model = new AccountBindingModel { Email = "tata@titi.com", Password = "Titi123)" };
            result = controller.Delete(model);
            response = result.Result.ExecuteAsync(t).Result;
            code = response.StatusCode;

            // if user was deleted BadRequest else OK
            Assert.IsTrue(HttpStatusCode.OK == code || HttpStatusCode.BadRequest == code);

           
            // Test 2
            model = new AccountBindingModel { Email = "tata@titi.com", Password = "" };
            result = controller.Delete(model);
            response = result.Result.ExecuteAsync(t).Result;
            code = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.BadRequest, code);

            // Test 3
            model = new AccountBindingModel { Email = "", Password = "" };
            result = controller.Delete(model);
            response = result.Result.ExecuteAsync(t).Result;
            code = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.BadRequest, code);

            // Test 4
            model = new AccountBindingModel { Email = "", Password = "toto" };
            result = controller.Delete(model);
            response = result.Result.ExecuteAsync(t).Result;
            code = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.BadRequest, code);

            // Test 5
            model = new AccountBindingModel { Email = "dd" };
            result = controller.Delete(model);
            response = result.Result.ExecuteAsync(t).Result;
            code = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.BadRequest, code);
            var value = response.Content.ReadAsStringAsync().Result;
            Assert.IsTrue(value.Contains("Parameters cannot be null"));

            // Test 6
            model = new AccountBindingModel { Password = "dd" };
            result = controller.Delete(model);
            response = result.Result.ExecuteAsync(t).Result;
            code = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.BadRequest, code);
            value = response.Content.ReadAsStringAsync().Result;
            Assert.IsTrue(value.Contains("Parameters cannot be null"));

            // Test 7
            result = controller.Delete(null);
            response = result.Result.ExecuteAsync(t).Result;
            code = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.BadRequest, code);
            value = response.Content.ReadAsStringAsync().Result;
            Assert.IsTrue(value.Contains("Parameters cannot be null"));


        }


        [TestMethod]
        public void ChangePassword()
        {
            var controller = InitController();


            CancellationTokenSource cs = new CancellationTokenSource();
            CancellationToken t = cs.Token;


            // Test 1
            var result = controller.ChangePassword(null);
            var response = result.Result.ExecuteAsync(t).Result;
            var code = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.BadRequest, code);
            var value = response.Content.ReadAsStringAsync().Result;
            Assert.IsTrue(value.Contains("Parameters cannot be null"));


            // Test 2
            var model = new ChangePasswordBindingModel { NewPassword = "dd" };
            result = controller.ChangePassword(model);
            response = result.Result.ExecuteAsync(t).Result;
            code = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.BadRequest, code);
            value = response.Content.ReadAsStringAsync().Result;
            Assert.IsTrue(value.Contains("Parameters cannot be null"));


            //Test 3 
            model = new ChangePasswordBindingModel { OldPassword = "dd" };
            result = controller.ChangePassword(model);
            response = result.Result.ExecuteAsync(t).Result;
            code = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.BadRequest, code);
            value = response.Content.ReadAsStringAsync().Result;
            Assert.IsTrue(value.Contains("Parameters cannot be null"));


            // Test 4
            model = new ChangePasswordBindingModel { NewPassword = "dd", OldPassword = ""};
            result = controller.ChangePassword(model);
            response = result.Result.ExecuteAsync(t).Result;
            code = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.BadRequest, code);

            // Test 5
            model = new ChangePasswordBindingModel { NewPassword = "", OldPassword = "dd" };
            result = controller.ChangePassword(model);
            response = result.Result.ExecuteAsync(t).Result;
            code = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.BadRequest, code);

            // Test 6
            model = new ChangePasswordBindingModel { NewPassword = "", OldPassword = "" };
            result = controller.ChangePassword(model);
            response = result.Result.ExecuteAsync(t).Result;
            code = response.StatusCode;

            Assert.AreEqual(HttpStatusCode.BadRequest, code);

            //test 7
            model = new ChangePasswordBindingModel { NewPassword = "Titi1232)", OldPassword = "Titi123)" };
            result = controller.ChangePassword(model);
            response = result.Result.ExecuteAsync(t).Result;
            code = response.StatusCode;

            // if unknown user BadRequest else OK
            Assert.IsTrue(HttpStatusCode.OK == code || HttpStatusCode.BadRequest == code);

        }


        private OAuthController InitController()
        {
            var controller = new OAuthController(new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext())));
            controller.Request = new HttpRequestMessage();
            controller.Request.SetOwinContext(new OwinContext());
            controller.Configuration = new HttpConfiguration();

            return controller;
        }

    }
}
