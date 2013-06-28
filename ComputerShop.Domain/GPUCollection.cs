using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;

namespace ComputerShop.Domain
{
	public class GPUCollection : IGPUCollection
    {
        #region Fields 

        protected SortedList<IGPU, int> gpus;
        
        #endregion


        #region Constructors

        public GPUCollection()
		{
			this.gpus = new SortedList<IGPU, int>();
		}

        #endregion


        #region Properties 

        public SortedList<IGPU, int> GPUs
		{
			get { return gpus; }
			set { gpus = value; }
		}

        #endregion


        #region Methods

        public void AddGPU(IGPU gpu, int quantity)
		{
			try
			{
				//check if we have the gpu in the list already
				if (gpus.ContainsKey(gpu))
				{
					//if we do, simply add the quantity
					gpus[gpu] += quantity;
				}
				else
				{
					//otherwise, add the new gpu and the quantity.
					gpus.Add(gpu, quantity);
				}
			}
			catch (ArgumentException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void RemoveGPU(IGPU gpu, int quantity)
		{
			try
			{
				//check the gpu is actually in out list.
				if (gpus.ContainsKey(gpu))
				{
					//check that the quantity we are removing is less than the
					//quantity stored
					if (gpus[gpu] > quantity)
					{
						gpus[gpu] -= quantity;
					}
					else
					{
						//otherwise remove all the gpus
						gpus.Remove(gpu);
					}
				}
				else
				{
					Console.WriteLine("The graphics card given is not part of this desktop");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void ClearGPUs()
		{
			this.gpus.Clear();
		}

		#endregion
	}
}
