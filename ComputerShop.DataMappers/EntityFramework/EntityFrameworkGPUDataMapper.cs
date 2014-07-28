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
            ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.manufacturer_id == data.Manufacturer.ID);

            gpu.manufacturer_id = data.Manufacturer.ID;
            gpu.Manufacturer = manufacturer;
            gpu.model = data.Model;
            gpu.price = data.Price;
            gpu.gpu_model = data.GpuModel;
            gpu.gpu_clock_speed = (short)data.GpuClockSpeed;
            gpu.vram_size = (short)data.VRamSize;
            gpu.vram_clock_speed = (short)data.VRamClockSpeed;
            gpu.vram_type = data.GddrVersion.ToString();
            gpu.gpu_type = data.GpuType.ToString();

            //insert into database and submit changes.
            context.GPUs.Add(gpu);
            context.SaveChanges();
        }

        public override void Delete(GPU data)
        {
            //find the EF GPU object
            GPUEntity gpu = context.GPUs.Single(g => g.gpu_id == data.ID);
            //delete it and submit changes
            context.GPUs.Remove(gpu);
            context.SaveChanges();
        }

        public override GPU Select(int id)
        {
            //find the EF GPU object
            GPUEntity gpu = context.GPUs.Single(g => g.gpu_id == id);
            //create a new domain GPU object and return it.
            return new GPU(gpu.gpu_id,
                           new Manufacturer(gpu.Manufacturer.manufacturer_id, gpu.Manufacturer.name),
                           gpu.model,
                           gpu.price,
                           gpu.gpu_model,
                           (ushort)gpu.gpu_clock_speed,
                           (ushort)gpu.vram_size,
                           (ushort)gpu.vram_clock_speed,
                           (GDDRVersion)Enum.Parse(typeof(GDDRVersion), gpu.vram_type),
                           (GPUType)Enum.Parse(typeof(GPUType), gpu.gpu_type));		
        }

        public override void Update(GPU data)
        {
            // find the EF GPU object
            GPUEntity gpu = context.GPUs.Single(g => g.gpu_id == data.ID);

            //get the EF manufacturer object, using the domain manufacturer object's id.
            ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.manufacturer_id == data.Manufacturer.ID);

            // update
            gpu.manufacturer_id = data.Manufacturer.ID;
            gpu.Manufacturer = manufacturer;
            gpu.model = data.Model;
            gpu.price = data.Price;
            gpu.gpu_model = data.GpuModel;
            gpu.gpu_clock_speed = (short)data.GpuClockSpeed;
            gpu.vram_size = (short)data.VRamSize;
            gpu.vram_clock_speed = (short)data.VRamClockSpeed;
            gpu.vram_type = data.GddrVersion.ToString();
            gpu.gpu_type = data.GpuType.ToString();

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
                                   ID = g.gpu_id,
                                   Manufacturer = new Manufacturer
                                   {
                                       ID = g.Manufacturer.manufacturer_id,
                                       Name = g.Manufacturer.name
                                   },
                                   Model = g.model,
                                   Price = g.price,
                                   GpuModel = g.gpu_model,
                                   GpuClockSpeed = (ushort)g.gpu_clock_speed,
                                   VRamSize = (ushort)g.vram_size,
                                   VRamClockSpeed = (ushort)g.vram_clock_speed,
                                   GddrVersion = (GDDRVersion)Enum.Parse(typeof(GDDRVersion), g.vram_type),
                                   GpuType = (GPUType)Enum.Parse(typeof(GPUType), g.gpu_type)
                               };

                gpus.Add(gpu);
            }

            return gpus;                                
        }
    }
}
