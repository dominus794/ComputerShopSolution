using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Interfaces
{
	public interface IComputer : IProduct
    {
        #region Properties

        ICPU Cpu { get; set; }	
		IRamModuleCollection RamModuleCollection { get; set; }
		ushort TotalRam { get; }

        #endregion


        #region Methods

        void AddRamModule(IRamModule ramModule, int quantity);
		void RemoveRamModule(IRamModule ramModule, int quantity);
		void ClearRamModules();

        #endregion
    }
}
