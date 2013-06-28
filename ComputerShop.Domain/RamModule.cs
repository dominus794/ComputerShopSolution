using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Domain
{
	using Interfaces;

	public class RamModule : Product, IRamModule, IEquatable<RamModule>, IComparable<IRamModule>
    {
        #region Fields

        protected ushort clockSpeed;
		protected DDRVersion ddrVersion;
		protected RAMFormFactor ramFormFactor;
		protected ushort size;

        #endregion


        #region Constructors

        public RamModule()
		{
			this.model = "DefaultRamModuleModel";
			this.clockSpeed = 0;
			this.ddrVersion = DDRVersion.DDR;
			this.ramFormFactor = RAMFormFactor.DIMM;
			this.size = 0;
		}

		public RamModule(int id, IManufacturer manufacturer, string model, decimal price,
						 ushort clockSpeed, DDRVersion ddrVersion, RAMFormFactor ramFormFactor, ushort size)
			:base(id, manufacturer, model, price)
		{
			this.clockSpeed = clockSpeed;
			this.ddrVersion = ddrVersion;
			this.ramFormFactor = ramFormFactor;
			this.size = size;
		}

        #endregion


        #region IRamModule Members

        public ushort ClockSpeed
		{
			get { return clockSpeed; }
			set { clockSpeed = value; }
		}

		public DDRVersion DDRVersion
		{
			get { return ddrVersion; }
			set { ddrVersion = value; }
		}

		public RAMFormFactor RamFormFactor
		{
			get { return ramFormFactor; }
			set { ramFormFactor = value; }
		}

		public ushort Size
		{
			get { return size; }
			set { size = value; }
		}

		#endregion


		#region Overrides

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			builder.Append(string.Format("{0} {1} {2}GB {3}Mhz {4} {5} {6:c}", manufacturer.Name, model, size, clockSpeed, ddrVersion, ramFormFactor, price));

			return builder.ToString();
		}

		#endregion


		#region IEquatable<RamModule> Members

		public bool Equals(RamModule other)
		{			
			if (this.model == other.model && 
                this.price == other.price && 
				this.clockSpeed == other.clockSpeed && 
                this.size == other.size && 
				this.ramFormFactor == other.ramFormFactor && 
                this.ddrVersion == other.ddrVersion)
				return true;
			else
				return false;
		}

		#endregion


		#region IComparable<IRamModule> Members

		public int CompareTo(IRamModule other)
		{
			//- Any number less than zero, this instance comes before the 
			//specified object in the sort order
			//- Zero This instance is equal to the specified object.
			//- Any number greater than zero, this instance comes after the 
			//specified object in the sort order

			//Perform comparison manually
			if (
					(this.Manufacturer.CompareTo(other.Manufacturer) == 1) ||
					(this.Model.CompareTo(other.Model) == 1) ||
					(this.Price > other.Price) ||
					(this.ClockSpeed > other.ClockSpeed) ||
					(this.Size > other.Size) ||
					(this.DDRVersion > other.DDRVersion) ||
					(this.RamFormFactor > other.RamFormFactor)
				)
				return 1;
			if (
					(this.Manufacturer.CompareTo(other.Manufacturer) == -1) ||
					(this.Model.CompareTo(other.Model) == -1) ||
					(this.Price < other.Price) ||
					(this.ClockSpeed < other.ClockSpeed) ||
					(this.Size < other.Size) ||
					(this.DDRVersion < other.DDRVersion) ||
					(this.RamFormFactor < other.RamFormFactor)
				)
				return -1;
			else
				return 0;
		}

		#endregion
	}
}
