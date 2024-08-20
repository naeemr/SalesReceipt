using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ProductCategoryConfig : IEntityTypeConfiguration<ProductCategory>
{
	public void Configure(EntityTypeBuilder<ProductCategory> builder)
	{
		builder.HasKey(p => new { p.Id });

		builder.HasMany(p => p.Products);

		builder.ToTable("ProductCategory");
	}
}
