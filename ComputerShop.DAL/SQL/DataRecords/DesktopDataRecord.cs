using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerShop.DAL.SQL.DataRecords
{
    public class DesktopDataRecord
    {
        public int DesktopID { get; set; }
        public int ManufacturerID { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public int CpuID { get; set; }       
    }
}
