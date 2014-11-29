using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSAgregator.Shared.Common
{
    public class LoginManager : ILoginManager
    {
        public bool IsLogged { get; private set; }

        public string UserInfo { get; private set; }

        #region Dependencies
        private IServiceManager ServiceManager { get; set; }

        #endregion

        public LoginManager(IServiceManager serviceManager)
        {
            ServiceManager = serviceManager;
        }

        public void LogOut()
        {
            IsLogged = false;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var success = await ServiceManager.GetTokenLoginAsync(username, password);
            if (success)
            {
                IsLogged = true;
            }
            return success;
        }

        public async Task<bool> RegisterAndLoginAsync(string username, string password)
        {
            var success = await ServiceManager.GetTokenRegisterAsync(username, password);
            if (success)
            {
                IsLogged = true;
            }
            return success;
        }



    }
}
