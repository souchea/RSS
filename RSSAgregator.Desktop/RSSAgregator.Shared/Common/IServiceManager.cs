using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RSSAgregator.Shared.Model;

namespace RSSAgregator.Shared.Common
{
    public interface IServiceManager
    {
        HttpClient WebApiClient { get; set; }
        Task<List<CategoryDTO>> GetCategoriesAsync(int userId);
    }
}