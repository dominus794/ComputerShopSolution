using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;
using ComputerShop.Repository;
using ComputerShop.MVC.Models;
using ComputerShop.MVC.Views;

namespace ComputerShop.MVC.Controllers
{
	public class ManufacturerFactoryController
	{
		private IRepository<Manufacturer> manufacturerRepository;
		private ManufacturerFactoryView view;
		private ManufacturerFactory factory;

		public ManufacturerFactoryController(IRepository<Manufacturer> manufacturerRepository)
		{
			this.manufacturerRepository = manufacturerRepository;
			view = new ManufacturerFactoryView();
			factory = new ManufacturerFactory(this.manufacturerRepository);
		}

		public void CreateManufacturer()
		{
			string name = view.EnterManufacturerName();
			//string name = Console.ReadLine();
			factory.CreateManufacturer(name);
		}

		public void EditManufacturer()
		{
			//create selector, and pass it a ref to the repository.			
			EntitySelectorController<Manufacturer> manufacturerSelector = new EntitySelectorController<Manufacturer>(manufacturerRepository);
			// user must have selected a valid option.
			Manufacturer selectedManufacturer = manufacturerSelector.Select(); // selector.SelectManufacturer();			
			//let the user choose a new name.
			string name = view.EnterManufacturerName();
			//string name = Console.ReadLine();
			//update the manufacturer object.
			selectedManufacturer.Name = name;
			//save to the datastore.
			factory.EditManufacturer(selectedManufacturer);
		}

		public void DeleteManufacturer()
		{
			EntitySelectorController<Manufacturer> manufacturerSelector = new EntitySelectorController<Manufacturer>(manufacturerRepository);
			// user must have selected a valid option.
			Manufacturer selectedManufacturer = manufacturerSelector.Select(); // selector.SelectManufacturer();
			//delete it
			factory.DeleteManufacturer(selectedManufacturer);
		}
	}
}
