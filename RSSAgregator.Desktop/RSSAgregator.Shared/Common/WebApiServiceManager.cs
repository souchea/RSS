using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using RSSAgregator.Shared.Model;

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

        public async Task<List<CategoryDTO>> GetCategoriesAsync(int userId)
        {
            HttpResponseMessage response = await WebApiClient.GetAsync(String.Format("Category/Get/{0}", userId));
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<CategoryDTO>>();
            }
            return null;
        }

        public async Task<bool> AddCategoryAsync(int userId, string catName)
        {
            try
            {
                HttpResponseMessage response = await WebApiClient.PostAsync(String.Format("Category/Add/{0}/{1}", userId, catName), null);
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
