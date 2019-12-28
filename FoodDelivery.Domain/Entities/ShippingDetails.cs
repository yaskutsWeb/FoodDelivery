using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Domain.Entities
{
	public class ShippingDetails
	{
		[Required(ErrorMessage = "Укажите как вас зовут")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Укажите ваш номер телефона")]
		public int Number { get; set; }

		[Required(ErrorMessage = "Укажите адрес")]
		public string City { get; set; }

		public bool GiftWrap { get; set; }
	}
}
