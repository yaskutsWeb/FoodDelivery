using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDelivery.Domain.Abstract;

namespace FoodDelivery.WebUI.Controllers
{
    public class NavController : Controller
    {
		private IFoodRepository repository;

		public NavController(IFoodRepository repo)
		{
			repository = repo;
		}

		public PartialViewResult Menu(string category = null)
		{
			ViewBag.SelectedCategory = category;

			IEnumerable<string> categories = repository.Foods
				.Select(food => food.Category)
				.Distinct()
				.OrderBy(x => x);
			return PartialView(categories);
		}
	}
}