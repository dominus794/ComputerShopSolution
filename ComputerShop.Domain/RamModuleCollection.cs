using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;

namespace ComputerShop.Domain
{
	public sealed class RamModuleCollection : IRamModuleCollection
    {
        #region Fields

        private SortedList<IRamModule, int> ramModules;

        #endregion


        #region Constructors

        public RamModuleCollection()
		{
			this.ramModules = new SortedList<IRamModule, int>();
		}

        #endregion


        #region Properties

        public SortedList<IRamModule, int> RamModules
		{
			get { return ramModules; }
			set { ramModules = value; }
		}

        #endregion


        #region Methods

        public void AddRamModule(IRamModule ramModule, int quantity)
		{
			try
			{
				//check if we have the ramModule in the list already
				if (ramModules.ContainsKey(ramModule))
				{
					//if we do, simply add the quantity
					ramModules[ramModule] += quantity;
				}
				else
				{
					//otherwise, add the new ramModule and the quantity.
					ramModules.Add(ramModule, quantity);
				}
			}
			catch (ArgumentException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void RemoveRamModule(IRamModule ramModule, int quantity)
		{
			try
			{
				//check the ramModule is actually in our list.
				if (ramModules.ContainsKey(ramModule))
				{
					//check that the quantity we are removing is less than the 
					//quantity stored.
					if (ramModules[ramModule] > quantity)
						ramModules[ramModule] -= quantity;
					else
					{
						//otherwise remove all the modules.
						ramModules.Remove(ramModule);
					}
				}
				else
				{
					//ram module not in the list.
					//Console.WriteLine("The ramModule {0} is not in this computer's list of ram modules.", ramModule);
					//throw InvalidArgumentException("RamModule does'nt exist in this collection");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void ClearRamModules()
		{
			this.ramModules.Clear();
        }

        #endregion
    }
}
