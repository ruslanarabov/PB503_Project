using System;
using PB503Project.Models;

namespace PB503Project.Repocitories.Interfaces
{
	public interface IGenericRepocitory<T> where T : BaseEntity , new()
	{
		void Add(T entity);

		List<T> GetAll();

		void Delete(T entity);

		void Commit();
	}
}

