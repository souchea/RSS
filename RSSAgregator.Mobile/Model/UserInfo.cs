using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace RSSAgregator.Mobile.Model
{
    public class UserInfo
    {
        public string UserName { get; set; }

        public string EmailAdress { get; set; }

        public DateTime SignUpDate { get; set; }
    }
}
