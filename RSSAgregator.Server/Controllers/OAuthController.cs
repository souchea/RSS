using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RSSAgregator.Server.APIAttribute;
using RSSAgregator.Server.Models;

namespace RSSAgregator.Server.Controllers
{

    [Authorize]
    public class OAuthController : ApiController
    {

        private ApplicationUserManager _userManager;

        public  OAuthController()
        { }

        public OAuthController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
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

        [HttpPost]
        [Scope("Register")]
        [Route("api/oauth/register")]
       public async Task<IHttpActionResult> Register(AccountBindingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model == null || model.Email == null || model.Password == null)
                return BadRequest("Parameters cannot be null");

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);
            
            if (!result.Succeeded)
                return BadRequest(result.Errors.Aggregate("", (current, error) => current + (error + ", ")));
            
            return Ok("ok");

        }

        [HttpPost]
        [Scope("isLogged")]
        [Route("api/oauth/delete")]
        public async Task<IHttpActionResult> Delete(AccountBindingModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model == null || model.Email == null || model.Password == null)
                return BadRequest("Parameters cannot be null");

          ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);

            if (user == null)
                return BadRequest("User not found");

            var result = await UserManager.DeleteAsync(user);

            if (!result.Succeeded)
              return BadRequest(result.Errors.Aggregate("", (current, error) => current + (error + ", ")));

            return Ok("ok");
        }


        [HttpPost]
        [Scope("isLogged")]
        [Route("api/oauth/changepassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (model == null || model.NewPassword == null || model.OldPassword == null)
                return BadRequest("Parameters cannot be null");

            if (User.Identity.GetUserId() == null)
                return BadRequest("Unknown User");

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);

            if (!result.Succeeded)
                return BadRequest(result.Errors.Aggregate("", (current, error) => current + (error + ", ")));

            return Ok("ok");
        }


    }
}