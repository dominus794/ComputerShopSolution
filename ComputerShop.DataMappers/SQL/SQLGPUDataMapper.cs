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

    public class SQLGPUDataMapper : DataMapper<GPU>, IDisposable
    {
        #region Fields

        private GPUSQLTableDataGateway gpuTableGateway;
        private ManufacturerSQLTableDataGateway manufacturerTableGateway;

        #endregion

        #region Constructor
        public SQLGPUDataMapper(string connectionString)
        {
            gpuTableGateway = new GPUSQLTableDataGateway(connectionString);
            manufacturerTableGateway = new ManufacturerSQLTableDataGateway(connectionString);
        }
        #endregion

        #region IDataMapper<GPU> Members
        public override void Insert(GPU data)
        {
            gpuTableGateway.Insert(data.Manufacturer.ID, data.Model, data.Price, data.GpuModel, data.GpuClockSpeed, data.VRamSize, data.VRamClockSpeed, data.GddrVersion.ToString(), data.GpuType.ToString());                 
        }

        public override void Delete(GPU data)
        {
            gpuTableGateway.Delete(data.ID);
        }

        public override GPU Select(int id)
        {
            GPU gpu = null;

            GPUDataRecord record = gpuTableGateway.Select(id);
            ManufacturerDataRecord manufacturerRecord = manufacturerTableGateway.Select(record.ManufacturerID);

            gpu = new GPU(record.GpuID,
                            new Manufacturer(manufacturerRecord.ManufacturerID, manufacturerRecord.Name),
                            record.Model,
                            record.Price,
                            record.GpuModel,
                            record.GpuClockSpeed,
                            record.VRamSize,
                            record.VRamClockSpeed,
                            (GDDRVersion)Enum.Parse(typeof(GDDRVersion), record.VRamType),
                            (GPUType)Enum.Parse(typeof(GPUType), record.GpuType));

            return gpu;
        }

        public override void Update(GPU data)
        {
            gpuTableGateway.Update(data.ID, data.Manufacturer.ID, data.Model, data.Price, data.GpuModel, data.GpuClockSpeed, data.VRamSize, data.VRamClockSpeed, data.GddrVersion.ToString(), data.GpuType.ToString());
        }

        public override IList<GPU> GetAll()
        {
            IList<GPU> gpus = new List<GPU>();

            IList<GPUDataRecord> records = gpuTableGateway.GetAll();

            foreach (GPUDataRecord record in records)
            {
                ManufacturerDataRecord manufacturerRecord = manufacturerTableGateway.Select(record.ManufacturerID);

                GPU gpu = new GPU(record.GpuID,
                                    new Manufacturer(manufacturerRecord.ManufacturerID, manufacturerRecord.Name),
                                    record.Model,
                                    record.Price,
                                    record.GpuModel,
                                    record.GpuClockSpeed,
                                    record.VRamSize,
                                    record.VRamClockSpeed,
                                    (GDDRVersion)Enum.Parse(typeof(GDDRVersion), record.VRamType),
                                    (GPUType)Enum.Parse(typeof(GPUType), record.GpuType));

                gpus.Add(gpu);
            }

            return gpus;
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            gpuTableGateway.Dispose();
            manufacturerTableGateway.Dispose();
        }
        #endregion
    }
}
