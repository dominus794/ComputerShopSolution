using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using ComputerShop.DAL.SQLDataSet;
using ComputerShop.DAL.SQLDataSet.ComputerShopDBTableAdapters;

namespace ComputerShop.DataMappers.SQLDataSet
{
	public abstract class SQLDataSetDataMapperBase<TEntity> : DataMapper<TEntity>
	{
		internal static SQLDataSetDatabase db = null;

		public SQLDataSetDataMapperBase()
		{
			db = SQLDataSetDatabase.Instance;
		}		

		#region Methods
		public abstract override void Insert(TEntity data);
		public abstract override void Delete(TEntity data);
		public abstract override TEntity Select(int id);
		public abstract override void Update(TEntity data);
		public abstract override IList<TEntity> GetAll();
		 
		#endregion
	}
}
