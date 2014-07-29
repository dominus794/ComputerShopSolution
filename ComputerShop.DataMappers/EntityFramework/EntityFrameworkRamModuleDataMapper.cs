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
            ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerId == data.Manufacturer.ID);

            ramModule.ManufacturerId = data.Manufacturer.ID;
            ramModule.Manufacturer = manufacturer;
            ramModule.Model = data.Model;
            ramModule.Price = data.Price;
            ramModule.BusSpeed = (short)data.ClockSpeed;
            ramModule.DDRVersion = data.DDRVersion.ToString();
            ramModule.RamFormFactor = data.RamFormFactor.ToString();
            ramModule.Size = (short)data.Size;

            //insert into database and save.
            context.RamModules.Add(ramModule);
            context.SaveChanges();
        }

        public override void Delete(RamModule data)
        {
            //find the EF RamModule object.
            RamModuleEntity ramModule = context.RamModules.Single(r => r.RamId == data.ID);
            //delete it and submit the changes
            context.RamModules.Remove(ramModule);
            context.SaveChanges();
        }

        public override RamModule Select(int id)
        {
            //find the EF RamModule object.
            RamModuleEntity ramModule = context.RamModules.Single(r => r.RamId == id);
            //create a domain RamModule object and return it.
            return new RamModule(ramModule.RamId,
                                 new Manufacturer(ramModule.Manufacturer.ManufacturerId, ramModule.Manufacturer.Name),
                                 ramModule.Model,
                                 ramModule.Price,
                                 (ushort)ramModule.BusSpeed,
                                 (DDRVersion)Enum.Parse(typeof(DDRVersion), ramModule.DDRVersion),
                                 (RAMFormFactor)Enum.Parse(typeof(RAMFormFactor), ramModule.RamFormFactor),
                                 (ushort)ramModule.Size);
        }

        public override void Update(RamModule data)
        {
            //find a EF ram module object
            RamModuleEntity ramModule = context.RamModules.Single(r => r.RamId == data.ID);
            //get the EF manufacturer object, using the domain manufacturer objects id
            ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerId == data.Manufacturer.ID);

            // update
            ramModule.ManufacturerId = data.Manufacturer.ID;
            ramModule.Manufacturer = manufacturer;
            ramModule.Model = data.Model;
            ramModule.Price = data.Price;
            ramModule.BusSpeed = (short)data.ClockSpeed;
            ramModule.DDRVersion = data.DDRVersion.ToString();
            ramModule.RamFormFactor = data.RamFormFactor.ToString();
            ramModule.Size = (short)data.Size;

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
                                            ID = r.RamId,
                                            Manufacturer = new Manufacturer
                                            {
                                                ID = r.Manufacturer.ManufacturerId,
                                                Name = r.Manufacturer.Name
                                            },
                                            Model = r.Model,
                                            Price = r.Price,
                                            ClockSpeed = (ushort)r.BusSpeed,
                                            DDRVersion = (DDRVersion)Enum.Parse(typeof(DDRVersion), r.DDRVersion),
                                            RamFormFactor = (RAMFormFactor)Enum.Parse(typeof(RAMFormFactor), r.RamFormFactor),
                                            Size = (ushort)r.Size
                                        };

                modules.Add(ramModule);
            }
                                            
            return modules;
        }
    }
}
