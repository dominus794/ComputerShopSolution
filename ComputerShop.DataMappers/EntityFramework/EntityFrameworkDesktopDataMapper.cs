using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Transactions;
using System.Diagnostics;

using ComputerShop.Interfaces;
using ComputerShop.Domain;

using ManufacturerEntity = ComputerShop.DAL.EntityFramework.Manufacturer;
using CPUEntity = ComputerShop.DAL.EntityFramework.CPU;
using RamModuleEntity = ComputerShop.DAL.EntityFramework.RamModule;
using HDDEntity = ComputerShop.DAL.EntityFramework.HDD;
using GPUEntity = ComputerShop.DAL.EntityFramework.GPU;
using DesktopEntity = ComputerShop.DAL.EntityFramework.Desktop;
using DesktopRamModuleEntity = ComputerShop.DAL.EntityFramework.DesktopRamModule;
using DesktopHDDEntity = ComputerShop.DAL.EntityFramework.DesktopHDD;
using DesktopGPUEntity = ComputerShop.DAL.EntityFramework.DesktopGPU;

namespace ComputerShop.DataMappers.EntityFramework
{
    public class EntityFrameworkDesktopDataMapper : EntityFrameworkDataMapperBase<Desktop>
    {
        public EntityFrameworkDesktopDataMapper(string connectionString)
            : base(connectionString) { }

        public EntityFrameworkDesktopDataMapper() { }

        public override void Insert(Desktop data)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    // create the EF Desktop object
                    DesktopEntity desktop = new DesktopEntity();
                    // find the EF Manufacturer object
                    ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerId == data.Manufacturer.ID);
                    // find the EF CPU object
                    CPUEntity cpu = context.CPUs.Single(c => c.cpu_id == data.Cpu.ID);

                    //create and EntitySet to hold the DesktopRamModuleEntity's
                    IList<DesktopRamModuleEntity> desktopRamModules = new List<DesktopRamModuleEntity>();
                    //add each RamModuleEntity form the data's collection
                    foreach (KeyValuePair<IRamModule, int> modules in data.RamModuleCollection.RamModules)
                    {
                        //get the domain RamModule object, and quantity.
                        IRamModule module = modules.Key;
                        int quantity = modules.Value;

                        //get the EF RamModuleEntity object that matches.
                        RamModuleEntity moduleEntity = context.RamModules.Single(r => r.RamId == module.ID);

                        //create the EF DesktopRamModuleEntity object
                        DesktopRamModuleEntity desktopRamModuleEntity = new DesktopRamModuleEntity();
                        desktopRamModuleEntity.RamId = moduleEntity.RamId;
                        desktopRamModuleEntity.RamModule = moduleEntity;
                        desktopRamModuleEntity.Quantity = quantity;

                        desktopRamModules.Add(desktopRamModuleEntity);
                    }

                    //create an EntitySet to hold the DesktopHDDEntity's
                    IList<DesktopHDDEntity> desktopHDDs = new List<DesktopHDDEntity>();
                    //add each HDDEntity from the data's hdd collection
                    foreach (KeyValuePair<IHDD, int> hdds in data.HddCollection.HDDs)
                    {
                        IHDD hdd = hdds.Key;
                        int quantity = hdds.Value;

                        HDDEntity hddEntity = context.HDDs.Single(h => h.HddId == hdd.ID);

                        DesktopHDDEntity desktopHDDEntity = new DesktopHDDEntity();
                        desktopHDDEntity.HddId = hddEntity.HddId;
                        desktopHDDEntity.HDD = hddEntity;
                        desktopHDDEntity.Quantity = quantity;

                        desktopHDDs.Add(desktopHDDEntity);
                    }

                    //create an EntitySet to hold the DesktopGPUEntity's
                    IList<DesktopGPUEntity> desktopGPUs = new List<DesktopGPUEntity>();
                    //add each GPUEntity from the data's gpu collection
                    foreach (KeyValuePair<IGPU, int> gpus in data.GpuCollection.GPUs)
                    {
                        IGPU gpu = gpus.Key;
                        int quantity = gpus.Value;

                        GPUEntity gpuEntity = context.GPUs.Single(g => g.GpuId == gpu.ID);

                        DesktopGPUEntity desktopGPUEntity = new DesktopGPUEntity();
                        desktopGPUEntity.GpuId = gpuEntity.GpuId;
                        desktopGPUEntity.GPU = gpuEntity;
                        desktopGPUEntity.Quantity = quantity;

                        desktopGPUs.Add(desktopGPUEntity);
                    }

                    desktop.ManufacturerId = data.Manufacturer.ID;
                    desktop.Manufacturer = manufacturer;
                    desktop.Model = data.Model;
                    desktop.Price = data.Price;
                    desktop.CpuId = data.Cpu.ID;
                    desktop.CPU = cpu;
                    desktop.DesktopRamModules = desktopRamModules;
                    desktop.DesktopHDDs = desktopHDDs;
                    desktop.DesktopGPUs = desktopGPUs;
                    desktop.MonitorId = 1;

                    //insert into database and submit changes.
                    context.Desktops.Add(desktop);
                    context.SaveChanges();

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
            // find the EF Desktop object
            DesktopEntity desktop = context.Desktops.Single(d => d.DesktopId == data.ID);
            //delete it and submit the changes.
            context.Desktops.Remove(desktop);
            context.SaveChanges();	
        }

        public override Desktop Select(int id)
        {
            // find the EF Desktop object
            DesktopEntity desktop = context.Desktops.Single(d => d.DesktopId == id);

            // create necessary mappers
            var cpuMapper = new EntityFrameworkCPUDataMapper();
            var manufacturerMapper = new EntityFrameworkManufacturerDataMapper();
            var ramModuleMapper = new EntityFrameworkRamModuleDataMapper();
            var hddMapper = new EntityFrameworkHDDDataMapper();
            var gpuMapper = new EntityFrameworkGPUDataMapper();

            Manufacturer manufacturer = manufacturerMapper.Select(desktop.ManufacturerId.Value);
            // from the EF Desktop object, create domain objects                        
            CPU cpu = cpuMapper.Select(desktop.CPU.cpu_id);

            //create a IRamModuleCollection to hold the RamModules.
            IRamModuleCollection desktopRamModulesCollection = new RamModuleCollection();
            foreach (DesktopRamModuleEntity desktopRamModuleEntity in desktop.DesktopRamModules)
            {
                //get the RamModuleEntity and quantity.
                RamModuleEntity ramModuleEntity = desktopRamModuleEntity.RamModule;
                int quantity = desktopRamModuleEntity.Quantity;

                //create a domain RamModule
                IRamModule ramModule = ramModuleMapper.Select(ramModuleEntity.RamId);

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
                IHDD hdd = hddMapper.Select(hddEntity.HddId);

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
                IGPU gpu = gpuMapper.Select(gpuEntity.GpuId);

                //add it to the collection with the required quantity.
                desktopGPUCollection.AddGPU(gpu, quantity);
            }

            //create a new domain Desktop object and return it.
            return new Desktop(desktop.DesktopId,
                               manufacturer,
                               desktop.Model,
                               desktop.Price,
                               cpu,
                               desktopRamModulesCollection,
                               desktopHDDCollection,
                               desktopGPUCollection);
        }

        public override void Update(Desktop data)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    // find the EF Desktop object
                    DesktopEntity desktop = context.Desktops.Single(d => d.DesktopId == data.ID);
                    // find the EF Manufacturer object
                    ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerId == data.Manufacturer.ID);
                    // find the EF CPU object
                    CPUEntity cpu = context.CPUs.Single(c => c.cpu_id == data.Cpu.ID);

                    //clear the desktop's collections
                    context.DesktopRamModules.RemoveRange(desktop.DesktopRamModules);
                    context.DesktopHDDs.RemoveRange(desktop.DesktopHDDs);
                    context.DesktopGPUs.RemoveRange(desktop.DesktopGPUs);

                    //create and EntitySet to hold the DesktopRamModuleEntity's
                    IList<DesktopRamModuleEntity> desktopRamModules = new List<DesktopRamModuleEntity>();
                    //add each RamModuleEntity form the data's collection
                    foreach (KeyValuePair<IRamModule, int> modules in data.RamModuleCollection.RamModules)
                    {
                        //get the domain RamModule object, and quantity.
                        IRamModule module = modules.Key;
                        int quantity = modules.Value;

                        //get the EF RamModuleEntity object that matches.
                        RamModuleEntity moduleEntity = context.RamModules.Single(r => r.RamId == module.ID);

                        //create the EF DesktopRamModuleEntity object
                        DesktopRamModuleEntity desktopRamModuleEntity = new DesktopRamModuleEntity();
                        desktopRamModuleEntity.RamId = moduleEntity.RamId;
                        desktopRamModuleEntity.RamModule = moduleEntity;
                        desktopRamModuleEntity.Quantity = quantity;

                        desktopRamModules.Add(desktopRamModuleEntity);
                    }

                    //create an EntitySet to hold the DesktopHDDEntity's
                    IList<DesktopHDDEntity> desktopHDDs = new List<DesktopHDDEntity>();
                    //add each HDDEntity from the data's hdd collection
                    foreach (KeyValuePair<IHDD, int> hdds in data.HddCollection.HDDs)
                    {
                        IHDD hdd = hdds.Key;
                        int quantity = hdds.Value;

                        HDDEntity hddEntity = context.HDDs.Single(h => h.HddId == hdd.ID);

                        DesktopHDDEntity desktopHDDEntity = new DesktopHDDEntity();
                        desktopHDDEntity.HddId = hddEntity.HddId;
                        desktopHDDEntity.HDD = hddEntity;
                        desktopHDDEntity.Quantity = quantity;

                        desktopHDDs.Add(desktopHDDEntity);
                    }

                    //create an EntitySet to hold the DesktopGPUEntity's
                    IList<DesktopGPUEntity> desktopGPUs = new List<DesktopGPUEntity>();
                    //add each GPUEntity from the data's gpu collection
                    foreach (KeyValuePair<IGPU, int> gpus in data.GpuCollection.GPUs)
                    {
                        IGPU gpu = gpus.Key;
                        int quantity = gpus.Value;

                        GPUEntity gpuEntity = context.GPUs.Single(g => g.GpuId == gpu.ID);

                        DesktopGPUEntity desktopGPUEntity = new DesktopGPUEntity();
                        desktopGPUEntity.GpuId = gpuEntity.GpuId;
                        desktopGPUEntity.GPU = gpuEntity;
                        desktopGPUEntity.Quantity = quantity;

                        desktopGPUs.Add(desktopGPUEntity);
                    }

                    // update
                    desktop.ManufacturerId = data.Manufacturer.ID;
                    desktop.Manufacturer = manufacturer;
                    desktop.Model = data.Model;
                    desktop.Price = data.Price;
                    desktop.CpuId = data.Cpu.ID;
                    desktop.CPU = cpu;
                    desktop.DesktopRamModules = desktopRamModules;
                    desktop.DesktopHDDs = desktopHDDs;
                    desktop.DesktopGPUs = desktopGPUs;
                    desktop.MonitorId = 1;

                    // save                    
                    context.SaveChanges();

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

            foreach (DesktopEntity d in context.Desktops)
            {
                desktops.Add(Select(d.DesktopId));
            }

            return desktops;
        }
    }
}
