using Domain;
using Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
	//private readonly ICurrentUserService _currentUserService;

	public ApplicationDbContext(DbContextOptions options) : base(options)
	{
		//_currentUserService = currentUserService;
	}

	public DbSet<Product> Products { get; set; }
	public DbSet<Receipt> Receipts { get; set; }
	public DbSet<ReceiptItem> ReceiptItems { get; set; }
	public DbSet<ProductCategory> ProductCategories { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}

	/// <summary>
	/// Save changes in database
	/// </summary>
	/// <returns>affected rows</returns>
	public override int SaveChanges()
	{
		try
		{
			//SetAuditFields(ChangeTracker.Entries<IBaseEntity>());

			var result = base.SaveChanges();

			return result;
		}
		catch (DbUpdateConcurrencyException)
		{
			throw;
		}
		catch (Exception)
		{
			throw;
		}
	}

	/// <summary>
	/// Save changes in database async
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns>affected rows</returns>
	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		try
		{
			//SetAuditFields(ChangeTracker.Entries<IBaseEntity>());

			var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

			return result;
		}
		catch (DbUpdateConcurrencyException)
		{
			throw;
		}
		catch (Exception)
		{
			throw;
		}
	}

	/// <summary>
	/// set table audit fields
	/// </summary>
	/// <param name="entries"></param>
	private void SetAuditFields(IEnumerable<EntityEntry<IBaseEntity>> entries)
	{
		try
		{
			foreach (var entry in entries.ToList())
			{
				switch (entry.State)
				{
					case EntityState.Added:

						//entry.Entity.SetAddedFields(_currentUserService.UserId);
						break;

					case EntityState.Modified:

						entry.Property(x => x.CreatedBy).IsModified = false;
						entry.Property(x => x.CreatedOn).IsModified = false;

						//entry.Entity.SetUpdatedFields(_currentUserService.UserId);
						break;
				}
				//var validationContext = new ValidationContext(entry);
				//Validator.ValidateObject(entry, validationContext);
			}
		}
		catch (Exception)
		{
			throw;
		}
	}
}
