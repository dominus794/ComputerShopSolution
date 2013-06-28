using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Interfaces
{	
	public interface ILaptop : IComputer
    {
        #region Properties

        IGPU Gpu { get; set; }
		IHDD Hdd { get; set; }
		byte Weight { get; set; }
		byte BatteryLife { get; set; }
		float DisplaySize { get; set; }
		ushort TotalHDDCapacity { get; }

        #endregion
    }
}
