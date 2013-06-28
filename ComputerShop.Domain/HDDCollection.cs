using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComputerShop.Interfaces;

namespace ComputerShop.Domain
{
	public class HDDCollection : IHDDCollection
    {
        #region Fields

        protected SortedList<IHDD, int> hdds;

        #endregion


        #region Constructors

        public HDDCollection()
		{
			this.hdds = new SortedList<IHDD, int>();
		}

        #endregion


        #region IHDDCollection Members

        public SortedList<IHDD, int> HDDs
		{
			get { return hdds; }
			set { hdds = value; }
		}

		public void AddHDD(IHDD hdd, int quantity)
		{
			try
			{
				//check if we have the hdd in the list already.
				if (hdds.ContainsKey(hdd))
				{
					//if we do, simply add the quantity
					hdds[hdd] += quantity;
				}
				else
				{
					//otherwise, add the new hdd and quantity.
					hdds.Add(hdd, quantity);
				}
			}
			catch (ArgumentException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void RemoveHDD(IHDD hdd, int quantity)
		{
			try
			{
				//check the hdd is actually in our list.
				if (hdds.ContainsKey(hdd))
				{
					//check that the quantity we are removing is less than the
					//quantity stored
					if (hdds[hdd] > quantity)
					{
						hdds[hdd] -= quantity;
					}
					else
					{
						//otherwise remove all the matching hdds
						hdds.Remove(hdd);
					}
				}
				else
				{
					Console.WriteLine("The hdd given is not part of this desktop");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void ClearHDDs()
		{
			this.hdds.Clear();
		}

		public ushort TotalHDDCapacity
		{
			get
			{
				ushort totalHddCapacity = 0;

				foreach (KeyValuePair<IHDD, int> hdd in hdds)
				{
					int quantity = hdd.Value;
					IHDD drive = hdd.Key;

					for (int i = 0; i < quantity; i++)
					{
						totalHddCapacity += drive.Capacity;
					}
				}

				return totalHddCapacity;
			}
		}

		#endregion
	}
}
