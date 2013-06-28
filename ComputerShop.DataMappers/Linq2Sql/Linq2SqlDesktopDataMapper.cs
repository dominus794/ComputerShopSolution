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
using DesktopEntity = ComputerShop.DAL.Linq2Sql.Desktop;
using DesktopRamModuleEntity = ComputerShop.DAL.Linq2Sql.DesktopRamModule;
using DesktopHDDEntity = ComputerShop.DAL.Linq2Sql.DesktopHDD;
using DesktopGPUEntity = ComputerShop.DAL.Linq2Sql.DesktopGPU;

namespace ComputerShop.DataMappers.Linq2Sql
{
	public class Linq2SqlDesktopDataMapper : Linq2SqlDataMapperBase<Desktop>
	{
		public override void Insert(Desktop data)
		{
			try
			{
				using (TransactionScope transaction = new TransactionScope())
				{
					//create a Linq2Sql Desktop object
					DesktopEntity desktop = new DesktopEntity();
					//get the Linq2Sql manufacturer object, using the domain manufacture object's id
					ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerID == data.Manufacturer.ID);
					//get the Linq2Sql cpu object, using the domain cpu object's id
					CPUEntity cpu = context.CPUs.Single(c => c.CpuID == data.Cpu.ID);

					//create and EntitySet to hold the DesktopRamModuleEntity's
					EntitySet<DesktopRamModuleEntity> desktopRamModulesEntitys = new EntitySet<DesktopRamModuleEntity>();
					//add each RamModuleEntity form the data's collection
					foreach (KeyValuePair<IRamModule, int> modules in data.RamModuleCollection.RamModules)
					{
						//get the domain RamModule object, and quantity.
						IRamModule module = modules.Key;
						int quantity = modules.Value;

						//get the Linq2Sql RamModuleEntity object that matches.
						RamModuleEntity moduleEntity = context.RamModules.Single(r => r.RamID == module.ID);

						//create the Linq2Sql DesktopRamModuleEntity object
						DesktopRamModuleEntity desktopRamModuleEntity = new DesktopRamModuleEntity();
						desktopRamModuleEntity.RamID = moduleEntity.RamID;
						desktopRamModuleEntity.RamModule = moduleEntity;
						desktopRamModuleEntity.Quantity = quantity;

						desktopRamModulesEntitys.Add(desktopRamModuleEntity);
					}

					//create an EntitySet to hold the DesktopHDDEntity's
					EntitySet<DesktopHDDEntity> desktopHDDEntitys = new EntitySet<DesktopHDDEntity>();
					//add each HDDEntity from the data's hdd collection
					foreach (KeyValuePair<IHDD, int> hdds in data.HddCollection.HDDs)
					{
						IHDD hdd = hdds.Key;
						int quantity = hdds.Value;

						HDDEntity hddEntity = context.HDDs.Single(h => h.HddID == hdd.ID);

						DesktopHDDEntity desktopHDDEntity = new DesktopHDDEntity();
						desktopHDDEntity.HddID = hddEntity.HddID;
						desktopHDDEntity.HDD = hddEntity;
						desktopHDDEntity.Quantity = quantity;

						desktopHDDEntitys.Add(desktopHDDEntity);
					}

					//create an EntitySet to hold the DesktopGPUEntity's
					EntitySet<DesktopGPUEntity> desktopGPUEntitys = new EntitySet<DesktopGPUEntity>();
					//add each GPUEntity from the data's gpu collection
					foreach (KeyValuePair<IGPU, int> gpus in data.GpuCollection.GPUs)
					{
						IGPU gpu = gpus.Key;
						int quantity = gpus.Value;

						GPUEntity gpuEntity = context.GPUs.Single(g => g.GpuID == gpu.ID);

						DesktopGPUEntity desktopGPUEntity = new DesktopGPUEntity();
						desktopGPUEntity.GpuID = gpuEntity.GpuID;
						desktopGPUEntity.GPU = gpuEntity;
						desktopGPUEntity.Quantity = quantity;

						desktopGPUEntitys.Add(desktopGPUEntity);
					}

					desktop.ManufacturerID = data.Manufacturer.ID;
					desktop.Manufacturer = manufacturer;
					desktop.Model = data.Model;
					desktop.Price = data.Price;
					desktop.CpuID = data.Cpu.ID;
					desktop.CPU = cpu;
					desktop.DesktopRamModules = desktopRamModulesEntitys;
					desktop.DesktopHDDs = desktopHDDEntitys;
					desktop.DesktopGPUs = desktopGPUEntitys;
					desktop.MonitorID = 1;

					//insert into database and submit changes.
					context.Desktops.InsertOnSubmit(desktop);
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

		public override void Delete(Desktop data)
		{
			try
			{
				//find the Linq2Sql Desktop object
				DesktopEntity desktop = context.Desktops.Single(d => d.DesktopID == data.ID);

				//clear the desktop's collections
				foreach (DesktopRamModuleEntity desktopRamModuleEntity in desktop.DesktopRamModules)
				{
					context.DesktopRamModules.DeleteOnSubmit(desktopRamModuleEntity);
				}
				foreach (DesktopHDDEntity desktopHDDEntity in desktop.DesktopHDDs)
				{
					context.DesktopHDDs.DeleteOnSubmit(desktopHDDEntity);
				}
				foreach (DesktopGPUEntity desktopGPUEntity in desktop.DesktopGPUs)
				{
					context.DesktopGPUs.DeleteOnSubmit(desktopGPUEntity);
				}

				//delete it and submit the changes.
				context.Desktops.DeleteOnSubmit(desktop);
				context.SubmitChanges();				
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				throw;
			}
		}

		public override Desktop Select(int id)
		{
			try
			{
				//find the Linq2Sql Desktop object
				DesktopEntity desktop = context.Desktops.Single(d => d.DesktopID == id);
				//find the Linq2Sql CPU object
				CPUEntity cpuEntity = context.CPUs.Single(c => c.CpuID == desktop.CPU.CpuID);
				//create a new domain CPU object.
				CPU cpu = new CPU(cpuEntity.CpuID,
								  new Manufacturer(cpuEntity.Manufacturer.ManufacturerID, cpuEntity.Manufacturer.Name),
								  cpuEntity.Model,
								  cpuEntity.Price,
								  cpuEntity.ClockSpeed,
								  (CPUFormFactor)Enum.Parse(typeof(CPUFormFactor), cpuEntity.CpuFormFactor),
								  (CPUCoreType)Enum.Parse(typeof(CPUCoreType), cpuEntity.NoOfCores));

				//create a IRamModuleCollection to hold the RamModules.
				IRamModuleCollection desktopRamModulesCollection = new RamModuleCollection();
				foreach(DesktopRamModuleEntity desktopRamModuleEntity in desktop.DesktopRamModules)
				{
					//get the RamModuleEntity and quantity.
					RamModuleEntity ramModuleEntity = desktopRamModuleEntity.RamModule;
					int quantity = desktopRamModuleEntity.Quantity;

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
					desktopRamModulesCollection.AddRamModule(ramModule, quantity);
				}

				//create a IHDDCollection to hold the HDD's
				IHDDCollection desktopHDDCollection = new HDDCollection();
				foreach (DesktopHDDEntity desktopHDDEntity in desktop.DesktopHDDs)
				{
					//get the hdd and quantity.
					HDDEntity hddEntity = desktopHDDEntity.HDD;
					int quantity = desktopHDDEntity.Quantity;

					//create the domain HDD
					IHDD hdd = new HDD(hddEntity.HddID,
									   new Manufacturer(hddEntity.Manufacturer.ManufacturerID, hddEntity.Manufacturer.Name),
									   hddEntity.Model,
									   hddEntity.Price,
									   hddEntity.Capacity,
									   (HDDFormFactor)Enum.Parse(typeof(HDDFormFactor), hddEntity.HddFormFactor),
									   (HDDInterfaceType)Enum.Parse(typeof(HDDInterfaceType), hddEntity.HddInterface),
									   (HDDType)Enum.Parse(typeof(HDDType), hddEntity.HddType),
									   hddEntity.Speed);

					//add it to the collection with the required quantity.
					desktopHDDCollection.AddHDD(hdd, quantity);
				}

				//create a IGPUCollection to hold the GPU's
				IGPUCollection desktopGPUCollection = new GPUCollection();
				foreach (DesktopGPUEntity desktopGPUEntity in desktop.DesktopGPUs)
				{
					//get the gpu and quantity.
					GPUEntity gpuEntity = desktopGPUEntity.GPU;
					int quantity = desktopGPUEntity.Quantity;

					//create the domain GPU
					IGPU gpu = new GPU(gpuEntity.GpuID,
									   new Manufacturer(gpuEntity.Manufacturer.ManufacturerID, gpuEntity.Manufacturer.Name),
									   gpuEntity.Model,
									   gpuEntity.Price,
									   gpuEntity.GpuModel,
									   gpuEntity.GpuClockSpeed,
									   gpuEntity.VRamSize,
									   gpuEntity.VRamClockSpeed,
									   (GDDRVersion)Enum.Parse(typeof(GDDRVersion), gpuEntity.GDDRVersion),
									   (GPUType)Enum.Parse(typeof(GPUType), gpuEntity.GpuType));

					//add it to the collection with the required quantity.
					desktopGPUCollection.AddGPU(gpu, quantity);
				}

				//create a new domain Desktop object and return it.
				return new Desktop(desktop.DesktopID,
								   new Manufacturer(desktop.Manufacturer.ManufacturerID, desktop.Manufacturer.Name),
								   desktop.Model,
								   desktop.Price,
								   cpu,
								   desktopRamModulesCollection,
								   desktopHDDCollection,
								   desktopGPUCollection);

			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				throw;
			}
		}

		public override void Update(Desktop data)
		{
			try
			{
				using (TransactionScope transaction = new TransactionScope())
				{
					//find the Linq2Sql Desktop object
					DesktopEntity desktop = context.Desktops.Single(d => d.DesktopID == data.ID);
					//get the Linq2Sql manufacturer object, using the domain manufacture object's id
					ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerID == data.Manufacturer.ID);
					//get the Linq2Sql cpu object, using the domain cpu object's id
					CPUEntity cpu = context.CPUs.Single(c => c.CpuID == data.Cpu.ID);	
				
					//clear the desktop's collections
					foreach (DesktopRamModuleEntity desktopRamModuleEntity in desktop.DesktopRamModules)
					{
						context.DesktopRamModules.DeleteOnSubmit(desktopRamModuleEntity);
					}
					foreach (DesktopHDDEntity desktopHDDEntity in desktop.DesktopHDDs)
					{
						context.DesktopHDDs.DeleteOnSubmit(desktopHDDEntity);
					}
					foreach (DesktopGPUEntity desktopGPUEntity in desktop.DesktopGPUs)
					{
						context.DesktopGPUs.DeleteOnSubmit(desktopGPUEntity);
					}

					//create and EntitySet to hold the DesktopRamModuleEntity's
					EntitySet<DesktopRamModuleEntity> desktopRamModulesEntitys = new EntitySet<DesktopRamModuleEntity>();
					//add each RamModuleEntity form the data's collection
					foreach (KeyValuePair<IRamModule, int> modules in data.RamModuleCollection.RamModules)
					{
						//get the domain RamModule object, and quantity.
						IRamModule module = modules.Key;
						int quantity = modules.Value;

						//get the Linq2Sql RamModuleEntity object that matches.
						RamModuleEntity moduleEntity = context.RamModules.Single(r => r.RamID == module.ID);						

						//create the Linq2Sql DesktopRamModuleEntity object
						DesktopRamModuleEntity desktopRamModuleEntity = new DesktopRamModuleEntity();
						desktopRamModuleEntity.DesktopID = desktop.DesktopID;
						desktopRamModuleEntity.Desktop = desktop;
						desktopRamModuleEntity.RamID = moduleEntity.RamID;
						desktopRamModuleEntity.RamModule = moduleEntity;
						desktopRamModuleEntity.Quantity = quantity;

						desktopRamModulesEntitys.Add(desktopRamModuleEntity);
					}

					//create an EntitySet to hold the DesktopHDDEntity's
					EntitySet<DesktopHDDEntity> desktopHDDEntitys = new EntitySet<DesktopHDDEntity>();
					//add each HDDEntity from the data's hdd collection
					foreach (KeyValuePair<IHDD, int> hdds in data.HddCollection.HDDs)
					{
						IHDD hdd = hdds.Key;
						int quantity = hdds.Value;

						HDDEntity hddEntity = context.HDDs.Single(h => h.HddID == hdd.ID);

						DesktopHDDEntity desktopHDDEntity = new DesktopHDDEntity();
						desktopHDDEntity.DesktopID = desktop.DesktopID;
						desktopHDDEntity.Desktop = desktop;
						desktopHDDEntity.HddID = hddEntity.HddID;
						desktopHDDEntity.HDD = hddEntity;
						desktopHDDEntity.Quantity = quantity;

						desktopHDDEntitys.Add(desktopHDDEntity);
					}

					//create an EntitySet to hold the DesktopGPUEntity's
					EntitySet<DesktopGPUEntity> desktopGPUEntitys = new EntitySet<DesktopGPUEntity>();
					//add each GPUEntity from the data's gpu collection
					foreach (KeyValuePair<IGPU, int> gpus in data.GpuCollection.GPUs)
					{
						IGPU gpu = gpus.Key;
						int quantity = gpus.Value;

						GPUEntity gpuEntity = context.GPUs.Single(g => g.GpuID == gpu.ID);

						DesktopGPUEntity desktopGPUEntity = new DesktopGPUEntity();
						desktopGPUEntity.DesktopID = desktop.DesktopID;
						desktopGPUEntity.Desktop = desktop;
						desktopGPUEntity.GpuID = gpuEntity.GpuID;
						desktopGPUEntity.GPU = gpuEntity;
						desktopGPUEntity.Quantity = quantity;

						desktopGPUEntitys.Add(desktopGPUEntity);
					}

					desktop.ManufacturerID = data.Manufacturer.ID;
					desktop.Manufacturer = manufacturer;
					desktop.Model = data.Model;
					desktop.Price = data.Price;
					desktop.CpuID = data.Cpu.ID;
					desktop.CPU = cpu;
					desktop.DesktopRamModules = desktopRamModulesEntitys;
					desktop.DesktopHDDs = desktopHDDEntitys;
					desktop.DesktopGPUs = desktopGPUEntitys;
					desktop.MonitorID = 1;
					
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

		public override IList<Desktop> GetAll()
		{
			IList<Desktop> desktops = new List<Desktop>();

			foreach (DesktopEntity desktop in context.Desktops)
			{
				try
				{					
					//find the Linq2Sql CPU object
					CPUEntity cpuEntity = context.CPUs.Single(c => c.CpuID == desktop.CPU.CpuID);
					//create a new domain CPU object.
					CPU cpu = new CPU(cpuEntity.CpuID,
									  new Manufacturer(cpuEntity.Manufacturer.ManufacturerID, cpuEntity.Manufacturer.Name),
									  cpuEntity.Model,
									  cpuEntity.Price,
									  cpuEntity.ClockSpeed,
									  (CPUFormFactor)Enum.Parse(typeof(CPUFormFactor), cpuEntity.CpuFormFactor),
									  (CPUCoreType)Enum.Parse(typeof(CPUCoreType), cpuEntity.NoOfCores));

					//create a IRamModuleCollection to hold the RamModules.
					IRamModuleCollection desktopRamModulesCollection = new RamModuleCollection();
					foreach (DesktopRamModuleEntity desktopRamModuleEntity in desktop.DesktopRamModules)
					{
						//get the RamModuleEntity and quantity.
						RamModuleEntity ramModuleEntity = desktopRamModuleEntity.RamModule;
						int quantity = desktopRamModuleEntity.Quantity;

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
						desktopRamModulesCollection.AddRamModule(ramModule, quantity);
					}

					//create a IHDDCollection to hold the HDD's
					IHDDCollection desktopHDDCollection = new HDDCollection();
					foreach (DesktopHDDEntity desktopHDDEntity in desktop.DesktopHDDs)
					{
						//get the hdd and quantity.
						HDDEntity hddEntity = desktopHDDEntity.HDD;
						int quantity = desktopHDDEntity.Quantity;

						//create the domain HDD
						IHDD hdd = new HDD(hddEntity.HddID,
										   new Manufacturer(hddEntity.Manufacturer.ManufacturerID, hddEntity.Manufacturer.Name),
										   hddEntity.Model,
										   hddEntity.Price,
										   hddEntity.Capacity,
										   (HDDFormFactor)Enum.Parse(typeof(HDDFormFactor), hddEntity.HddFormFactor),
										   (HDDInterfaceType)Enum.Parse(typeof(HDDInterfaceType), hddEntity.HddInterface),
										   (HDDType)Enum.Parse(typeof(HDDType), hddEntity.HddType),
										   hddEntity.Speed);

						//add it to the collection with the required quantity.
						desktopHDDCollection.AddHDD(hdd, quantity);
					}

					//create a IGPUCollection to hold the GPU's
					IGPUCollection desktopGPUCollection = new GPUCollection();
					foreach (DesktopGPUEntity desktopGPUEntity in desktop.DesktopGPUs)
					{
						//get the gpu and quantity.
						GPUEntity gpuEntity = desktopGPUEntity.GPU;
						int quantity = desktopGPUEntity.Quantity;

						//create the domain GPU
						IGPU gpu = new GPU(gpuEntity.GpuID,
										   new Manufacturer(gpuEntity.Manufacturer.ManufacturerID, gpuEntity.Manufacturer.Name),
										   gpuEntity.Model,
										   gpuEntity.Price,
										   gpuEntity.GpuModel,
										   gpuEntity.GpuClockSpeed,
										   gpuEntity.VRamSize,
										   gpuEntity.VRamClockSpeed,
										   (GDDRVersion)Enum.Parse(typeof(GDDRVersion), gpuEntity.GDDRVersion),
										   (GPUType)Enum.Parse(typeof(GPUType), gpuEntity.GpuType));

						//add it to the collection with the required quantity.
						desktopGPUCollection.AddGPU(gpu, quantity);
					}

					//create a new domain Desktop object and add it to the list.
					Desktop d =  new Desktop(desktop.DesktopID,
									         new Manufacturer(desktop.Manufacturer.ManufacturerID, desktop.Manufacturer.Name),
											 desktop.Model,
											 desktop.Price,
											 cpu,
											 desktopRamModulesCollection,
											 desktopHDDCollection,
											 desktopGPUCollection);

					desktops.Add(d);

				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
					throw;
				}				
			}
			return desktops;
		}
	}
}
