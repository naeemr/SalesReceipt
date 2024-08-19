using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ReceiptItemConfig : IEntityTypeConfiguration<ReceiptItem>
{
	public void Configure(EntityTypeBuilder<ReceiptItem> builder)
	{
		builder.HasKey(p => new { p.Id });

		builder.HasOne(x => x.Receipt)
			.WithMany(x => x.ReceiptItems)
			.HasForeignKey(x => x.ReceiptId);

		builder.HasOne(x => x.Product)
			.WithMany(x => x.ReceiptItems)
			.HasForeignKey(x => x.ProductId);

		builder.OwnsOne(r => r.SalesTax, tax =>
		{
			tax.Property(t => t.TaxRate).HasColumnName("TaxRate");
			tax.Property(t => t.Amount).HasColumnName("TaxAmount");
		});

		builder.ToTable("ReceiptItem");

		builder.Ignore(r => r.ProductName);
	}
}
