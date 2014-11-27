using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace RSSAgregator.Server.APIAttribute
{
    public class ScopeAttribute : AuthorizeAttribute
    {
        private string[] _scopes;
        private string _scopeClaimType = "scope";

        public ScopeAttribute(params string[] scopes)
        {
            if (scopes == null)
            {
                throw new ArgumentNullException("scopes");
            }

            _scopes = scopes;
        }


        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var grantedScopes = ClaimsPrincipal.Current.FindAll(_scopeClaimType).Select(c => c.Value).ToList();

            if (_scopes.Any(scope => !grantedScopes.Contains(scope)))
               return false;
         
            return true;
        }
    }
}