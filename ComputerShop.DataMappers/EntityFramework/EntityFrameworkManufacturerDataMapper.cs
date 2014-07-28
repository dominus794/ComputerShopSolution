using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Domain;

using ComputerShop.DAL.EntityFramework;
using ManufacturerEntity = ComputerShop.DAL.EntityFramework.Manufacturer;
using Manufacturer = ComputerShop.Domain.Manufacturer;

namespace ComputerShop.DataMappers.EntityFramework
{
    public class EntityFrameworkManufacturerDataMapper : EntityFrameworkDataMapperBase<Manufacturer>
    {
        public EntityFrameworkManufacturerDataMapper(string connectionString)
            : base(connectionString) { }

        public EntityFrameworkManufacturerDataMapper() { }

        public override void Insert(Manufacturer data)
        {
            // create an EF Manufacturer object
            ManufacturerEntity manufacturer = new ManufacturerEntity();
            manufacturer.name = data.Name;
            // insert into database and save
            context.Manufacturers.Add(manufacturer);
            context.SaveChanges();
        }

        public override void Delete(Manufacturer data)
        {
            // find the EF Manufacturer object
            ManufacturerEntity manufacturer = context.Manufacturers.Single(i => i.manufacturer_id == data.ID);
            // remove it and save the change.
            context.Manufacturers.Remove(manufacturer);
            context.SaveChanges();
        }

        public override Manufacturer Select(int id)
        {
            // find the EF Manufacturer object
            ManufacturerEntity manufacturer = context.Manufacturers.Single(i => i.manufacturer_id == id);
            return new Manufacturer(manufacturer.manufacturer_id, manufacturer.name);
        }

        public override void Update(Manufacturer data)
        {
            // find the EF Manufacturer object
            ManufacturerEntity manufacturer = context.Manufacturers.Single(i => i.manufacturer_id == data.ID);
            // update it and save the change
            manufacturer.name = data.Name;
            context.SaveChanges();
        }

        public override IList<Manufacturer> GetAll()
        {
            //IList<Manufacturer> manufacturers = (from m in context.Manufacturers
            //                                     select new Manufacturer
            //                                         {
            //                                             ID = m.manufacturer_id,
            //                                             Name = m.name
            //                                         }).ToList<Manufacturer>();

            IList<Manufacturer> manufacturers = new List<Manufacturer>();

            foreach (var m in context.Manufacturers)
            {
                var manufacturer = new Manufacturer
                                        {
                                            ID = m.manufacturer_id,
                                            Name = m.name
                                        };

                manufacturers.Add(manufacturer);
            }
                
            return manufacturers;
        }
    }
}
