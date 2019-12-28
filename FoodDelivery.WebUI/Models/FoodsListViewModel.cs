using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.WebUI.Models
{
	public class FoodsListViewModel
	{
		public IEnumerable<Food> Foods { get; set; }
		public PagingInfo PagingInfo { get; set; }
		public string CurrentCategory { get; set; }
	}
}