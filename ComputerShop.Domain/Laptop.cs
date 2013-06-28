using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Domain
{
	using Interfaces;

	public class Laptop : Computer, ILaptop
    {
        #region Fields

        protected IGPU gpu;
		protected IHDD hdd;
		protected byte weight;
		protected byte batteryLife;
		protected float displaySize;

        #endregion


        #region Constructors

        public Laptop()
		{
			this.model = "DefaultLaptopModel";
			this.gpu = new GPU();
			this.hdd = new HDD();
			this.weight = 0;
			this.batteryLife = 0;
			this.displaySize = 15.6F;
		}

		public Laptop(int id, IManufacturer manufacturer, string model, decimal price, ICPU cpu)
			:base(id, manufacturer, model, price, cpu)
		{			
			this.gpu = new GPU();
			this.hdd = new HDD();
			this.weight = 0;
			this.batteryLife = 0;
			this.displaySize = 15.6F;
		}		

		public Laptop(int id, IManufacturer manufacturer, string model, decimal price, ICPU cpu,
					  IRamModuleCollection ramModuleCollection, IGPU gpu, IHDD hdd,
					  byte weight, byte batteryLife, float displaySize)
			: base(id, manufacturer, model, price, cpu, ramModuleCollection)
		{
			this.gpu = gpu;
			this.hdd = hdd;
			this.weight = weight;
			this.batteryLife = batteryLife;
			this.displaySize = displaySize;
		}

        #endregion


        #region ILaptop Members

        public IGPU Gpu
		{
			get { return gpu; }
			set { gpu = value; }
		}

		public IHDD Hdd
		{
			get { return hdd; }
			set { hdd = value; }
		}

		public ushort TotalHDDCapacity
		{
			get { return hdd.Capacity; }
		}

		public byte Weight
		{
			get { return weight; }
			set { weight = value; }
		}

		public byte BatteryLife
		{
			get { return batteryLife; }
			set { batteryLife = value; }
		}

		public float DisplaySize
		{
			get { return displaySize; }
			set { displaySize = value; }
		}

		#endregion


		#region Overrides
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			//add base output
			builder.Append(base.ToString());

			//output hdd
			builder.AppendLine(hdd.ToString());
			//output gpu
			builder.AppendLine(gpu.ToString());
			//output weight
			builder.AppendLine(string.Format("{0}Kg", weight));
			//output battery life.
			builder.AppendLine(string.Format("{0}hrs", batteryLife));
			//out put display size.
			builder.AppendLine(string.Format("{0}\"", displaySize));

			return builder.ToString();
		}
		#endregion
	}
}
