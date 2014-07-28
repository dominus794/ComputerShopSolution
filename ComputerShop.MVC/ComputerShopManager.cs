using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;
using ComputerShop.Repository;
using ComputerShop.DataMappers;
using ComputerShop.DataMappers.SQLDataSet;
using ComputerShop.DataMappers.Linq2Sql;
using ComputerShop.DataMappers.EntityFramework;
using ComputerShop.MVC.Controllers;

namespace ComputerShop.MVC
{
	public class ComputerShopManager
	{
		#region Declarations
		private static string errorMessage;
		private static bool showError = false;

		//repositorys
		private IRepository<Desktop> desktopRepository;
		private IRepository<Laptop> laptopRepository;
		private IRepository<Manufacturer> manufacturerRepository;
		private IRepository<CPU> cpuRepository;
		private IRepository<RamModule> ramModuleRepository;
		private IRepository<HDD> hddRepository;
		private IRepository<GPU> gpuRepository;
		//factory controllers
		ManufacturerFactoryController manufacturerFactoryController;
		CPUFactoryController cpuFactoryController;
		RamModuleFactoryController ramModuleFactoryController;
		HDDFactoryController hddFactoryController;
		GPUFactoryController gpuFactoryController;
		DesktopFactoryController desktopFactoryController;
		LaptopFactoryController laptopFactoryController;
		#endregion

		#region Constructor
		public ComputerShopManager()
		{
			//instantiate repositorys			
			desktopRepository = new Repository<Desktop>(new DesktopDataMapperFactory());
			laptopRepository = new Repository<Laptop>(new LaptopDataMapperFactory());
			manufacturerRepository = new Repository<Manufacturer>(new ManufacturerDataMapperFactory());
			cpuRepository = new Repository<CPU>(new CPUDataMapperFactory());
			ramModuleRepository = new Repository<RamModule>(new RamModuleDataMapperFactory());
			hddRepository = new Repository<HDD>(new HDDDataMapperFactory());
			gpuRepository = new Repository<GPU>(new GPUDataMapperFactory());
			

			/* SQLDataSet
			desktopRepository = new Repository<Desktop>(new SQLDataSetDesktopDataMapperFactory());
			laptopRepository = new Repository<Laptop>(new SQLDataSetLaptopDataMapperFactory());			
			manufacturerRepository = new Repository<Manufacturer>(new SQLDataSetManufacturerDataMapperFactory());
			cpuRepository = new Repository<CPU>(new SQLDataSetCPUDataMapperFactory());						
			ramModuleRepository = new Repository<RamModule>(new SQLDataSetRamModuleDataMapperFactory());
			hddRepository = new Repository<HDD>(new SQLDataSetHDDDataMapperFactory());
			gpuRepository = new Repository<GPU>(new SQLDataSetGPUDataMapperFactory());
			*/

			/* Linq2Sql
			desktopRepository = new Repository<Desktop>(new Linq2SqlDesktopDataMapperFactory());
			laptopRepository = new Repository<Laptop>(new Linq2SqlLaptopDataMapperFactory());			
			manufacturerRepository = new Repository<Manufacturer>(new Linq2SqlManufacturerDataMapperFactory());
			cpuRepository = new Repository<CPU>(new Linq2SqlCPUDataMapperFactory());						
			ramModuleRepository = new Repository<RamModule>(new Linq2SqlRamModuleDataMapperFactory());
			hddRepository = new Repository<HDD>(new Linq2SqlHDDDataMapperFactory());
			gpuRepository = new Repository<GPU>(new Linq2SqlGPUDataMapperFactory());			
			*/

			//instantiate controllers
			manufacturerFactoryController = new ManufacturerFactoryController(manufacturerRepository);
			cpuFactoryController = new CPUFactoryController(cpuRepository, manufacturerRepository);
			ramModuleFactoryController = new RamModuleFactoryController(ramModuleRepository, manufacturerRepository);
			hddFactoryController = new HDDFactoryController(hddRepository, manufacturerRepository);
			gpuFactoryController = new GPUFactoryController(gpuRepository, manufacturerRepository);

			desktopFactoryController = new DesktopFactoryController(desktopRepository,
																    manufacturerRepository,
																    cpuRepository,
																    ramModuleRepository,
																    hddRepository,
																    gpuRepository);

			laptopFactoryController = new LaptopFactoryController(laptopRepository,
																  manufacturerRepository,
																  cpuRepository,
																  ramModuleRepository,
																  hddRepository,
																  gpuRepository);
		}
		#endregion

		#region Properties
		/*
		public IRepository<Desktop> Desktops { get { return desktopRepository; } }
		public IRepository<Laptop> Laptops { get { return laptopRepository; } }
		public IRepository<Manufacturer> Manufacturers { get { return manufacturerRepository; }	}
		public IRepository<CPU> CPUs { get { return cpuRepository; } }
		public IRepository<RamModule> RamModules { get { return ramModuleRepository; } }
		public IRepository<HDD> HDDs { get { return hddRepository; } }
		public IRepository<GPU> GPUs { get { return gpuRepository; } }
		*/
		public DesktopFactoryController DesktopFactory { get { return desktopFactoryController; } }
		public LaptopFactoryController LaptopFactory { get { return laptopFactoryController; } }
		#endregion

		#region GetAll() Method Wrappers
		public IList<Desktop> GetAllDesktops()
		{
			return this.desktopRepository.GetAll().ToList<Desktop>();
		}
		public IList<Laptop> GetAllLaptops()
		{
			return this.laptopRepository.GetAll().ToList<Laptop>();
		}
		#endregion

		#region Search Methods
		public IList<Desktop> SearchDesktops(Expression<Func<Desktop, bool>> predicate)
		{
			return this.desktopRepository.Search(predicate).ToList<Desktop>();
		}
		public IList<Laptop> SearchLaptops(Expression<Func<Laptop, bool>> predicate)
		{
			return this.laptopRepository.Search(predicate).ToList<Laptop>();
		}
		#endregion

		#region Main Menu
		public void DisplayMainMenu()
		{
			bool finished = false;
			string input = String.Empty;
			int choice = 0;

			while (!finished)
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("----ComputerShop Database Manager----");
				Console.ResetColor();				
				/*
				Console.WriteLine("Please select a command below:");
				Console.WriteLine("1. Manage Manufacturers");
				Console.WriteLine("2. Manage Processors");
				Console.WriteLine("3. Manage Ram Modules");
				Console.WriteLine("4. Manage Hard drives");
				Console.WriteLine("5. Manage Graphics cards");
				Console.WriteLine("6. Manage Desktops");
				Console.WriteLine("7. Manage Laptops");
				Console.WriteLine("8. Go Back");
				*/

				Console.Write("Please select a "); Console.ForegroundColor = ConsoleColor.Green; Console.Write("command "); Console.ResetColor(); Console.WriteLine("below:");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("1"); Console.ResetColor(); Console.WriteLine(". Manage Manufacturers");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("2"); Console.ResetColor(); Console.WriteLine(". Manage CPUs");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("3"); Console.ResetColor(); Console.WriteLine(". Manage Ram Modules");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("4"); Console.ResetColor(); Console.WriteLine(". Manage Hard Drives");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("5"); Console.ResetColor(); Console.WriteLine(". Manage Graphics Cards");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("6"); Console.ResetColor(); Console.WriteLine(". Manage Desktops");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("7"); Console.ResetColor(); Console.WriteLine(". Manage Laptops");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("8"); Console.ResetColor(); Console.WriteLine(". Go Back");

				if (showError)
				{
					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine(errorMessage);
					Console.ResetColor();
					showError = false;
				}

				input = Console.ReadLine();

				if (!int.TryParse(input, out choice))
				{
					errorMessage = "Invalid choice. Please try again.";
					showError = true;
					continue;
				}

				switch (choice)
				{
					case 1:
						DisplayManageManufacturersMenu();
						break;
					case 2:
						DisplayManageCPUsMenu();
						break;
					case 3:
						DisplayManageRamModulesMenu();
						break;
					case 4:
						DisplayManageHarddrivesMenu();
						break;
					case 5:
						DisplayManageGPUsMenu();
						break;
					case 6:
						DisplayManageDesktopsMenu();
						break;
					case 7:
						DisplayManageLaptopsMenu();
						break;
					case 8:
						finished = true;
						break;
					default:
						{
							errorMessage = "Invalid choice. Please try again.";
							showError = true;
						}
						break;
				}
			}
		}
		#endregion

		#region Manage Manufacturers
		public void DisplayManageManufacturersMenu()
		{
			bool finished = false;
			string input = String.Empty;
			int choice = 0;

			while (!finished)
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("----Current Manufacturers----");
				Console.ResetColor();

				ListManufacturers();
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("----Manage Manufacturers----");
				Console.ResetColor();
				Console.Write("Please select a "); Console.ForegroundColor = ConsoleColor.Green; Console.Write("command "); Console.ResetColor(); Console.WriteLine("below:");
				/*
				Console.WriteLine("Please select a command below:");				
				Console.WriteLine("1. Add Manufacturer");
				Console.WriteLine("2. Edit Manufacturer");
				Console.WriteLine("3. Delete Manufacturer");
				Console.WriteLine("4. Go Back");
				*/

				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("1"); Console.ResetColor(); Console.WriteLine(". Add Manufacturer");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("2"); Console.ResetColor(); Console.WriteLine(". Edit Manufacturer");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("3"); Console.ResetColor(); Console.WriteLine(". Delete Manufacturer");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("4"); Console.ResetColor(); Console.WriteLine(". Go Back");

				if (showError)
				{
					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine(errorMessage);
					Console.ResetColor();
					showError = false;
				}

				input = Console.ReadLine();

				if (!int.TryParse(input, out choice))
				{
					errorMessage = "Invalid choice. Please try again.";
					showError = true;
					continue;
				}
				
				switch (choice)
				{
					case 1:
						try
						{
							manufacturerFactoryController.CreateManufacturer();
						}
						catch (Exception ex)
						{
							errorMessage = ex.Message;
							showError = true;							
						}
						break;
					case 2:
						try
						{
							manufacturerFactoryController.EditManufacturer();
						}
						catch (Exception ex)
						{
							errorMessage = ex.Message;
							showError = true;
						}						
						break;
					case 3:
						try
						{
							manufacturerFactoryController.DeleteManufacturer();
						}
						catch (Exception ex)
						{
							errorMessage = ex.Message;
							showError = true;
						}
						break;
					case 4:
						finished = true;
						break;
					default:
						{
							errorMessage = "Invalid choice. Please try again.";
							showError = true;
						}
						break;
				}
			}			 
		}

		public void ListManufacturers()
		{						
			IList<Manufacturer> manufacturers = manufacturerRepository.GetAll();

			for (int i = 1; i <= manufacturers.Count; i++)
			{
				//Console.WriteLine(string.Format("{0}.{1}", i, manufacturers[i - 1].ToString()));
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.Write(i); 
				Console.ResetColor();
				Console.WriteLine(string.Format(".{0}", manufacturers[i - 1].ToString()));
			}
		}
		#endregion

		#region Manage CPUs
		public void DisplayManageCPUsMenu()
		{
			bool finished = false;
			string input = String.Empty;
			int choice = 0;

			while (!finished)
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("----Current CPUs ----");
				Console.ResetColor();
				ListCPUs();
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("----Manage CPUs----");
				Console.ResetColor();
				/*
				Console.WriteLine("Please select a command below:");
				Console.WriteLine("1. Add CPU");
				Console.WriteLine("2. Edit CPU");
				Console.WriteLine("3. Delete CPU");
				Console.WriteLine("4. Go Back");
				*/
				Console.Write("Please select a "); Console.ForegroundColor = ConsoleColor.Green; Console.Write("command "); Console.ResetColor(); Console.WriteLine("below:");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("1"); Console.ResetColor(); Console.WriteLine(". Add CPU");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("2"); Console.ResetColor(); Console.WriteLine(". Edit CPU");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("3"); Console.ResetColor(); Console.WriteLine(". Delete CPU");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("4"); Console.ResetColor(); Console.WriteLine(". Go Back");
				Console.WriteLine();

				if (showError)
				{
					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine(errorMessage);
					Console.ResetColor();
					showError = false;
				}

				input = Console.ReadLine();

				if (!int.TryParse(input, out choice))
				{
					errorMessage = "Invalid choice. Please try again.";
					showError = true;
					continue;
				}																	

				switch (choice)
				{
					case 1:
						try
						{
							cpuFactoryController.CreateCPU();
						}
						catch (Exception ex)
						{
							errorMessage = ex.Message;
							showError = true;							
						}
						break;
					case 2:
						try
						{
							cpuFactoryController.EditCPU();
						}
						catch (Exception ex)
						{							
							errorMessage = ex.Message;
							showError = true;
						}
						break;
					case 3:
						try
						{
							cpuFactoryController.DeleteCPU();
						}
						catch (Exception ex)
						{
							errorMessage = ex.Message;
							showError = true;							
						}
						break;
					case 4:
						finished = true;
						break;
					default:
						{
							errorMessage = "Invalid choice. Please try again.";
							showError = true;
						}
						break;
				}
			}
		}

		public void ListCPUs()
		{			
			IList<CPU> cpus = cpuRepository.GetAll();

			for (int i = 1; i <= cpus.Count; i++)
			{
				//Console.WriteLine(string.Format("{0}.{1}", i, cpus[i - 1].ToString()));
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.Write(i);
				Console.ResetColor();
				Console.WriteLine(string.Format(".{0}", cpus[i - 1].ToString()));
			}
		}
		#endregion

		#region Manage RamModules
		public void DisplayManageRamModulesMenu()
		{
			bool finished = false;
			string input = String.Empty;
			int choice = 0;

			while (!finished)
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("----Current RamModules----");
				Console.ResetColor();

				ListRamModules();
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("----Manage RamModules----");
				Console.ResetColor();
				/*
				Console.WriteLine("Please select a command below:");
				Console.WriteLine("1. Add RamModule");
				Console.WriteLine("2. Edit RamModule");
				Console.WriteLine("3. Delete RamModule");
				Console.WriteLine("4. Go Back");
				*/
				Console.Write("Please select a "); Console.ForegroundColor = ConsoleColor.Green; Console.Write("command "); Console.ResetColor(); Console.WriteLine("below:");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("1"); Console.ResetColor(); Console.WriteLine(". Add RamModule");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("2"); Console.ResetColor(); Console.WriteLine(". Edit RamModule");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("3"); Console.ResetColor(); Console.WriteLine(". Delete RamModule");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("4"); Console.ResetColor(); Console.WriteLine(". Go Back");
				Console.WriteLine();

				if (showError)
				{
					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine(errorMessage);
					Console.ResetColor();
					showError = false;
				}

				input = Console.ReadLine();

				if (!int.TryParse(input, out choice))
				{
					errorMessage = "Invalid choice. Please try again.";
					showError = true;
					continue;
				}
				
				switch (choice)
				{
					case 1:
						try
						{
							ramModuleFactoryController.CreateRamModule();
						}
						catch (Exception ex)
						{
							errorMessage = ex.Message;
							showError = true;
						}
						break;
					case 2:
						try
						{
							ramModuleFactoryController.EditRamModule();
						}
						catch (Exception ex)
						{
							errorMessage = ex.Message;
							showError = true;
						}
						break;
					case 3:
						try
						{
							ramModuleFactoryController.DeleteRamModule();
						}
						catch (Exception ex)
						{
							errorMessage = ex.Message;
							showError = true;
						}
						break;
					case 4:
						finished = true;
						break;
					default:
						{
							errorMessage = "Invalid choice. Please try again.";
							showError = true;
						}
						break;
				}
			}
		}

		public void ListRamModules()
		{			
			IList<RamModule> modules = ramModuleRepository.GetAll();

			for (int i = 1; i <= modules.Count; i++)
			{
				//Console.WriteLine(string.Format("{0}.{1}", i, modules[i - 1].ToString()));
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.Write(i);
				Console.ResetColor();
				Console.WriteLine(".{0}", modules[i - 1].ToString());
			}
		}
		#endregion

		#region Manage HDDs
		public void DisplayManageHarddrivesMenu()
		{
			bool finished = false;
			string input = String.Empty;
			int choice = 0;

			while (!finished)
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("----Current Harddrives----");
				Console.ResetColor();
				ListHarddrives();
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("----Manage Harddrives----");
				Console.ResetColor();
				/*
				Console.WriteLine("Please select a command below:");
				Console.WriteLine("1. Add Harddrive");
				Console.WriteLine("2. Edit Harddrive");
				Console.WriteLine("3. Delete Harddrive");
				Console.WriteLine("4. Go Back");
				*/
				Console.Write("Please select a "); Console.ForegroundColor = ConsoleColor.Green; Console.Write("command "); Console.ResetColor(); Console.WriteLine("below:");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("1"); Console.ResetColor(); Console.WriteLine(". Add Harddrive");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("2"); Console.ResetColor(); Console.WriteLine(". Edit Harddrive");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("3"); Console.ResetColor(); Console.WriteLine(". Delete Harddrive");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("4"); Console.ResetColor(); Console.WriteLine(". Go Back");
				Console.WriteLine();

				if (showError)
				{
					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine(errorMessage);
					Console.ResetColor();
					showError = false;
				}

				input = Console.ReadLine();

				if (!int.TryParse(input, out choice))
				{
					errorMessage = "Invalid choice. Please try again.";
					showError = true;
					continue;
				}								

				switch (choice)
				{
					case 1:
						try
						{
							hddFactoryController.CreateHDD();
						}
						catch (Exception ex)
						{
							errorMessage = ex.Message;
							showError = true;
						}
						break;
					case 2:
						try
						{
							hddFactoryController.EditHDD();
						}
						catch (Exception ex)
						{
							errorMessage = ex.Message;
							showError = true;
						}
						break;
					case 3:
						try
						{
							hddFactoryController.DeleteHDD();
						}
						catch (Exception ex)
						{
							errorMessage = ex.Message;
							showError = true;
						}
						break;
					case 4:
						finished = true;
						break;
					default:
						{
							errorMessage = "Invalid choice. Please try again.";
							showError = true;
						}
						break;
				}
			}
		}

		public void ListHarddrives()
		{
			IList<HDD> hdds = hddRepository.GetAll().ToList<HDD>();

			for (int i = 1; i <= hdds.Count; i++)
			{
				//Console.WriteLine(string.Format("{0}. {1}", i, hdds[i - 1].ToString()));
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.Write(i);
				Console.ResetColor();
				Console.WriteLine(".{0}", hdds[i - 1].ToString());
			}
		}
		#endregion

		#region Manage GPUs
		public void DisplayManageGPUsMenu()
		{
			bool finished = false;
			string input = String.Empty;
			int choice = 0;

			while (!finished)
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("----Current GPUs----");
				Console.ResetColor();
				ListGraphicsCards();
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("----Manage GPUs----");
				Console.ResetColor();
				/*
				Console.WriteLine("Please select a command below:");
				Console.WriteLine("1. Add GPU");
				Console.WriteLine("2. Edit GPU");
				Console.WriteLine("3. Delete GPU");
				Console.WriteLine("4. Go Back");
				*/
				Console.Write("Please select a "); Console.ForegroundColor = ConsoleColor.Green; Console.Write("command "); Console.ResetColor(); Console.WriteLine("below:");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("1"); Console.ResetColor(); Console.WriteLine(". Add GPU");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("2"); Console.ResetColor(); Console.WriteLine(". Edit GPU");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("3"); Console.ResetColor(); Console.WriteLine(". Delete GPU");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("4"); Console.ResetColor(); Console.WriteLine(". Go Back");
				Console.WriteLine();

				if (showError)
				{
					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine(errorMessage);
					Console.ResetColor();
					showError = false;
				}

				input = Console.ReadLine();

				if (!int.TryParse(input, out choice))
				{
					errorMessage = "Invalid choice. Please try again.";
					showError = true;
					continue;
				}												

				switch (choice)
				{
					case 1:
						try
						{
							gpuFactoryController.CreateGPU();
						}
						catch (Exception ex)
						{
							errorMessage = ex.Message;
							showError = true;
						}
						break;
					case 2:
						try
						{
							gpuFactoryController.EditGPU();
						}
						catch (Exception ex)
						{
							errorMessage = ex.Message;
							showError = true;
						}
						break;
					case 3:
						try
						{
							gpuFactoryController.DeleteGPU();
						}
						catch (Exception ex)
						{
							errorMessage = ex.Message;
							showError = true;
						}
						break;
					case 4:
						finished = true;
						break;
					default:
						{
							errorMessage = "Invalid choice. Please try again.";
							showError = true;
						}
						break;
				}
			}
		}

		public void ListGraphicsCards()
		{			
			List<GPU> gpus = gpuRepository.GetAll().ToList<GPU>();

			for (int i = 1; i <= gpus.Count; i++)
			{
				//Console.WriteLine(string.Format("{0}. {1}", i, gpus[i - 1].ToString()));
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.Write(i);
				Console.ResetColor();
				Console.WriteLine(".{0}", gpus[i - 1].ToString());
			}
		}
		#endregion

		#region Manage Desktops
		public void DisplayManageDesktopsMenu()
		{
			bool finished = false;
			string input = String.Empty;
			int choice = 0;

			while (!finished)
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("----Current Desktops----");
				Console.ResetColor();
				ListDesktops();
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("----Manage Desktops----");
				Console.ResetColor();
				/*
				Console.WriteLine("Please select a command below:");
				Console.WriteLine("1. Add Desktop");
				Console.WriteLine("2. Edit Desktop");
				Console.WriteLine("3. Delete Desktop");
				Console.WriteLine("4. Go Back");
				*/
				Console.Write("Please select a "); Console.ForegroundColor = ConsoleColor.Green; Console.Write("command "); Console.ResetColor(); Console.WriteLine("below:");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("1"); Console.ResetColor(); Console.WriteLine(". Add Desktop");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("2"); Console.ResetColor(); Console.WriteLine(". Edit Desktop");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("3"); Console.ResetColor(); Console.WriteLine(". Delete Desktop");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("4"); Console.ResetColor(); Console.WriteLine(". Go Back");
				Console.WriteLine();

				if (showError)
				{
					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine(errorMessage);
					Console.ResetColor();
					showError = false;
				}

				input = Console.ReadLine();

				if (!int.TryParse(input, out choice))
				{
					errorMessage = "Invalid choice. Please try again.";
					showError = true;
					continue;
				}				

				switch (choice)
				{
					case 1:
						try
						{
							desktopFactoryController.CreateDesktop();
						}
						catch (Exception ex)
						{
							errorMessage = ex.Message;
							showError = true;
						}						
						break;
					case 2:
						try
						{
							desktopFactoryController.EditDesktop();
						}
						catch (Exception ex)
						{
							errorMessage = ex.Message;
							showError = true;
						}
						break;
					case 3:
						try
						{
							desktopFactoryController.DeleteDesktop();
						}
						catch (Exception ex)
						{
							errorMessage = ex.Message;
							showError = true;
						}
						break;
					case 4:
						finished = true;
						break;
					default:
						Console.WriteLine("Invalid choice. Please try again.");
						break;
				}
			}
		}

		public void ListDesktops()
		{			
			IList<Desktop> desktops = desktopRepository.GetAll();

			for (int i = 1; i <= desktops.Count; i++)
			{
				//Console.WriteLine("{0}. {1}", i, desktops[i - 1].ToString());
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.Write(i);
				Console.ResetColor();
				Console.WriteLine(".{0}", desktops[i - 1].ToString());
			}
		}
		#endregion

		#region Manage Laptops
		public void DisplayManageLaptopsMenu()
		{
			bool finished = false;
			string input = String.Empty;
			int choice = 0;

			while (!finished)
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("----Current Laptops----");
				Console.ResetColor();
				ListLaptops();
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("----Manage Laptops----");
				Console.ResetColor();
				/*
				Console.WriteLine("Please select a command below:");
				Console.WriteLine("1. Add Laptop");
				Console.WriteLine("2. Edit Laptop");
				Console.WriteLine("3. Delete Laptop");
				Console.WriteLine("4. Go Back");
				*/
				Console.Write("Please select a "); Console.ForegroundColor = ConsoleColor.Green; Console.Write("command "); Console.ResetColor(); Console.WriteLine("below:");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("1"); Console.ResetColor(); Console.WriteLine(". Add Laptop");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("2"); Console.ResetColor(); Console.WriteLine(". Edit Laptop");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("3"); Console.ResetColor(); Console.WriteLine(". Delete Laptop");
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write("4"); Console.ResetColor(); Console.WriteLine(". Go Back");
				Console.WriteLine();

				if (showError)
				{
					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.DarkRed;
					Console.WriteLine(errorMessage);
					Console.ResetColor();
					showError = false;
				}

				input = Console.ReadLine();

				if (!int.TryParse(input, out choice))
				{
					errorMessage = "Invalid choice. Please try again.";
					showError = true;
					continue;
				}									

				switch (choice)
				{
					case 1:
						try
						{
							laptopFactoryController.CreateLaptop();
						}
						catch (Exception ex)
						{
							errorMessage = ex.Message;
							showError = true;
						}
						break;
					case 2:
						try
						{
							laptopFactoryController.EditLaptop();
						}
						catch (Exception ex)
						{
							errorMessage = ex.Message;
							showError = true;
						}
						break;
					case 3:
						try
						{
							laptopFactoryController.DeleteLaptop();
						}
						catch (Exception ex)
						{
							errorMessage = ex.Message;
							showError = true;
						}
						break;
					case 4:
						finished = true;
						break;
					default:
						Console.WriteLine("Invalid choice. Please try again.");
						break;
				}
			}
		}

		public void ListLaptops()
		{			
			IList<Laptop> laptops = laptopRepository.GetAll();

			for (int i = 1; i <= laptops.Count; i++)
			{
				//Console.WriteLine("{0}. {1}", i, laptops[i - 1].ToString());
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.Write(i);
				Console.ResetColor();
				Console.WriteLine(".{0}", laptops[i - 1].ToString());
			}
		}
		#endregion
	}
}
