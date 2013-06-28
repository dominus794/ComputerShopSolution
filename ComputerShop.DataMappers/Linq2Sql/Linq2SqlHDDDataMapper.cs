using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;

using ComputerShopDB = ComputerShop.DAL.Linq2Sql;
using ManufacturerEntity = ComputerShop.DAL.Linq2Sql.Manufacturer;
using HDDEntity = ComputerShop.DAL.Linq2Sql.HDD;

namespace ComputerShop.DataMappers.Linq2Sql
{
	public class Linq2SqlHDDDataMapper : Linq2SqlDataMapperBase<HDD>
	{
		public override void Insert(HDD data)
		{
			//create a Linq2Sql HDD object
			HDDEntity hdd = new HDDEntity();
			//get the Linq2Sql manufacturer object, using the domain manufacturer objects id
			ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerID == data.Manufacturer.ID);

			hdd.ManufacturerID = data.Manufacturer.ID;
			hdd.Manufacturer = manufacturer;
			hdd.Model = data.Model;
			hdd.Price = data.Price;
			hdd.Capacity = data.Capacity;
			hdd.HddFormFactor = data.HddFormFactor.ToString();
			hdd.HddInterface = data.HddInterfaceType.ToString();
			hdd.HddType = data.HddType.ToString();
			hdd.Speed = data.Speed;

			//insert into database and submit changes.			
			context.HDDs.InsertOnSubmit(hdd);
			context.SubmitChanges();
		}

		public override void Delete(HDD data)
		{
			//find the Linq2Sql HDD object.
			HDDEntity hdd = context.HDDs.Single(h => h.HddID == data.ID);
			//delete it and submit the changes
			context.HDDs.DeleteOnSubmit(hdd);
			context.SubmitChanges();
		}

		public override HDD Select(int id)
		{
			//find the Linq2Sql HDD object.
			HDDEntity hdd = context.HDDs.Single(h => h.HddID == id);
			//create a new domain HDD object and return it.
			return new HDD(hdd.HddID,
						   new Manufacturer(hdd.Manufacturer.ManufacturerID, hdd.Manufacturer.Name),
						   hdd.Model,
						   hdd.Price,
						   hdd.Capacity,
						   (HDDFormFactor)Enum.Parse(typeof(HDDFormFactor), hdd.HddFormFactor),
						   (HDDInterfaceType)Enum.Parse(typeof(HDDInterfaceType), hdd.HddInterface),
						   (HDDType)Enum.Parse(typeof(HDDType), hdd.HddType),
						   hdd.Speed);
		}

		public override void Update(HDD data)
		{
			//find the Linq2Sql HDD object.
			HDDEntity hdd = context.HDDs.Single(h => h.HddID == data.ID);
			//get the Linq2Sql manufacturer object, using the domain manufacturer object's id.
			ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerID == data.Manufacturer.ID);

			//update it and submit the changes.
			hdd.ManufacturerID = data.Manufacturer.ID;
			hdd.Manufacturer = manufacturer;
			hdd.Model = data.Model;
			hdd.Price = data.Price;
			hdd.Capacity = data.Capacity;
			hdd.HddFormFactor = data.HddFormFactor.ToString();
			hdd.HddInterface = data.HddInterfaceType.ToString();
			hdd.HddType = data.HddType.ToString();
			hdd.Speed = data.Speed;
			//submit the changes
			context.SubmitChanges();
		}

		public override IList<HDD> GetAll()
		{
			IList<HDD> hdds = (from h in context.HDDs
							   select new HDD(h.HddID,
											  new Manufacturer(h.Manufacturer.ManufacturerID, h.Manufacturer.Name),
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
