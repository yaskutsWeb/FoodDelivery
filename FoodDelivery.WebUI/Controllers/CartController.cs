using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Abstract;
using FoodDelivery.WebUI.Models;

namespace FoodDelivery.WebUI.Controllers
{
	public class CartController : Controller
	{
		private IFoodRepository repository;
		private IOrderProcessor orderProcessor;

		public CartController(IFoodRepository repo, IOrderProcessor processor)
		{
			repository = repo;
			orderProcessor = processor;
		}

		public ViewResult Checkout()
		{
			return View(new ShippingDetails());
		}

		[HttpPost]
		public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
		{
			if (cart.Lines.Count() == 0)
			{
				ModelState.AddModelError("", "Извините, ваша корзина пуста!");
			}

			if (ModelState.IsValid)
			{
				orderProcessor.ProcessOrder(cart, shippingDetails);
				cart.Clear();
				return View("Completed");
			}
			else
			{
				return View(shippingDetails);
			}
		}
		public ViewResult Index(Cart cart, string returnUrl)
		{
			return View(new CartIndexViewModel
			{
				Cart = cart,
				ReturnUrl = returnUrl
			});
		}

		public RedirectToRouteResult AddToCart(Cart cart, int foodId, string returnUrl)
		{
			Food food = repository.Foods
				.FirstOrDefault(g => g.FoodId == foodId);

			if (food != null)
			{
				cart.AddItem(food, 1);
			}
			return RedirectToAction("Index", new { returnUrl });
		}

		public RedirectToRouteResult RemoveFromCart(Cart cart, int foodId, string returnUrl)
		{
			Food food = repository.Foods
				.FirstOrDefault(g => g.FoodId == foodId);

			if (food != null)
			{
				cart.RemoveLine(food);
			}
			return RedirectToAction("Index", new { returnUrl });
		}

		public PartialViewResult Summary(Cart cart)
		{
			return PartialView(cart);
		}
	}
}