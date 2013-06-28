using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;

namespace ComputerShop.DataMappers.XML
{
	public abstract class XMLDataMapperBase<TEntity> : DataMapper<TEntity>
	{				
		public abstract override void Insert(TEntity data);
		public abstract override void Delete(TEntity data);
		public abstract override TEntity Select(int id);
		public abstract override void Update(TEntity data);
		public abstract override IList<TEntity> GetAll();
	}
}
