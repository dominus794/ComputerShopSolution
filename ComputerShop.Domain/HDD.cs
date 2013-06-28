using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Domain
{
	using Interfaces;

	public class HDD : Product, IHDD, IEquatable<HDD>, IComparable<IHDD>
    {
        #region Fields

        protected ushort capacity;
		protected HDDFormFactor hddFormFactor;
		protected HDDInterfaceType hddInterfaceType;
		protected HDDType hddType;
		protected ushort speed;

        #endregion


        #region Constructors

        public HDD()
		{
			this.model = "DefaultHDDModel";
			this.capacity = 0;
			this.hddFormFactor = HDDFormFactor.DESKTOP;
			this.hddInterfaceType = HDDInterfaceType.SATA;
			this.hddType = HDDType.Mechanical;
			this.speed = 6;
		}

		public HDD(int id, IManufacturer manufacturer, string model, decimal price,
				   ushort capacity, HDDFormFactor hddFormFactor, HDDInterfaceType hddInterfaceType, HDDType hddType, ushort speed)
			: base(id, manufacturer, model, price)
		{
			this.capacity = capacity;
			this.hddFormFactor = hddFormFactor;
			this.hddInterfaceType = hddInterfaceType;
			this.hddType = hddType;
			this.speed = speed;
		}

        #endregion


        #region IHDD Members

        public ushort Capacity
		{
			get { return capacity; }
			set { capacity = value; }
		}

		public HDDFormFactor HddFormFactor
		{
			get { return hddFormFactor; }
			set { hddFormFactor = value; }
		}

		public HDDInterfaceType HddInterfaceType
		{
			get { return hddInterfaceType; }
			set { hddInterfaceType = value; }
		}

		public HDDType HddType
		{
			get { return hddType; }
			set { hddType = value; }
		}

		public ushort Speed
		{
			get { return speed; }
			set { speed = value; }
		}

		#endregion


		#region Overrides

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			string formFactorString = String.Empty;
			string typeString = String.Empty;

			switch (hddFormFactor)
			{
				case HDDFormFactor.DESKTOP:
					formFactorString = "3.5";
					break;
				case HDDFormFactor.LAPTOP:
					formFactorString = "2.5";
					break;
			}

			switch (hddType)
			{
				case HDDType.Mechanical:
					typeString = "7200RPM";
					break;
				case HDDType.SSD:
					typeString = "SSD";
					break;
			}

			builder.Append(string.Format("{0} {1} {2}GB {3}\" {4} {5}Gb/s {6} {7:c}", manufacturer.Name, model, capacity,
							   formFactorString, typeString, speed, hddInterfaceType, price));

			return builder.ToString();
		}

		#endregion


		#region IEquatable<HDD> Members

		public bool Equals(HDD other)
		{
			if (this.manufacturer == other.manufacturer &&
                this.model == other.model &&
				this.price == other.price && 
                this.capacity == other.capacity &&
				this.hddFormFactor == other.hddFormFactor && 
                this.hddInterfaceType == other.hddInterfaceType &&
				this.hddType == other.hddType && 
                this.speed == other.speed)
				return true;
			else
				return false;
		}

		#endregion


		#region IComparable<IHDD> Members

		public int CompareTo(IHDD other)
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
					(this.Capacity > other.Capacity) ||
					(this.Speed > other.Speed) ||
					(this.HddType > other.HddType) ||
					(this.HddFormFactor > other.HddFormFactor) ||
					(this.HddInterfaceType > other.HddInterfaceType)
				)
				return 1;
			if (
					(this.Manufacturer.CompareTo(other.Manufacturer) == -1) ||
					(this.Model.CompareTo(other.Model) == -1) ||
					(this.Price < other.Price) ||
					(this.Capacity < other.Capacity) ||
					(this.Speed < other.Speed) ||
					(this.HddType < other.HddType) ||
					(this.HddFormFactor < other.HddFormFactor) ||
					(this.HddInterfaceType < other.HddInterfaceType)
				)
				return -1;
			else
				return 0;
		}

		#endregion
	}
}
