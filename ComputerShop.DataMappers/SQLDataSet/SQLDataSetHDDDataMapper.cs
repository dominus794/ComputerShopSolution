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
	public class SQLDataSetHDDDataMapper : SQLDataSetDataMapperBase<HDD>
	{
		//private ManufacturersTableAdapter manufacturersTableAdapter;
		//private HDDsTableAdapter hddsTableAdapter;

		public SQLDataSetHDDDataMapper()
		{
			//manufacturersTableAdapter = new ManufacturersTableAdapter();
			//manufacturersTableAdapter.Fill(shopDB.Manufacturers);
			//hddsTableAdapter = new HDDsTableAdapter();
			//hddsTableAdapter.Fill(shopDB.HDDs);
		}

		public override void Insert(HDD data)
		{
			try
			{
				//add a new row to the dataset's HDDs datatable.
				db.shopDB.HDDs.AddHDDsRow(db.shopDB.Manufacturers.FindByManufacturerID(data.Manufacturer.ID),
					data.Model, data.Price, data.Capacity, data.Speed, data.HddType.ToString(), data.HddFormFactor.ToString(), data.HddInterfaceType.ToString());

				//update the database.
				db.hddsTableAdapter.Update(db.shopDB.HDDs);
			}
			catch (Exception ex)
			{
				db.shopDB.HDDs.RejectChanges();
				Debug.WriteLine(ex.Message);
				throw;
			}
		}

		public override void Delete(HDD data)
		{
			try
			{
				//locate the row to delete, and delete it.	
				db.shopDB.HDDs.FindByHddID(data.ID).Delete();
				//delete the row from the database
				db.hddsTableAdapter.Update(db.shopDB.HDDs);
			}
			catch (Exception ex)
			{
				db.shopDB.HDDs.RejectChanges();
				Debug.WriteLine(ex.Message);
				throw;				
			}
		}

		public override HDD Select(int id)
		{
			ComputerShopDB.HDDsRow row = db.shopDB.HDDs.FindByHddID(id);

			return new HDD(row.HddID,
						   new Manufacturer(row.ManufacturersRow.ManufacturerID, row.ManufacturersRow.Name),
						   row.Model,
						   row.Price,
						   row.Capacity,
						   (HDDFormFactor)Enum.Parse(typeof(HDDFormFactor), row.HddFormFactor),
						   (HDDInterfaceType)Enum.Parse(typeof(HDDInterfaceType), row.HddInterface),
						   (HDDType)Enum.Parse(typeof(HDDType), row.HddType),
						   row.Speed);
		}

		public override void Update(HDD data)
		{
			try
			{
				//update the DataRow in the dataset		
				ComputerShopDB.HDDsRow row = db.shopDB.HDDs.FindByHddID(data.ID);

				row.ManufacturerID = data.Manufacturer.ID;
				row.Model = data.Model;
				row.Price = data.Price;
				row.Capacity = data.Capacity;
				row.Speed = data.Speed;
				row.HddType = data.HddType.ToString();
				row.HddFormFactor = data.HddFormFactor.ToString();
				row.HddInterface = data.HddInterfaceType.ToString();

				//update the database
				db.hddsTableAdapter.Update(db.shopDB.HDDs);
			}
			catch (Exception ex)
			{
				db.shopDB.HDDs.RejectChanges();
				Debug.WriteLine(ex.Message);
				throw;
			}
		}

		public override IList<HDD> GetAll()
		{
			IList<HDD> hdds = (from h in db.shopDB.HDDs
							   orderby h.Model
							   select new HDD(h.HddID,
											  new Manufacturer(h.ManufacturersRow.ManufacturerID, h.ManufacturersRow.Name),
											  h.Model,
											  h.Price,
											  h.Capacity,
											  (HDDFormFactor)Enum.Parse(typeof(HDDFormFactor), h.HddFormFactor),
											  (HDDInterfaceType)Enum.Parse(typeof(HDDInterfaceType), h.HddInterface),
											  (HDDType)Enum.Parse(typeof(HDDType), h.HddType),
											  h.Speed)).ToList<HDD>();

			return hdds;			
		}
	}
}
