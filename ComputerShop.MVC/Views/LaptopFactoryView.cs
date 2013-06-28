using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;
using ComputerShop.Repository;
using ComputerShop.MVC.Utilities;

namespace ComputerShop.MVC.Views
{
	public class LaptopFactoryView : FactoryViewBase
	{
		internal int EnterNumberOfRamModules()
		{
			int numberOfRamModules = 0;
			UserInputValidator.GetUserInput(ref numberOfRamModules, "Please select the number of ram modules you would like:", "Number of ram modules must be between 1 and 4.", 1, 4);
			return numberOfRamModules;
		}

		internal byte EnterWeight()
		{
			byte weight = 0;
			UserInputValidator.GetUserInput(ref weight, "Please enter a weight:", "Weight must be between 1 and 5", 1, 5);
			return weight;
		}

		internal byte EnterBatteryLife()
		{
			byte batteryLife = 0;
			UserInputValidator.GetUserInput(ref batteryLife, "Please enter a battery life:", "Battery Life must be between 1 and 8", 1, 8);
			return batteryLife;
		}

		internal float EnterDisplaySize()
		{
			float displaySize = 0.0F;
			UserInputValidator.GetUserInput<float>(ref displaySize, new List<float> { 15.6F, 17.0F }, "Please select a display size:", "Invalid Selection.");
			return displaySize;
		}
	}
}
