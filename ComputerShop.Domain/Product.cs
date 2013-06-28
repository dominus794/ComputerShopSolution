using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Domain
{
	using Interfaces;

	public abstract class Product : Entity, IProduct
    {
        #region Fields

        protected IManufacturer manufacturer;
		protected string model;
		protected decimal price;

        #endregion


        #region Constructors

        public Product()
		{
			this.manufacturer = new Manufacturer();
			this.model = "DefaultProductModel";
			this.price = 0.00M;
		}

		public Product(int id, IManufacturer manufacturer, string model, decimal price)
			: base(id)
		{
			this.manufacturer = manufacturer;
			this.model = model;
			this.price = price;
		}

        #endregion


        #region IProduct Members

        public IManufacturer Manufacturer
		{
			get { return manufacturer; }
			set { manufacturer = value; }
		}

		public string Model
		{
			get { return model; }
			set { model = value; }
		}

		public decimal Price
		{
			get { return price; }
			set { price = value; }
		}

		#endregion
	}
}
