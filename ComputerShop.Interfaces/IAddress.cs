using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Interfaces
{
	public interface IAddress : IEntity
    {
        #region Properties

        string AddressLine1 { get; set; }
		string AddressLine2 { get; set; }
		string City { get; set; }
		string Country { get; set; }
		string PostCode { get; set; }

        #endregion
    }
}
