using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnCourse.Domo.Sdk.Groups
{
    public interface IGroupClient
    {
        /// <summary>
        /// Retrieves information about a group
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns>Group requested</returns>
        Task<Group> RetrieveGroupAsync(int groupId);
        /// <summary>
        /// Creates a group
        /// </summary>
        /// <param name="group"></param>
        /// <returns>Boolean whether method is successful</returns>
        Task<bool> CreateGroupAsync(Group group);
        /// <summary>
        /// Updates an existing group
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="groupSettings"></param>
        /// <returns>Boolean whether method is successful</returns>
        Task<bool> UpdateGroupAsync(int groupId, Group groupSettings);
        /// <summary>
        /// Permanently deletes a group
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns>Boolean whether method is successful</returns>
        Task<bool> DeleteGroupAsync(int groupId);
        /// <summary>
        /// Gets a list of groups
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns>A list of groups</returns>
        Task<IEnumerable<Group>> ListGroupsAsync(int offset, int limit);
        /// <summary>
        /// Adds an existing user to a group
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="userId"></param>
        /// <returns>Boolean whether method is successful</returns>
        Task<bool> AddUserAsync(int groupId, int userId);
        /// <summary>
        /// Lists users in a group
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns>A list of user Ids</returns>
        Task<IEnumerable<int>> ListUsersAsync(int groupId, int offset, int limit);
        /// <summary>
        /// Removes a user from a group
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="userId"></param>
        /// <returns>Boolean whether method is successful</returns>
        Task<bool> RemoveUserAsync(int groupId, int userId);
    }
}