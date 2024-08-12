using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ReceiptItemConfig : IEntityTypeConfiguration<ReceiptItem>
{
	public void Configure(EntityTypeBuilder<ReceiptItem> builder)
	{
		builder.HasKey(p => new { p.Id });

		builder.HasOne(s => s.Receipt);

		builder.ToTable("ReceiptItem");

		builder.Ignore(r => r.ProductName);
	}
}
