using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;

using RSSAgregator.Server.APIAttribute;

namespace RSSAgregator.Server.Controllers
{
    public class OAuthController : ApiController
    {

        // GET api/Account/ExternalLogin
        //[OverrideAuthentication]
        //[HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]

        [HttpPost]
        [Scope("Register")]
        public async Task<IHttpActionResult> Register([FromBody] string param)

    {



        return Ok("heheh");

    }
    }
}