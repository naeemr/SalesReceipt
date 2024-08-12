using Domain;
using Infrastructure.Persistence;
using System;

namespace Infrastructure;

public static class DataGenerator
{
	public static void CreateData(this ApplicationDbContext context)
	{
		context.AddRange(
				new ProductCategory(1, "Book", true),
				new ProductCategory(2, "Food", true),
				new ProductCategory(3, "Medical Products", true),
				new ProductCategory(4, "Music CDs", false),
				new ProductCategory(5, "Perfumes", false)
			);


		context.AddRange(

				new Product(1, "Book", "Book", 1, Convert.ToDecimal(12.49), false),
				new Product(2, "Chocolate Bar", "chocolate bar", 2, Convert.ToDecimal(0.85), false),
				new Product(3, "Imported Chocolates", "imported box of chocolates", 2, Convert.ToDecimal(10.00), true),
				new Product(4, "Headache Pills", "packet of headache pills", 3, Convert.ToDecimal(9.75), false),
				new Product(5, "Music CD", "Music CD", 4, Convert.ToDecimal(14.99), false),
				new Product(6, "Imported Perfume", "imported bottle of perfume", 5, Convert.ToDecimal(47.50), true),
				new Product(7, "Perfume", "bottle of perfume", 5, Convert.ToDecimal(18.99), false)
			);

		context.SaveChanges();
	}
}
