using System;
using System.Net.Http;
using System.Threading.Tasks;
using Bct.SearchFight.Shared.Helpers;
using Bct.SearchFight.Services.Models;
using Bct.SearchFight.Services.Interfaces;
using Bct.SearchFight.Services.Models.Config;

namespace Bct.SearchFight.Services.Impl
{
    public class BingSearch : ISearchEngine
    {
        #region Settings

        public string Name => "Bing";        
        private HttpClient _client { get; }               

        #endregion

        #region Constructors

        public BingSearch()
        {
            _client = new HttpClient { DefaultRequestHeaders = { { "Ocp-Apim-Subscription-Key", BingConfig.ApiKey } } };            
        }

        #endregion

        #region Interface Methods

        public async Task<long> GetTotalResultsAsync(string query)
        {
            if (string.IsNullOrEmpty(query))
                throw new ArgumentException("The specified parameter is invalid.", nameof(query));

            string searchRequest = BingConfig.BaseUrl.Replace("{Query}", query);

            using (var response = await _client.GetAsync(searchRequest))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception("We weren't able to process your request. Please try again later.");
                
                BingResponse results = JsonHelper.Deserialize<BingResponse>(await response.Content.ReadAsStringAsync());                
                return long.Parse(results.WebPages.TotalEstimatedMatches);
            }
        }

        #endregion
    }
}
