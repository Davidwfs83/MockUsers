using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MockUsers.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Reflection;

namespace MockUsers.Services
{
    
    public class RandomUserApiService : IRanomUserApiService
    {
        private string p_baseUrl = "https://randomuser.me/api/";
        private readonly ILogger<RandomUserApiService> p_logger;

        private HttpClient p_client;
        public RandomUserApiService(ILogger<RandomUserApiService> logger)
        {           
            p_client = new HttpClient();
            p_logger = logger;
        }

        // Basic fetching method for the services, responsible to make the request to appropiate endpoint
        // and than map it to the response model
        private async Task<RandomUserResponse> Fetcher(string endpoint)
        {
            try
            {
                string responseJson = await p_client.GetStringAsync(p_baseUrl + endpoint);

                return JsonConvert.DeserializeObject<RandomUserResponse>(responseJson);
            }
            catch(Exception ex)
            {
                p_logger.LogError(ex, "An error occurred while fetching data from the API.");
                throw; // Rethrow the exception after logging

            }             
        }

        public async Task<List<User>> GetUsersData(string gender, int recordsCount)
        {
            Task<RandomUserResponse> response = Fetcher($"?gender={gender}&results={recordsCount}");
            return response.Result.results;
        }
        public async Task<KeyValuePair<string, int>> GetMostPopularCountry(int randomNumOfUsers)
        {
            Task<RandomUserResponse> response = Fetcher($"?results={randomNumOfUsers}");
            Dictionary<string,int> countryCounterDic = new Dictionary<string,int>();
            foreach (User user in response.Result.results)
            {
                if (!countryCounterDic.ContainsKey(user.location.country))
                {
                    countryCounterDic.Add(user.location.country, 1);
                    continue;
                }
                countryCounterDic[user.location.country]++;
            }
            // Calcuate country which appeared mostly
            return countryCounterDic.OrderByDescending(x => x.Value).FirstOrDefault();

        }

        public async Task<List<string>> RandomMails(int amountOfRandomUsers)
        {
            Task<RandomUserResponse> response = Fetcher($"?results={amountOfRandomUsers}");
            List<string> mails = new List<string>();
            foreach (User user in response.Result.results)
            {
                mails.Add(user.email);
            }
            return mails;
        }
    }
}
