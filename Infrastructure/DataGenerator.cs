using Domain;
using Infrastructure.Persistence;
using System;

namespace Infrastructure;

public static class DataGenerator
{
	private static bool isDataLoaded = false;

	public static void CreateData(this ApplicationDbContext context)
	{
		context.AddRange(
				new ProductCategory("Book", true),
				new ProductCategory("Food", true),
				new ProductCategory("Medical Products", true),
				new ProductCategory("Music CDs", false),
				new ProductCategory("Perfumes", false)
			);

		context.AddRange(

				new Product("Book", "Book", 1, Convert.ToDecimal(12.49), false),
				new Product("Chocolate Bar", "chocolate bar", 2, Convert.ToDecimal(0.85), false),
				new Product("Imported Chocolates", "imported box of chocolates", 2, Convert.ToDecimal(10.00), true),
				new Product("Headache Pills", "packet of headache pills", 3, Convert.ToDecimal(9.75), false),
				new Product("Music CD", "Music CD", 4, Convert.ToDecimal(14.99), false),
				new Product("Imported Perfume", "imported bottle of perfume", 5, Convert.ToDecimal(47.50), true),
				new Product( "Perfume", "bottle of perfume", 5, Convert.ToDecimal(18.99), false),
				new Product("Imported Chocolates", "box of imported chocolates", 2, Convert.ToDecimal(11.25), true),
				new Product( "Imported Perfume", "imported bottle of perfume", 5, Convert.ToDecimal(27.99), true)
			);

		context.SaveChanges();

		isDataLoaded = true;
	}

	public static bool CheckDataLoaded(this ApplicationDbContext context)
	{
		return isDataLoaded;
	}
}
