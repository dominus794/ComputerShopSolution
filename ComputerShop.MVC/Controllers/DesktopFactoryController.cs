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
	public class DesktopFactoryController
	{
		private IRepository<Desktop> desktopRepository;
		private IRepository<Manufacturer> manufacturerRepository;
		private IRepository<CPU> cpuRepository;
		private IRepository<RamModule> ramModuleRepository;
		private IRepository<HDD> hddRepository;
		private IRepository<GPU> gpuRepository;
		private DesktopFactoryView view;
		private DesktopFactory factory;

		public DesktopFactoryController(IRepository<Desktop> desktopRepository, IRepository<Manufacturer> manufacturerRepository,
										IRepository<CPU> cpuRepository, IRepository<RamModule> ramModuleRepository,
										IRepository<HDD> hddRepository, IRepository<GPU> gpuRepository)
		{
			this.desktopRepository = desktopRepository;
			this.manufacturerRepository = manufacturerRepository;
			this.cpuRepository = cpuRepository;
			this.ramModuleRepository = ramModuleRepository;
			this.hddRepository = hddRepository;
			this.gpuRepository = gpuRepository;

			view = new DesktopFactoryView();
			factory = new DesktopFactory(this.desktopRepository);
		}

		public void CreateDesktop()
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
			CPU cpu = cpuSelector.Select(c => c.CpuFormFactor == CPUFormFactor.Desktop);
			//get the number of ram modules.
			int numberOfRamModules = view.EnterNumberOfRamModules();
			//let the user choose the ram module(s).
			EntitySelectorController<RamModule> ramModuleSelector = new EntitySelectorController<RamModule>(ramModuleRepository);
			IRamModuleCollection ramModuleCollection = new RamModuleCollection();
			for (int i = 0; i < numberOfRamModules; i++)
			{
				RamModule module = ramModuleSelector.Select(r => r.RamFormFactor == RAMFormFactor.DIMM);
				ramModuleCollection.AddRamModule(module, 1);
			}
			//get the number of hdds
			int numberOfHdds = view.EnterNumberOfHDDs();
			//let the user choose the hdds
			EntitySelectorController<HDD> hddSelector = new EntitySelectorController<HDD>(hddRepository);
			IHDDCollection hddCollection = new HDDCollection();
			for (int i = 0; i < numberOfHdds; i++)
			{
				HDD hdd = hddSelector.Select();
				hddCollection.AddHDD(hdd, 1);
			}
			//get the number of gpus.
			int numberOfGPUs = view.EnterNumberOfGPUs();
			//let the user choose the gpus
			EntitySelectorController<GPU> gpuSelector = new EntitySelectorController<GPU>(gpuRepository);
			IGPUCollection gpuCollection = new GPUCollection();
			for (int i = 0; i < numberOfGPUs; i++)
			{
				GPU gpu = gpuSelector.Select();
				gpuCollection.AddGPU(gpu, 1);
			}

			//finally ask the factory to create a Desktop
			factory.CreateDesktop(manufacturer, model, price, cpu, ramModuleCollection, hddCollection, gpuCollection);
		}

		public void EditDesktop()
		{
			//let the user choose a desktop to edit.
			EntitySelectorController<Desktop> desktopSelector = new EntitySelectorController<Desktop>(desktopRepository);
			Desktop desktop = desktopSelector.Select();
			//let the user choose a manufacturer.
			EntitySelectorController<Manufacturer> manufacturerSelector = new EntitySelectorController<Manufacturer>(manufacturerRepository);
			Manufacturer manufacturer = manufacturerSelector.Select();
			//get the model.
			string model = view.EnterModel();
			//get the price
			decimal price = view.EnterPrice();
			//let the user choose a cpu
			EntitySelectorController<CPU> cpuSelector = new EntitySelectorController<CPU>(cpuRepository);
			CPU cpu = cpuSelector.Select(c => c.CpuFormFactor == CPUFormFactor.Desktop);
			//get the number of ram modules.
			int numberOfRamModules = view.EnterNumberOfRamModules();
			//let the user choose the ram module(s).
			EntitySelectorController<RamModule> ramModuleSelector = new EntitySelectorController<RamModule>(ramModuleRepository);
			IRamModuleCollection ramModuleCollection = new RamModuleCollection();
			for (int i = 0; i < numberOfRamModules; i++)
			{
				RamModule module = ramModuleSelector.Select(r => r.RamFormFactor == RAMFormFactor.DIMM);
				ramModuleCollection.AddRamModule(module, 1);
			}
			//get the number of hdds
			int numberOfHdds = view.EnterNumberOfHDDs();
			//let the user choose the hdds
			EntitySelectorController<HDD> hddSelector = new EntitySelectorController<HDD>(hddRepository);
			IHDDCollection hddCollection = new HDDCollection();
			for (int i = 0; i < numberOfHdds; i++)
			{
				HDD hdd = hddSelector.Select();
				hddCollection.AddHDD(hdd, 1);
			}
			//get the number of gpus.
			int numberOfGPUs = view.EnterNumberOfGPUs();
			//let the user choose the gpus
			EntitySelectorController<GPU> gpuSelector = new EntitySelectorController<GPU>(gpuRepository);
			IGPUCollection gpuCollection = new GPUCollection();
			for (int i = 0; i < numberOfGPUs; i++)
			{
				GPU gpu = gpuSelector.Select();
				gpuCollection.AddGPU(gpu, 1);
			}

			//update the desktop
			desktop.Manufacturer = manufacturer;
			desktop.Model = model;
			desktop.Price = price;
			desktop.Cpu = cpu;
			desktop.RamModuleCollection = ramModuleCollection;
			desktop.HddCollection = hddCollection;
			desktop.GpuCollection = gpuCollection;
			//ask the factory to update the desktop
			factory.EditDesktop(desktop);
		}

		public void DeleteDesktop()
		{
			//let the user choose a desktop to edit.
			EntitySelectorController<Desktop> desktopSelector = new EntitySelectorController<Desktop>(desktopRepository);
			Desktop desktop = desktopSelector.Select();
			//ask the factory to delete the desktop
			factory.DeleteDesktop(desktop);
		}
	}
}
