using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;

using ComputerShopDB = ComputerShop.DAL.Linq2Sql;
using ManufacturerEntity = ComputerShop.DAL.Linq2Sql.Manufacturer;
using GPUEntity = ComputerShop.DAL.Linq2Sql.GPU;

namespace ComputerShop.DataMappers.Linq2Sql
{
	public class Linq2SqlGPUDataMapper : Linq2SqlDataMapperBase<GPU>
	{
		public override void Insert(GPU data)
		{
			//create a Linq2Sql GPU object
			GPUEntity gpu = new GPUEntity();
			//get the Linq2Sql manufacturer object, using the domain manufacturer objects id
			ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerID == data.Manufacturer.ID);

			gpu.ManufacturerID = data.Manufacturer.ID;
			gpu.Manufacturer = manufacturer;
			gpu.Model = data.Model;
			gpu.Price = data.Price;
			gpu.GpuModel = data.GpuModel;
			gpu.GpuClockSpeed = data.GpuClockSpeed;
			gpu.VRamSize = data.VRamSize;
			gpu.VRamClockSpeed = data.VRamClockSpeed;
			gpu.GDDRVersion = data.GddrVersion.ToString();
			gpu.GpuType = data.GpuType.ToString();

			//insert into database and submit changes.
			context.GPUs.InsertOnSubmit(gpu);
			context.SubmitChanges();
		}

		public override void Delete(GPU data)
		{
			//find the Linq2Sql GPU object
			GPUEntity gpu = context.GPUs.Single(g => g.GpuID == data.ID);
			//delete it and submit changes
			context.GPUs.DeleteOnSubmit(gpu);
			context.SubmitChanges();
		}

		public override GPU Select(int id)
		{
			//find the Linq2Sql GPU object
			GPUEntity gpu = context.GPUs.Single(g => g.GpuID == id);
			//create a new domain GPU object and return it.
			return new GPU(gpu.GpuID,
						   new Manufacturer(gpu.Manufacturer.ManufacturerID, gpu.Manufacturer.Name),
						   gpu.Model,
						   gpu.Price,
						   gpu.GpuModel,
						   gpu.GpuClockSpeed,
						   gpu.VRamSize,
						   gpu.VRamClockSpeed,
						   (GDDRVersion)Enum.Parse(typeof(GDDRVersion), gpu.GDDRVersion),
						   (GPUType)Enum.Parse(typeof(GPUType), gpu.GpuType));			
		}

		public override void Update(GPU data)
		{
			//find the Linq2Sql GPU object
			GPUEntity gpu = context.GPUs.Single(g => g.GpuID == data.ID);
			//get the Linq2Sql manufacturer object, using the domain manufacturer object's id.
			ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerID == data.Manufacturer.ID);

			//update it and submit the changes.
			gpu.ManufacturerID = data.Manufacturer.ID;
			gpu.Manufacturer = manufacturer;
			gpu.Model = data.Model;
			gpu.Price = data.Price;
			gpu.GpuModel = data.GpuModel;
			gpu.GpuClockSpeed = data.GpuClockSpeed;
			gpu.VRamSize = data.VRamSize;
			gpu.VRamClockSpeed = data.VRamClockSpeed;
			gpu.GDDRVersion = data.GddrVersion.ToString();
			gpu.GpuType = data.GpuType.ToString();
			//submit the changes
			context.SubmitChanges();
		}

		public override IList<GPU> GetAll()
		{
			IList<GPU> gpus = (from g in context.GPUs
							   select new GPU(g.GpuID,
											  new Manufacturer(g.Manufacturer.ManufacturerID, g.Manufacturer.Name),
											  g.Model,
											  g.Price,
											  g.GpuModel,
											  g.GpuClockSpeed,
											  g.VRamSize,
											  g.VRamClockSpeed,
											  (GDDRVersion)Enum.Parse(typeof(GDDRVersion), g.GDDRVersion),
											  (GPUType)Enum.Parse(typeof(GPUType), g.GpuType))).ToList<GPU>();
			return gpus;
		}
	}
}
