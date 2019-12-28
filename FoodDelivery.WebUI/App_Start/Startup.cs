using Microsoft.Owin;
using Owin;

using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Concrete;

[assembly: OwinStartup(typeof(FoodDelivery.App_Start.Startup))]


namespace FoodDelivery.App_Start
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.CreatePerOwinContext<ApplicationContext>(ApplicationContext.Create);
			app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString("/Account/Login"),
			});
		}
	}
}