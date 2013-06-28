using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.DAL.SQL.DataRecords
{
    public class HDDDataRecord
    {
        public int HddID { get; set; }
        public int ManufacturerID { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public ushort Capacity { get; set; }
        public ushort Speed { get; set; }
        public string HDDType { get; set; }
        public string HDDFormFactor { get; set; }
        public string HDDInterface { get; set; }
    }
}
