using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ComputerShop.DataMappers
{
    /// <summary>
    /// Represents an abstract DataMapper object, which is an object that maps domain
    /// entities to database records.
    /// </summary>
    /// <typeparam name="TEntity">The type of domain object to be mapped to/from the database.</typeparam>
	public abstract class DataMapper<TEntity> : IDataMapper<TEntity>
	{
		#region IDataMapper<TEntity> Members

		public abstract void Insert(TEntity data);
		public abstract void Delete(TEntity data);
		public abstract TEntity Select(int id);
		public abstract void Update(TEntity data);
		public abstract IList<TEntity> GetAll();

		public IQueryable<TEntity> Search(Expression<Func<TEntity, bool>> predicate)
		{
			IList<TEntity> results = GetAll().ToList<TEntity>();

			return results.AsQueryable<TEntity>().Where<TEntity>(predicate);
		}

		#endregion
	}
}
