using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace Munchkin.Shared.Identity
{
	[CollectionName("Users")]
	public class ApplicationUser : MongoIdentityUser<Guid>
	{
		public ApplicationUser() : base()
		{
		}

		public ApplicationUser(string userName, string email) : base(userName, email)
		{
		}
	}
}
