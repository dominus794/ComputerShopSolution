namespace ComputerShop.DAL.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RamModule
    {
        public RamModule()
        {
            DesktopRamModules = new HashSet<DesktopRamModule>();
            LaptopRamModules = new HashSet<LaptopRamModule>();
        }

        [Key]
        public int ram_id { get; set; }

        public int manufacturer_id { get; set; }

        [Required]
        [StringLength(50)]
        public string model { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal price { get; set; }

        public short bus_speed { get; set; }

        public short size { get; set; }

        [Required]
        [StringLength(4)]
        public string ddr_version { get; set; }

        [Required]
        [StringLength(10)]
        public string ram_form_factor { get; set; }

        public virtual ICollection<DesktopRamModule> DesktopRamModules { get; set; }

        public virtual ICollection<LaptopRamModule> LaptopRamModules { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
    }
}
