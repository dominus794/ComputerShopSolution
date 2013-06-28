using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;
using ComputerShop.Repository;
using ComputerShop.MVC.Models;
using ComputerShop.MVC.Utilities;

namespace ComputerShop.MVC.Views
{
	public class ManufacturerFactoryView
	{
		private string errorMessage = string.Empty;
		public string ErrorMessage
		{
			get { return errorMessage; }
			set { errorMessage = value; }
		}

		public string EnterManufacturerName()
		{
			string name = string.Empty;			
			UserInputValidator.GetUserInput(ref name, "Please enter the manufacturer name:", "Invalid characters in input. Please try again", true, true, true);
			return name;
		}
	}
}
