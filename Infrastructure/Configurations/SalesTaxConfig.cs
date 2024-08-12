using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class SalesTaxConfig : IEntityTypeConfiguration<SalesTax>
{
	public void Configure(EntityTypeBuilder<SalesTax> builder)
	{
		builder.HasKey(p => new { p.Id });

		builder.ToTable("SalesTax");
	}
}
