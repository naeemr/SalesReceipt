using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder.HasKey(p => new { p.Id });

		builder.HasOne(x => x.ProductCategory)
			.WithMany(x => x.Products)
			.HasForeignKey(x => x.ProductCategoryId);

		builder.ToTable("Product");

		builder.Ignore(p => p.IsTaxExempt);
	}
}
