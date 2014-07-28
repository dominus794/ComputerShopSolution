using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.DAL.Linq2Sql;

namespace ComputerShop.DataMappers.Linq2Sql
{
	public abstract class Linq2SqlDataMapperBase<TEntity> : DataMapper<TEntity>
	{
		protected ComputerShopDataClassesDataContext context;		

		public Linq2SqlDataMapperBase(string connectionString)
		{
            context = new ComputerShopDataClassesDataContext(connectionString);
		}

		public abstract override void Insert(TEntity data);
		public abstract override void Delete(TEntity data);	
		public abstract override TEntity Select(int id);
		public abstract override void Update(TEntity data);
		public abstract override IList<TEntity> GetAll();
	}
}
