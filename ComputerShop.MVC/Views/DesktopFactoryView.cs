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
	public class DesktopFactoryView : FactoryViewBase
	{
		internal int EnterNumberOfRamModules()
		{
			int numberOfRamModules = 0;
			UserInputValidator.GetUserInput(ref numberOfRamModules, "Please select the number of ram modules you would like:", "Number of ram modules must be between 1 and 4.", 1, 4);
			return numberOfRamModules;
		}

		internal int EnterNumberOfHDDs()
		{
			int numberOfHdds = 0;
			UserInputValidator.GetUserInput(ref numberOfHdds, "Please select the number of hard drives you would like:", "Number of hard drives must be between 1 and 4.", 1, 4);
			return numberOfHdds;
		}

		internal int EnterNumberOfGPUs()
		{
			int numberOfGPUs = 0;
			UserInputValidator.GetUserInput(ref numberOfGPUs, "Please select the number of graphics cards you would like:", "Number of graphics cards must be between 1 and 3.", 1, 3);
			return numberOfGPUs;
		}
	}
}
