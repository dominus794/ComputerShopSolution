namespace ComputerShop.DAL.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Desktop
    {
        public Desktop()
        {
            DesktopGPUs = new HashSet<DesktopGPU>();
            DesktopHDDs = new HashSet<DesktopHDD>();
            DesktopRamModules = new HashSet<DesktopRamModule>();
        }

        [Key]
        [Column("desktop_id")]
        public int DesktopId { get; set; }

        [Column("manufacturer_id")]
        public int? ManufacturerId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("model")]
        public string Model { get; set; }

        [Column("Price", TypeName = "smallmoney")]
        public decimal Price { get; set; }

        [Column("cpu_id")]
        public int CpuId { get; set; }

        [Column("monitor_id")]
        public int? MonitorId { get; set; }

        public virtual CPU CPU { get; set; }

        public virtual ICollection<DesktopGPU> DesktopGPUs { get; set; }

        public virtual ICollection<DesktopHDD> DesktopHDDs { get; set; }

        public virtual ICollection<DesktopRamModule> DesktopRamModules { get; set; }

        public virtual Monitor Monitor { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
    }
}
