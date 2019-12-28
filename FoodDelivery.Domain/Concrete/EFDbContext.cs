using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Domain.Concrete
{
	public class EFDbContext: DbContext
	{
		public DbSet<Food> Foods { get; set; }
	}
}
