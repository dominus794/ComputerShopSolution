using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;
using ComputerShop.Repository;

namespace ComputerShop.MVC.Models
{
	public class DesktopFactory
	{
		private IRepository<Desktop> desktopRepository;

		public DesktopFactory(IRepository<Desktop> desktopRepository)
		{
			this.desktopRepository = desktopRepository;
		}

		public void CreateDesktop(IManufacturer manufacturer, string model, decimal price, ICPU cpu, 
								  IRamModuleCollection ramModuleCollection,
								  IHDDCollection hddCollection,
								  IGPUCollection gpuCollection)
		{
			Desktop newDesktop = new Desktop(0, manufacturer, model, price, cpu, ramModuleCollection, hddCollection, gpuCollection);
			desktopRepository.Add(newDesktop);
		}

		public void EditDesktop(Desktop desktop)
		{
			desktopRepository.Save(desktop);
		}

		public void DeleteDesktop(Desktop desktop)
		{
			desktopRepository.Remove(desktop);
		}
	}
}
