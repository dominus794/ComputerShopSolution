using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;
using ComputerShop.Repository;

namespace ComputerShop.MVC.Models
{
	public class RamModuleFactory
	{
		private IRepository<RamModule> ramModuleRepository;

		public RamModuleFactory(IRepository<RamModule> ramModuleRepository)
		{
			this.ramModuleRepository = ramModuleRepository;
		}

		public void CreateRamModule(IManufacturer manufacturer, string model, decimal price,
									ushort clockSpeed, DDRVersion ddrVersion, RAMFormFactor ramFormFactor, ushort size)
		{
			RamModule newRamModule = new RamModule(0, manufacturer, model, price, clockSpeed, ddrVersion, ramFormFactor, size);
			ramModuleRepository.Add(newRamModule);
		}

		public void EditRamModule(RamModule module)
		{
			ramModuleRepository.Save(module);
		}

		public void DeleteRamModule(RamModule module)
		{
			ramModuleRepository.Remove(module);
		}
	}
}
