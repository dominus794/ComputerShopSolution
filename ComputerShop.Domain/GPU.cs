using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Domain
{
	using Interfaces;

	public class GPU : Product, IGPU, IEquatable<GPU>, IComparable<IGPU>
    {
        #region Fields

        protected string gpuModel;
		protected ushort gpuClockSpeed;
		protected ushort vramSize;
		protected ushort vramClockSpeed;
		protected GDDRVersion gddrVersion;
		protected GPUType gpuType;

        #endregion


        #region Constructors

        public GPU()
		{
			this.model = "DefaultGPUModel";
			this.gpuModel = "DefaultGPUModelModel";
			this.gpuClockSpeed = 0;
			this.vramSize = 0;
			this.vramClockSpeed = 0;
			this.gddrVersion = GDDRVersion.GDDR5;
			this.gpuType = GPUType.Dedicated;
		}

		public GPU(int id, IManufacturer manufacturer, string model, decimal price,
				   string gpuModel, ushort gpuClockSpeed, ushort vramSize, ushort vramClockSpeed,
				   GDDRVersion gddrVersion, GPUType gpuType)
			: base(id, manufacturer, model, price)
		{
			this.gpuModel = gpuModel;
			this.gpuClockSpeed = gpuClockSpeed;
			this.vramSize = vramSize;
			this.vramClockSpeed = vramClockSpeed;
			this.gddrVersion = gddrVersion;
			this.gpuType = gpuType;
		}

        #endregion


        #region IGPU Members

        public string GpuModel
		{
			get { return gpuModel; }
			set { gpuModel = value; }
		}

		public ushort GpuClockSpeed
		{
			get { return gpuClockSpeed; }
			set { gpuClockSpeed = value; }
		}

		public ushort VRamSize
		{
			get { return vramSize; }
			set { vramSize = value; }
		}

		public ushort VRamClockSpeed
		{
			get { return vramClockSpeed; }
			set { vramClockSpeed = value; }
		}

		public GDDRVersion GddrVersion
		{
			get { return gddrVersion; }
			set { gddrVersion = value; }
		}

		public GPUType GpuType
		{
			get { return gpuType; }
			set { gpuType = value; }
		}

		#endregion


		#region Overrides

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			builder.Append(string.Format("{0} {1} {2}({3}Mhz) {4}MB {5} ({6}Mhz effective) {7:c}", manufacturer.Name, model, gpuModel, gpuClockSpeed, vramSize, gddrVersion, vramClockSpeed, price));

			return builder.ToString();
		}

		#endregion


		#region IEquatable<GPU> Members

		public bool Equals(GPU other)
		{
			if (this.manufacturer == other.manufacturer && 
                this.model == other.model &&
				this.price == other.price && 
                this.gpuModel == other.gpuModel &&
				this.gpuClockSpeed == other.gpuClockSpeed && 
                this.vramSize == other.vramSize &&
				this.vramClockSpeed == other.vramClockSpeed && 
                this.gddrVersion == other.gddrVersion && 
                this.gpuType == other.gpuType)
				return true;
			else
				return false;
		}

		#endregion


		#region IComparable<IGPU> Members

		public int CompareTo(IGPU other)
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
					(this.GpuModel.CompareTo(other.GpuModel) == 1) ||
					(this.GpuClockSpeed > other.GpuClockSpeed) ||
					(this.VRamSize > other.VRamSize) ||
					(this.VRamClockSpeed > other.VRamClockSpeed) ||
					(this.GddrVersion > other.GddrVersion) ||
					(this.GpuType > other.GpuType)
				)
				return 1;
			if (
					(this.Manufacturer.CompareTo(other.Manufacturer) == -1) ||
					(this.Model.CompareTo(other.Model) == -1) ||
					(this.Price < other.Price) ||
					(this.GpuModel.CompareTo(other.GpuModel) == -1) ||
					(this.GpuClockSpeed < other.GpuClockSpeed) ||
					(this.VRamSize < other.VRamSize) ||
					(this.VRamClockSpeed < other.VRamClockSpeed) ||
					(this.GddrVersion < other.GddrVersion) ||
					(this.GpuType < other.GpuType)
				)
				return -1;
			else
				return 0;
		}

		#endregion
	}
}
