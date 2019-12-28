using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Domain.Abstract
{
	public interface IFoodRepository
	{
		IEnumerable<Food> Foods { get; }
		void SaveFood(Food food);
		Food DeleteFood(int foodId);
	}
}
