using Application.Common.Interfaces;
using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Persistence;

public class BaseRepository<TEntity> : IBaseRepository<TEntity>
	where TEntity : class, IBaseEntity, IAggregateRoot
{
	protected readonly ApplicationDbContext _context;

	public BaseRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<TEntity> AddAsync(TEntity entity)
	{
		await _context.Set<TEntity>().AddAsync(entity)
			.ConfigureAwait(false);

		await _context.SaveChangesAsync();

		return entity;
	}

	public async Task<TEntity> UpdateAsync(TEntity entity)
	{
		_context.Set<TEntity>().Update(entity);

		await _context.SaveChangesAsync();

		return entity;
	}

	public async Task DeleteAsync(TEntity entity)
	{
		_context.Set<TEntity>().Remove(entity);

		await _context.SaveChangesAsync();
	}

	public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
	{
		_context.Set<TEntity>().RemoveRange(entities);

		await _context.SaveChangesAsync();
	}

	public void Attach(TEntity entity)
	{
		_context.Set<TEntity>().Attach(entity);
	}

	public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
	{
		var entities = _context.Set<TEntity>().Where(predicate);

		return await Task.FromResult(entities.ToList())
		   .ConfigureAwait(false);
	}

	public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
	{
		var entities = _context.Set<TEntity>().Where(predicate);

		return await Task.FromResult(entities.FirstOrDefault())
		   .ConfigureAwait(false);
	}

	public async Task<List<TEntity>> GetAllAsync()
	{
		var entities = _context.Set<TEntity>().ToList();

		return await Task.FromResult(entities)
			.ConfigureAwait(false);
	}

	public async Task<TEntity> GetByIdAsync(int id)
	{
		var entity = _context.Set<TEntity>().Find(id);

		return await Task.FromResult(entity)
		   .ConfigureAwait(false);
	}
}
