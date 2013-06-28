using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Domain
{
	using Interfaces;

	public class CPU : Product, ICPU
    {
        #region Fields

        protected float clockSpeed;
		protected CPUFormFactor cpuFormFactor;
		protected CPUCoreType cpuCoreType;

        #endregion


        #region Constructors

        public CPU()
		{
			this.model = "DefaultCPUModel";
			this.clockSpeed = 0.00F;
			this.cpuFormFactor = CPUFormFactor.Desktop;
			this.cpuCoreType = CPUCoreType.Single;
		}

		public CPU(int id, IManufacturer manufacturer, string model, decimal price,
				   float clockSpeed, CPUFormFactor cpuFormFactor, CPUCoreType cpuCoreType)
			:base(id, manufacturer, model, price)
		{
			this.clockSpeed = clockSpeed;
			this.cpuFormFactor = cpuFormFactor;
			this.cpuCoreType = cpuCoreType;
		}

        #endregion


        #region ICPU Members

        public float ClockSpeed
		{
			get { return clockSpeed; }
			set { clockSpeed = value; }
		}

		public CPUFormFactor CpuFormFactor
		{
			get { return cpuFormFactor; }
			set { cpuFormFactor = value; }
		}

		public CPUCoreType CpuCoreType
		{
			get { return cpuCoreType; }
			set { cpuCoreType = value; }
		}

		#endregion


		#region Overrides

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			builder.Append(string.Format("{0} {1} {2}Ghz {3:c}", manufacturer.Name, model, clockSpeed, price));

			return builder.ToString();
		}

		#endregion
	}
}
