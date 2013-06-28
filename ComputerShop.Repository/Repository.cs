using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using System.Configuration;

using ComputerShop.Interfaces;
using ComputerShop.Domain;
using ComputerShop.DataMappers;

namespace ComputerShop.Repository
{
	public class Repository<TEntity> : IRepository<TEntity>
	{		
		#region Fields
		
        protected IDataMapper<TEntity> dataMapper;

		#endregion


		#region Constructor

		public Repository(IDataMapperFactory<TEntity> mapperFactory)
		{			

			this.dataMapper = mapperFactory.CreateMapper();			
		}

		public Repository(IDataMapper<TEntity> mapper)
		{
			this.dataMapper = mapper;			
		}      

		#endregion


		#region IRepository<TEntity> Members

		public virtual TEntity GetByID(int id)
		{
			return dataMapper.Select(id);
		}

		public virtual void Add(TEntity entity)
		{
			dataMapper.Insert(entity);
		}

		public virtual void Remove(TEntity entity)
		{
			dataMapper.Delete(entity);
		}

		public virtual void Save(TEntity entity)
		{
			dataMapper.Update(entity);
		}

		public virtual IList<TEntity> GetAll()
		{
			return dataMapper.GetAll();
		}

		public IQueryable<TEntity> Search(Expression<Func<TEntity, bool>> predicate)
		{
			IList<TEntity> results = GetAll().ToList<TEntity>();

			return results.AsQueryable<TEntity>().Where<TEntity>(predicate);
		}

		#endregion
	}
}
