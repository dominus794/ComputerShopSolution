using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.DAL.SQL.DataRecords
{
    public class RamModuleDataRecord
    {
        public int RamModuleID { get; set; }
        public int ManufacturerID { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public ushort ClockSpeed { get; set; }
        public ushort Size { get; set; }
        public string DDRVersion { get; set; }
        public string RamFormFactor { get; set; }
    }
}
