using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.WebPages;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using RSSAgregator.Server.Models;

namespace RSSAgregator.Server.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private string _clientId;
        private string _clientSecret;

        public ApplicationOAuthProvider(string clientId, string clientSecret)
        {
            if (clientId == null)
                throw new ArgumentNullException("clientId");
            if (clientSecret == null)
                throw new ArgumentNullException("clientSecret");
            
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            if (context.UserName.IsEmpty() || context.Password.IsEmpty())
            {
                context.SetError("error_parameter", "The user name or password cannot be null.");
                return;
            }

            ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
               OAuthDefaults.AuthenticationType);
            oAuthIdentity.AddClaim(new Claim("scope", "isLogged"));

            ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
               CookieAuthenticationDefaults.AuthenticationType);

            AuthenticationProperties properties = CreateProperties(user.UserName);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }


        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {

            var oauthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            oauthIdentity.AddClaim(new Claim("scope", "Register"));
            var ticket = new AuthenticationTicket(oauthIdentity, new AuthenticationProperties());
            context.Validated(ticket);
            return base.GrantClientCredentials(context);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;

            if ((context.TryGetFormCredentials(out clientId, out clientSecret) ||
                context.TryGetBasicCredentials(out clientId, out clientSecret)) && clientId == _clientId
                && clientSecret == _clientSecret)
                context.Validated(clientId);

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            context.AdditionalResponseParameters.Add("userID", context.Identity.GetUserId());
            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string username)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "username", username }
            
           };
            return  new AuthenticationProperties(data);
        }
    }
}