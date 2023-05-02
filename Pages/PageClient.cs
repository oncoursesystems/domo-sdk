using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domo.Pages
{
    public class PageClient : IPageClient
    {
        private DomoHttpClient _domoHttpClient;
        private JsonSerializerOptions _serializerOptions;

        public PageClient(IDomoConfig config)
        {
            _domoHttpClient = new DomoHttpClient(config);
            _serializerOptions = new JsonSerializerOptions()
            {
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }

        /// <summary>
		/// Retrieves information about a page
		/// </summary>
		/// <param name="pageId"></param>
		/// <returns>Page information</returns>
		public async Task<Page> RetrievePageAsync(string pageId)
        {
            string pageUri = $"v1/pages/{pageId}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            var response = await _domoHttpClient.Client.GetAsync(pageUri);
            string stringResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Page>(stringResponse, _serializerOptions);
        }

        /// <summary>
        /// Creates a new page
        /// </summary>
        /// <param name="page"></param>
        /// <returns>Newly created page information</returns>
        public async Task<Page> CreatePageAsync(Page page)
        {
            string pageUri = "v1/pages";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            StringContent content = new StringContent(JsonSerializer.Serialize(page, _serializerOptions), Encoding.UTF8, "application/json");
            var response = await _domoHttpClient.Client.PostAsync(pageUri, content);
            string stringResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Page>(stringResponse, _serializerOptions);
        }

        /// <summary>
        /// Updates an existing page
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="page"></param>
        /// <returns>Boolean whether method is successful</returns>
        public async Task<bool> UpdatePageAsync(string pageId, Page page)
        {
            string pageUri = $"v1/pages/{pageId}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            StringContent content = new StringContent(JsonSerializer.Serialize(page, _serializerOptions), Encoding.UTF8, "application/json");
            var response = await _domoHttpClient.Client.PutAsync(pageUri, content);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Deletes a page
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns>Boolean whether method is successful</returns>
        public async Task<bool> DeletePageAsync(string pageId)
        {
            string pageUri = $"v1/pages/{pageId}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            var response = await _domoHttpClient.Client.DeleteAsync(pageUri);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Gets a list of pages
        /// </summary>
        /// <param name="limit">Limit of pages to return. Limit is 50.</param>
        /// <param name="offset">Offset of Pages to start retrieving from.</param>
        /// <returns>List of pages</returns>
        public async Task<IEnumerable<Page>> ListPagesAsync(long offset = 0, long limit = 50)
        {
            string pageUri = $"v1/pages?offset={offset}&limit={limit}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            var response = await _domoHttpClient.Client.GetAsync(pageUri);
            string stringResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Page>>(stringResponse, _serializerOptions);
        }

        /// <summary>
        /// Retrives a page collection from a page Id
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns>Page collection information</returns>
        public async Task<PageCollection> RetrievePageCollectionAsync(long pageId)
        {
            string pageUri = $"v1/pages/{pageId}/collections";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            var response = await _domoHttpClient.Client.GetAsync(pageUri);
            string stringResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PageCollection>(stringResponse, _serializerOptions);
        }

        /// <summary>
        /// Creates a page collection
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="pageInfo"></param>
        /// <returns>Boolean whether method is successful</returns>
        public async Task<bool> CreatePageCollectionAsync(long pageId, PageInfo pageInfo)
        {
            string pageUri = $"v1/pages/{pageId}/collections";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            StringContent content = new StringContent(JsonSerializer.Serialize(pageInfo, _serializerOptions), Encoding.UTF8, "application/json");
            var response = await _domoHttpClient.Client.PostAsync(pageUri, content);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Updates an existing page collection
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="pageCollectionId"></param>
        /// <param name="pageInfo"></param>
        /// <returns>Boolean whether method is successful</returns>
        public async Task<bool> UpdatePageCollectionAsync(long pageId, long pageCollectionId, PageInfo pageInfo)
        {
            string pageUri = $"v1/pages/{pageId}/collections/{pageCollectionId}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            StringContent content = new StringContent(JsonSerializer.Serialize(pageInfo, _serializerOptions), Encoding.UTF8, "application/json");
            var response = await _domoHttpClient.Client.PutAsync(pageUri, content);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Deletes a page collection
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="pageCollectionId"></param>
        /// <returns>Boolean whether method is successful</returns>
        public async Task<bool> DeletePageCollectionAsync(long pageId, long pageCollectionId)
        {
            string pageUri = $"v1/pages/{pageId}/collections/{pageCollectionId}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            var response = await _domoHttpClient.Client.DeleteAsync(pageUri);
            return response.IsSuccessStatusCode;
        }
    }
}