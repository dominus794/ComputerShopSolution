using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;
using ComputerShop.Repository;

namespace ComputerShop.MVC.Models
{
	public class GPUFactory
	{
		private IRepository<GPU> gpuRepository;

		public GPUFactory(IRepository<GPU> gpuRepository)
		{
			this.gpuRepository = gpuRepository;
		}

		public void CreateGPU(IManufacturer manufacturer, string model, decimal price,
							  string gpuModel, ushort gpuClockSpeed, ushort vramSize, ushort vramClockSpeed,
							  GDDRVersion gddrVersion, GPUType gpuType)
		{
			GPU newGPU = new GPU(0, manufacturer, model, price, gpuModel, gpuClockSpeed, vramSize, vramClockSpeed, gddrVersion, gpuType);
			gpuRepository.Add(newGPU);
		}

		public void EditGPU(GPU gpu)
		{
			gpuRepository.Save(gpu);
		}

		public void DeleteGPU(GPU gpu)
		{
			gpuRepository.Remove(gpu);
		}
	}
}
