using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Abstract;

namespace FoodDelivery.Domain.Concrete
{
	public class EFFoodRepository: IFoodRepository
	{
		EFDbContext context = new EFDbContext();

		public IEnumerable<Food> Foods
		{
			get { return context.Foods; }
		}

		public void SaveFood(Food food)
		{
			if (food.FoodId == 0)
				context.Foods.Add(food);
			else
			{
				Food dbEntry = context.Foods.Find(food.FoodId);
				if (dbEntry != null)
				{
					dbEntry.Name = food.Name;
					dbEntry.Description = food.Description;
					dbEntry.Price = food.Price;
					dbEntry.Category = food.Category;
					dbEntry.ImageData = food.ImageData;
					dbEntry.ImageMimeType = food.ImageMimeType;
				}
			}
			context.SaveChanges();
		}

		public Food DeleteFood(int foodId)
		{
			Food dbEntry = context.Foods.Find(foodId);
			if (dbEntry != null)
			{
				context.Foods.Remove(dbEntry);
				context.SaveChanges();
			}
			return dbEntry;
		}
	}
}
