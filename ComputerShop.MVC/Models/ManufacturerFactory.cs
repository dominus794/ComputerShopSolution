using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;
using ComputerShop.Repository;

namespace ComputerShop.MVC.Models
{
	public class ManufacturerFactory
	{
		private IRepository<Manufacturer> manufacturerRepository;

		public ManufacturerFactory(IRepository<Manufacturer> manufacturerRepository)
		{
			this.manufacturerRepository = manufacturerRepository; //= new Repository<Manufacturer>(new SQLDataSetManufacturerDataMapperFactory());
		}

		public void CreateManufacturer(string name)
		{
			//create the Manufacturer object.
			Manufacturer newManufacturer = new Manufacturer(0, name);
			//store it in the repository.
			manufacturerRepository.Add(newManufacturer);
		}

		public void EditManufacturer(Manufacturer manufacturer)
		{
			manufacturerRepository.Save(manufacturer);
		}

		public void DeleteManufacturer(Manufacturer manufacturer)
		{
			manufacturerRepository.Remove(manufacturer);
		}
	}
}
