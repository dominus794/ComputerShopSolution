using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Interfaces
{
	public interface IRamModuleCollection
    {
        #region Properties

        SortedList<IRamModule, int> RamModules { get; set; }

        #endregion


        #region Methods

        void AddRamModule(IRamModule module, int quantity);
		void RemoveRamModule(IRamModule module, int quantity);
		void ClearRamModules();

        #endregion
    }
}
