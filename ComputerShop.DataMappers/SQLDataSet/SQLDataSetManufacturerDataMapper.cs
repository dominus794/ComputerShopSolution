using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using ComputerShop.Interfaces;
using ComputerShop.Domain;
using ComputerShop.DAL.SQLDataSet;
using ComputerShop.DAL.SQLDataSet.ComputerShopDBTableAdapters;

namespace ComputerShop.DataMappers.SQLDataSet
{
	class SQLDataSetManufacturerDataMapper : SQLDataSetDataMapperBase<Manufacturer>
	{
		//private ManufacturersTableAdapter manufacturersTableAdapter;

		public SQLDataSetManufacturerDataMapper()			
		{
			//manufacturersTableAdapter = new ManufacturersTableAdapter();
			//manufacturersTableAdapter.Fill(shopDB.Manufacturers);			
		}

		public override void Insert(Manufacturer data)
		{
			try
			{
				//add a new row to the dataset's Manufacturers datatable.
				db.shopDB.Manufacturers.AddManufacturersRow(data.Name);
				//update the database.
				db.manufacturersTableAdapter.Update(db.shopDB.Manufacturers);
			}
			catch (Exception ex)
			{
				db.shopDB.Manufacturers.RejectChanges();
				Debug.WriteLine(ex.Message);			
				throw;
			}
		}

		public override void Delete(Manufacturer data)
		{
			try
			{
				//locate the row to delete, and delete it.
				db.shopDB.Manufacturers.FindByManufacturerID(data.ID).Delete();
				//delete the row from the database
				db.manufacturersTableAdapter.Update(db.shopDB.Manufacturers);
			}
			catch (Exception ex)
			{
				db.shopDB.Manufacturers.RejectChanges();
				Debug.WriteLine(ex.Message);
				throw;
			}
		}

		public override Manufacturer Select(int id)
		{
			//get the row by id.
			ComputerShopDB.ManufacturersRow row = db.shopDB.Manufacturers.FindByManufacturerID(id);
			//create a Manufacturer object with the row data.
			return new Manufacturer(row.ManufacturerID, row.Name);			
		}

		public override void Update(Manufacturer data)
		{
			try
			{
				//update the DataRow in the dataset
				db.shopDB.Manufacturers.FindByManufacturerID(data.ID).Name = data.Name;
				db.manufacturersTableAdapter.Update(db.shopDB.Manufacturers);
			}
			catch (Exception ex)
			{
				db.shopDB.Manufacturers.RejectChanges();
				Debug.WriteLine(ex.Message);
				throw;
			}
		}

		public override IList<Manufacturer> GetAll()
		{
			//use Linq to DataSet to create a list of Manufacturer objects
			IList<Manufacturer> manufacturers = (from m in db.shopDB.Manufacturers
												 orderby m.Name
												 select new Manufacturer(m.ManufacturerID, m.Name)).ToList<Manufacturer>();

			return manufacturers;
		}
	}
}