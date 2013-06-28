using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Domain
{
	using Interfaces;

	public class Manufacturer : Entity, IManufacturer, IComparable<IManufacturer>
    {
        #region Fields

        /// <summary>The name of the Manufacturer.</summary>
        protected string name;

        #endregion


        #region Constructors
        
        public Manufacturer()			
		{			
			this.name = "DefaultManufacturerName";
		}
        
		public Manufacturer(int id, string name)
			:base(id)
		{			
			this.name = name;
		}

        #endregion


        #region IManufacturer Members

        public string Name
		{
			get { return name; }
			set { name = value; }
		}

		#endregion


		#region Overrides

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			builder.Append(string.Format("{0}", name));

			return builder.ToString();
		}

		#endregion


		#region IComparable<IManufacturer> Members

		public int CompareTo(IManufacturer other)
		{
			//- Any number less than zero, this instance comes before the 
			//specified object in the sort order
			//- Zero This instance is equal to the specified object.
			//- Any number greater than zero, this instance comes after the 
			//specified object in the sort order

			//Perform comparison manually
			if (
					(this.name.CompareTo(other.Name) == 1) ||
					(this.ID > other.ID)
				)
				return 1;
			if (
					(this.name.CompareTo(other.Name) == -1) ||
					(this.ID < other.ID)
				)
				return -1;
			else
				return 0;
		}

		#endregion
	}
}
