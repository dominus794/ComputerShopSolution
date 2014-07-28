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
        public int desktop_id { get; set; }

        public int? manufacturer_id { get; set; }

        [Required]
        [StringLength(50)]
        public string model { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal price { get; set; }

        public int cpu_id { get; set; }

        public int? monitor_id { get; set; }

        public virtual CPU CPU { get; set; }

        public virtual ICollection<DesktopGPU> DesktopGPUs { get; set; }

        public virtual ICollection<DesktopHDD> DesktopHDDs { get; set; }

        public virtual ICollection<DesktopRamModule> DesktopRamModules { get; set; }

        public virtual Monitor Monitor { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
    }
}
