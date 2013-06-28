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
	public class HDDFactoryController
	{
		private IRepository<HDD> hddRepository;
		private IRepository<Manufacturer> manufacturerRepository;
		private HDDFactoryView view;
		private HDDFactory factory;

		public HDDFactoryController(IRepository<HDD> hddRepository, IRepository<Manufacturer> manufacturerRepository)
		{
			this.hddRepository = hddRepository;
			this.manufacturerRepository = manufacturerRepository;
			view = new HDDFactoryView();
			factory = new HDDFactory(this.hddRepository);
		}

		public void CreateHDD()
		{		
			//let the user choose a manufacturer
			EntitySelectorController<Manufacturer> manufacturerSelector = new EntitySelectorController<Manufacturer>(manufacturerRepository);
			Manufacturer selectedManufacturer = manufacturerSelector.Select();
			//get the hdd model.
			string model = view.EnterModel();
			//get the price 
			decimal price = view.EnterPrice();
			//get the capacity
			ushort capacity = view.EnterCapacity();
			//get the speed
			ushort speed = view.EnterSpeed();
			//get the hddType
			HDDType hddType = view.SelectHDDType();
			//get the hddInterfaceType
			HDDInterfaceType hddInterfaceType = view.SelectHDDInterfaceType();
			//get the hddFormFactor
			HDDFormFactor hddFormFactor = view.SelectHDDFormFactor();

			//ask the factory to create a HDD
			factory.CreateHDD(selectedManufacturer, model, price, capacity, hddFormFactor, hddInterfaceType, hddType, speed);
		}

		public void EditHDD()
		{
			//let the user choose the hdd to edit.
			EntitySelectorController<HDD> hddSelector = new EntitySelectorController<HDD>(hddRepository);
			HDD hdd = hddSelector.Select();
			//let the user choose a manufacturer
			EntitySelectorController<Manufacturer> manufacturerSelector = new EntitySelectorController<Manufacturer>(manufacturerRepository);
			Manufacturer selectedManufacturer = manufacturerSelector.Select();
			//get the hdd model.
			string model = view.EnterModel();
			//get the price 
			decimal price = view.EnterPrice();
			//get the capacity
			ushort capacity = view.EnterCapacity();
			//get the speed
			ushort speed = view.EnterSpeed();
			//get the hddType
			HDDType hddType = view.SelectHDDType();
			//get the hddInterfaceType
			HDDInterfaceType hddInterfaceType = view.SelectHDDInterfaceType();
			//get the hddFormFactor
			HDDFormFactor hddFormFactor = view.SelectHDDFormFactor();

			//update the hdd
			hdd.Manufacturer = selectedManufacturer;
			hdd.Model = model;
			hdd.Price = price;
			hdd.Capacity = capacity;
			hdd.Speed = speed;
			hdd.HddType = hddType;
			hdd.HddInterfaceType = hddInterfaceType;
			hdd.HddFormFactor = hddFormFactor;
			//ask the factory to update it.
			factory.EditHDD(hdd);
		}

		public void DeleteHDD()
		{
			//let the user choose the hdd to delete.
			EntitySelectorController<HDD> hddSelector = new EntitySelectorController<HDD>(hddRepository);
			HDD hdd = hddSelector.Select();
			//ask the factory to delete it.
			factory.DeleteHDD(hdd);
		}
	}
}
