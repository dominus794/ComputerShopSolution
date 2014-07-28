using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using ComputerShop.Interfaces;

namespace ComputerShop.Repository
{
	public interface IRepository<TEntity> where TEntity : class, IEntity
	{
		// gets the entity by its id.
		TEntity GetByID(int id);
		// adds the entity to this repository, and returns it also (with its new id)
		void Add(TEntity entity);
		// removes the entity from the repository.
		void Remove(TEntity entity);
		// saves the modified entity to the repository.
		void Save(TEntity entity);

        // gets all the entities from the repository.
		IList<TEntity> GetAll();
        // searched for an entity.
		IQueryable<TEntity> Search(Expression<Func<TEntity, bool>> predicate);
	}
}
