using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSAgregator.Shared.Common;

namespace RSSAgregator.Shared.ViewModel
{
    public class LoginPageViewModel : BaseViewModel
    {
        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyPropertyChanged("Email");
            }
        }

        #region Dependencies

        private ILoginManager LoginManager { get; set; }

        #endregion

        public LoginPageViewModel(ILoginManager loginManager)
        {
            LoginManager = loginManager;
        }

        public async Task<bool> RegisterAsync(string password)
        {
            return await LoginManager.RegisterAndLoginAsync(Email, password);
        }

        public Task<bool> LoginAsync(string password)
        {
            var test = LoginManager.LoginAsync(Email, password);

            return test;
        }
    }
}
