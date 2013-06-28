using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Domain
{
	using Interfaces;

	public abstract class Entity : IEntity
    {
        #region Fields

        protected int id;

        #endregion


        #region Constructors

        public Entity()
		{
			this.id = 0;
		}

		public Entity(int id)
		{
			this.id = id;
		}

        #endregion


        #region IEntity Members

        public int ID
		{
			get { return id; }
			set { id = value; }
		}

		#endregion
	}
}
