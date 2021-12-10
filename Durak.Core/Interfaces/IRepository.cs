using System.Collections.Generic;

namespace Durak.Core.Interfaces;

public interface IRepository<T>
{
	List<T> GetAll();
	T? Get(int id);
	T? Get(Predicate<T> filter);

	int Delete(int id);
	int Delete(T entity);
	int Add(T entity);

	int Update(T entity);
}
