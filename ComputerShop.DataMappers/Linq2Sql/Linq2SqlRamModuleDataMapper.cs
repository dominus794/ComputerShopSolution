using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;

using ComputerShopDB = ComputerShop.DAL.Linq2Sql;
using ManufacturerEntity = ComputerShop.DAL.Linq2Sql.Manufacturer;
using RamModuleEntity = ComputerShop.DAL.Linq2Sql.RamModule;

namespace ComputerShop.DataMappers.Linq2Sql
{
	public class Linq2SqlRamModuleDataMapper : Linq2SqlDataMapperBase<RamModule>
	{
		public override void Insert(RamModule data)
		{
			//create a Linq2Sql ram module object
			RamModuleEntity ramModule = new RamModuleEntity();
			//get the Linq2Sql manufacturer object, using the domain manufacturer objects id
			ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerID == data.Manufacturer.ID);

			ramModule.ManufacturerID = data.Manufacturer.ID;
			ramModule.Manufacturer = manufacturer;
			ramModule.Model = data.Model;
			ramModule.Price = data.Price;
			ramModule.ClockSpeed = data.ClockSpeed;
			ramModule.DDRVersion = data.DDRVersion.ToString();
			ramModule.RamFormFactor = data.RamFormFactor.ToString();
			ramModule.Size = data.Size;

			//insert into database and save.
			context.RamModules.InsertOnSubmit(ramModule);
			context.SubmitChanges();
		}

		public override void Delete(RamModule data)
		{
			//find the Linq2Sql RamModule object.
			RamModuleEntity ramModule = context.RamModules.Single(r => r.RamID == data.ID);
			//delete it and submit the changes
			context.RamModules.DeleteOnSubmit(ramModule);
			context.SubmitChanges();
		}

		public override RamModule Select(int id)
		{
			//find the Linq2Sql RamModule object.
			RamModuleEntity ramModule = context.RamModules.Single(r => r.RamID == id);
			//create a domain RamModule object and return it.
			return new RamModule(ramModule.RamID,
								 new Manufacturer(ramModule.Manufacturer.ManufacturerID, ramModule.Manufacturer.Name),
								 ramModule.Model,
								 ramModule.Price,
								 ramModule.ClockSpeed,
								 (DDRVersion)Enum.Parse(typeof(DDRVersion), ramModule.DDRVersion),
								 (RAMFormFactor)Enum.Parse(typeof(RAMFormFactor), ramModule.RamFormFactor),
								 ramModule.Size);
		}

		public override void Update(RamModule data)
		{
			//find the Linq2Sql RamModule object.
			RamModuleEntity ramModule = context.RamModules.Single(r => r.RamID == data.ID);
			//get the Linq2Sql manufacturer object, using the domain manufacturer object's id.
			ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerID == data.Manufacturer.ID);

			//update it and submit the changes.
			ramModule.ManufacturerID = data.Manufacturer.ID;
			ramModule.Manufacturer = manufacturer;
			ramModule.Model = data.Model;
			ramModule.Price = data.Price;
			ramModule.ClockSpeed = data.ClockSpeed;
			ramModule.DDRVersion = data.DDRVersion.ToString();
			ramModule.RamFormFactor = data.RamFormFactor.ToString();
			ramModule.Size = data.Size;
			//submit the changes
			context.SubmitChanges();
		}

		public override IList<RamModule> GetAll()
		{
			IList<RamModule> modules = (from r in context.RamModules
										select new RamModule(r.RamID,
															 new Manufacturer(r.Manufacturer.ManufacturerID, r.Manufacturer.Name),
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
