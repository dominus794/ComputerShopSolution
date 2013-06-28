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
	public class LaptopFactoryController
	{
		private IRepository<Laptop> laptopRepository;
		private IRepository<Manufacturer> manufacturerRepository;
		private IRepository<CPU> cpuRepository;
		private IRepository<RamModule> ramModuleRepository;
		private IRepository<HDD> hddRepository;
		private IRepository<GPU> gpuRepository;
		private LaptopFactoryView view;
		private LaptopFactory factory;

		public LaptopFactoryController(IRepository<Laptop> laptopRepository, IRepository<Manufacturer> manufacturerRepository,
										IRepository<CPU> cpuRepository, IRepository<RamModule> ramModuleRepository,
										IRepository<HDD> hddRepository, IRepository<GPU> gpuRepository)
		{
			this.laptopRepository = laptopRepository;
			this.manufacturerRepository = manufacturerRepository;
			this.cpuRepository = cpuRepository;
			this.ramModuleRepository = ramModuleRepository;
			this.hddRepository = hddRepository;
			this.gpuRepository = gpuRepository;

			view = new LaptopFactoryView();
			factory = new LaptopFactory(this.laptopRepository);
		}

		public void CreateLaptop()
		{
			//let the user choose a manufacturer.
			EntitySelectorController<Manufacturer> manufacturerSelector = new EntitySelectorController<Manufacturer>(manufacturerRepository);
			Manufacturer manufacturer = manufacturerSelector.Select();
			//get the model.
			string model = view.EnterModel();
			//get the price
			decimal price = view.EnterPrice();
			//let the user choose a cpu
			EntitySelectorController<CPU> cpuSelector = new EntitySelectorController<CPU>(cpuRepository);
			CPU cpu = cpuSelector.Select();
			//get the number of ram modules.
			int numberOfRamModules = view.EnterNumberOfRamModules();
			//let the user choose the ram module(s).
			EntitySelectorController<RamModule> ramModuleSelector = new EntitySelectorController<RamModule>(ramModuleRepository);
			IRamModuleCollection ramModuleCollection = new RamModuleCollection();
			for (int i = 0; i < numberOfRamModules; i++)
			{
				RamModule module = ramModuleSelector.Select();
				ramModuleCollection.AddRamModule(module, 1);
			}
			//let the user choose a gpu
			EntitySelectorController<GPU> gpuSelector = new EntitySelectorController<GPU>(gpuRepository);
			GPU gpu = gpuSelector.Select();
			//let the user choose a hdd
			EntitySelectorController<HDD> hddSelector = new EntitySelectorController<HDD>(hddRepository);
			HDD hdd = hddSelector.Select();
			//get the weight
			byte weight = view.EnterWeight();
			//get the batteryLife
			byte batteryLife = view.EnterBatteryLife();
			//get the displaySize
			float displaySize = view.EnterDisplaySize();

			//finally ask the factory to create a Laptop
			factory.CreateLaptop(manufacturer, model, price, cpu, ramModuleCollection, gpu, hdd, weight, batteryLife, displaySize);
		}

		public void EditLaptop()
		{
			//let the user choose the laptop
			EntitySelectorController<Laptop> laptopSelector = new EntitySelectorController<Laptop>(laptopRepository);
			Laptop laptop = laptopSelector.Select();
			//let the user choose a manufacturer.
			EntitySelectorController<Manufacturer> manufacturerSelector = new EntitySelectorController<Manufacturer>(manufacturerRepository);
			Manufacturer manufacturer = manufacturerSelector.Select();
			//get the model.
			string model = view.EnterModel();
			//get the price
			decimal price = view.EnterPrice();
			//let the user choose a cpu
			EntitySelectorController<CPU> cpuSelector = new EntitySelectorController<CPU>(cpuRepository);
			CPU cpu = cpuSelector.Select();
			//get the number of ram modules.
			int numberOfRamModules = view.EnterNumberOfRamModules();
			//let the user choose the ram module(s).
			EntitySelectorController<RamModule> ramModuleSelector = new EntitySelectorController<RamModule>(ramModuleRepository);
			IRamModuleCollection ramModuleCollection = new RamModuleCollection();
			for (int i = 0; i < numberOfRamModules; i++)
			{
				RamModule module = ramModuleSelector.Select();
				ramModuleCollection.AddRamModule(module, 1);
			}
			//let the user choose a gpu
			EntitySelectorController<GPU> gpuSelector = new EntitySelectorController<GPU>(gpuRepository);
			GPU gpu = gpuSelector.Select();
			//let the user choose a hdd
			EntitySelectorController<HDD> hddSelector = new EntitySelectorController<HDD>(hddRepository);
			HDD hdd = hddSelector.Select();
			//get the weight
			byte weight = view.EnterWeight();
			//get the batteryLife
			byte batteryLife = view.EnterBatteryLife();
			//get the displaySize
			float displaySize = view.EnterDisplaySize();

			//update the laptop
			laptop.Manufacturer = manufacturer;
			laptop.Model = model;
			laptop.Price = price;
			laptop.Cpu = cpu;
			laptop.RamModuleCollection = ramModuleCollection;
			laptop.Gpu = gpu;
			laptop.Hdd = hdd;
			laptop.Weight = weight;
			laptop.BatteryLife = batteryLife;
			laptop.DisplaySize = displaySize;
			//ask the factory to update the laptop.
			factory.EditLaptop(laptop);
		}

		public void DeleteLaptop()
		{
			//let the user choose the laptop
			EntitySelectorController<Laptop> laptopSelector = new EntitySelectorController<Laptop>(laptopRepository);
			Laptop laptop = laptopSelector.Select();
			//ask the factory to remove this laptop
			factory.DeleteLaptop(laptop);
		}
	}
}
