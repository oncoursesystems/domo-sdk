using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domo.Users
{
    /// <summary>
    /// https://developer.domo.com/docs/users-api-reference/users-2
    /// </summary>
    public interface IUserClient
    {
        /// <summary>
        /// Retrieves the details of an existing user.
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <returns>Returns a Domo User. <see cref="Domo.Users.User"/></returns>
        Task<User> RetrieveUserAsync(long userId);

        /// <summary>
		/// Creates a new user in your Domo instance.
		/// </summary>
		/// <param name="user">Properties and values for the user being created</param>
		/// <param name="sendInvite">Whether or not to send a "You Just Got Domo'd!" invitation email to new user</param>
		/// <returns>Returns the created Domo User. <see cref="Domo.Users.User"/></returns>
        Task<User> CreateUserAsync(User user, bool sendInvite);

        /// <summary>
        /// Updates the specified user by providing values to parameters passed. Any parameter left out of the request 
        /// will cause the specific user’s attribute to remain unchanged.
        /// </summary>
        /// <param name="userId">Id of Domo User to Update.</param>
        /// <param name="user">Domo User Info to update to.</param>
        /// <returns>Returns a bool of whether the Domo User was succesfully updated.</returns>
        Task<bool> UpdateUserAsync(long userId, User user);

        /// <summary>
        /// Permanently deletes a user from your Domo instance.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Returns a bool of whether or not the Domo User was successfully deleted</returns>
        Task<bool> DeleteUserAsync(long userId);

        /// <summary>
        /// Get a list of all users in your Domo instance.
        /// </summary>
        /// <param name="limit">Max number of users to return. Maximum amount of users to return is 500.</param>
        /// <param name="offset">Offset of users to begin the list of users from.</param>
        /// <returns>Returns a list of Domo Users. <see cref="Domo.Users.User"/></returns>
        Task<IEnumerable<User>> ListUsersAsync(long limit = 50, long offset = 0);
    }
}