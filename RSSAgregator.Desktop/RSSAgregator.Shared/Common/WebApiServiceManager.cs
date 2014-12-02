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
                BaseAddress = new Uri("https://rssagregator.azurewebsites.net/api/")
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
            return new List<CategoryDTO>();
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

        public async Task<List<FeedDTO>> GetFeedsFromDateAsync(int sourceId, DateTime date)
        {
            var toReturnFeedList = new List<FeedDTO>();
            try
            {
                HttpResponseMessage response =
                    await WebApiClient.GetAsync(String.Format("Source/GetItemsFromDate/{0}?nb={1}&year={2}&month={3}&day={4}&hour={5}&minute={6}",
                                    sourceId, 1, date.Year, date.Month, date.Day, date.Hour, date.Minute));
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

        public async Task<List<FeedDTO>> GetFeedsToDateAsync(int sourceId, int nb, DateTime date)
        {
            var toReturnFeedList = new List<FeedDTO>();
            try
            {
                HttpResponseMessage response =
                    await WebApiClient.GetAsync(String.Format("Source/GetItemsToDate/{0}?nb={1}&year={2}&month={3}&day={4}&hour={5}&minute={6}",
                                    sourceId, 1, date.Year, date.Month, date.Day, date.Hour, date.Minute));
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
            var values = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", "self"),
                    new KeyValuePair<string, string>("client_secret", "self")
                };

            var content = new FormUrlEncodedContent(values);

            try
            {
                HttpResponseMessage response =
                    await WebApiClient.PostAsync("Token", content);
                if (response.IsSuccessStatusCode)
                {

                    var token1 = await response.Content.ReadAsAsync<TokenResult>();

                    SetToken(token1.access_token);

                    var values2 = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("client_id", "self"),
                    new KeyValuePair<string, string>("client_secret", "self"),
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)
                };

                    var content2 = new FormUrlEncodedContent(values2);

                    HttpResponseMessage response2 = await WebApiClient.PostAsync("oauth/register", content2);
                    if (response2.IsSuccessStatusCode)
                    {
                        var token = await response2.Content.ReadAsAsync<TokenResult>();

                        SetToken(token.access_token);

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

        public async Task<string> GetTokenLoginAsync(string username, string password)
        {
            try
            {
                var values = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("client_id", "self"),
                    new KeyValuePair<string, string>("client_secret", "self"),
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)
                };

                var content = new FormUrlEncodedContent(values);

                HttpResponseMessage response = await WebApiClient.PostAsync("Token", content);
                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsAsync<TokenResult>();

                    SetToken(token.access_token);

                    return token.userID;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
