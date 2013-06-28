using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.Domain
{
	using Interfaces;

	public class Desktop : Computer, IDesktop
    {
        #region Fields

        protected IGPUCollection gpuCollection;
		protected IHDDCollection hddCollection;

        #endregion


        #region Constructors

        public Desktop()
		{
			this.model = "DefaultDesktopModel";
			this.gpuCollection = new GPUCollection();
			this.hddCollection = new HDDCollection();
		}

		public Desktop(int id, IManufacturer manufacturer, string model, decimal price, ICPU cpu)
			:base(id, manufacturer, model, price, cpu)
		{
			this.gpuCollection = new GPUCollection();
			this.hddCollection = new HDDCollection();
		}		

		public Desktop(int id, IManufacturer manufacturer, string model, decimal price, ICPU cpu,
					   IRamModuleCollection ramModuleCollection,
					   IHDDCollection hddCollection,
					   IGPUCollection gpuCollection)
			:base(id, manufacturer, model, price, cpu, ramModuleCollection)
		{
			this.gpuCollection = gpuCollection;
			this.hddCollection = hddCollection;
		}

        #endregion      		


		#region IDesktop Members

		public IGPUCollection GpuCollection
		{
			get { return gpuCollection; }
			set { gpuCollection = value; }
		}

		public void AddGPU(IGPU gpu, int quantity)
		{
			gpuCollection.AddGPU(gpu, quantity);
		}

		public void RemoveGPU(IGPU gpu, int quantity)
		{
			gpuCollection.RemoveGPU(gpu, quantity);
		}

		public void ClearGPUs()
		{
			gpuCollection.ClearGPUs();
		}

		public IHDDCollection HddCollection
		{
			get { return hddCollection; }
			set { hddCollection = value; }
		}

		public void AddHDD(IHDD hdd, int quantity)
		{
			hddCollection.AddHDD(hdd, quantity);
		}

		public void RemoveHDD(IHDD hdd, int quantity)
		{
			hddCollection.RemoveHDD(hdd, quantity);
		}

		public void ClearHDDs()
		{
			hddCollection.ClearHDDs();
		}

		public ushort TotalHDDCapacity
		{
			get { return hddCollection.TotalHDDCapacity; }
		}

		#endregion


        #region Overrides
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            //add base output
            builder.Append(base.ToString());

            //output hdds
            foreach (KeyValuePair<IHDD, int> hdd in hddCollection.HDDs)
            {
                int quantity = hdd.Value;
                IHDD drive = hdd.Key;

                builder.AppendLine(string.Format("{0} x{1}", drive.ToString(), quantity));
            }

            //output gpus
            foreach (KeyValuePair<IGPU, int> gpu in gpuCollection.GPUs)
            {
                int quantity = gpu.Value;
                IGPU card = gpu.Key;

                builder.AppendLine(string.Format("{0} x{1}", card.ToString(), quantity));
            }

            return builder.ToString();
        }
        #endregion
	}
}
