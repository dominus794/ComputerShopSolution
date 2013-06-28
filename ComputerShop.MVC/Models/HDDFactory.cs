using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;
using ComputerShop.Repository;

namespace ComputerShop.MVC.Models
{
	public class HDDFactory
	{
		private IRepository<HDD> hddRepository;

		public HDDFactory(IRepository<HDD> hddRepository)
		{
			this.hddRepository = hddRepository;
		}

		public void CreateHDD(IManufacturer manufacturer, string model, decimal price,
							  ushort capacity, HDDFormFactor hddFormFactor, HDDInterfaceType hddInterfaceType, HDDType hddType, ushort speed)
		{
			HDD newHDD = new HDD(0, manufacturer, model, price, capacity, hddFormFactor, hddInterfaceType, hddType, speed);
			hddRepository.Add(newHDD);
		}

		public void EditHDD(HDD hdd)
		{
			hddRepository.Save(hdd);
		}

		public void DeleteHDD(HDD hdd)
		{
			hddRepository.Remove(hdd);
		}
	}
}
