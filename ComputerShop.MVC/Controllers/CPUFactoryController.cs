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
	public class CPUFactoryController
	{
		private IRepository<CPU> cpuRepository;
		private IRepository<Manufacturer> manufacturerRepository;
		private CPUFactoryView view;
		private CPUFactory factory;

		public CPUFactoryController(IRepository<CPU> cpuRepository, IRepository<Manufacturer> manufacturerRepository)
		{
			this.cpuRepository = cpuRepository;
			this.manufacturerRepository = manufacturerRepository;
			view = new CPUFactoryView();
			factory = new CPUFactory(this.cpuRepository);
		}

		public void CreateCPU()
		{
			//get the cpu manufacturer. create selector, and pass it a ref to the repository.
			EntitySelectorController<Manufacturer> manufacturerSelector = new EntitySelectorController<Manufacturer>(manufacturerRepository);			
			Manufacturer selectedManufacturer = manufacturerSelector.Select(m => m.Name == "Intel" || m.Name == "Amd");
			//get the cpu model.
			string model = view.EnterModel();			
			//get the price
			decimal price = view.EnterPrice();			
			//get the clockspeed;
			float clockSpeed = view.EnterClockSpeed();
			//get the cpuCoreType
			CPUCoreType cpuCoreType = view.SelectCPUCoreType();
			//get the cpuFormFactor
			CPUFormFactor cpuFormFactor = view.SelectCPUFormFactor();			
									
			//finally ask the factory to create a CPU;
			factory.CreateCPU(selectedManufacturer, model, price, clockSpeed, cpuFormFactor, cpuCoreType);
		}

		public void EditCPU()
		{
			//get the cpu to edit.
			EntitySelectorController<CPU> cpuSelector = new EntitySelectorController<CPU>(cpuRepository);
			CPU selectedCPU = cpuSelector.Select();
			//get the cpu manufacturer. create selector, and pass it a ref to the repository.
			EntitySelectorController<Manufacturer> manufacturerSelector = new EntitySelectorController<Manufacturer>(manufacturerRepository);
			Manufacturer selectedManufacturer = manufacturerSelector.Select(m => m.Name == "Intel" || m.Name == "Amd");
			//get the cpu model.
			string model = view.EnterModel();
			//get the price
			decimal price = view.EnterPrice();
			//get the clockspeed;
			float clockSpeed = view.EnterClockSpeed();
			//get the cpuCoreType
			CPUCoreType cpuCoreType = view.SelectCPUCoreType();
			//get the cpuFormFactor
			CPUFormFactor cpuFormFactor = view.SelectCPUFormFactor();
			//update the selectedCPU
			selectedCPU.Manufacturer = selectedManufacturer;
			selectedCPU.Model = model;
			selectedCPU.Price = price;
			selectedCPU.ClockSpeed = clockSpeed;
			selectedCPU.CpuCoreType = cpuCoreType;
			selectedCPU.CpuFormFactor = cpuFormFactor;
			//save it
			factory.EditCPU(selectedCPU);
		}

		public void DeleteCPU()
		{
			//get the cpu to delete.
			EntitySelectorController<CPU> cpuSelector = new EntitySelectorController<CPU>(cpuRepository);
			CPU selectedCPU = cpuSelector.Select();
			factory.DeleteCPU(selectedCPU);
		}
	}
}
