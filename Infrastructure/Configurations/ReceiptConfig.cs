using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ReceiptConfig : IEntityTypeConfiguration<Receipt>
{
	public void Configure(EntityTypeBuilder<Receipt> builder)
	{
		builder.HasKey(p => new { p.Id });

		builder.HasMany(s => s.ReceiptItems);

		builder.ToTable("Receipt");
	}
}
