using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RSSAgregator.Models;

namespace RSSAgregator.Shared.Common
{
    public interface IServiceManager
    {
        HttpClient WebApiClient { get; set; }

        Task<List<CategoryDTO>> GetCategoriesAsync(string userId);

        Task<bool> AddCategoryAsync(string userId, string catName);

        Task<bool> AddSourceAsync(string userId, int catId, string url);

        Task<List<FeedDTO>> GetFeedsAsync(int sourceId, int nb);

        Task<List<FeedDTO>> GetFeedsToDateAsync(int sourceId, int nb, DateTime date);

        Task<bool> DeleteCategory(int catId);

        Task<bool> DeleteSource(int sourceId);

        Task<bool> SendReadAsync(int sourceId);

        Task<bool> SendStageAsync(int sourceId, string state);

        Task<bool> GetTokenRegisterAsync(string username, string password);

        Task<string> GetTokenLoginAsync(string username, string password);

        Task<bool> RenameCategory(int catId, string newName);
    }
}