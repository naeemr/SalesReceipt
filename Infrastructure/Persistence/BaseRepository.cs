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

	/// <summary>
	/// Adds new object async
	/// </summary>
	/// <param name="entity">object that need to be added</param>  
	/// <returns>Task.</returns>
	public async Task<TEntity> AddAsync(TEntity entity)
	{
		await _context.Set<TEntity>().AddAsync(entity)
			.ConfigureAwait(false);

		await _context.SaveChangesAsync();

		return entity;
	}

	/// <summary>
	/// Update entity partially
	/// </summary>
	/// <typeparam name="TEntity">entity type</typeparam>
	/// <param name="entity">object that need to be update partially</param>
	public void Attach(TEntity entity)
	{
		_context.Set<TEntity>().Attach(entity);
	}

	/// <summary>
	/// Find list of matching criteria
	/// </summary>
	/// <param name="predicate">This will contain the criteria</param>
	/// <returns>list of matching entities</returns>
	public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
	{
		var entities = _context.Set<TEntity>().Where(predicate);

		return await Task.FromResult(entities.ToList())
		   .ConfigureAwait(false);
	}

	/// <summary>
	/// Find First or Default
	/// </summary>
	/// <param name="predicate">This will contain the criteria</param>
	/// <returns>matching entity</returns>
	public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
	{
		var entities = _context.Set<TEntity>().Where(predicate);

		return await Task.FromResult(entities.FirstOrDefault())
		   .ConfigureAwait(false);
	}

	/// <summary>
	/// Gets all objects async
	/// </summary>
	/// <returns>List of objects</returns>
	public async Task<List<TEntity>> GetAllAsync()
	{
		var entities = _context.Set<TEntity>().ToList();

		return await Task.FromResult(entities)
			.ConfigureAwait(false);
	}

	/// <summary>
	/// Get object by id async
	/// </summary>
	/// <param name="id">primary key</param>
	/// <returns></returns>
	public async Task<TEntity> GetByIdAsync(int id)
	{
		var entity = _context.Set<TEntity>().Find(id);

		return await Task.FromResult(entity)
		   .ConfigureAwait(false);
	}

	/// <summary>
	/// Remove object async
	/// </summary>
	/// <param name="entity">object that need to be removed</param>
	public async Task DeleteAsync(TEntity entity)
	{
		_context.Set<TEntity>().Remove(entity);

		await _context.SaveChangesAsync();
	}

	/// <summary>
	/// Remove object async
	/// </summary>
	/// <param name="entity">object that need to be removed</param>
	public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
	{
		_context.Set<TEntity>().RemoveRange(entities);

		await _context.SaveChangesAsync();
	}

	/// <summary>
	/// update existing object
	/// </summary>
	/// <param name="entity">object that need to be updated</param>
	public async Task<TEntity> UpdateAsync(TEntity entity)
	{
		_context.Set<TEntity>().Update(entity);

		await _context.SaveChangesAsync();

		return entity;
	}
}
