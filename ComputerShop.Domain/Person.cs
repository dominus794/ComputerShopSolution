using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;

namespace ComputerShop.Domain
{
	public abstract class Person : Entity, IPerson
	{
		#region Fields

		protected string firstName;
		protected string lastName;
		protected IAddress invoiceAddress;
		protected IAddress deliveryAddress;

		#endregion


		#region Constructor

		public Person(string fName, string lName, IAddress invAddr, IAddress delAddr)
		{
			this.firstName = fName;
			this.lastName = lName;
			this.invoiceAddress = invAddr;
			this.deliveryAddress = delAddr;
		}

		#endregion


		#region Properties

		public string FirstName
		{
			get { return firstName; }
			set { firstName = value; }
		}

		public string LastName
		{
			get { return lastName; }
			set { lastName = value; }
		}

		public string FullName
		{
			get { return firstName + " " + lastName; }
		}

		public IAddress InvoiceAddress
		{
			get { return invoiceAddress; }
			set { invoiceAddress = value; }
		}

		public IAddress DeliveryAddress
		{
			get { return deliveryAddress; }
			set { deliveryAddress = value; }
		}

		#endregion


		#region Overrides

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			builder.AppendLine(FullName);
			builder.AppendLine(string.Format("Invoice Address:\n{0}", invoiceAddress.ToString()));
			builder.AppendLine(string.Format("Delivery Address:\n{0}", deliveryAddress.ToString()));

			return builder.ToString();
		}

		#endregion
	}
}
