using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSSAgregator.Shared.ViewModel;

namespace RSSAgregator.Desktop.ViewModel
{
    public class ViewModelContainer
    {
        public MainPageViewModel MainPageVM { get; set;}
        public LoginPageViewModel LoginPageVM { get; set; }
        public FeedPageViewModel FeedPageVM { get; set; }

    }
}
