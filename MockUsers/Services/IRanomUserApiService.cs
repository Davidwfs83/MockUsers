using MockUsers.Models;

namespace MockUsers.Services
{
    public interface IRanomUserApiService
    {
        public Task<List<User>> GetUsersData(string gender, int recordsCount);
        public Task<KeyValuePair<string, int>> GetMostPopularCountry(int randomNumOfUsers);
        public Task<List<string>> RandomMails(int amountOfRandomUsers);
    }
}
