using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.DataMappers.SQL
{
	using ComputerShop.Domain;
    using ComputerShop.DAL.SQL.TableDataGateways;
    using ComputerShop.DAL.SQL.DataRecords;

	public class SQLManufacturerDataMapper : DataMapper<Manufacturer>, IDisposable
	{
		#region Fields

		ManufacturerSQLTableDataGateway manufacturerTableGateway;

		#endregion


		#region Constructor

		public SQLManufacturerDataMapper(string connectionString)
		{
			manufacturerTableGateway = new ManufacturerSQLTableDataGateway(connectionString);
		}

		#endregion


		#region IDataMapper<Manufacturer> Members

		/// <summary>
		/// Inserts a manufacturer database record represented by a Manufacturer object.
		/// </summary>
		/// <param name="data">The Manufacturer object that represents the manufacturer database record to be inserted.</param>		
		public override void Insert(Manufacturer data)
		{
			manufacturerTableGateway.Insert(data.Name);
		}

		/// <summary>
		/// Deletes a manufacturer database record represented by the Manufacturer object.
		/// </summary>
		/// <param name="data">The Manufacturer object that represents the manufacturer database record.</param>
		public override void Delete(Manufacturer data)
		{
			manufacturerTableGateway.Delete(data.ID);
		}

		/// <summary>
		/// Selects the manufacturer record identified by id, and returns a Manufacturer object representation of it.
		/// </summary>
		/// <param name="id">The id of the manufacturer record we wish to retrieve from the database.</param>
		/// <returns>A Manufacturer object that represents this manufacturer database record.</returns>
		public override Manufacturer Select(int id)
		{
			ManufacturerDataRecord record = manufacturerTableGateway.Select(id);

			return new Manufacturer(record.ManufacturerID, record.Name);
		}

		/// <summary>
		/// Updates the manufacturer database record represented by a Manufacturer object.
		/// </summary>
		/// <param name="data">The Manufacturer object representation of the manufacturer database record we are updating.</param>
		public override void Update(Manufacturer data)
		{
			manufacturerTableGateway.Update(data.ID, data.Name);
		}

        /// <summary>
        /// Returns a List of all Manufacturer objects from the Manufacturers database table.
        /// </summary>
        /// <returns>A List of Manufacturer objects.</returns>
		public override IList<Manufacturer> GetAll()
		{
			IList<ManufacturerDataRecord> manufacturerRecords = manufacturerTableGateway.GetAll();
			IList<Manufacturer> manufacturers = new List<Manufacturer>();

			foreach (ManufacturerDataRecord manufacturerRecord in manufacturerRecords)
			{
				Manufacturer manufacturer = new Manufacturer(manufacturerRecord.ManufacturerID, manufacturerRecord.Name);
				manufacturers.Add(manufacturer);
			}

			return manufacturers;
		}

		#endregion


		#region IDisposable Members

		public void Dispose()
		{
			manufacturerTableGateway.Dispose();
		}

		#endregion
	}
}
