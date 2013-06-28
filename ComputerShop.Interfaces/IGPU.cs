using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Interfaces
{
    #region IGPU Enumerations

    public enum GPUType
	{
		Dedicated, Onboard
	}

	public enum GDDRVersion
	{
		GDDR1, GDDR2, GDDR3, GDDR4, GDDR5
	}

    #endregion

    public interface IGPU : IProduct, IComparable<IGPU>
    {
        #region Properties

        string GpuModel { get; set; }
		ushort GpuClockSpeed { get; set; }
		ushort VRamSize { get; set; }
		ushort VRamClockSpeed { get; set; }
		GDDRVersion GddrVersion { get; set; }
		GPUType GpuType { get; set; }

        #endregion
    }
}
