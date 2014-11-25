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
        Task<List<CategoryDTO>> GetCategoriesAsync(int userId);
        Task<bool> AddCategoryAsync(int userId, string catName);

        Task<List<FeedDTO>> GetFeedsAsync(int sourceId, int nb);
    }
}