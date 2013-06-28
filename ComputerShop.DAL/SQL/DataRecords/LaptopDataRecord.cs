using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.DAL.SQL.DataRecords
{
    public class LaptopDataRecord
    {
        public int LaptopID { get; set; }
        public int ManufacturerID { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public byte Weight { get; set; }
        public byte BatteryLife { get; set; }
        public float DisplaySize { get; set; }
        public int CpuID { get; set; }
        public int HddID { get; set; }
        public int GpuID { get; set; }
    }
}
