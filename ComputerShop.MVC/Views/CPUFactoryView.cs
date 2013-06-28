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
	public class CPUFactoryView : FactoryViewBase
	{
		internal float EnterClockSpeed()
		{
			float speed = 0.0F;
			UserInputValidator.GetUserInput(ref speed, "Please enter a speed:", "Speed must be between 1.0 and 4.0", 1.0F, 4.0F);
			return speed;
		}

		internal CPUFormFactor SelectCPUFormFactor()
		{
			CPUFormFactor cpuFormFactor = CPUFormFactor.Desktop;
			UserInputValidator.GetUserInput<CPUFormFactor>(ref cpuFormFactor, "Please select the type of cpu:", "Invalid Selection. Please try again");
			return cpuFormFactor;
		}

		internal CPUCoreType SelectCPUCoreType()
		{
			CPUCoreType cpuCoreType = CPUCoreType.Single;
			UserInputValidator.GetUserInput<CPUCoreType>(ref cpuCoreType, "Please select the number of cpu cores:", "Invalid Selection. Please try again");
			return cpuCoreType;
		}
	}
}
