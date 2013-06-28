using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;
using ComputerShop.Repository;
using ComputerShop.MVC.Views;
using ComputerShop.MVC.Models;

namespace ComputerShop.MVC.Controllers
{
	public class RamModuleFactoryController
	{
		private IRepository<RamModule> ramModuleRepository;
		private IRepository<Manufacturer> manufacturerRepository;
		private RamModuleFactoryView view;
		private RamModuleFactory factory;

		public RamModuleFactoryController(IRepository<RamModule> ramModuleRepository, IRepository<Manufacturer> manufacturerRepository)
		{
			this.ramModuleRepository = ramModuleRepository;
			this.manufacturerRepository = manufacturerRepository;
			view = new RamModuleFactoryView();
			factory = new RamModuleFactory(this.ramModuleRepository);
		}

		public void CreateRamModule()
		{
			//let the user choose a manufacturer
			EntitySelectorController<Manufacturer> manufacturerSelector = new EntitySelectorController<Manufacturer>(manufacturerRepository);
			Manufacturer selectedManufacturer = manufacturerSelector.Select();
			//get the ram model.
			string model = view.EnterModel();
			//get the price
			decimal price = view.EnterPrice();
			//get the clockSpeed
			ushort clockSpeed = view.EnterClockSpeed();
			//get the size;
			ushort size = view.EnterSize();
			//get the ddrVersion
			DDRVersion ddrVersion = view.SelectDDRVersion();
			//get the ramFormFactor
			RAMFormFactor ramFormFactor = view.SelectRamFormFactor();

			//finally ask the factory to create a RamModule.
			factory.CreateRamModule(selectedManufacturer, model, price, clockSpeed, ddrVersion, ramFormFactor, size);
		}

		public void EditRamModule()
		{
			//let the user choose the ramModule to edit.
			EntitySelectorController<RamModule> ramModuleSelector = new EntitySelectorController<RamModule>(ramModuleRepository);
			RamModule ramModule = ramModuleSelector.Select();
			//let the user choose a manufacturer
			EntitySelectorController<Manufacturer> manufacturerSelector = new EntitySelectorController<Manufacturer>(manufacturerRepository);
			Manufacturer selectedManufacturer = manufacturerSelector.Select();
			//get the ram model.
			string model = view.EnterModel();
			//get the price
			decimal price = view.EnterPrice();
			//get the clockSpeed
			ushort clockSpeed = view.EnterClockSpeed();
			//get the size;
			ushort size = view.EnterSize();
			//get the ddrVersion
			DDRVersion ddrVersion = view.SelectDDRVersion();
			//get the ramFormFactor
			RAMFormFactor ramFormFactor = view.SelectRamFormFactor();

			//update the ramModule
			ramModule.Manufacturer = selectedManufacturer;
			ramModule.Model = model;
			ramModule.Price = price;
			ramModule.ClockSpeed = clockSpeed;
			ramModule.Size = size;
			ramModule.DDRVersion = ddrVersion;
			ramModule.RamFormFactor = ramFormFactor;
			//ask the factory to update it.
			factory.EditRamModule(ramModule);
		}

		public void DeleteRamModule()
		{
			//let the user choose the ramModule to edit.
			EntitySelectorController<RamModule> ramModuleSelector = new EntitySelectorController<RamModule>(ramModuleRepository);
			RamModule ramModule = ramModuleSelector.Select();
			//ask the factory to delete it.
			factory.DeleteRamModule(ramModule);
		}
	}
}
