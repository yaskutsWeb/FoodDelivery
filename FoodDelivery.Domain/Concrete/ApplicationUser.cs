using Microsoft.AspNet.Identity.EntityFramework;

namespace FoodDelivery.Concrete
{
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; }
		public string Surname { get; set; }

		public ApplicationUser()
		{
		}
	}
}
