using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domo.Users
{
    public class UserClient : IUserClient
    {
        private DomoHttpClient _domoHttpClient;
        private JsonSerializerOptions _serializerOptions;

        public UserClient(IDomoConfig config)
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
        /// Retreives a given Domo User by User Id
        /// </summary>
        /// <param name="userId">Id of user to retreive</param>
        /// <returns>Returns a Domo User. <see cref="Domo.Users.User"/></returns>
        public async Task<User> RetrieveUserAsync(long userId)
        {
            string userUri = $"v1/users/{userId}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            var response = await _domoHttpClient.Client.GetAsync(userUri);
            string stringResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(stringResponse, _serializerOptions);
        }

        /// <summary>
        /// Create a Domo User
        /// </summary>
        /// <param name="user">Properties and values for the user being created</param>
        /// <param name="sendInvite">Whether or not to send a "You Just Got Domo'd!" invitation email to new user</param>
        /// <returns>Returns the created Domo User. <see cref="Domo.Users.User"/></returns>
        public async Task<User> CreateUserAsync(User user, bool sendInvite)
        {
            string userId = $"v1/users?sendInvite={sendInvite}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            StringContent content = new StringContent(JsonSerializer.Serialize(user, _serializerOptions), Encoding.UTF8, "application/json");
            var response = await _domoHttpClient.Client.PostAsync(userId, content);
            string stringResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(stringResponse, _serializerOptions);
        }

        /// <summary>
        /// Update a given Domo User by User Id
        /// </summary>
        /// <param name="userId">Id of Domo User to Update.</param>
        /// <param name="user">Domo User Info to update to.</param>
        /// <returns>Returns a bool of whether the Domo User was succesfully updated.</returns>
        public async Task<bool> UpdateUserAsync(long userId, User user)
        {
            string userUri = $"v1/users/{userId}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            StringContent content = new StringContent(JsonSerializer.Serialize(user, _serializerOptions), Encoding.UTF8, "application/json");
            var response = await _domoHttpClient.Client.PutAsync(userUri, content);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Delete a given Domo User by User Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Returns a bool of whether or not the Domo User was successfully deleted</returns>
        public async Task<bool> DeleteUserAsync(long userId)
        {
            string userUri = $"v1/users/{userId}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            var response = await _domoHttpClient.Client.DeleteAsync(userUri);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Retreive a list of users up to the specified limit, starting at a given offset
        /// </summary>
        /// <param name="limit">Max number of users to return. Maximum amount of users to return is 500.</param>
        /// <param name="offset">Offset of users to begin the list of users from.</param>
        /// <returns>Returns a list of Domo Users. <see cref="Domo.Users.User"/></returns>
        public async Task<IEnumerable<User>> ListUsersAsync(long offset, long limit = 50)
        {
            if (limit < 1 || limit > 500) throw new ArgumentOutOfRangeException("limit", $"List limit {limit} cannot be used. Use a limit value between 1 and 500");

            string userUri = $"v1/users?limit={limit}&offset={offset}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            var response = await _domoHttpClient.Client.GetAsync(userUri);
            string stringResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<User>>(stringResponse, _serializerOptions);
        }
    }
}