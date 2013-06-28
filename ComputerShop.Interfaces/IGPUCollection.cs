using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Interfaces
{
	public interface IGPUCollection
    {
        #region Properties

        SortedList<IGPU, int> GPUs { get; set; }

        #endregion


        #region Methods

        void AddGPU(IGPU gpu, int quantity);
		void RemoveGPU(IGPU gpu, int quantity);
		void ClearGPUs();

        #endregion
    }
}
