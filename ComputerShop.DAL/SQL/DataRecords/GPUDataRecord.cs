using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.DAL.SQL.DataRecords
{
    public class GPUDataRecord
    {
        public int GpuID { get; set; }
        public int ManufacturerID { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public string GpuModel { get; set; }
        public ushort GpuClockSpeed { get; set; }
        public ushort VRamSize { get; set; }
        public ushort VRamClockSpeed { get; set; }
        public string VRamType { get; set; }
        public string GpuType { get; set; }
    }
}
