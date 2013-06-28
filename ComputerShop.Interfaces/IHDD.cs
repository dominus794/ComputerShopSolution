using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Interfaces
{
    #region IHDD Enumerations

    public enum HDDType
	{
		Mechanical, SSD
	}

	public enum HDDFormFactor
	{
		DESKTOP, LAPTOP
	}

	public enum HDDInterfaceType
	{
		IDE, SATA
	}

    #endregion

    public interface IHDD : IProduct, IComparable<IHDD>
    {
        #region Properties

        ushort Capacity { get; set; }
		HDDFormFactor HddFormFactor { get; set; }
		HDDInterfaceType HddInterfaceType { get; set; }
		HDDType HddType { get; set; }
		ushort Speed { get; set; }

        #endregion
    }
}
