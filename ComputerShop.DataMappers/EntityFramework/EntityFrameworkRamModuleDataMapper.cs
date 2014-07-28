using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;

using RamModuleEntity = ComputerShop.DAL.EntityFramework.RamModule;
using RamModule = ComputerShop.Domain.RamModule;

using ManufacturerEntity = ComputerShop.DAL.EntityFramework.Manufacturer;
using Manufacturer = ComputerShop.Domain.Manufacturer;

namespace ComputerShop.DataMappers.EntityFramework
{
    public class EntityFrameworkRamModuleDataMapper : EntityFrameworkDataMapperBase<RamModule>
    {
        public EntityFrameworkRamModuleDataMapper(string connectionString)
            : base(connectionString) { }

        public EntityFrameworkRamModuleDataMapper() { }

        public override void Insert(RamModule data)
        {
            //create a EF ram module object
            RamModuleEntity ramModule = new RamModuleEntity();
            //get the EF manufacturer object, using the domain manufacturer objects id
            ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.manufacturer_id == data.Manufacturer.ID);

            ramModule.manufacturer_id = data.Manufacturer.ID;
            ramModule.Manufacturer = manufacturer;
            ramModule.model = data.Model;
            ramModule.price = data.Price;
            ramModule.bus_speed = (short)data.ClockSpeed;
            ramModule.ddr_version = data.DDRVersion.ToString();
            ramModule.ram_form_factor = data.RamFormFactor.ToString();
            ramModule.size = (short)data.Size;

            //insert into database and save.
            context.RamModules.Add(ramModule);
            context.SaveChanges();
        }

        public override void Delete(RamModule data)
        {
            //find the EF RamModule object.
            RamModuleEntity ramModule = context.RamModules.Single(r => r.ram_id == data.ID);
            //delete it and submit the changes
            context.RamModules.Remove(ramModule);
            context.SaveChanges();
        }

        public override RamModule Select(int id)
        {
            //find the EF RamModule object.
            RamModuleEntity ramModule = context.RamModules.Single(r => r.ram_id == id);
            //create a domain RamModule object and return it.
            return new RamModule(ramModule.ram_id,
                                 new Manufacturer(ramModule.Manufacturer.manufacturer_id, ramModule.Manufacturer.name),
                                 ramModule.model,
                                 ramModule.price,
                                 (ushort)ramModule.bus_speed,
                                 (DDRVersion)Enum.Parse(typeof(DDRVersion), ramModule.ddr_version),
                                 (RAMFormFactor)Enum.Parse(typeof(RAMFormFactor), ramModule.ram_form_factor),
                                 (ushort)ramModule.size);
        }

        public override void Update(RamModule data)
        {
            //find a EF ram module object
            RamModuleEntity ramModule = context.RamModules.Single(r => r.ram_id == data.ID);
            //get the EF manufacturer object, using the domain manufacturer objects id
            ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.manufacturer_id == data.Manufacturer.ID);

            // update
            ramModule.manufacturer_id = data.Manufacturer.ID;
            ramModule.Manufacturer = manufacturer;
            ramModule.model = data.Model;
            ramModule.price = data.Price;
            ramModule.bus_speed = (short)data.ClockSpeed;
            ramModule.ddr_version = data.DDRVersion.ToString();
            ramModule.ram_form_factor = data.RamFormFactor.ToString();
            ramModule.size = (short)data.Size;

            // save            
            context.SaveChanges();
        }

        public override IList<RamModule> GetAll()
        {
            //IList<RamModule> modules = (from r in context.RamModules
            //                            select new RamModule
            //                            {
            //                                ID = r.ram_id,
            //                                Manufacturer = new Manufacturer
            //                                {
            //                                    ID = r.Manufacturer.manufacturer_id,
            //                                    Name = r.Manufacturer.name
            //                                },
            //                                Model = r.model,
            //                                Price = r.price,
            //                                ClockSpeed = (ushort)r.bus_speed,
            //                                DDRVersion = (DDRVersion)Enum.Parse(typeof(DDRVersion), r.ddr_version),
            //                                RamFormFactor = (RAMFormFactor)Enum.Parse(typeof(RAMFormFactor), r.ram_form_factor),
            //                                Size = (ushort)r.size
            //                            }).ToList<RamModule>();

            IList<RamModule> modules = new List<RamModule>();

            foreach (var r in context.RamModules)
            {
                var ramModule = new RamModule
                                        {
                                            ID = r.ram_id,
                                            Manufacturer = new Manufacturer
                                            {
                                                ID = r.Manufacturer.manufacturer_id,
                                                Name = r.Manufacturer.name
                                            },
                                            Model = r.model,
                                            Price = r.price,
                                            ClockSpeed = (ushort)r.bus_speed,
                                            DDRVersion = (DDRVersion)Enum.Parse(typeof(DDRVersion), r.ddr_version),
                                            RamFormFactor = (RAMFormFactor)Enum.Parse(typeof(RAMFormFactor), r.ram_form_factor),
                                            Size = (ushort)r.size
                                        };

                modules.Add(ramModule);
            }
                                            
            return modules;
        }
    }
}
