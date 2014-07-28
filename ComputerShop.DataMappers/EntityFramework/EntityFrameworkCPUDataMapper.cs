using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;

using ComputerShop.DAL.EntityFramework;
using CPUEntity = ComputerShop.DAL.EntityFramework.CPU;
using CPU = ComputerShop.Domain.CPU;

using ManufacturerEntity = ComputerShop.DAL.EntityFramework.Manufacturer;
using Manufacturer = ComputerShop.Domain.Manufacturer;

namespace ComputerShop.DataMappers.EntityFramework
{
    public class EntityFrameworkCPUDataMapper : EntityFrameworkDataMapperBase<CPU>
    {
        public EntityFrameworkCPUDataMapper(string connectionString)
            : base(connectionString) { }

        public EntityFrameworkCPUDataMapper() { }


        public override void Insert(CPU data)
        {
            // create the EF CPU object
            CPUEntity cpu = new CPUEntity();
            // get the EF Manufacturer object using the domain objects manufacturer id
            ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.manufacturer_id == data.Manufacturer.ID);

            cpu.Manufacturer = manufacturer;
            cpu.model = data.Model;
            cpu.price = data.Price;
            cpu.clock_speed = Decimal.Parse(data.ClockSpeed.ToString());
            cpu.no_of_cores = data.CpuCoreType.ToString();
            cpu.cpu_form_factor = data.CpuFormFactor.ToString();

            // insert into the database and save
            context.CPUs.Add(cpu);
            context.SaveChanges();
        }

        public override void Delete(CPU data)
        {
            // find the EF CPU object
            CPUEntity cpu = context.CPUs.Single(c => c.cpu_id == data.ID);
            // delete it and save
            context.CPUs.Remove(cpu);
            context.SaveChanges();
        }

        public override CPU Select(int id)
        {
            // find the EF CPU object
            CPUEntity cpu = context.CPUs.Single(c => c.cpu_id == id);

            // create a domain CPU object and return it
            return new CPU(cpu.cpu_id,
                           new Manufacturer(cpu.Manufacturer.manufacturer_id, cpu.Manufacturer.name),
                           cpu.model,
                           cpu.price,
                           (float)cpu.clock_speed,
                           (CPUFormFactor)Enum.Parse(typeof(CPUFormFactor), cpu.cpu_form_factor),
                           (CPUCoreType)Enum.Parse(typeof(CPUCoreType), cpu.no_of_cores));

        }

        public override void Update(CPU data)
        {
            // find the EF CPU object
            CPUEntity cpu = context.CPUs.Single(c => c.cpu_id == data.ID);

            // find the EF Manufacturer object from the database, in case it has changed in the domain object
            ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.manufacturer_id == data.Manufacturer.ID);
            
            // update
            cpu.manufacturer_id = data.Manufacturer.ID;
            cpu.Manufacturer = manufacturer;
            cpu.model = data.Model;
            cpu.price = data.Price;
            cpu.clock_speed = Decimal.Parse(data.ClockSpeed.ToString());
            cpu.no_of_cores = data.CpuCoreType.ToString();
            cpu.cpu_form_factor = data.CpuFormFactor.ToString();

            // save            
            context.SaveChanges();
        }

        public override IList<CPU> GetAll()
        {
            //IList<CPU> cpus = (from c in context.CPUs
            //                   select new CPU
            //                   {
            //                       ID = c.cpu_id,
            //                       Manufacturer = new Manufacturer
            //                       {
            //                           ID = c.Manufacturer.manufacturer_id,
            //                           Name = c.Manufacturer.name
            //                       },
            //                       Model = c.model,
            //                       Price = c.price,
            //                       ClockSpeed = (float)c.clock_speed,
            //                       CpuFormFactor = (CPUFormFactor)Enum.Parse(typeof(CPUFormFactor), c.cpu_form_factor),
            //                       CpuCoreType = (CPUCoreType)Enum.Parse(typeof(CPUCoreType), c.no_of_cores)
            //                   }).ToList<CPU>();

            IList<CPU> cpus = new List<CPU>();

            foreach(var c in context.CPUs)
            {
                var cpu = new CPU
                               {
                                   ID = c.cpu_id,
                                   Manufacturer = new Manufacturer
                                   {
                                       ID = c.Manufacturer.manufacturer_id,
                                       Name = c.Manufacturer.name
                                   },
                                   Model = c.model,
                                   Price = c.price,
                                   ClockSpeed = (float)c.clock_speed,
                                   CpuFormFactor = (CPUFormFactor)Enum.Parse(typeof(CPUFormFactor), c.cpu_form_factor),
                                   CpuCoreType = (CPUCoreType)Enum.Parse(typeof(CPUCoreType), c.no_of_cores)
                               };

                cpus.Add(cpu);
            }
                                                                                             
            return cpus;
        }
    }
}
