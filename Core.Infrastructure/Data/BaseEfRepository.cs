using System;
using System.Collections.Generic;
using System.Linq;
using Durak.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Data;

public class BaseEfRepository<T> : IRepository<T> where T : class,IRootEntity
{
	protected readonly DbContext _dataContext;

	public BaseEfRepository(DbContext dataContext) => _dataContext = dataContext;

	public virtual List<T> GetAll() =>
		_dataContext.Set<T>().ToList();

	public virtual T? Get(int id) =>
		_dataContext.Set<T>().Find(id);

	public virtual int Delete(int id)
	{
		var entry =
			_dataContext.Set<T>().Find(id);

		if (entry == null)
			throw new ApplicationException("Object cant be found");

		_dataContext.Set<T>().Remove(entry);
		return _dataContext.SaveChanges();

	}

	public virtual int Delete(T entity)
	{
		_dataContext.Set<T>().Remove(entity);

		return _dataContext.SaveChanges();
	}

	public virtual int Add(T entity)
	{
		_dataContext.Set<T>().Add(entity);

		return _dataContext.SaveChanges();
	}

	public virtual int Update(T entity)
	{
		_dataContext.Set<T>().Update(entity);

		return _dataContext.SaveChanges();
	}
}
