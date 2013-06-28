using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Interfaces
{
	public interface IDesktop : IComputer
    {
        #region Properties

        IGPUCollection GpuCollection { get; set; }
        IHDDCollection HddCollection { get; set; }
        ushort TotalHDDCapacity { get; }

        #endregion


        #region Methods

        // HDD Methods
        void AddHDD(IHDD hdd, int quantity);
        void RemoveHDD(IHDD hdd, int quantity);
        void ClearHDDs();

        // GPU Methods
        void AddGPU(IGPU gpu, int quantity);
		void RemoveGPU(IGPU gpu, int quantity);
		void ClearGPUs();
                        		
        #endregion
    }
}
