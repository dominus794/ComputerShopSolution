using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Interfaces
{
    #region ICPU Enumerations

    public enum CPUFormFactor
	{
		Desktop, Laptop
	}

	public enum CPUCoreType
	{
		Single, Dual, Tri, Quad, Hex, Octo
	}

    #endregion

    public interface ICPU : IProduct
    {
        #region Properties

        float ClockSpeed { get; set; }
		CPUFormFactor CpuFormFactor { get; set; }
		CPUCoreType CpuCoreType { get; set; }

        #endregion
    }
}
