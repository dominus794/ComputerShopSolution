using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;

namespace ComputerShop.Domain
{	
	public class Customer : Person, ICustomer
	{
		#region Fields

		protected ComputerType computerType;
		protected decimal maxPrice;
		protected float minimumCpuSpeed;
		protected ushort minimumRamAmount;
		protected ushort minimumHDDCapacity;

		#endregion


		#region Constructor

		public Customer(string fName, string lName, IAddress invAddr, IAddress delAddr, ComputerType computerType,
						decimal maxPrice, float minimumCpuSpeed, ushort minimumRamAmount, ushort minimumHDDCapacity)
			: base(fName, lName, invAddr, delAddr)
		{
			this.computerType = computerType;
			this.maxPrice = maxPrice;
			this.minimumCpuSpeed = minimumCpuSpeed;
			this.minimumRamAmount = minimumRamAmount;
			this.minimumHDDCapacity = minimumHDDCapacity;
		}

		#endregion


		#region Properties

		public ComputerType ComputerType
		{
			get { return computerType; }
			set { computerType = value; }
		}

		public float MinimumCpuSpeed
		{
			get { return minimumCpuSpeed; }
			set
			{
				if (value < 0)
					minimumCpuSpeed = 0.0F;
				else
					minimumCpuSpeed = value;
			}
		}

		public ushort MinimumRamAmount
		{
			get { return minimumRamAmount; }
			set
			{
				if (value < 0)
					minimumRamAmount = 0;
				else
					minimumRamAmount = value;
			}
		}

		public ushort MinimumHDDCapacity
		{
			get { return minimumHDDCapacity; }
			set
			{
				if (value < 0)
					minimumHDDCapacity = 0;
				else
					minimumHDDCapacity = value;
			}
		}

		public decimal MaxPrice
		{
			get { return maxPrice; }
			set
			{
				if (value < 0)
					maxPrice = 0.0M;
				else
					maxPrice = value;
			}
		}

		#endregion


		#region Overrides

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			builder.AppendLine(base.ToString());
			builder.AppendLine(string.Format("Computer Type: {0}", computerType.ToString()));
			builder.AppendLine(string.Format("Maximum Price: £{0}", maxPrice.ToString()));
			builder.AppendLine(string.Format("Minimum CPU Speed:{0} Ghz", minimumCpuSpeed.ToString()));
			builder.AppendLine(string.Format("Minimum Ram Amount:{0} GB", minimumRamAmount.ToString()));
			builder.AppendLine(string.Format("Minimum HDD Capacity:{0} GB", minimumHDDCapacity.ToString()));
			

			return builder.ToString();
		}

		#endregion
	}
}
