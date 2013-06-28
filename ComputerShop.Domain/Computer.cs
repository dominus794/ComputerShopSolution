using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Domain
{
	using Interfaces;

	public abstract class Computer : Product, IComputer
	{
		#region Fields

		protected ICPU cpu;
		protected IRamModuleCollection ramModuleCollection;
		
		#endregion


		#region Constructors

		public Computer()
		{
			this.cpu = new CPU();			
			this.ramModuleCollection = new RamModuleCollection();
		}

		public Computer(int id, IManufacturer manufacturer, string model, decimal price, ICPU cpu)
			:base(id, manufacturer, model, price)
		{
			this.cpu = cpu;			
			this.ramModuleCollection = new RamModuleCollection();
		}		

		public Computer(int id, IManufacturer manufacturer, string model, decimal price,
						ICPU cpu, IRamModuleCollection ramModuleCollection)
			:base(id, manufacturer, model, price)
		{
			this.cpu = cpu;
			this.ramModuleCollection = ramModuleCollection;
		}

		#endregion


		#region IComputer Members

		public ICPU Cpu
		{
			get { return cpu; }
			set { cpu = value; }
		}		

		public IRamModuleCollection RamModuleCollection
		{
			get { return ramModuleCollection; }
			set { ramModuleCollection = value; }
		}

		public ushort TotalRam
		{
			get
			{
				ushort totalRam = 0;
			
				foreach (KeyValuePair<IRamModule, int> ram in ramModuleCollection.RamModules)
				{
					int quantity = ram.Value;
					IRamModule module = ram.Key;

					for (int i = 0; i < quantity; i++)
					{
						totalRam += module.Size;
					}
				}
				return totalRam;
			}
		}

		public void AddRamModule(IRamModule module, int quantity)
		{
			this.ramModuleCollection.AddRamModule(module, quantity);
		}

		public void RemoveRamModule(IRamModule module, int quantity)
		{
			this.ramModuleCollection.RemoveRamModule(module, quantity);
		}

		public void ClearRamModules()
		{
			this.ramModuleCollection.ClearRamModules();
		}

		#endregion


		#region Overrides

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendLine(string.Format("{0} {1} {2:c}", manufacturer.Name, model, price));
			builder.AppendLine(cpu.ToString());

			foreach (KeyValuePair<IRamModule, int> ram in ramModuleCollection.RamModules)
			{
				int quantity = ram.Value;
				IRamModule module = ram.Key;
				builder.AppendLine(string.Format("{0} x{1}", module.ToString(), quantity));
			}

			return builder.ToString();
		}

		#endregion
	}
}
