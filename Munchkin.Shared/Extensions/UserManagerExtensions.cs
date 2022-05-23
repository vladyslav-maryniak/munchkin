using Microsoft.AspNetCore.Identity;

namespace Munchkin.Shared.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<TUser> FindByNameOrEmailAsync<TUser>(
            this UserManager<TUser> userManager, string nickname, string email) where TUser : class
        {
            bool byName = string.IsNullOrEmpty(email);
            return await (byName ?
                userManager.FindByNameAsync(nickname) :
                userManager.FindByEmailAsync(email));
        }
    }
}
