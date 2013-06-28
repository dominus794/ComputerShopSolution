using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.MVC.Utilities;

namespace ComputerShop.MVC.Views
{
	public abstract class FactoryViewBase
	{
		protected string errorMessage = string.Empty;
		public string ErrorMessage
		{
			get { return errorMessage; }
			set { errorMessage = value; }
		}

		internal virtual string EnterModel()
		{
			string model = string.Empty;
			UserInputValidator.GetUserInput(ref model, "Please enter a model name:", "Invalid Characters in input.", true, true, true);
			return model;
		}

		internal virtual decimal EnterPrice()
		{
			decimal price = 0.0M;
			UserInputValidator.GetUserInput(ref price, "Please enter a price:", "Price must be between 0.00 and 1000.00", 0M, 1000M);
			return price;
		}
	}
}
