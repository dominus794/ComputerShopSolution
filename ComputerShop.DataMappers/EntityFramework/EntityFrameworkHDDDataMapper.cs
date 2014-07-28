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
            ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.manufacturer_id == data.Manufacturer.ID);

            hdd.manufacturer_id = data.Manufacturer.ID;
            hdd.Manufacturer = manufacturer;
            hdd.model = data.Model;
            hdd.price = data.Price;
            hdd.capacity = (short)data.Capacity;
            hdd.hdd_form_factor = data.HddFormFactor.ToString();
            hdd.hdd_interface = data.HddInterfaceType.ToString();
            hdd.hdd_type = data.HddType.ToString();
            hdd.speed = (short)data.Speed;

            // insert into database and save changes
            context.HDDs.Add(hdd);
            context.SaveChanges();            
        }

        public override void Delete(HDD data)
        {
            // find the EF HDD object
            HDDEntity hdd = context.HDDs.Single(h => h.hdd_id == data.ID);
            // delete it and save changes
            context.HDDs.Remove(hdd);
            context.SaveChanges();
        }

        public override HDD Select(int id)
        {
            //find the EF HDD object.
            HDDEntity hdd = context.HDDs.Single(h => h.hdd_id == id);
            //create a new domain HDD object and return it.
            return new HDD(hdd.hdd_id,
                           new Manufacturer(hdd.Manufacturer.manufacturer_id, hdd.Manufacturer.name),
                           hdd.model,
                           hdd.price,
                           (ushort)hdd.capacity,
                           (HDDFormFactor)Enum.Parse(typeof(HDDFormFactor), hdd.hdd_form_factor),
                           (HDDInterfaceType)Enum.Parse(typeof(HDDInterfaceType), hdd.hdd_interface),
                           (HDDType)Enum.Parse(typeof(HDDType), hdd.hdd_type),
                           (ushort)hdd.speed);
        }

        public override void Update(HDD data)
        {
            // find the EF HDD object
            HDDEntity hdd = context.HDDs.Single(h => h.hdd_id == data.ID);
            // get the EF manufacturer object
            ManufacturerEntity manufacturer = context.Manufacturers.Single(m => m.manufacturer_id == data.Manufacturer.ID);

            // update
            hdd.manufacturer_id = data.Manufacturer.ID;
            hdd.Manufacturer = manufacturer;
            hdd.model = data.Model;
            hdd.price = data.Price;
            hdd.capacity = (short)data.Capacity;
            hdd.hdd_form_factor = data.HddFormFactor.ToString();
            hdd.hdd_interface = data.HddInterfaceType.ToString();
            hdd.hdd_type = data.HddType.ToString();
            hdd.speed = (short)data.Speed;

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
                                   ID = h.hdd_id,
                                   Manufacturer = new Manufacturer
                                   {
                                       ID = h.Manufacturer.manufacturer_id,
                                       Name = h.Manufacturer.name
                                   },
                                   Model = h.model,
                                   Price = h.price,
                                   Capacity = (ushort)h.capacity,
                                   HddFormFactor = (HDDFormFactor)Enum.Parse(typeof(HDDFormFactor), h.hdd_form_factor),
                                   HddInterfaceType = (HDDInterfaceType)Enum.Parse(typeof(HDDInterfaceType), h.hdd_interface),
                                   HddType = (HDDType)Enum.Parse(typeof(HDDType), h.hdd_type),
                                   Speed = (ushort)h.speed
                               };

                hdds.Add(hdd);
            }
                
            return hdds;
        }
    }
}
