using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domo.Groups
{
    public class GroupClient : IGroupClient
    {
        private DomoHttpClient _domoHttpClient;
        private JsonSerializerOptions _serializerOptions;
        public GroupClient(IDomoConfig config)
        {
            _domoHttpClient = new DomoHttpClient(config);
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
        }

        /// <summary>
        /// Retrieves information about a group
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns>Group requested</returns>
        public async Task<Group> RetrieveGroupAsync(int groupId)
        {
            string groupUri = $"v1/groups/{groupId}";

            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            var response = await _domoHttpClient.Client.GetAsync(groupUri);
            string stringResponse = await response.Content.ReadAsStringAsync();
            var objectResponse = JsonSerializer.Deserialize<Group>(stringResponse, _serializerOptions);
            return objectResponse;
        }

        /// <summary>
        /// Creates a group
        /// </summary>
        /// <param name="group"></param>
        /// <returns>Boolean whether method is successful</returns>
        public async Task<Group> CreateGroupAsync(Group group)
        {
            string groupUri = $"v1/groups";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");
            string groupJson = JsonSerializer.Serialize(new { name = group.Name }, _serializerOptions);
            StringContent content = new StringContent(groupJson, Encoding.UTF8, "application/json");

            var response = await _domoHttpClient.Client.PostAsync(groupUri, content);
            groupJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Group>(groupJson, _serializerOptions);
        }

        /// <summary>
        /// Updates an existing group
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="groupSettings"></param>
        /// <returns>Boolean whether method is successful</returns>
        public async Task<bool> UpdateGroupAsync(int groupId, Group groupSettings)
        {
            string groupUri = $"v1/groups/{groupId}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");
            string groupSettingsJson = JsonSerializer.Serialize(groupSettings, _serializerOptions);
            StringContent content = new StringContent(groupSettingsJson, Encoding.UTF8, "application/json");

            var response = await _domoHttpClient.Client.PutAsync(groupUri, content);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Permanently deletes a group
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns>Boolean whether method is successful</returns>
        public async Task<bool> DeleteGroupAsync(int groupId)
        {
            string groupUri = $"v1/groups/{groupId}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            var response = await _domoHttpClient.Client.DeleteAsync(groupUri);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Gets a list of groups
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns>A list of groups</returns>
        public async Task<IEnumerable<Group>> ListGroupsAsync(int offset, int limit)
        {
            if (limit < 1 || limit > 500) throw new ArgumentOutOfRangeException("limit", $"List limit {limit} cannot be used. Use a limit value between 1 and 500");

            string groupUri = $"v1/groups?offset={offset}&limit={limit}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            var response = await _domoHttpClient.Client.GetAsync(groupUri);
            string stringResponse = await response.Content.ReadAsStringAsync();
            var objectResponse = JsonSerializer.Deserialize<IEnumerable<Group>>(stringResponse, _serializerOptions);
            return objectResponse;
        }

        /// <summary>
        /// Adds an existing user to a group
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="userId"></param>
        /// <returns>Boolean whether method is successful</returns>
        public async Task<bool> AddUserAsync(int groupId, int userId)
        {
            string groupUri = $"v1/groups/{groupId}/users/{userId}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            var response = await _domoHttpClient.Client.PutAsync(groupUri, null);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Lists users in a group
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns>A list of user Ids</returns>
        public async Task<IEnumerable<int>> ListUsersAsync(int groupId, int offset, int limit)
        {
            if (limit < 1 || limit > 500) throw new ArgumentOutOfRangeException("limit", $"List limit {limit} cannot be used. Use a limit value between 1 and 500");

            string groupUri = $"v1/groups/{groupId}/users?offset={offset}&limit={limit}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            var response = await _domoHttpClient.Client.GetAsync(groupUri);
            string stringResponse = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<IEnumerable<int>>(stringResponse, _serializerOptions);
            return users;
        }

        /// <summary>
        /// Removes a user from a group
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="userId"></param>
        /// <returns>Boolean whether method is successful</returns>
        public async Task<bool> RemoveUserAsync(int groupId, int userId)
        {
            string groupUri = $"v1/groups/{groupId}/users/{userId}";
            _domoHttpClient.SetAcceptRequestHeaders("application/json");

            var response = await _domoHttpClient.Client.DeleteAsync(groupUri);
            return response.IsSuccessStatusCode;
        }
    }
}