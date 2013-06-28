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

    public class SQLDesktopDataMapper : DataMapper<Desktop>, IDisposable
    {
        #region Fields
        // Access to the Desktops Table
        private DesktopSQLTableDataGateway desktopTableGateway;
        // Access to the DesktopsRamModules Table
        private DesktopRamModulesSQLTableDataGateway desktopRamModulesTableGateway;
        // Access to the DesktopHDDs Table
        private DesktopHDDsSQLTableDataGateway desktopHDDsTableGateway;
        // Access to the DesktopGPUs Table
        private DesktopGPUsSQLTableDataGateway desktopGPUsTableGateway;

        //Other Required Mappers
        SQLManufacturerDataMapper sqlManufacturerDataMapper;
        SQLCPUDataMapper sqlCPUDataMapper;
        SQLHDDDataMapper sqlHDDDataMapper;
        SQLGPUDataMapper sqlGPUDataMapper;
        SQLRamModuleDataMapper sqlRamModuleDataMapper;
        #endregion

        #region Constructor
        public SQLDesktopDataMapper(string connectionString)
        {
            desktopTableGateway = new DesktopSQLTableDataGateway(connectionString);
            desktopRamModulesTableGateway = new DesktopRamModulesSQLTableDataGateway(connectionString);
            desktopHDDsTableGateway = new DesktopHDDsSQLTableDataGateway(connectionString);
            desktopGPUsTableGateway = new DesktopGPUsSQLTableDataGateway(connectionString);

            sqlManufacturerDataMapper = new SQLManufacturerDataMapper(connectionString);
            sqlCPUDataMapper = new SQLCPUDataMapper(connectionString);
            sqlHDDDataMapper = new SQLHDDDataMapper(connectionString);
            sqlGPUDataMapper = new SQLGPUDataMapper(connectionString);
            sqlRamModuleDataMapper = new SQLRamModuleDataMapper(connectionString);
        }
        #endregion

        #region IDataMapper<Desktop> Members
        public override void Insert(Desktop data)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                // insert the Desktops Table record.
                desktopTableGateway.Insert(data.Manufacturer.ID, data.Model, data.Price, data.Cpu.ID);

                // get the id for the new desktop record.
                data.ID = desktopTableGateway.GetLatestID();

                // insert this desktop's ram modules into the DesktopRamModules table.
                foreach (KeyValuePair<IRamModule, int> modules in data.RamModuleCollection.RamModules)
                {
                    IRamModule module = modules.Key;
                    int quantity = modules.Value;

                    desktopRamModulesTableGateway.Insert(data.ID, module.ID, quantity);
                }

                // insert this desktop's hdd's into the DesktopHDDs table.
                foreach (KeyValuePair<IHDD, int> hdds in data.HddCollection.HDDs)
                {
                    IHDD hdd = hdds.Key;
                    int quantity = hdds.Value;

                    desktopHDDsTableGateway.Insert(data.ID, hdd.ID, quantity);
                }

                // insert this desktop's gpu's into the DesktopGPUs table
                foreach (KeyValuePair<IGPU, int> gpus in data.GpuCollection.GPUs)
                {
                    IGPU gpu = gpus.Key;
                    int quantity = gpus.Value;

                    desktopGPUsTableGateway.Insert(data.ID, gpu.ID, quantity);
                }

                transaction.Complete();
            }
        }

        public override void Delete(Desktop data)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                // remove this desktops gpus from DesktopGPUs table.
                foreach (KeyValuePair<IGPU, int> gpus in data.GpuCollection.GPUs)
                {
                    IGPU gpu = gpus.Key;
                    int quantity = gpus.Value;

                    desktopGPUsTableGateway.Delete(data.ID);
                }

                // remove this desktops hdds from DesktopHDDs table.
                foreach (KeyValuePair<IHDD, int> hdds in data.HddCollection.HDDs)
                {
                    IHDD hdd = hdds.Key;
                    int quantity = hdds.Value;

                    desktopHDDsTableGateway.Delete(data.ID);
                }

                // remove this desktops ram modules from DesktopRamModules table.
                foreach (KeyValuePair<IRamModule, int> modules in data.RamModuleCollection.RamModules)
                {
                    IRamModule module = modules.Key;
                    int quantity = modules.Value;

                    desktopRamModulesTableGateway.Delete(data.ID);
                }

                // finally delete the desktop from the Desktops table.
                desktopTableGateway.Delete(data.ID);

                transaction.Complete();
            }
        }

        public override Desktop Select(int id)
        {
            Desktop desktop = null;

            // Get the database records, from the Desktop, DesktopRamModules, DesktopHDDs, DesktopGPUs tables.
            DesktopDataRecord desktopRecord = desktopTableGateway.Select(id);
            IList<DesktopRamModuleDataRecord> desktopRamModulesRecords = desktopRamModulesTableGateway.Select(id);
            IList<DesktopHDDDataRecord> desktopHDDsRecords = desktopHDDsTableGateway.Select(id);
            IList<DesktopGPUDataRecord> desktopGPUsRecords = desktopGPUsTableGateway.Select(id);

            // Instantiate a Manufacturer object.
            IManufacturer desktopManufacturer = sqlManufacturerDataMapper.Select(desktopRecord.ManufacturerID);
            // Instantiate a CPU object.
            ICPU desktopCPU = sqlCPUDataMapper.Select(desktopRecord.CpuID);

            // Populate this dekstop's RamModuleCollection
            IRamModuleCollection desktopRamModulesCollection = new RamModuleCollection();
            foreach (DesktopRamModuleDataRecord desktopRamModuleRecord in desktopRamModulesRecords)
            {
                desktopRamModulesCollection.AddRamModule(sqlRamModuleDataMapper.Select(desktopRamModuleRecord.RamID), desktopRamModuleRecord.Quantity);
            }

            // Populate this desktop's HDDCollection
            IHDDCollection desktopHDDsCollection = new HDDCollection();
            foreach (DesktopHDDDataRecord desktopHDDRecord in desktopHDDsRecords)
            {
                desktopHDDsCollection.AddHDD(sqlHDDDataMapper.Select(desktopHDDRecord.HddID), desktopHDDRecord.Quantity);
            }

            // Populate this desktop's GPUCollection
            IGPUCollection desktopGPUsCollection = new GPUCollection();
            foreach (DesktopGPUDataRecord desktopGPURecord in desktopGPUsRecords)
            {
                desktopGPUsCollection.AddGPU(sqlGPUDataMapper.Select(desktopGPURecord.GpuID), desktopGPURecord.Quantity);
            }

            // Create the Desktop object.
            desktop = new Desktop(id,
                desktopManufacturer,
                desktopRecord.Model,
                desktopRecord.Price,
                desktopCPU,
                desktopRamModulesCollection,
                desktopHDDsCollection,
                desktopGPUsCollection);

            return desktop;
        }

        public override void Update(Desktop data)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                // update the Desktop table record.
                desktopTableGateway.Update(data.ID, data.Manufacturer.ID, data.Model, data.Price, data.Cpu.ID);

                // delete the existing DesktopRamModules records for this desktop.
                desktopRamModulesTableGateway.Delete(data.ID);
                // delete the existing DesktopHDDs records for this desktop.
                desktopHDDsTableGateway.Delete(data.ID);
                // delete the exiting DesktopGPUs records for this desktop
                desktopGPUsTableGateway.Delete(data.ID);

                // insert the new desktop's ram modules into the DesktopRamModules table.
                foreach (KeyValuePair<IRamModule, int> modules in data.RamModuleCollection.RamModules)
                {
                    IRamModule module = modules.Key;
                    int quantity = modules.Value;

                    desktopRamModulesTableGateway.Insert(data.ID, module.ID, quantity);
                }

                // insert the new desktop's hdd's into the DesktopHDDs table.
                foreach (KeyValuePair<IHDD, int> hdds in data.HddCollection.HDDs)
                {
                    IHDD hdd = hdds.Key;
                    int quantity = hdds.Value;

                    desktopHDDsTableGateway.Insert(data.ID, hdd.ID, quantity);
                }

                // insert the new desktop's gpu's into the DesktopGPUs table
                foreach (KeyValuePair<IGPU, int> gpus in data.GpuCollection.GPUs)
                {
                    IGPU gpu = gpus.Key;
                    int quantity = gpus.Value;

                    desktopGPUsTableGateway.Insert(data.ID, gpu.ID, quantity);
                }

                transaction.Complete();
            }
        }

        public override IList<Desktop> GetAll()
        {
            IList<Desktop> desktops = new List<Desktop>();

            IList<DesktopDataRecord> records = desktopTableGateway.GetAll();

            foreach (DesktopDataRecord desktopRecord in records)
            {
                IList<DesktopRamModuleDataRecord> desktopRamModulesRecords = desktopRamModulesTableGateway.Select(desktopRecord.DesktopID);
                IList<DesktopHDDDataRecord> desktopHDDsRecords = desktopHDDsTableGateway.Select(desktopRecord.DesktopID);
                IList<DesktopGPUDataRecord> desktopGPUsRecords = desktopGPUsTableGateway.Select(desktopRecord.DesktopID);

                // Instantiate a Manufacturer object.
                IManufacturer desktopManufacturer = sqlManufacturerDataMapper.Select(desktopRecord.ManufacturerID);
                // Instantiate a CPU object.
                ICPU desktopCPU = sqlCPUDataMapper.Select(desktopRecord.CpuID);

                // Populate this dekstop's RamModuleCollection
                IRamModuleCollection desktopRamModulesCollection = new RamModuleCollection();
                foreach (DesktopRamModuleDataRecord desktopRamModuleRecord in desktopRamModulesRecords)
                {
                    desktopRamModulesCollection.AddRamModule(sqlRamModuleDataMapper.Select(desktopRamModuleRecord.RamID), desktopRamModuleRecord.Quantity);
                }

                // Populate this desktop's HDDCollection
                IHDDCollection desktopHDDsCollection = new HDDCollection();
                foreach (DesktopHDDDataRecord desktopHDDRecord in desktopHDDsRecords)
                {
                    desktopHDDsCollection.AddHDD(sqlHDDDataMapper.Select(desktopHDDRecord.HddID), desktopHDDRecord.Quantity);
                }

                // Populate this desktop's GPUCollection
                IGPUCollection desktopGPUsCollection = new GPUCollection();
                foreach (DesktopGPUDataRecord desktopGPURecord in desktopGPUsRecords)
                {
                    desktopGPUsCollection.AddGPU(sqlGPUDataMapper.Select(desktopGPURecord.GpuID), desktopGPURecord.Quantity);
                }

                // Create the Desktop object.
                Desktop desktop = new Desktop(desktopRecord.DesktopID,
                    desktopManufacturer,
                    desktopRecord.Model,
                    desktopRecord.Price,
                    desktopCPU,
                    desktopRamModulesCollection,
                    desktopHDDsCollection,
                    desktopGPUsCollection);

                desktops.Add(desktop);
            }

            return desktops;
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            desktopTableGateway.Dispose();
            desktopRamModulesTableGateway.Dispose();
            desktopHDDsTableGateway.Dispose();
            desktopGPUsTableGateway.Dispose();

            sqlManufacturerDataMapper.Dispose();
            sqlCPUDataMapper.Dispose();
            sqlHDDDataMapper.Dispose();
            sqlGPUDataMapper.Dispose();
            sqlRamModuleDataMapper.Dispose();
        }
        #endregion
    }
}
