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
	public class GPUFactoryView : FactoryViewBase
	{
		internal string EnterGPUModel()
		{
			string gpuModel = string.Empty;
			UserInputValidator.GetUserInput(ref gpuModel, "Please enter a gpu model no:", "Invalid Characters in input.", true, true, true);
			return gpuModel;
		}

		internal ushort EnterGPUClockSpeed()
		{
			ushort gpuClockSpeed = 0;
			UserInputValidator.GetUserInput(ref gpuClockSpeed, "Please enter a gpu clock speed:", "Gpu clock speed must be between 100 and 2000.", 100, 2000);
			return gpuClockSpeed;
		}

		internal ushort EnterVRamSize()
		{
			ushort vramSize = 0;
			UserInputValidator.GetUserInput(ref vramSize, "Please enter a vram amount:", "Vram amount must be between 16 and 3072.", 16, 3072);
			return vramSize;
		}

		internal ushort EnterVRamClockSpeed()
		{
			ushort vramClockSpeed = 0;
			UserInputValidator.GetUserInput(ref vramClockSpeed, "Please enter the vram clock speed:", "Vram clock speed must be between 100 and 5000.", 100, 5000);
			return vramClockSpeed;
		}

		internal GDDRVersion SelectGDDRVersion()
		{
			GDDRVersion gddrVersion = GDDRVersion.GDDR1;
			UserInputValidator.GetUserInput<GDDRVersion>(ref gddrVersion, "Please select a Vram type:", "Invalid Selection. Please try again.");
			return gddrVersion;
		}

		internal GPUType SelectGPUType()
		{
			GPUType gpuType = GPUType.Dedicated;
			UserInputValidator.GetUserInput<GPUType>(ref gpuType, "Please select a gpu type:", "Invalid Selection. Please try again.");
			return gpuType;
		}
	}
}
