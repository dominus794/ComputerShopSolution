using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;
using ComputerShop.Repository;

namespace ComputerShop.MVC.Models
{
	public class LaptopFactory
	{
		private IRepository<Laptop> laptopRepository;

		public LaptopFactory(IRepository<Laptop> laptopRepository)
		{
			this.laptopRepository = laptopRepository;
		}

		public void CreateLaptop(IManufacturer manufacturer, string model, decimal price, ICPU cpu,
								 IRamModuleCollection ramModuleCollection, IGPU gpu, IHDD hdd,
								 byte weight, byte batteryLife, float displaySize)
		{
			Laptop newLaptop = new Laptop(0, manufacturer, model, price, cpu,
										  ramModuleCollection, gpu, hdd,
										  weight, batteryLife, displaySize);

			laptopRepository.Add(newLaptop);
		}

		public void EditLaptop(Laptop laptop)
		{
			laptopRepository.Save(laptop);
		}

		public void DeleteLaptop(Laptop laptop)
		{
			laptopRepository.Remove(laptop);
		}
	}
}
