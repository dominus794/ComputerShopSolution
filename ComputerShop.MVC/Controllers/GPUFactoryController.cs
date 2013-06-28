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
	public class GPUFactoryController
	{
		private IRepository<GPU> gpuRepository;
		private IRepository<Manufacturer> manufacturerRepository;
		private GPUFactoryView view;
		private GPUFactory factory;

		public GPUFactoryController(IRepository<GPU> gpuRepository, IRepository<Manufacturer> manufacturerRepository)
		{
			this.gpuRepository = gpuRepository;
			this.manufacturerRepository = manufacturerRepository;
			view = new GPUFactoryView();
			factory = new GPUFactory(this.gpuRepository);
		}

		public void CreateGPU()
		{
			//let the user choose a manufacturer
			EntitySelectorController<Manufacturer> manufacturerSelector = new EntitySelectorController<Manufacturer>(manufacturerRepository);
			Manufacturer selectedManufacturer = manufacturerSelector.Select();
			//get the model.
			string model = view.EnterModel();
			//get the price
			decimal price = view.EnterPrice();
			//get the gpuModel
			string gpuModel = view.EnterGPUModel();
			//get the gpuClockSpeed
			ushort gpuClockSpeed = view.EnterGPUClockSpeed();
			//get the vramSize
			ushort vramSize = view.EnterVRamSize();
			//get the vramClockspeed
			ushort vramClockSpeed = view.EnterVRamClockSpeed();
			//get the gddrVersion
			GDDRVersion gddrVersion = view.SelectGDDRVersion();
			//get the gpuType
			GPUType gpuType = view.SelectGPUType();

			//ask the factory to create a GPU
			factory.CreateGPU(selectedManufacturer, model, price, gpuModel, gpuClockSpeed, vramSize, vramClockSpeed, gddrVersion, gpuType);
		}

		public void EditGPU()
		{
			//let the user choose a gpu to edit.
			EntitySelectorController<GPU> gpuSelector = new EntitySelectorController<GPU>(gpuRepository);
			GPU gpu = gpuSelector.Select();
			//let the user choose a manufacturer
			EntitySelectorController<Manufacturer> manufacturerSelector = new EntitySelectorController<Manufacturer>(manufacturerRepository);
			Manufacturer selectedManufacturer = manufacturerSelector.Select();
			//get the model.
			string model = view.EnterModel();
			//get the price
			decimal price = view.EnterPrice();
			//get the gpuModel
			string gpuModel = view.EnterGPUModel();
			//get the gpuClockSpeed
			ushort gpuClockSpeed = view.EnterGPUClockSpeed();
			//get the vramSize
			ushort vramSize = view.EnterVRamSize();
			//get the vramClockspeed
			ushort vramClockSpeed = view.EnterVRamClockSpeed();
			//get the gddrVersion
			GDDRVersion gddrVersion = view.SelectGDDRVersion();
			//get the gpuType
			GPUType gpuType = view.SelectGPUType();

			//update the gpu.
			gpu.Manufacturer = selectedManufacturer;
			gpu.Model = model;
			gpu.Price = price;
			gpu.GpuModel = gpuModel;
			gpu.GpuClockSpeed = gpuClockSpeed;
			gpu.VRamSize = vramSize;
			gpu.VRamClockSpeed = vramClockSpeed;
			gpu.GddrVersion = gddrVersion;
			gpu.GpuType = gpuType;
			//ask the factory to update the gpu.
			factory.EditGPU(gpu);
		}

		public void DeleteGPU()
		{
			//let the user choose a gpu to delete.
			EntitySelectorController<GPU> gpuSelector = new EntitySelectorController<GPU>(gpuRepository);
			GPU gpu = gpuSelector.Select();
			//ask the factory to remove it.
			factory.DeleteGPU(gpu);
		}
	}
}
