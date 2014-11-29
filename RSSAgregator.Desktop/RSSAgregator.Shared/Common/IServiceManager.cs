using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RSSAgregator.Shared.Model;
using RSSAgregator.Shared.Model.RSSAgregator.Shared.Model;

namespace RSSAgregator.Shared.Common
{
    public interface IServiceManager
    {
        HttpClient WebApiClient { get; set; }

        Task<List<CategoryDTO>> GetCategoriesAsync(string userId);

        Task<bool> AddCategoryAsync(string userId, string catName);

        Task<bool> AddSourceAsync(string userId, int catId, string url);

        Task<List<FeedDTO>> GetFeedsAsync(int sourceId, int nb);

        Task<bool> DeleteCategory(int catId);

        Task<bool> DeleteSource(int sourceId);

        Task<bool> SendReadAsync(int sourceId);

        Task<bool> SendStageAsync(int sourceId, string state);

        Task<bool> GetTokenRegisterAsync(string username, string password);

        Task<bool> GetTokenLoginAsync(string username, string password);
    }
}