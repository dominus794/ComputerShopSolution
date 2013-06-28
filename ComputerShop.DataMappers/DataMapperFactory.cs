using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;

using ComputerShop.Interfaces;
using ComputerShop.Domain;
using ComputerShop.DataMappers;
using ComputerShop.DataMappers.SQL;
using ComputerShop.DataMappers.SQLDataSet;
using ComputerShop.DataMappers.Linq2Sql;

using ComputerShop.DAL;
using ComputerShop.DAL.SQL;

namespace ComputerShop.DataMappers
{
    public enum DataMapperType { SQL, SQLDataSet, Linq2Sql };

    public abstract class DataMapperFactory<TEntity> : IDataMapperFactory<TEntity>
    {
        #region IDataMapperFactory<TEntity> Members

        public abstract IDataMapper<TEntity> CreateMapper();       

        #endregion
    }

    #region Manufacturer DataMapperFactory
    public class ManufacturerDataMapperFactory : DataMapperFactory<Manufacturer>
    {
        public override IDataMapper<Manufacturer> CreateMapper()
        {
            DataMapperType dataMapperType = (DataMapperType)Enum.Parse(typeof(DataMapperType), ConfigurationManager.AppSettings["DataMapperType"]);

            IDataMapper<Manufacturer> dataMapper = null;

            switch (dataMapperType)
            {
                case DataMapperType.SQL:
                    dataMapper = new SQLManufacturerDataMapper(ConfigurationManager.ConnectionStrings["ComputerShopConnectionString"].ConnectionString);
                    break;
                case DataMapperType.SQLDataSet:
                    dataMapper = new SQLDataSetManufacturerDataMapper();
                    break;
                case DataMapperType.Linq2Sql:
                    dataMapper = new Linq2SqlManufacturerDataMapper();
                    break;
            }

            return dataMapper;
        }
    }
    #endregion

    #region CPU DataMapperFactory
    public class CPUDataMapperFactory : DataMapperFactory<CPU>
    {
        public override IDataMapper<CPU> CreateMapper()
        {
            DataMapperType dataMapperType = (DataMapperType)Enum.Parse(typeof(DataMapperType), ConfigurationManager.AppSettings["DataMapperType"]);

            IDataMapper<CPU> dataMapper = null;

            switch (dataMapperType)
            {
                case DataMapperType.SQL:
                    dataMapper = new SQLCPUDataMapper(ConfigurationManager.ConnectionStrings["ComputerShopConnectionString"].ConnectionString);
                    break;
                case DataMapperType.SQLDataSet:
                    dataMapper = new SQLDataSetCPUDataMapper();
                    break;
                case DataMapperType.Linq2Sql:
                    dataMapper = new Linq2SqlCPUDataMapper();
                    break;
            }

            return dataMapper;
        }
    }
    #endregion

    #region RamModule DataMapperFactory
    public class RamModuleDataMapperFactory : DataMapperFactory<RamModule>
    {
        public override IDataMapper<RamModule> CreateMapper()
        {
            DataMapperType dataMapperType = (DataMapperType)Enum.Parse(typeof(DataMapperType), ConfigurationManager.AppSettings["DataMapperType"]);

            IDataMapper<RamModule> dataMapper = null;

            switch (dataMapperType)
            {
                case DataMapperType.SQL:
                    dataMapper = new SQLRamModuleDataMapper(ConfigurationManager.ConnectionStrings["ComputerShopConnectionString"].ConnectionString);
                    break;
                case DataMapperType.SQLDataSet:
                    dataMapper = new SQLDataSetRamModuleDataMapper();
                    break;
                case DataMapperType.Linq2Sql:
                    dataMapper = new Linq2SqlRamModuleDataMapper();
                    break;
            }

            return dataMapper;
        }
    }
    #endregion

    #region HDD DataMapperFactory
    public class HDDDataMapperFactory : DataMapperFactory<HDD>
    {
        public override IDataMapper<HDD> CreateMapper()
        {
            DataMapperType dataMapperType = (DataMapperType)Enum.Parse(typeof(DataMapperType), ConfigurationManager.AppSettings["DataMapperType"]);

            IDataMapper<HDD> dataMapper = null;

            switch (dataMapperType)
            {
                case DataMapperType.SQL:
                    dataMapper = new SQLHDDDataMapper(ConfigurationManager.ConnectionStrings["ComputerShopConnectionString"].ConnectionString);
                    break;
                case DataMapperType.SQLDataSet:
                    dataMapper = new SQLDataSetHDDDataMapper();
                    break;
                case DataMapperType.Linq2Sql:
                    dataMapper = new Linq2SqlHDDDataMapper();
                    break;
            }

            return dataMapper;
        }
    }
    #endregion

    #region GPU DataMapperFactory
    public class GPUDataMapperFactory : DataMapperFactory<GPU>
    {
        public override IDataMapper<GPU> CreateMapper()
        {
            DataMapperType dataMapperType = (DataMapperType)Enum.Parse(typeof(DataMapperType), ConfigurationManager.AppSettings["DataMapperType"]);

            IDataMapper<GPU> dataMapper = null;

            switch (dataMapperType)
            {
                case DataMapperType.SQL:
                    dataMapper = new SQLGPUDataMapper(ConfigurationManager.ConnectionStrings["ComputerShopConnectionString"].ConnectionString);
                    break;
                case DataMapperType.SQLDataSet:
                    dataMapper = new SQLDataSetGPUDataMapper();
                    break;
                case DataMapperType.Linq2Sql:
                    dataMapper = new Linq2SqlGPUDataMapper();
                    break;
            }

            return dataMapper;
        }
    }
    #endregion

    #region Desktop DataMapperFactory
    public class DesktopDataMapperFactory : DataMapperFactory<Desktop>
    {
        public override IDataMapper<Desktop> CreateMapper()
        {
            DataMapperType dataMapperType = (DataMapperType)Enum.Parse(typeof(DataMapperType), ConfigurationManager.AppSettings["DataMapperType"]);

            IDataMapper<Desktop> dataMapper = null;

            switch (dataMapperType)
            {
                case DataMapperType.SQL:
                    dataMapper = new SQLDesktopDataMapper(ConfigurationManager.ConnectionStrings["ComputerShopConnectionString"].ConnectionString);
                    break;
                case DataMapperType.SQLDataSet:
                    dataMapper = new SQLDataSetDesktopDataMapper();
                    break;
                case DataMapperType.Linq2Sql:
                    dataMapper = new Linq2SqlDesktopDataMapper();
                    break;
            }

            return dataMapper;
        }
    }
    #endregion

    #region Laptop DataMapperFactory
    public class LaptopDataMapperFactory : DataMapperFactory<Laptop>
    {
        public override IDataMapper<Laptop> CreateMapper()
        {
            DataMapperType dataMapperType = (DataMapperType)Enum.Parse(typeof(DataMapperType), ConfigurationManager.AppSettings["DataMapperType"]);

            IDataMapper<Laptop> dataMapper = null;

            switch (dataMapperType)
            {
                case DataMapperType.SQL:
                    dataMapper = new SQLLaptopDataMapper(ConfigurationManager.ConnectionStrings["ComputerShopConnectionString"].ConnectionString);
                    break;
                case DataMapperType.SQLDataSet:
                    dataMapper = new SQLDataSetLaptopDataMapper();
                    break;
                case DataMapperType.Linq2Sql:
                    dataMapper = new Linq2SqlLaptopDataMapper();
                    break;
            }

            return dataMapper;
        }
    }
    #endregion
}
