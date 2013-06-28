using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Interfaces
{    
	public interface IProduct : IEntity
    {
        #region Properties

        IManufacturer Manufacturer { get; set; }        
		string Model { get; set; }
		decimal Price { get; set; }

        #endregion
    }
}
