using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;
using ComputerShop.Domain;
using ComputerShop.Repository;
using ComputerShop.MVC.Utilities;
using ComputerShop.MVC.Controllers;

namespace ComputerShop.MVC.Views
{
	public class RamModuleFactoryView : FactoryViewBase
	{
		internal ushort EnterClockSpeed()
		{
			ushort clockSpeed = 0;
			UserInputValidator.GetUserInput(ref clockSpeed, "Please enter a clock speed:", "Clock Speed must be between 100 and 2666", 100, 2666);
			return clockSpeed;
		}

		internal DDRVersion SelectDDRVersion()
		{
			DDRVersion ddrVersion = DDRVersion.DDR;
			UserInputValidator.GetUserInput<DDRVersion>(ref ddrVersion, "Please select a DDR Version:", "Invalid Selection. Please try again");
			return ddrVersion;
		}

		internal RAMFormFactor SelectRamFormFactor()
		{
			RAMFormFactor ramFormFactor = RAMFormFactor.DIMM;
			UserInputValidator.GetUserInput<RAMFormFactor>(ref ramFormFactor, "Please select a form factor:", "Invalid Selection. Please try again");
			return ramFormFactor;
		}

		internal ushort EnterSize()
		{
			ushort size = 0;
			UserInputValidator.GetUserInput(ref size, "Please enter a size:", "Size must be between 64 and 8192.", 64, 8192);
			return size;
		}
	}
}
