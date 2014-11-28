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

        public string Token { get; private set; }

        public IServiceManager ServiceManager { get; set; }

        public LoginManager(IServiceManager serviceManager)
        {
            ServiceManager = serviceManager;
        }

        public void LogOut()
        {
            
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            return true;
        }

        public async Task<bool> RegisterAndLoginAsync(string username, string password)
        {
            return true;
        }



    }
}
