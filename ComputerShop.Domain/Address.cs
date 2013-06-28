using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;

namespace ComputerShop.Domain
{
	public class Address : Entity, IAddress
	{
		#region Fields

		protected string addressLine1;
		protected string addressLine2;
		protected string city;
		protected string country;
		protected string postcode;

		#endregion


		#region Constructor

		public Address(string addressLine1, string addressLine2, string city, string country, string postcode)
		{
			this.addressLine1 = addressLine1;
			this.addressLine2 = addressLine2;
			this.city = city;
			this.country = country;
			this.postcode = postcode;
		}

		#endregion


		#region Properties

		public string AddressLine1
		{
			get { return addressLine1; }
			set { addressLine1 = value; }
		}

		public string AddressLine2
		{
			get { return addressLine2; }
			set { addressLine2 = value; }
		}

		public string City
		{
			get { return city; }
			set { city = value; }
		}

		public string Country
		{
			get { return country; }
			set { country = value; }
		}

		public string PostCode
		{
			get { return postcode; }
			set { postcode = value; }
		}

		#endregion


		#region Overrides

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendLine(addressLine1);
			builder.AppendLine(addressLine2);
			builder.AppendLine(city);
			builder.AppendLine(country);
			builder.AppendLine(postcode);

			return builder.ToString();
		}

		#endregion
	}
}
