using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Interfaces
{
	public interface IManufacturer : IEntity, IComparable<IManufacturer>
    {
        #region Properties

        string Name { get; set; }

        #endregion
    }
}
