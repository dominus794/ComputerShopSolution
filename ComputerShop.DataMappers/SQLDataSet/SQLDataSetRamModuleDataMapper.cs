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
	public class SQLDataSetRamModuleDataMapper : SQLDataSetDataMapperBase<RamModule>
	{
		//private ManufacturersTableAdapter manufacturersTableAdapter;
		//private RamModulesTableAdapter ramModulesTableAdapter;

		public SQLDataSetRamModuleDataMapper()
		{
			Debug.WriteLine("In SQLDataSetRamModuleDataMapper Constructor");
			//manufacturersTableAdapter = new ManufacturersTableAdapter();
			//manufacturersTableAdapter.Fill(shopDB.Manufacturers);
			//ramModulesTableAdapter = new RamModulesTableAdapter();
			//ramModulesTableAdapter.Fill(shopDB.RamModules);
		}

		public override void Insert(RamModule data)
		{
			try
			{
				//add a new row to the dataset's RamModules datatable.
				db.shopDB.RamModules.AddRamModulesRow(db.shopDB.Manufacturers.FindByManufacturerID(data.Manufacturer.ID),
					data.Model, data.Price, data.ClockSpeed, data.Size, data.DDRVersion.ToString(), data.RamFormFactor.ToString());

				//update the database.
				db.ramModulesTableAdapter.Update(db.shopDB.RamModules);
			}
			catch (Exception ex)
			{
				db.shopDB.RamModules.RejectChanges();
				Debug.WriteLine(ex.Message);
				throw;
			}		
		}

		public override void Delete(RamModule data)
		{
			try
			{
				//locate the row to delete, and delete it.	
				db.shopDB.RamModules.FindByRamID(data.ID).Delete();
				//delete the row from the database
				db.ramModulesTableAdapter.Update(db.shopDB.RamModules);
			}
			catch (Exception ex)
			{
				db.shopDB.RamModules.RejectChanges();
				//log the error
				Debug.WriteLine(ex.Message);
				//rethrow it
				throw;
			}
		}

		public override RamModule Select(int id)
		{
			ComputerShopDB.RamModulesRow row = db.shopDB.RamModules.FindByRamID(id);

			return new RamModule(row.RamID,
								 new Manufacturer(row.ManufacturersRow.ManufacturerID, row.ManufacturersRow.Name),
								 row.Model,
								 row.Price,
								 row.ClockSpeed,
								 (DDRVersion)Enum.Parse(typeof(DDRVersion), row.DDRVersion),
								 (RAMFormFactor)Enum.Parse(typeof(RAMFormFactor), row.RamFormFactor),
								 row.Size);
		}

		public override void Update(RamModule data)
		{
			try
			{
				//update the DataRow in the dataset				
				ComputerShopDB.RamModulesRow row = db.shopDB.RamModules.FindByRamID(data.ID);
				row.ManufacturerID = data.Manufacturer.ID;
				row.Model = data.Model;
				row.Price = data.Price;
				row.ClockSpeed = data.ClockSpeed;
				row.Size = data.Size;
				row.DDRVersion = data.DDRVersion.ToString();
				row.RamFormFactor = data.RamFormFactor.ToString();

				//update the database
				db.ramModulesTableAdapter.Update(db.shopDB.RamModules);
			}
			catch (Exception ex)
			{
				db.shopDB.RamModules.RejectChanges();
				Debug.WriteLine(ex.Message);
				throw;
			}
		}

		public override IList<RamModule> GetAll()
		{
			IList<RamModule> modules = (from r in db.shopDB.RamModules
										orderby r.Model
										select new RamModule(r.RamID,
															 new Manufacturer(r.ManufacturersRow.ManufacturerID, r.ManufacturersRow.Name),
															 r.Model,
															 r.Price,
															 r.ClockSpeed,
															 (DDRVersion)Enum.Parse(typeof(DDRVersion), r.DDRVersion),
															 (RAMFormFactor)Enum.Parse(typeof(RAMFormFactor), r.RamFormFactor),
															 r.Size)).ToList<RamModule>();

			return modules;
		}
	}
}