using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;

namespace ComputerShop.DataMappers.SQLDataSet
{
	public class SQLDataSetManufacturerDataMapperFactory : DataMapperFactory<Manufacturer>
	{
		public override IDataMapper<Manufacturer> CreateMapper()
		{
			return new SQLDataSetManufacturerDataMapper();
		}
	}

	public class SQLDataSetCPUDataMapperFactory : DataMapperFactory<CPU>
	{
		public override IDataMapper<CPU> CreateMapper()
		{
			return new SQLDataSetCPUDataMapper();
		}
	}

	public class SQLDataSetRamModuleDataMapperFactory : DataMapperFactory<RamModule>
	{
		public override IDataMapper<RamModule> CreateMapper()
		{
			return new SQLDataSetRamModuleDataMapper();
		}
	}

	public class SQLDataSetHDDDataMapperFactory : DataMapperFactory<HDD>
	{
		public override IDataMapper<HDD> CreateMapper()
		{
			return new SQLDataSetHDDDataMapper();
		}
	}

	public class SQLDataSetGPUDataMapperFactory : DataMapperFactory<GPU>
	{
		public override IDataMapper<GPU> CreateMapper()
		{
			return new SQLDataSetGPUDataMapper();
		}
	}

	public class SQLDataSetDesktopDataMapperFactory : DataMapperFactory<Desktop>
	{
		public override IDataMapper<Desktop> CreateMapper()
		{
			return new SQLDataSetDesktopDataMapper();
		}
	}

	public class SQLDataSetLaptopDataMapperFactory : DataMapperFactory<Laptop>
	{
		public override IDataMapper<Laptop> CreateMapper()
		{
			return new SQLDataSetLaptopDataMapper();
		}
	}
}
