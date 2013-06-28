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

    public class SQLHDDDataMapper : DataMapper<HDD>, IDisposable
    {
        #region Fields

        private HDDSQLTableDataGateway hddTableGateway;
        private ManufacturerSQLTableDataGateway manufacturerTableGateway;

        #endregion

        #region Constructor

        public SQLHDDDataMapper(string connectionString)
        {
            hddTableGateway = new HDDSQLTableDataGateway(connectionString);
            manufacturerTableGateway = new ManufacturerSQLTableDataGateway(connectionString);
        }

        #endregion

        #region IDataMapper<HDD> Members

        public override void Insert(HDD data)
        {
            hddTableGateway.Insert(data.Manufacturer.ID, data.Model, data.Price, data.Capacity, data.Speed, data.HddType.ToString(), data.HddFormFactor.ToString(), data.HddInterfaceType.ToString());
        }

        public override void Delete(HDD data)
        {
            hddTableGateway.Delete(data.ID);
        }

        public override HDD Select(int id)
        {
            HDD hdd = null;

            HDDDataRecord record = hddTableGateway.Select(id);
            ManufacturerDataRecord manufacturerRecord = manufacturerTableGateway.Select(record.ManufacturerID);

            hdd = new HDD(record.HddID,
                            new Manufacturer(manufacturerRecord.ManufacturerID, manufacturerRecord.Name),
                            record.Model,
                            record.Price,
                            record.Capacity,
                            (HDDFormFactor)Enum.Parse(typeof(HDDFormFactor), record.HDDFormFactor),
                            (HDDInterfaceType)Enum.Parse(typeof(HDDInterfaceType), record.HDDInterface),
                            (HDDType)Enum.Parse(typeof(HDDType), record.HDDType),
                            record.Speed);

            return hdd;
        }

        public override void Update(HDD data)
        {
            hddTableGateway.Update(data.ID, data.Manufacturer.ID, data.Model, data.Price, data.Capacity, data.Speed, data.HddType.ToString(), data.HddFormFactor.ToString(), data.HddInterfaceType.ToString());
        }

        public override IList<HDD> GetAll()
        {
            IList<HDD> hdds = new List<HDD>();

            IList<HDDDataRecord> records = hddTableGateway.GetAll();

            foreach (HDDDataRecord record in records)
            {
                ManufacturerDataRecord manufacturerRecord = manufacturerTableGateway.Select(record.ManufacturerID);
                
                HDD hdd = new HDD(record.HddID,
                                new Manufacturer(manufacturerRecord.ManufacturerID, manufacturerRecord.Name),
                                record.Model,
                                record.Price,
                                record.Capacity,
                                (HDDFormFactor)Enum.Parse(typeof(HDDFormFactor), record.HDDFormFactor),
                                (HDDInterfaceType)Enum.Parse(typeof(HDDInterfaceType), record.HDDInterface),
                                (HDDType)Enum.Parse(typeof(HDDType), record.HDDType),
                                record.Speed);

                hdds.Add(hdd);
            }

            return hdds;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            hddTableGateway.Dispose();
            manufacturerTableGateway.Dispose();
        }

        #endregion
    }
}
