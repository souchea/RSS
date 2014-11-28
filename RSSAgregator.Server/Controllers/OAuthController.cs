using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.WebPages;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using RSSAgregator.Server.APIAttribute;
using RSSAgregator.Server.Models;

namespace RSSAgregator.Server.Controllers
{
    [Authorize]
    public class OAuthController : ApiController
    {


        private ApplicationUserManager _userManager { get; set; }

        public OAuthController()
        {

        }


       public OAuthController(ApplicationUserManager userManager)
       {
           UserManager = userManager;
       }

       public ApplicationUserManager UserManager
       {
           get
           {
               return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
           }
           private set
           {
               _userManager = value;
           }
       }

        // GET api/Account/ExternalLogin
        //[OverrideAuthentication]
        //[HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]

        [HttpPost]
        [Scope("Register")]
        [Route("api/oauth/register")]


       public async Task<IHttpActionResult> Register(AccountBindingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);
            
            //revoir le renvoi des messages
            if (!result.Succeeded)
                return BadRequest(result.Errors.Aggregate("", (current, error) => current + (error + ", ")));
            
            return Ok("ok");

        }

        [HttpPost]
        [Scope("isLogged")]
        [Route("api/oauth/delete")]
        public async Task<IHttpActionResult> Delete(AccountBindingModel model)
        {

          ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);

            if (user == null)
                return BadRequest("User not found");

            var result = await UserManager.DeleteAsync(user);

            if (!result.Succeeded)
              return BadRequest(result.Errors.Aggregate("", (current, error) => current + (error + ", ")));

            return Ok("ok");
        }



    }
}