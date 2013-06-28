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
	public class SQLDataSetGPUDataMapper : SQLDataSetDataMapperBase<GPU>
	{
		//private ManufacturersTableAdapter manufacturersTableAdapter;
		//private GPUsTableAdapter gpusTableAdapter;

		public SQLDataSetGPUDataMapper()
		{
			//manufacturersTableAdapter = new ManufacturersTableAdapter();
			//manufacturersTableAdapter.Fill(shopDB.Manufacturers);
			//gpusTableAdapter = new GPUsTableAdapter();
			//gpusTableAdapter.Fill(shopDB.GPUs);
		}

		public override void Insert(GPU data)
		{
			try
			{
				//add a new row to the dataset's GPUs datatable.
				db.shopDB.GPUs.AddGPUsRow(db.shopDB.Manufacturers.FindByManufacturerID(data.Manufacturer.ID),
					data.Model, data.Price, data.GpuModel, data.GpuClockSpeed, data.VRamSize, data.VRamClockSpeed,
					data.GddrVersion.ToString(), data.GpuType.ToString());

				//update the database.
				db.gpusTableAdapter.Update(db.shopDB.GPUs);
			}
			catch (Exception ex)
			{
				db.shopDB.GPUs.RejectChanges();
				Debug.WriteLine(ex.Message);
				throw;
			}
		}

		public override void Delete(GPU data)
		{
			try
			{
				//locate the row to delete, and delete it.	
				db.shopDB.GPUs.FindByGpuID(data.ID).Delete();
				//delete the row from the database
				db.gpusTableAdapter.Update(db.shopDB.GPUs);
			}
			catch (Exception ex)
			{
				db.shopDB.GPUs.RejectChanges();
				Debug.WriteLine(ex.Message);
				throw;				
			}
		}

		public override GPU Select(int id)
		{
			ComputerShopDB.GPUsRow row = db.shopDB.GPUs.FindByGpuID(id);

			return new GPU(row.GpuID,
						   new Manufacturer(row.ManufacturersRow.ManufacturerID, row.ManufacturersRow.Name),
						   row.Model,
						   row.Price,
						   row.GpuModel.Trim(),
						   (ushort)row.GpuClockSpeed,
						   (ushort)row.VRamSize,
						   (ushort)row.VRamClockSpeed,
						   (GDDRVersion)Enum.Parse(typeof(GDDRVersion), row.GDDRVersion),
						   (GPUType)Enum.Parse(typeof(GPUType), row.GpuType));			
		}

		public override void Update(GPU data)
		{
			try
			{
				//update the DataRow in the dataset	
				ComputerShopDB.GPUsRow row = db.shopDB.GPUs.FindByGpuID(data.ID);
				row.ManufacturerID = data.Manufacturer.ID;
				row.Model = data.Model;
				row.Price = data.Price;
				row.GpuModel = data.GpuModel;
				row.GpuClockSpeed = data.GpuClockSpeed;
				row.VRamSize = data.VRamSize;
				row.VRamClockSpeed = data.VRamClockSpeed;
				row.GDDRVersion = data.GddrVersion.ToString();
				row.GpuType = data.GpuType.ToString();

				//update the database
				db.gpusTableAdapter.Update(db.shopDB.GPUs);
			}
			catch (Exception ex)
			{
				db.shopDB.GPUs.RejectChanges();
				Debug.WriteLine(ex.Message);
				throw;
			}
		}

		public override IList<GPU> GetAll()
		{
			IList<GPU> gpus = (from g in db.shopDB.GPUs
							   orderby g.Model
							   select new GPU(g.GpuID,
											  new Manufacturer(g.ManufacturersRow.ManufacturerID, g.ManufacturersRow.Name),
											  g.Model,
											  g.Price,
											  g.GpuModel.Trim(),
											  (ushort)g.GpuClockSpeed,
											  (ushort)g.VRamSize,
											  (ushort)g.VRamClockSpeed,
											  (GDDRVersion)Enum.Parse(typeof(GDDRVersion), g.GDDRVersion),
											  (GPUType)Enum.Parse(typeof(GPUType), g.GpuType))).ToList<GPU>();

			return gpus;
		}
	}
}