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
	public class SQLDataSetCPUDataMapper : SQLDataSetDataMapperBase<CPU>
	{		
		//private ManufacturersTableAdapter manufacturersTableAdapter;
		//private CPUsTableAdapter cpusTableAdapter;

		public SQLDataSetCPUDataMapper()			
		{
			//manufacturersTableAdapter = new ManufacturersTableAdapter();
			//manufacturersTableAdapter.Fill(shopDB.Manufacturers);

			//cpusTableAdapter = new CPUsTableAdapter();
			//cpusTableAdapter.Fill(shopDB.CPUs);
		}

		public override void Insert(CPU data)
		{
			try
			{
				//add a new row to the dataset's CPUs datatable			
				db.shopDB.CPUs.AddCPUsRow(db.shopDB.Manufacturers.FindByManufacturerID(data.Manufacturer.ID),
									   data.Model, data.Price, data.ClockSpeed, data.CpuCoreType.ToString(), data.CpuFormFactor.ToString());
				//update the database.
				db.cpusTableAdapter.Update(db.shopDB.CPUs);
			}
			catch (Exception ex)
			{
				db.shopDB.CPUs.RejectChanges();
				Debug.WriteLine(ex.Message);
				throw;
			}
		}

		public override void Delete(CPU data)
		{
			try
			{
				//locate the row to delete, and delete it.				
				db.shopDB.CPUs.FindByCpuID(data.ID).Delete();
				//delete the row from the database
				db.cpusTableAdapter.Update(db.shopDB.CPUs);
			}
			catch (Exception ex)
			{
				db.shopDB.CPUs.RejectChanges();
				Debug.WriteLine(ex.Message);
				throw;
			}
		}

		public override CPU Select(int id)
		{
			//get the row by id.
			ComputerShopDB.CPUsRow row = db.shopDB.CPUs.FindByCpuID(id);

			return new CPU(row.CpuID,
						   new Manufacturer(row.ManufacturersRow.ManufacturerID, row.ManufacturersRow.Name),
						   row.Model,
						   row.Price,
						   row.ClockSpeed,
						   (CPUFormFactor)Enum.Parse(typeof(CPUFormFactor), row.CpuFormFactor),
						   (CPUCoreType)Enum.Parse(typeof(CPUCoreType), row.NoOfCores));
		}

		public override void Update(CPU data)
		{
			try
			{
				//update the DataRow in the dataset				
				ComputerShopDB.CPUsRow row = db.shopDB.CPUs.FindByCpuID(data.ID);
				row.ManufacturerID = data.Manufacturer.ID;
				row.Model = data.Model;
				row.Price = data.Price;
				row.ClockSpeed = data.ClockSpeed;
				row.NoOfCores = data.CpuCoreType.ToString();
				row.CpuFormFactor = data.CpuFormFactor.ToString();

				//update the database
				db.cpusTableAdapter.Update(db.shopDB.CPUs);
			}
			catch (Exception ex)
			{
				db.shopDB.CPUs.RejectChanges();
				Debug.WriteLine(ex.Message);
				throw;
			}
		}

		public override IList<CPU> GetAll()
		{
			IList<CPU> cpus = (from c in db.shopDB.CPUs
							   orderby c.Model
							   select new CPU(c.CpuID,
								 			  new Manufacturer(c.ManufacturersRow.ManufacturerID, c.ManufacturersRow.Name),
											  c.Model,
											  c.Price,
											  c.ClockSpeed,											  
											  (CPUFormFactor)Enum.Parse(typeof(CPUFormFactor), c.CpuFormFactor),
											  (CPUCoreType)Enum.Parse(typeof(CPUCoreType), c.NoOfCores))
							   ).ToList<CPU>();
			return cpus;
		}
	}
}