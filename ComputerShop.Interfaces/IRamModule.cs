using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Interfaces
{
    #region IRamModule Enumerations

    public enum RAMFormFactor
	{
		DIMM, SODIMM
	}

	public enum DDRVersion
	{
		DDR, DDR2, DDR3
	}

    #endregion

    public interface IRamModule : IProduct, IComparable<IRamModule>
    {
        #region Properties

        ushort ClockSpeed { get; set; }
		DDRVersion DDRVersion { get; set; }
		RAMFormFactor RamFormFactor { get; set; }
		ushort Size { get; set; }

        #endregion
    }
}
