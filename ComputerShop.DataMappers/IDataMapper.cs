using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ComputerShop.DataMappers
{
	public interface IDataMapper<TEntity>
	{
		void Insert(TEntity data);
		void Delete(TEntity data);
		TEntity Select(int id);
		void Update(TEntity data);

		IList<TEntity> GetAll();
		IQueryable<TEntity> Search(Expression<Func<TEntity, bool>> predicate);
	}
}
