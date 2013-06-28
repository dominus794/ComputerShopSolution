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

	public class SQLCPUDataMapper : DataMapper<CPU>, IDisposable
	{
		#region Fields

		private CPUSQLTableDataGateway cpuTableGateway;
		private ManufacturerSQLTableDataGateway manufacturerTableGateway;

		#endregion

		#region Constructor

		public SQLCPUDataMapper(string connectionString)
		{
			cpuTableGateway = new CPUSQLTableDataGateway(connectionString);
			manufacturerTableGateway = new ManufacturerSQLTableDataGateway(connectionString);
		}

		#endregion

		#region IDataMapper<Processor> Members

		/// <summary>
		/// Inserts a cpu database record represented by the Processor object.
		/// </summary>
		/// <param name="data">The Processor object that represents a cpu database record to be inserted.</param>
		public override void Insert(CPU data)
		{
			cpuTableGateway.Insert(data.Manufacturer.ID, data.Model, data.Price, data.ClockSpeed, data.CpuCoreType.ToString(), data.CpuFormFactor.ToString());			
		}

		/// <summary>
		/// Deletes a cpu database record represented by the Processor object.
		/// </summary>
		/// <param name="data">The Processor object that represents the cpu database record to be deleted.</param>
		public override void Delete(CPU data)
		{
			cpuTableGateway.Delete(data.ID);
		}

		/// <summary>
		/// Returns a CPU object representation of the cpu database record identified by id.
		/// </summary>
		/// <param name="id">The id of the cpu database record to be retrieved.</param>
		/// <returns>A CPU object representation of the cpu database record.</returns>
		public override CPU Select(int id)
		{
			CPU cpu = null;

			CPUDataRecord record = cpuTableGateway.Select(id);
			ManufacturerDataRecord manufacturerRow = manufacturerTableGateway.Select(record.ManufacturerID);

			cpu = new CPU(record.CpuID,
								new Manufacturer(manufacturerRow.ManufacturerID, manufacturerRow.Name),
								record.Model,
								record.Price,
								record.ClockSpeed,								
								(CPUFormFactor)Enum.Parse(typeof(CPUFormFactor), record.CpuFormFactor),
								(CPUCoreType)Enum.Parse(typeof(CPUCoreType), record.NoOfCores));

			return cpu;
		}

		/// <summary>
		/// Updates the cpu record represented by the Processor object.
		/// </summary>
		/// <param name="data">The Processor object that represents the cpu database record.</param>
		public override void Update(CPU data)
		{
			cpuTableGateway.Update(data.ID, data.Manufacturer.ID, data.Model, data.Price, data.ClockSpeed, data.CpuCoreType.ToString(), data.CpuFormFactor.ToString());
		}

		public override IList<CPU> GetAll()
		{
			IList<CPU> processors = new List<CPU>();

			IList<CPUDataRecord> records = cpuTableGateway.GetAll();

			foreach (CPUDataRecord record in records)
			{
				ManufacturerDataRecord manufacturerRow = manufacturerTableGateway.Select(record.ManufacturerID);

				CPU cpu = new CPU(record.CpuID,
											  new Manufacturer(manufacturerRow.ManufacturerID, manufacturerRow.Name),
											  record.Model,
											  record.Price,
											  record.ClockSpeed,
											  (CPUFormFactor)Enum.Parse(typeof(CPUFormFactor), record.CpuFormFactor),
											  (CPUCoreType)Enum.Parse(typeof(CPUCoreType), record.NoOfCores));

				processors.Add(cpu);
			}

			return processors;
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			cpuTableGateway.Dispose();
			manufacturerTableGateway.Dispose();
		}

		#endregion
	}
}
