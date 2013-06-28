using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Domain;

using ComputerShopDB = ComputerShop.DAL.Linq2Sql;
using ManufacturerEntity = ComputerShop.DAL.Linq2Sql.Manufacturer;

namespace ComputerShop.DataMappers.Linq2Sql
{
	public class Linq2SqlManufacturerDataMapper : Linq2SqlDataMapperBase<Manufacturer>
	{
		public override void Insert(Manufacturer data)
		{
			//create a Linq2Sql manufacturer object
			ManufacturerEntity manufacturer = new ManufacturerEntity();
			manufacturer.Name = data.Name;
			//insert into database and save
			context.Manufacturers.InsertOnSubmit(manufacturer);
			context.SubmitChanges();
		}

		public override void Delete(Manufacturer data)
		{
			//find the Linq2Sql manufacturer object
			ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerID == data.ID);
			//delete it and submit the changes.
			context.Manufacturers.DeleteOnSubmit(manufacturer);
			context.SubmitChanges();
		}

		public override Manufacturer Select(int id)
		{
			//find the Linq2Sql manufacturer object
			ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerID == id);
			return new Manufacturer(manufacturer.ManufacturerID, manufacturer.Name);
		}

		public override void Update(Manufacturer data)
		{
			//find the Linq2Sql manufacturer object
			ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerID == data.ID);
			//update it and submit the changes.
			manufacturer.Name = data.Name;
			context.SubmitChanges();
		}

		public override IList<Manufacturer> GetAll()
		{
			IList<Manufacturer> manufacturers = (from m in context.Manufacturers
												 select new Manufacturer(m.ManufacturerID, m.Name)).ToList<Manufacturer>();

			return manufacturers;
		}
	}
}
