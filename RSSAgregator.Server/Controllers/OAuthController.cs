using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.WebPages;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;

using RSSAgregator.Server.APIAttribute;
using RSSAgregator.Database.DataContext;
using RSSAgregator.Database.Manager;

namespace RSSAgregator.Server.Controllers
{
    [Authorize]
    public class OAuthController : ApiController
    {

        // GET api/Account/ExternalLogin
        //[OverrideAuthentication]
        //[HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]

        [HttpPost]
        [Scope("Register")]
        [Route("api/oauth/register")]
        public async Task<IHttpActionResult> Register()

        {
            var postValues = HttpContext.Current.Request.Form;

            if (!postValues.AllKeys.Contains("username") && !postValues.AllKeys.Contains("password")
                && postValues["username"].IsEmpty() && postValues["password"].IsEmpty())
                return NotFound();
            
            // user manager and register + check si il exist deja
            if (UserManager.GetAllUsers().Any(user => user.EmailAddress == postValues["username"]))
                return Ok("user exist");

           // var user = new User {EmailAddress = postValues["password"]};
           // UserManager.AddUser(user);

            return Ok();

        }

        [HttpPost]
        [Scope("isLogged")]
        [Route("api/oauth/delete")]
        public async Task<IHttpActionResult> Delete()
        {


            //var user = new User {EmailAddress = User.Identity.GetUserName()};
            //UserManager.DeleteUser(user);

            return Ok(User.Identity.GetUserName());
        }



    }
}