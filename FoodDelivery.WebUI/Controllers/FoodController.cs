using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDelivery.Domain.Abstract;
using FoodDelivery.Domain.Entities;
using FoodDelivery.WebUI.Models;

namespace FoodDelivery.WebUI.Controllers
{
    public class FoodController : Controller
    {
		private IFoodRepository repository;
		public int pageSize = 4;

		public FoodController(IFoodRepository repo)
		{
			repository = repo;
		}

		public ViewResult List(string category, int page = 1)
		{
			FoodsListViewModel model = new FoodsListViewModel
			{
				Foods = repository.Foods.Where(p => category == null || p.Category == category)
				.OrderBy(food => food.FoodId).Skip((page - 1) * pageSize)
				.Take(pageSize),
				PagingInfo = new PagingInfo
				{
					CurrentPage = page,
					ItemsPerPage = pageSize,
					TotalItems = category == null ?repository.Foods.Count() :repository.Foods.Where(food => food.Category == category).Count()
				},
				CurrentCategory = category
			};
			return View(model);
		}

		public FileContentResult GetImage(int foodId)
		{
			Food food = repository.Foods
				.FirstOrDefault(g => g.FoodId == foodId);

			if (food != null)
			{
				return File(food.ImageData, food.ImageMimeType);
			}
			else
			{
				return null;
			}
		}
	}
}