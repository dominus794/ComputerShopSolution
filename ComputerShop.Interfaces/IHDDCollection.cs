using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Interfaces
{
	public interface IHDDCollection
    {
        #region Properties

        SortedList<IHDD, int> HDDs { get; set; }
        ushort TotalHDDCapacity { get; }

        #endregion


        #region Methods

        void AddHDD(IHDD hdd, int quantity);
		void RemoveHDD(IHDD hdd, int quantity);
		void ClearHDDs();		

        #endregion
    }
}
