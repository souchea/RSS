using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using RSSAgregator.Shared.Model;
using RSSAgregator.Shared.Model.RSSAgregator.Shared.Model;

namespace RSSAgregator.Shared.Common
{
    public class WebApiServiceManager : IServiceManager
    {
        public HttpClient WebApiClient { get; set; }

        public WebApiServiceManager()
        {
            WebApiClient = new HttpClient
            {
                BaseAddress = new Uri("http://rssagregator.azurewebsites.net/api/")
            };
            WebApiClient.DefaultRequestHeaders.Accept.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void SetToken(string token)
        {
            WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);           
        }

        public async Task<List<CategoryDTO>> GetCategoriesAsync(string userId)
        {
            HttpResponseMessage response = await WebApiClient.GetAsync(String.Format("Category/Get/{0}", userId));
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<CategoryDTO>>();
            }
            return null;
        }

        public async Task<bool> AddCategoryAsync(string userId, string catName)
        {
            try
            {
                HttpResponseMessage response =
                    await WebApiClient.PostAsync(String.Format("Category/Add/{0}/{1}", userId, catName), null);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddSourceAsync(string userId, int catId, string url)
        {
            try
            {
                HttpResponseMessage response =
                    await
                        WebApiClient.PostAsync(String.Format("Source/Add/{0}?url={1}&catId={2}", userId, url, catId),
                            null);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<FeedDTO>> GetFeedsAsync(int sourceId, int nb = 10)
        {
            var toReturnFeedList = new List<FeedDTO>();
            try
            {
                HttpResponseMessage response =
                    await WebApiClient.GetAsync(String.Format("Source/GetItems/{0}?nb={1}", sourceId, nb));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<List<FeedDTO>>();
                }
                return toReturnFeedList;
            }
            catch (Exception)
            {
                return toReturnFeedList;
            }
        }

        public async Task<bool> DeleteCategory(int catId)
        {
            try
            {
                HttpResponseMessage response =
                    await
                        WebApiClient.DeleteAsync(String.Format("Category/Delete/{0}", catId));
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteSource(int sourceId)
        {
            try
            {
                HttpResponseMessage response =
                    await
                        WebApiClient.DeleteAsync(String.Format("Source/Delete/{0}", sourceId));
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SendReadAsync(int sourceId)
        {
            try
            {
                HttpResponseMessage response =
                    await
                        WebApiClient.PostAsync(String.Format("Source/Read/{0}", sourceId),
                            null);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SendStageAsync(int sourceId, string state)
        {
            try
            {
                HttpResponseMessage response =
                    await
                        WebApiClient.PostAsync(String.Format("Source/SetState/{0}?state={1}", sourceId, state),
                            null);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> GetTokenRegisterAsync(string username, string password)
        {
            try
            {
                HttpResponseMessage response =
                    await WebApiClient.PostAsync(String.Format("Token?grant_type=client_credentials&client_id=self&client_secret=self"), null);
                if (response.IsSuccessStatusCode)
                {
                    HttpResponseMessage response2 =
    await WebApiClient.PostAsync(String.Format("oauth/register?grant_type=password&client_id=self&client_secret=self&email={0}&password={1}", username, password), null);
                    if (response2.IsSuccessStatusCode)
                    {
                        var token = await response2.Content.ReadAsStringAsync();

                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> GetTokenLoginAsync(string username, string password)
        {
            try
            {
                HttpResponseMessage response =
                    await WebApiClient.PostAsync(String.Format("Token?grant_type=password&client_id=self&client_secret=self&username={0}&password={1}", username, password), null);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
