using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;

namespace ComputerShop.DataMappers
{	
	public interface IDataMapperFactory<TEntity>
	{
		IDataMapper<TEntity> CreateMapper();      
	}
}
