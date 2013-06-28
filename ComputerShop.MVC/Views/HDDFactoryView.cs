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
	public class HDDFactoryView : FactoryViewBase
	{
		internal ushort EnterCapacity()
		{
			ushort capacity = 0;
			UserInputValidator.GetUserInput(ref capacity, "Please enter a capacity:", "Capacity must be between 32 and 3072.", 32, 3072);
			return capacity;
		}

		internal HDDFormFactor SelectHDDFormFactor()
		{
			HDDFormFactor hddFormFactor = HDDFormFactor.DESKTOP;
			UserInputValidator.GetUserInput<HDDFormFactor>(ref hddFormFactor, "Please select a form factor:", "Invalid Selection. Please try again.");
			return hddFormFactor;
		}

		internal HDDInterfaceType SelectHDDInterfaceType()
		{
			HDDInterfaceType hddInterfaceType = HDDInterfaceType.SATA;
			UserInputValidator.GetUserInput<HDDInterfaceType>(ref hddInterfaceType, "Please select a Hard drive interface:", "Invalid Selection. Please try again.");
			return hddInterfaceType;
		}

		internal HDDType SelectHDDType()
		{
			HDDType hddType = HDDType.Mechanical;
			UserInputValidator.GetUserInput<HDDType>(ref hddType, "Please select a Harddrive type:", "Invalid Selection. Please try again.");
			return hddType;
		}

		internal ushort EnterSpeed()
		{
			ushort speed = 0;
			UserInputValidator.GetUserInput(ref speed, "Please enter a speed:", "Speed must be 1.5, 3 or 6.", 1, 6);
			return speed;
		}
	}
}
