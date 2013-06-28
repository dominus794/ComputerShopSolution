using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Interfaces
{
	public interface IPerson : IEntity
    {
        #region Properties

        string FirstName { get; set; }
		string LastName { get; set; }
		IAddress InvoiceAddress { get; set; }
		IAddress DeliveryAddress { get; set; }

        #endregion
    }
}
