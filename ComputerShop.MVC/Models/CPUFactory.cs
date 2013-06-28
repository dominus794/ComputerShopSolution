using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;
using ComputerShop.Repository;

namespace ComputerShop.MVC.Models
{
	public class CPUFactory
	{
		private IRepository<CPU> cpuRepository;

		public CPUFactory(IRepository<CPU> cpuRepository)
		{
			this.cpuRepository = cpuRepository;
		}

		public void CreateCPU(IManufacturer manufacturer, string model, decimal price,
							  float clockSpeed, CPUFormFactor cpuFormFactor, CPUCoreType cpuCoreType)
		{
			CPU newCPU = new CPU(0, manufacturer, model, price, clockSpeed, cpuFormFactor, cpuCoreType);
			cpuRepository.Add(newCPU);
		}

		public void EditCPU(CPU cpu)
		{
			cpuRepository.Save(cpu);
		}

		public void DeleteCPU(CPU cpu)
		{
			cpuRepository.Remove(cpu);
		}
	}
}
