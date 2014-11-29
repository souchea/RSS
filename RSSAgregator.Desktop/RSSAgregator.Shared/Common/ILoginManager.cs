using System.Threading.Tasks;

namespace RSSAgregator.Shared.Common
{
    public interface ILoginManager
    {
        bool IsLogged { get; }
        string UserInfo { get; }
        void LogOut();
        Task<bool> LoginAsync(string username, string password);
        Task<bool> RegisterAndLoginAsync(string username, string password);
    }
}