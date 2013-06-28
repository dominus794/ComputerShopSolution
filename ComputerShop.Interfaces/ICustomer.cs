using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Interfaces
{
    #region ICustomer Enumerations

    public enum ComputerType { Desktop, Laptop };

    #endregion

    public interface ICustomer : IPerson
    {
        #region Properties

        ComputerType ComputerType { get; set; }
		decimal MaxPrice { get; set; }
		float MinimumCpuSpeed { get; set; }
		ushort  MinimumRamAmount { get; set; }
		ushort  MinimumHDDCapacity { get; set; }

        #endregion
    }
}
