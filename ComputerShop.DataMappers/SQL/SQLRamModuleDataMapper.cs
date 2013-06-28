using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.DataMappers.SQL
{
    using ComputerShop.Interfaces;
    using ComputerShop.Domain;
    using ComputerShop.DAL.SQL.TableDataGateways;
    using ComputerShop.DAL.SQL.DataRecords;

    public class SQLRamModuleDataMapper : DataMapper<RamModule>, IDisposable
    {
        #region Fields

        private RamModuleSQLTableDataGateway ramModuleTableGateway;
        private ManufacturerSQLTableDataGateway manufacturerTableGateway;

        #endregion

        #region Constructor 
        public SQLRamModuleDataMapper(string connectionString)
        {
            ramModuleTableGateway = new RamModuleSQLTableDataGateway(connectionString);
            manufacturerTableGateway = new ManufacturerSQLTableDataGateway(connectionString);
        }
        #endregion

        #region IDataMapper<RamModule> Members

        /// <summary>
        /// Inserts a RamModule database record represented by the RamModule object.
        /// </summary>
        /// <param name="data">The RamModule object that represents a ram module database record to be inserted.</param>
        public override void Insert(RamModule data)
        {
            ramModuleTableGateway.Insert(data.Manufacturer.ID, data.Model, data.Price, data.ClockSpeed, data.Size, data.DDRVersion.ToString(), data.RamFormFactor.ToString());
        }

        /// <summary>
        /// Deletes a ramModule database record represented by the RamModule object.
        /// </summary>
        /// <param name="data">The RamModule object that represents the rammodule database record to be deleted.</param>
        public override void Delete(RamModule data)
        {
            ramModuleTableGateway.Delete(data.ID);
        }

        /// <summary>
        /// Returns a RamModule object representation of the rammodule database record identified by id.
        /// </summary>
        /// <param name="id">The id of the rammodule database record to be retrieved</param>
        /// <returns>A RamModule object representation of the rammodule database record.</returns>
        public override RamModule Select(int id)
        {
            RamModule ramModule = null;

            RamModuleDataRecord record = ramModuleTableGateway.Select(id);
            ManufacturerDataRecord manufacturerRecord = manufacturerTableGateway.Select(record.ManufacturerID);

            ramModule = new RamModule(record.RamModuleID,
                                        new Manufacturer(manufacturerRecord.ManufacturerID, manufacturerRecord.Name),
                                        record.Model,
                                        record.Price,
                                        record.ClockSpeed,
                                        (DDRVersion)Enum.Parse(typeof(DDRVersion), record.DDRVersion),
                                        (RAMFormFactor)Enum.Parse(typeof(RAMFormFactor), record.RamFormFactor),
                                        record.Size);

            return ramModule;
        }

        /// <summary>
        /// Updates the rammodule database record represented by the RamModule object.
        /// </summary>
        /// <param name="data">The RamModule object that represents the rammodule database record.</param>
        public override void Update(RamModule data)
        {
            ramModuleTableGateway.Update(data.ID, data.Manufacturer.ID, data.Model, data.Price, data.ClockSpeed, data.Size, data.DDRVersion.ToString(), data.RamFormFactor.ToString());
        }

        public override IList<RamModule> GetAll()
        {
            IList<RamModule> ramModules = new List<RamModule>();
            IList<RamModuleDataRecord> records = ramModuleTableGateway.GetAll();

            foreach (RamModuleDataRecord record in records)
            {
                ManufacturerDataRecord manufacturerRecord = manufacturerTableGateway.Select(record.ManufacturerID);

                RamModule ramModule = new RamModule(record.RamModuleID,
                                        new Manufacturer(manufacturerRecord.ManufacturerID, manufacturerRecord.Name),
                                        record.Model,
                                        record.Price,
                                        record.ClockSpeed,
                                        (DDRVersion)Enum.Parse(typeof(DDRVersion), record.DDRVersion),
                                        (RAMFormFactor)Enum.Parse(typeof(RAMFormFactor), record.RamFormFactor),
                                        record.Size);

                ramModules.Add(ramModule);
            }

            return ramModules;
        }

        #endregion

        public void Dispose()
        {
            ramModuleTableGateway.Dispose();
            manufacturerTableGateway.Dispose();
        }
    }
}
