using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Linq;
using System.Transactions;
using System.Diagnostics;

using ComputerShop.Interfaces;
using ComputerShop.Domain;

using ComputerShopDB = ComputerShop.DAL.Linq2Sql;
using ManufacturerEntity = ComputerShop.DAL.Linq2Sql.Manufacturer;
using CPUEntity = ComputerShop.DAL.Linq2Sql.CPU;
using RamModuleEntity = ComputerShop.DAL.Linq2Sql.RamModule;
using HDDEntity = ComputerShop.DAL.Linq2Sql.HDD;
using GPUEntity = ComputerShop.DAL.Linq2Sql.GPU;
using LaptopEntity = ComputerShop.DAL.Linq2Sql.Laptop;
using LaptopRamModuleEntity = ComputerShop.DAL.Linq2Sql.LaptopRamModule;

namespace ComputerShop.DataMappers.Linq2Sql
{
	public class Linq2SqlLaptopDataMapper : Linq2SqlDataMapperBase<Laptop>
	{
		public override void Insert(Laptop data)
		{
			try
			{
				using (TransactionScope transaction = new TransactionScope())
				{
					//create a Linq2Sql Laptop object
					LaptopEntity laptop = new LaptopEntity();
					//get the Linq2Sql manufacturer object, using the domain manufacture object's id
					ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerID == data.Manufacturer.ID);
					//get the Linq2Sql cpu object, using the domain cpu object's id
					CPUEntity cpu = context.CPUs.Single(c => c.CpuID == data.Cpu.ID);

					//create and EntitySet to hold the DesktopRamModuleEntity's
					EntitySet<LaptopRamModuleEntity> laptopRamModulesEntitys = new EntitySet<LaptopRamModuleEntity>();
					//add each RamModuleEntity form the data's collection
					foreach (KeyValuePair<IRamModule, int> modules in data.RamModuleCollection.RamModules)
					{
						//get the domain RamModule object, and quantity.
						IRamModule module = modules.Key;
						int quantity = modules.Value;

						//get the Linq2Sql RamModuleEntity object that matches.
						RamModuleEntity moduleEntity = context.RamModules.Single(r => r.RamID == module.ID);

						//create the Linq2Sql DesktopRamModuleEntity object
						LaptopRamModuleEntity laptopRamModuleEntity = new LaptopRamModuleEntity();
						laptopRamModuleEntity.RamID = moduleEntity.RamID;
						laptopRamModuleEntity.RamModule = moduleEntity;
						laptopRamModuleEntity.Quantity = quantity;

						laptopRamModulesEntitys.Add(laptopRamModuleEntity);
					}

					//get the Linq2Sql HDD object, using the domain hdd object's id.
					HDDEntity hdd = context.HDDs.Single(h => h.HddID == data.Hdd.ID);
					//get the Linq2Sql GPU object using the domain gpu object's id.
					GPUEntity gpu = context.GPUs.Single(g => g.GpuID == data.Gpu.ID);

					laptop.ManufacturerID = data.Manufacturer.ID;
					laptop.Manufacturer = manufacturer;
					laptop.Model = data.Model;
					laptop.Price = data.Price;
					laptop.CpuID = cpu.CpuID;
					laptop.CPU = cpu;
					laptop.LaptopRamModules = laptopRamModulesEntitys;
					laptop.GpuID = gpu.GpuID;
					laptop.GPU = gpu;
					laptop.HddID = hdd.HddID;
					laptop.HDD = hdd;
					laptop.Weight = data.Weight;
					laptop.BatteryLife = data.BatteryLife;
					laptop.DisplaySize = data.DisplaySize;

					//insert into database and submit changes.
					context.Laptops.InsertOnSubmit(laptop);
					context.SubmitChanges();

					transaction.Complete();
				}				
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				throw;
			}
		}

		public override void Delete(Laptop data)
		{
			try
			{
				//find the Linq2Sql Laptop object
				LaptopEntity laptop = context.Laptops.Single(l => l.LaptopID == data.ID);
				
				//clear the laptops ram collection
				foreach (LaptopRamModuleEntity laptopRamModuleEntity in laptop.LaptopRamModules)
				{
					context.LaptopRamModules.DeleteOnSubmit(laptopRamModuleEntity);
				}				

				//delete it and submit the changes.
				context.Laptops.DeleteOnSubmit(laptop);
				context.SubmitChanges();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				throw;
			}
		}

		public override Laptop Select(int id)
		{
			try
			{
				using (TransactionScope transaction = new TransactionScope())
				{
					//find the Linq2Sql Laptop object
					LaptopEntity laptop = context.Laptops.Single(l => l.LaptopID == id);
					//get the Linq2Sql manufacturer object, using the domain manufacture object's id
					ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerID == laptop.Manufacturer.ManufacturerID);
					//get the Linq2Sql cpu object, using the domain cpu object's id
					CPUEntity cpuEntity = context.CPUs.Single(c => c.CpuID == laptop.CPU.CpuID);
					//create a new domain CPU object.
					CPU cpu = new CPU(cpuEntity.CpuID,
									  new Manufacturer(cpuEntity.Manufacturer.ManufacturerID, cpuEntity.Manufacturer.Name),
									  cpuEntity.Model,
									  cpuEntity.Price,
									  cpuEntity.ClockSpeed,
									  (CPUFormFactor)Enum.Parse(typeof(CPUFormFactor), cpuEntity.CpuFormFactor),
									  (CPUCoreType)Enum.Parse(typeof(CPUCoreType), cpuEntity.NoOfCores));

					//create a IRamModuleCollection to hold the RamModules.
					IRamModuleCollection laptopRamModulesCollection = new RamModuleCollection();
					foreach (LaptopRamModuleEntity laptopRamModuleEntity in laptop.LaptopRamModules)
					{
						//get the RamModuleEntity and quantity.
						RamModuleEntity ramModuleEntity = laptopRamModuleEntity.RamModule;
						int quantity = laptopRamModuleEntity.Quantity;

						//create a domain RamModule
						IRamModule ramModule = new RamModule(ramModuleEntity.RamID,
															 new Manufacturer(ramModuleEntity.Manufacturer.ManufacturerID, ramModuleEntity.Manufacturer.Name),
															 ramModuleEntity.Model,
															 ramModuleEntity.Price,
															 ramModuleEntity.ClockSpeed,
															 (DDRVersion)Enum.Parse(typeof(DDRVersion), ramModuleEntity.DDRVersion),
															 (RAMFormFactor)Enum.Parse(typeof(RAMFormFactor), ramModuleEntity.RamFormFactor),
															 ramModuleEntity.Size);

						//add it to the collection with the required quantity.
						laptopRamModulesCollection.AddRamModule(ramModule, quantity);
					}

					//get the Linq2Sql GPU object
					GPUEntity gpuEntity = laptop.GPU;
					//create the domain GPU object.
					GPU gpu = new GPU(gpuEntity.GpuID,
									  new Manufacturer(gpuEntity.Manufacturer.ManufacturerID, gpuEntity.Manufacturer.Name),
									  gpuEntity.Model,
									  gpuEntity.Price,
									  gpuEntity.GpuModel,
									  gpuEntity.GpuClockSpeed,
									  gpuEntity.VRamSize,
									  gpuEntity.VRamClockSpeed,
									  (GDDRVersion)Enum.Parse(typeof(GDDRVersion), gpuEntity.GDDRVersion),
									  (GPUType)Enum.Parse(typeof(GPUType), gpuEntity.GpuType));

					//get the Linq2Sql HDD object
					HDDEntity hddEntity = laptop.HDD;
					//create the domain HDD object.
					HDD hdd = new HDD(hddEntity.HddID,
									  new Manufacturer(hddEntity.Manufacturer.ManufacturerID, hddEntity.Manufacturer.Name),
									  hddEntity.Model,
									  hddEntity.Price,
									  hddEntity.Capacity,
									  (HDDFormFactor)Enum.Parse(typeof(HDDFormFactor), hddEntity.HddFormFactor),
									  (HDDInterfaceType)Enum.Parse(typeof(HDDInterfaceType), hddEntity.HddInterface),
									  (HDDType)Enum.Parse(typeof(HDDType), hddEntity.HddType),
									  hddEntity.Speed);

					//create a new domain Laptop object and return it.
					return new Laptop(laptop.LaptopID,
									  new Manufacturer(laptop.Manufacturer.ManufacturerID, laptop.Manufacturer.Name),
									  laptop.Model,
									  laptop.Price,
									  cpu,
									  laptopRamModulesCollection,
									  gpu,
									  hdd,
									  laptop.Weight,
									  laptop.BatteryLife,
									  laptop.DisplaySize);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				throw;
			}
		}

		public override void Update(Laptop data)
		{
			try
			{
				using (TransactionScope transaction = new TransactionScope())
				{
					//get a Linq2Sql Laptop object
					LaptopEntity laptop = context.Laptops.Single(l => l.LaptopID == data.ID);
					//get the Linq2Sql manufacturer object, using the domain manufacture object's id
					ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerID == data.Manufacturer.ID);
					//get the Linq2Sql cpu object, using the domain cpu object's id
					CPUEntity cpu = context.CPUs.Single(c => c.CpuID == data.Cpu.ID);

					//clear the laptops ram collection
					foreach (LaptopRamModuleEntity laptopRamModuleEntity in laptop.LaptopRamModules)
					{
						context.LaptopRamModules.DeleteOnSubmit(laptopRamModuleEntity);
					}	

					//create and EntitySet to hold the LaptopRamModuleEntity's
					EntitySet<LaptopRamModuleEntity> laptopRamModulesEntitys = new EntitySet<LaptopRamModuleEntity>();
					//add each RamModuleEntity form the data's collection
					foreach (KeyValuePair<IRamModule, int> modules in data.RamModuleCollection.RamModules)
					{
						//get the domain RamModule object, and quantity.
						IRamModule module = modules.Key;
						int quantity = modules.Value;

						//get the Linq2Sql RamModuleEntity object that matches.
						RamModuleEntity moduleEntity = context.RamModules.Single(r => r.RamID == module.ID);

						//create the Linq2Sql DesktopRamModuleEntity object
						LaptopRamModuleEntity laptopRamModuleEntity = new LaptopRamModuleEntity();
						laptopRamModuleEntity.RamID = moduleEntity.RamID;
						laptopRamModuleEntity.RamModule = moduleEntity;
						laptopRamModuleEntity.Quantity = quantity;

						laptopRamModulesEntitys.Add(laptopRamModuleEntity);
					}

					//get the Linq2Sql HDD object, using the domain hdd object's id.
					HDDEntity hdd = context.HDDs.Single(h => h.HddID == data.Hdd.ID);
					//get the Linq2Sql GPU object using the domain gpu object's id.
					GPUEntity gpu = context.GPUs.Single(g => g.GpuID == data.Gpu.ID);

					laptop.ManufacturerID = data.Manufacturer.ID;
					laptop.Manufacturer = manufacturer;
					laptop.Model = data.Model;
					laptop.Price = data.Price;
					laptop.CpuID = cpu.CpuID;
					laptop.CPU = cpu;
					laptop.LaptopRamModules = laptopRamModulesEntitys;
					laptop.GpuID = gpu.GpuID;
					laptop.GPU = gpu;
					laptop.HddID = hdd.HddID;
					laptop.HDD = hdd;
					laptop.Weight = data.Weight;
					laptop.BatteryLife = data.BatteryLife;
					laptop.DisplaySize = data.DisplaySize;

					//submit changes.					
					context.SubmitChanges();

					transaction.Complete();
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				throw;
			}
		}

		public override IList<Laptop> GetAll()
		{
			IList<Laptop> laptops = new List<Laptop>();

			foreach (LaptopEntity laptop in context.Laptops)
			{
				try
				{
					//get the Linq2Sql manufacturer object, using the domain manufacture object's id
					ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerID == laptop.ManufacturerID);
					//get the Linq2Sql cpu object, using the domain cpu object's id
					CPUEntity cpuEntity = context.CPUs.Single(c => c.CpuID == laptop.CpuID);
					//create a new domain CPU object.
					CPU cpu = new CPU(cpuEntity.CpuID,
									  new Manufacturer(cpuEntity.Manufacturer.ManufacturerID, cpuEntity.Manufacturer.Name),
									  cpuEntity.Model,
									  cpuEntity.Price,
									  cpuEntity.ClockSpeed,
									  (CPUFormFactor)Enum.Parse(typeof(CPUFormFactor), cpuEntity.CpuFormFactor),
									  (CPUCoreType)Enum.Parse(typeof(CPUCoreType), cpuEntity.NoOfCores));

					//create a IRamModuleCollection to hold the RamModules.
					IRamModuleCollection laptopRamModulesCollection = new RamModuleCollection();
					foreach (LaptopRamModuleEntity laptopRamModuleEntity in laptop.LaptopRamModules)
					{
						//get the RamModuleEntity and quantity.
						RamModuleEntity ramModuleEntity = laptopRamModuleEntity.RamModule;
						int quantity = laptopRamModuleEntity.Quantity;

						//create a domain RamModule
						IRamModule ramModule = new RamModule(ramModuleEntity.RamID,
															 new Manufacturer(ramModuleEntity.Manufacturer.ManufacturerID, ramModuleEntity.Manufacturer.Name),
															 ramModuleEntity.Model,
															 ramModuleEntity.Price,
															 ramModuleEntity.ClockSpeed,
															 (DDRVersion)Enum.Parse(typeof(DDRVersion), ramModuleEntity.DDRVersion),
															 (RAMFormFactor)Enum.Parse(typeof(RAMFormFactor), ramModuleEntity.RamFormFactor),
															 ramModuleEntity.Size);

						//add it to the collection with the required quantity.
						laptopRamModulesCollection.AddRamModule(ramModule, quantity);
					}

					//get the Linq2Sql GPU object
					GPUEntity gpuEntity = laptop.GPU;
					//create the domain GPU object.
					GPU gpu = new GPU(gpuEntity.GpuID,
									  new Manufacturer(gpuEntity.Manufacturer.ManufacturerID, gpuEntity.Manufacturer.Name),
									  gpuEntity.Model,
									  gpuEntity.Price,
									  gpuEntity.GpuModel,
									  gpuEntity.GpuClockSpeed,
									  gpuEntity.VRamSize,
									  gpuEntity.VRamClockSpeed,
									  (GDDRVersion)Enum.Parse(typeof(GDDRVersion), gpuEntity.GDDRVersion),
									  (GPUType)Enum.Parse(typeof(GPUType), gpuEntity.GpuType));

					//get the Linq2Sql HDD object
					HDDEntity hddEntity = laptop.HDD;
					//create the domain HDD object.
					HDD hdd = new HDD(hddEntity.HddID,
									  new Manufacturer(hddEntity.Manufacturer.ManufacturerID, hddEntity.Manufacturer.Name),
									  hddEntity.Model,
									  hddEntity.Price,
									  hddEntity.Capacity,
									  (HDDFormFactor)Enum.Parse(typeof(HDDFormFactor), hddEntity.HddFormFactor),
									  (HDDInterfaceType)Enum.Parse(typeof(HDDInterfaceType), hddEntity.HddInterface),
									  (HDDType)Enum.Parse(typeof(HDDType), hddEntity.HddType),
									  hddEntity.Speed);

					//create a new domain Laptop object and add it to the list of laptops
					Laptop l = new Laptop(laptop.LaptopID,
									  new Manufacturer(laptop.Manufacturer.ManufacturerID, laptop.Manufacturer.Name),
									  laptop.Model,
									  laptop.Price,
									  cpu,
									  laptopRamModulesCollection,
									  gpu,
									  hdd,
									  laptop.Weight,
									  laptop.BatteryLife,
									  laptop.DisplaySize);

					laptops.Add(l);

				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
					throw;
				}
			}
			return laptops;
		}
	}
}
