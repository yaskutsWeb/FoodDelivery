using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDelivery.Domain.Abstract;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.WebUI.Controllers
{
	[Authorize]
	public class AdminController : Controller
    {
		IFoodRepository repository;

		public AdminController(IFoodRepository repo)
		{
			repository = repo;
		}

		public ViewResult Index()
		{
			return View(repository.Foods);
		}

		public ViewResult Edit(int foodId)
		{
			Food food = repository.Foods
				.FirstOrDefault(g => g.FoodId == foodId);
			return View(food);
		}

		[HttpPost]
		public ActionResult Edit(Food food, HttpPostedFileBase image = null)
		{
			if (ModelState.IsValid)
			{
				if (image != null)
				{
					food.ImageMimeType = image.ContentType;
					food.ImageData = new byte[image.ContentLength];
					image.InputStream.Read(food.ImageData, 0, image.ContentLength);
				}
				repository.SaveFood(food);
				TempData["message"] = string.Format("Изменения в позиции \"{0}\" были сохранены", food.Name);
				return RedirectToAction("Index");
			}
			else
			{
				// Что-то не так со значениями данных
				return View(food);
			}
		}

		public ViewResult Create()
		{
			return View("Edit", new Food());
		}

		[HttpPost]
		public ActionResult Delete(int foodId)
		{
			Food deletedFood = repository.DeleteFood(foodId);
			if (deletedFood != null)
			{
				TempData["message"] = string.Format("Позиция \"{0}\" была удалена",
					deletedFood.Name);
			}
			return RedirectToAction("Index");
		}
	}
}