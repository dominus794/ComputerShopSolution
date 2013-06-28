using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Transactions;

namespace ComputerShop.DataMappers.SQL
{
    using ComputerShop.Interfaces;
    using ComputerShop.Domain;
    using ComputerShop.DAL.SQL.TableDataGateways;
    using ComputerShop.DAL.SQL.DataRecords;

    public class SQLLaptopDataMapper : DataMapper<Laptop>, IDisposable
    {
        #region Fields
        // Access to the Laptops Table
        private LaptopSQLTableDataGateway laptopTableGateway;
        
        // Access to the LaptopRamModules Table.
        private LaptopRamModulesSQLTableDataGateway laptopRamModulesTableGateway;
        
        // Other required mappers
        SQLManufacturerDataMapper sqlManufacturerDataMapper;
        SQLCPUDataMapper sqlCPUDataMapper;
        SQLHDDDataMapper sqlHDDDataMapper;
        SQLGPUDataMapper sqlGPUDataMapper;
        SQLRamModuleDataMapper sqlRamModuleDataMapper;
        #endregion

        #region Constructor
        public SQLLaptopDataMapper(string connectionString)
        {
            laptopTableGateway = new LaptopSQLTableDataGateway(connectionString);            
            laptopRamModulesTableGateway = new LaptopRamModulesSQLTableDataGateway(connectionString);

            sqlManufacturerDataMapper = new SQLManufacturerDataMapper(connectionString);
            sqlCPUDataMapper = new SQLCPUDataMapper(connectionString);
            sqlHDDDataMapper = new SQLHDDDataMapper(connectionString);
            sqlGPUDataMapper = new SQLGPUDataMapper(connectionString);
            sqlRamModuleDataMapper = new SQLRamModuleDataMapper(connectionString);
        }
        #endregion

        #region IDataMapper<Laptop> Members
        public override void Insert(Laptop data)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                // insert the Laptops Table record.
                laptopTableGateway.Insert(data.Manufacturer.ID, data.Model, data.Price, data.Weight, data.BatteryLife, data.DisplaySize, data.Cpu.ID, data.Hdd.ID, data.Gpu.ID);

                // get the id for the new laptop.
                data.ID = laptopTableGateway.GetLatestID();

                foreach (KeyValuePair<IRamModule, int> modules in data.RamModuleCollection.RamModules)
                {
                    IRamModule module = modules.Key;
                    int quantity = modules.Value;

                    laptopRamModulesTableGateway.Insert(data.ID, module.ID, quantity);
                }

                transaction.Complete();
            }
        }

        public override void Delete(Laptop data)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                laptopTableGateway.Delete(data.ID);

                foreach (KeyValuePair<IRamModule, int> modules in data.RamModuleCollection.RamModules)
                {
                    IRamModule module = modules.Key;
                    int quantity = modules.Value;

                    laptopRamModulesTableGateway.Delete(data.ID);
                }

                transaction.Complete();
            }
        }

        public override Laptop Select(int id)
        {
            Laptop laptop = null;

            LaptopDataRecord laptopRecord = laptopTableGateway.Select(id);                        
            IList<LaptopRamModuleDataRecord> laptopRamModulesRecords = laptopRamModulesTableGateway.Select(id);

            IManufacturer laptopManufacturer = sqlManufacturerDataMapper.Select(laptopRecord.ManufacturerID);
            ICPU laptopCPU = sqlCPUDataMapper.Select(laptopRecord.CpuID);
            IHDD laptopHDD = sqlHDDDataMapper.Select(laptopRecord.HddID);
            IGPU laptopGPU = sqlGPUDataMapper.Select(laptopRecord.GpuID);

            IRamModuleCollection laptopRamModulesCollection = new RamModuleCollection();
            foreach (LaptopRamModuleDataRecord laptopRamModulesRecord in laptopRamModulesRecords)
            {
                laptopRamModulesCollection.AddRamModule(sqlRamModuleDataMapper.Select(laptopRamModulesRecord.RamID), laptopRamModulesRecord.Quantity);
            }   
         
            laptop = new Laptop(id,
                laptopManufacturer,
                laptopRecord.Model,
                laptopRecord.Price,
                laptopCPU,
                laptopRamModulesCollection,
                laptopGPU,
                laptopHDD,
                (byte)laptopRecord.Weight,
                (byte)laptopRecord.BatteryLife,
                laptopRecord.DisplaySize);

            return laptop;
        }

        public override void Update(Laptop data)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                // update the Laptops Table record.
                laptopTableGateway.Update(data.ID, data.Manufacturer.ID, data.Model, data.Price, data.Weight, data.BatteryLife, data.DisplaySize, data.Cpu.ID, data.Hdd.ID, data.Gpu.ID);
                
                // delete the existing laptopRamModuleRecords.
                laptopRamModulesTableGateway.Delete(data.ID);
                
                // insert each new one.
                foreach (KeyValuePair<IRamModule, int> modules in data.RamModuleCollection.RamModules)
                {
                    IRamModule module = modules.Key;
                    int quantity = modules.Value;
                    
                    laptopRamModulesTableGateway.Insert(data.ID, module.ID, quantity);
                }

                transaction.Complete();
            }
        }

        public override IList<Laptop> GetAll()
        {
            IList<Laptop> laptops = new List<Laptop>();

            IList<LaptopDataRecord> records = laptopTableGateway.GetAll();

            foreach (LaptopDataRecord laptopRecord in records)
            {
                IList<LaptopRamModuleDataRecord> laptopRamModulesRecords = laptopRamModulesTableGateway.Select(laptopRecord.LaptopID);

                IManufacturer laptopManufacturer = sqlManufacturerDataMapper.Select(laptopRecord.ManufacturerID);
                ICPU laptopCPU = sqlCPUDataMapper.Select(laptopRecord.CpuID);
                IHDD laptopHDD = sqlHDDDataMapper.Select(laptopRecord.HddID);
                IGPU laptopGPU = sqlGPUDataMapper.Select(laptopRecord.GpuID);

                IRamModuleCollection laptopRamModulesCollection = new RamModuleCollection();
                foreach (LaptopRamModuleDataRecord laptopRamModulesRecord in laptopRamModulesRecords)
                {
                    laptopRamModulesCollection.AddRamModule(sqlRamModuleDataMapper.Select(laptopRamModulesRecord.RamID), laptopRamModulesRecord.Quantity);
                }            

                Laptop laptop = new Laptop(laptopRecord.LaptopID,
                    laptopManufacturer,
                    laptopRecord.Model,
                    laptopRecord.Price,
                    laptopCPU,
                    laptopRamModulesCollection,
                    laptopGPU,
                    laptopHDD,
                    (byte)laptopRecord.Weight,
                    (byte)laptopRecord.BatteryLife,
                    laptopRecord.DisplaySize);

                laptops.Add(laptop);
            }

            return laptops;
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            laptopTableGateway.Dispose();            
            laptopRamModulesTableGateway.Dispose();

            sqlManufacturerDataMapper.Dispose();
            sqlCPUDataMapper.Dispose();
            sqlHDDDataMapper.Dispose();
            sqlGPUDataMapper.Dispose();
            sqlRamModuleDataMapper.Dispose();
        }
        #endregion
    }
}
