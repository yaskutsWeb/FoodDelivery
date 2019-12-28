using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FoodDelivery.Concrete;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using FoodDelivery.Models;

namespace FShop.Controllers
{
	public class AccountController : Controller
	{

		private ApplicationUserManager UserManager
		{
			get
			{
				return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
		}

		public ActionResult Register()
		{
			return View();
		}




		[HttpPost]
		public async Task<ActionResult> Register(RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, Name = model.Name, Surname = model.Surname };

				IdentityResult result = await UserManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					return RedirectToAction("Login", "Account");
				}
				else
				{
					foreach (string error in result.Errors)
					{

						ModelState.AddModelError("", "Этот адрес электронной почты уже используется");
					}
				}
			}
			return View(model);
		}

		private IAuthenticationManager AuthenticationManager
		{
			get
			{
				return HttpContext.GetOwinContext().Authentication;
			}
		}

		public ActionResult Login(string returnUrl)
		{
			ViewBag.returnUrl = returnUrl;
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);
				if (user == null)
				{
					ModelState.AddModelError("", "Неверный логин или пароль");
				}
				else
				{
					ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
					AuthenticationManager.SignOut();
					AuthenticationManager.SignIn(new AuthenticationProperties
					{
						IsPersistent = true
					}, claim);
					if (string.IsNullOrEmpty(returnUrl))
						return RedirectToAction("List", "Food");
					return Redirect(returnUrl);
				}
			}
			ViewBag.returnUrl = returnUrl;
			return View(model);
		}

		public ActionResult Logout()
		{
			AuthenticationManager.SignOut();
			return RedirectToAction("Login");
		}

		public PartialViewResult LoginNav(LoginModel loginModel)
		{
			return PartialView(loginModel);
		}

		public ViewResult Index(LoginModel loginModel, string returnUrl)
		{
			return View(new LoginIndexModel
			{
				LoginModel = loginModel,
				ReturnUrl = returnUrl
			});
		}
	}
}
