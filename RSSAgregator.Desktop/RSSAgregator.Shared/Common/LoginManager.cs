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

        public string UserId { get; private set; }

        #region Dependencies

        private IServiceManager ServiceManager { get; set; }

        #endregion

        #region Events

        public event EventHandler UserChanged;

        protected void OnUserChanged()
        {
            if (UserChanged != null)
                UserChanged(this, EventArgs.Empty);
        }

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
            if (success != null)
            {
                UserId = success;
                IsLogged = true;
                OnUserChanged();
                return true;
            }
            return false;
        }

        public async Task<bool> RegisterAndLoginAsync(string username, string password)
        {
            var success = await ServiceManager.GetTokenRegisterAsync(username, password);
            if (success)
            {
                var successLogin = await ServiceManager.GetTokenLoginAsync(username, password);
                if (successLogin != null)
                {
                    UserId = successLogin;
                    IsLogged = true;
                    OnUserChanged();
                    return true;
                }
            }
            return false;
        }



    }
}
