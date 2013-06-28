using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.DAL.SQL.DataRecords
{
	public class CPUDataRecord
	{
		public int CpuID { get; set; }
		public int ManufacturerID { get; set; }
		public string Model { get; set; }
		public decimal Price { get; set; }
		public float ClockSpeed { get; set; }
		public string NoOfCores { get; set; }
		public string CpuFormFactor { get; set; }
	}
}
