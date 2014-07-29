using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;

using GPUEntity = ComputerShop.DAL.EntityFramework.GPU;
using GPU = ComputerShop.Domain.GPU;

using ManufacturerEntity = ComputerShop.DAL.EntityFramework.Manufacturer;
using Manufacturer = ComputerShop.Domain.Manufacturer;

namespace ComputerShop.DataMappers.EntityFramework
{
    public class EntityFrameworkGPUDataMapper : EntityFrameworkDataMapperBase<GPU>
    {
        public EntityFrameworkGPUDataMapper(string connectionString)
            : base(connectionString) { }


        public EntityFrameworkGPUDataMapper() { }

        public override void Insert(GPU data)
        {
            //create a EF GPU object
            GPUEntity gpu = new GPUEntity();
            //get the EF manufacturer object, using the domain manufacturer objects id
            ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerId == data.Manufacturer.ID);

            gpu.ManufacturerId = data.Manufacturer.ID;
            gpu.Manufacturer = manufacturer;
            gpu.model = data.Model;
            gpu.Price = data.Price;
            gpu.GpuModel = data.GpuModel;
            gpu.GpuClockSpeed = (short)data.GpuClockSpeed;
            gpu.VramSize = (short)data.VRamSize;
            gpu.VramClockSpeed = (short)data.VRamClockSpeed;
            gpu.VramType = data.GddrVersion.ToString();
            gpu.GpuType = data.GpuType.ToString();

            //insert into database and submit changes.
            context.GPUs.Add(gpu);
            context.SaveChanges();
        }

        public override void Delete(GPU data)
        {
            //find the EF GPU object
            GPUEntity gpu = context.GPUs.Single(g => g.GpuId == data.ID);
            //delete it and submit changes
            context.GPUs.Remove(gpu);
            context.SaveChanges();
        }

        public override GPU Select(int id)
        {
            //find the EF GPU object
            GPUEntity gpu = context.GPUs.Single(g => g.GpuId == id);
            //create a new domain GPU object and return it.
            return new GPU(gpu.GpuId,
                           new Manufacturer(gpu.Manufacturer.ManufacturerId, gpu.Manufacturer.Name),
                           gpu.model,
                           gpu.Price,
                           gpu.GpuModel,
                           (ushort)gpu.GpuClockSpeed,
                           (ushort)gpu.VramSize,
                           (ushort)gpu.VramClockSpeed,
                           (GDDRVersion)Enum.Parse(typeof(GDDRVersion), gpu.VramType),
                           (GPUType)Enum.Parse(typeof(GPUType), gpu.GpuType));		
        }

        public override void Update(GPU data)
        {
            // find the EF GPU object
            GPUEntity gpu = context.GPUs.Single(g => g.GpuId == data.ID);

            //get the EF manufacturer object, using the domain manufacturer object's id.
            ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerId == data.Manufacturer.ID);

            // update
            gpu.ManufacturerId = data.Manufacturer.ID;
            gpu.Manufacturer = manufacturer;
            gpu.model = data.Model;
            gpu.Price = data.Price;
            gpu.GpuModel = data.GpuModel;
            gpu.GpuClockSpeed = (short)data.GpuClockSpeed;
            gpu.VramSize = (short)data.VRamSize;
            gpu.VramClockSpeed = (short)data.VRamClockSpeed;
            gpu.VramType = data.GddrVersion.ToString();
            gpu.GpuType = data.GpuType.ToString();

            // save 
            context.SaveChanges();
        }

        public override IList<GPU> GetAll()
        {
            //IList<GPU> gpus = (from g in context.GPUs
            //                   select new GPU
            //                   {
            //                       ID = g.gpu_id,
            //                       Manufacturer = new Manufacturer
            //                       {
            //                           ID = g.Manufacturer.manufacturer_id,
            //                           Name = g.Manufacturer.name
            //                       },
            //                       Model = g.model,
            //                       Price = g.price,
            //                       GpuModel = g.gpu_model,
            //                       GpuClockSpeed = (ushort)g.gpu_clock_speed,
            //                       VRamSize = (ushort)g.vram_size,
            //                       VRamClockSpeed = (ushort)g.vram_clock_speed,
            //                       GddrVersion = (GDDRVersion)Enum.Parse(typeof(GDDRVersion), g.vram_type),
            //                       GpuType = (GPUType)Enum.Parse(typeof(GPUType), g.gpu_type)
            //                   }).ToList<GPU>();

            IList<GPU> gpus = new List<GPU>();

            foreach (var g in context.GPUs)
            {
                var gpu = new GPU
                               {
                                   ID = g.GpuId,
                                   Manufacturer = new Manufacturer
                                   {
                                       ID = g.Manufacturer.ManufacturerId,
                                       Name = g.Manufacturer.Name
                                   },
                                   Model = g.model,
                                   Price = g.Price,
                                   GpuModel = g.GpuModel,
                                   GpuClockSpeed = (ushort)g.GpuClockSpeed,
                                   VRamSize = (ushort)g.VramSize,
                                   VRamClockSpeed = (ushort)g.VramClockSpeed,
                                   GddrVersion = (GDDRVersion)Enum.Parse(typeof(GDDRVersion), g.VramType),
                                   GpuType = (GPUType)Enum.Parse(typeof(GPUType), g.GpuType)
                               };

                gpus.Add(gpu);
            }

            return gpus;                                
        }
    }
}
