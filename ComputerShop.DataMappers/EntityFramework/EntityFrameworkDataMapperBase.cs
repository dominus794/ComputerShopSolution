using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.DAL.EntityFramework;

namespace ComputerShop.DataMappers.EntityFramework
{
    public abstract class EntityFrameworkDataMapperBase<TEntity> : DataMapper<TEntity> where TEntity : class, IEntity
    {
        protected ComputerShopContext context;

        public EntityFrameworkDataMapperBase(string connectionString)
        {
            context = new ComputerShopContext();
        }

        public EntityFrameworkDataMapperBase() 
        {
            context = new ComputerShopContext();
        }

        public abstract override void Insert(TEntity data);
        public abstract override void Delete(TEntity data);
        public abstract override TEntity Select(int id);
        public abstract override void Update(TEntity data);
        public abstract override IList<TEntity> GetAll();
    }
}
