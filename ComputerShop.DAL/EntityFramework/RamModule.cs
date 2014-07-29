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
        [Column("ram_id")]
        public int RamId { get; set; }

        [Column("manufacturer_id")]
        public int ManufacturerId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("model")]
        public string Model { get; set; }

        [Column("price", TypeName = "smallmoney")]
        public decimal Price { get; set; }

        [Column("bus_speed")]
        public short BusSpeed { get; set; }

        [Column("size")]
        public short Size { get; set; }

        [Required]
        [StringLength(4)]
        [Column("ddr_version")]
        public string DDRVersion { get; set; }

        [Required]
        [StringLength(10)]
        [Column("ram_form_factor")]
        public string RamFormFactor { get; set; }

        public virtual ICollection<DesktopRamModule> DesktopRamModules { get; set; }

        public virtual ICollection<LaptopRamModule> LaptopRamModules { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
    }
}
