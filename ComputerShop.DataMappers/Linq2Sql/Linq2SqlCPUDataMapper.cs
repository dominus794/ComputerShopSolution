using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;

using ComputerShopDB = ComputerShop.DAL.Linq2Sql;
using ManufacturerEntity = ComputerShop.DAL.Linq2Sql.Manufacturer;
using CPUEntity = ComputerShop.DAL.Linq2Sql.CPU;

namespace ComputerShop.DataMappers.Linq2Sql
{
	public class Linq2SqlCPUDataMapper : Linq2SqlDataMapperBase<CPU>
	{
        public Linq2SqlCPUDataMapper(string connectionString)
            : base(connectionString) { }

		public override void Insert(CPU data)
		{
			//create a Linq2Sql cpu object
			CPUEntity cpu = new CPUEntity();				
			//get the Linq2Sql manufacturer object, using the domain manufacturer objects id.
			ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerID == data.Manufacturer.ID);

			cpu.ManufacturerID = data.Manufacturer.ID;
			cpu.Manufacturer = manufacturer;
			cpu.Model = data.Model;
			cpu.Price = data.Price;
			cpu.ClockSpeed = data.ClockSpeed;
			cpu.NoOfCores = data.CpuCoreType.ToString();
			cpu.CpuFormFactor = data.CpuFormFactor.ToString();

			//insert into database and save
			context.CPUs.InsertOnSubmit(cpu);
			context.SubmitChanges();
		}

		public override void Delete(CPU data)
		{
			//find the Linq2Sql CPU object
            CPUEntity cpu = (CPUEntity)context.GetCPUByID(data.ID); //context.CPUs.Single(c => c.CpuID == data.ID);
			//delete it and submit the changes.
			context.CPUs.DeleteOnSubmit(cpu);
			context.SubmitChanges();
		}

		public override CPU Select(int id)
		{
			//find the Linq2Sql CPU object
            CPUEntity cpu = (CPUEntity) context.GetCPUByID(id);//context.CPUs.Single(c => c.CpuID == id);

			//create a domain CPU object and return it
			return new CPU(cpu.CpuID,
						   new Manufacturer(cpu.Manufacturer.ManufacturerID, cpu.Manufacturer.Name),
						   cpu.Model,
						   cpu.Price,
						   cpu.ClockSpeed,
						   (CPUFormFactor)Enum.Parse(typeof(CPUFormFactor), cpu.CpuFormFactor),
						   (CPUCoreType)Enum.Parse(typeof(CPUCoreType), cpu.NoOfCores));
		}

		public override void Update(CPU data)
		{
			//find the Linq2Sql CPU object
			CPUEntity cpu = context.CPUs.Single(c => c.CpuID == data.ID);

			//get the Linq2Sql manufacturer object, using the domain manufacturer objects id.
			ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerID == data.Manufacturer.ID);

			//update it and submit the changes.
			cpu.ManufacturerID = data.Manufacturer.ID;
			cpu.Manufacturer = manufacturer;
			cpu.Model = data.Model;
			cpu.Price = data.Price;
			cpu.ClockSpeed = data.ClockSpeed;
			cpu.NoOfCores = data.CpuCoreType.ToString();
			cpu.CpuFormFactor = data.CpuFormFactor.ToString();
						
			//submit the changes
			context.SubmitChanges();
		}

		public override IList<CPU> GetAll()
		{
			IList<CPU> cpus = (from c in context.CPUs
							   select new CPU(c.CpuID,
											  new Manufacturer(c.Manufacturer.ManufacturerID, c.Manufacturer.Name),
											  c.Model,
											  c.Price,
											  c.ClockSpeed,
											  (CPUFormFactor)Enum.Parse(typeof(CPUFormFactor), c.CpuFormFactor),
											  (CPUCoreType)Enum.Parse(typeof(CPUCoreType), c.NoOfCores))).ToList<CPU>();

			return cpus;
		}
	}
}
