using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;

using HDDEntity = ComputerShop.DAL.EntityFramework.HDD;
using HDD = ComputerShop.Domain.HDD;

using ManufacturerEntity = ComputerShop.DAL.EntityFramework.Manufacturer;
using Manufacturer = ComputerShop.Domain.Manufacturer;

namespace ComputerShop.DataMappers.EntityFramework
{
    public class EntityFrameworkHDDDataMapper : EntityFrameworkDataMapperBase<HDD>
    {
        public EntityFrameworkHDDDataMapper(string connectionString)
            : base(connectionString) { }

        public EntityFrameworkHDDDataMapper() { }

        public override void Insert(HDD data)
        {
            // create the EF HDD object
            HDDEntity hdd = new HDDEntity();
            // get the EF manufacturer object
            ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerId == data.Manufacturer.ID);

            hdd.ManufacturerId = data.Manufacturer.ID;
            hdd.Manufacturer = manufacturer;
            hdd.Model = data.Model;
            hdd.Price = data.Price;
            hdd.Capacity = (short)data.Capacity;
            hdd.HddFormFactor = data.HddFormFactor.ToString();
            hdd.HddInterface = data.HddInterfaceType.ToString();
            hdd.HddType = data.HddType.ToString();
            hdd.Speed = (short)data.Speed;

            // insert into database and save changes
            context.HDDs.Add(hdd);
            context.SaveChanges();            
        }

        public override void Delete(HDD data)
        {
            // find the EF HDD object
            HDDEntity hdd = context.HDDs.Single(h => h.HddId == data.ID);
            // delete it and save changes
            context.HDDs.Remove(hdd);
            context.SaveChanges();
        }

        public override HDD Select(int id)
        {
            //find the EF HDD object.
            HDDEntity hdd = context.HDDs.Single(h => h.HddId == id);
            //create a new domain HDD object and return it.
            return new HDD(hdd.HddId,
                           new Manufacturer(hdd.Manufacturer.ManufacturerId, hdd.Manufacturer.Name),
                           hdd.Model,
                           hdd.Price,
                           (ushort)hdd.Capacity,
                           (HDDFormFactor)Enum.Parse(typeof(HDDFormFactor), hdd.HddFormFactor),
                           (HDDInterfaceType)Enum.Parse(typeof(HDDInterfaceType), hdd.HddInterface),
                           (HDDType)Enum.Parse(typeof(HDDType), hdd.HddType),
                           (ushort)hdd.Speed);
        }

        public override void Update(HDD data)
        {
            // find the EF HDD object
            HDDEntity hdd = context.HDDs.Single(h => h.HddId == data.ID);
            // get the EF manufacturer object
            ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.ManufacturerId == data.Manufacturer.ID);

            // update
            hdd.ManufacturerId = data.Manufacturer.ID;
            hdd.Manufacturer = manufacturer;
            hdd.Model = data.Model;
            hdd.Price = data.Price;
            hdd.Capacity = (short)data.Capacity;
            hdd.HddFormFactor = data.HddFormFactor.ToString();
            hdd.HddInterface = data.HddInterfaceType.ToString();
            hdd.HddType = data.HddType.ToString();
            hdd.Speed = (short)data.Speed;

            // save
            context.SaveChanges();   
        }

        public override IList<HDD> GetAll()
        {
            //IList<HDD> hdds = (from h in context.HDDs
            //                   select new HDD
            //                   {
            //                       ID = h.hdd_id,
            //                       Manufacturer = new Manufacturer
            //                       {
            //                           ID = h.Manufacturer.manufacturer_id,
            //                           Name = h.Manufacturer.name
            //                       },
            //                       Model = h.model,
            //                       Price = h.price,
            //                       Capacity = (ushort)h.capacity,
            //                       HddFormFactor = (HDDFormFactor)Enum.Parse(typeof(HDDFormFactor), h.hdd_form_factor),
            //                       HddInterfaceType = (HDDInterfaceType)Enum.Parse(typeof(HDDInterfaceType), h.hdd_interface),
            //                       HddType = (HDDType)Enum.Parse(typeof(HDDType), h.hdd_type),
            //                       Speed = (ushort)h.speed
            //                   }).ToList<HDD>();

            IList<HDD> hdds = new List<HDD>();

            foreach (var h in context.HDDs)
            {
                var hdd = new HDD
                               {
                                   ID = h.HddId,
                                   Manufacturer = new Manufacturer
                                   {
                                       ID = h.Manufacturer.ManufacturerId,
                                       Name = h.Manufacturer.Name
                                   },
                                   Model = h.Model,
                                   Price = h.Price,
                                   Capacity = (ushort)h.Capacity,
                                   HddFormFactor = (HDDFormFactor)Enum.Parse(typeof(HDDFormFactor), h.HddFormFactor),
                                   HddInterfaceType = (HDDInterfaceType)Enum.Parse(typeof(HDDInterfaceType), h.HddInterface),
                                   HddType = (HDDType)Enum.Parse(typeof(HDDType), h.HddType),
                                   Speed = (ushort)h.Speed
                               };

                hdds.Add(hdd);
            }
                
            return hdds;
        }
    }
}
