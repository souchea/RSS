using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Ninject;
using RSSAgregator.Desktop.Manager;
using RSSAgregator.Shared.Common;

namespace RSSAgregator.Desktop
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Global parameters

        internal static IKernel Kernel { get; set; }

        #endregion

        public App()
        {

            Kernel = new StandardKernel();

            Kernel.Bind<IStorageManager>().To<StorageManager>();
            Kernel.Bind<IDataManager>().To<RssDataManager>().InSingletonScope();
            Kernel.Bind<IServiceManager>().To<WebApiServiceManager>();
            Kernel.Bind<ILoginManager>().To<LoginManager>();    
        }

    }
}
