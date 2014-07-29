namespace ComputerShop.DAL.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Laptop
    {
        public Laptop()
        {
            LaptopRamModules = new HashSet<LaptopRamModule>();
        }

        [Key]
        [Column("laptop_id")]
        public int LaptopId { get; set; }

        [Column("manufacturer_id")]
        public int ManufacturerId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("model")]
        public string Model { get; set; }

        [Column("price", TypeName = "smallmoney")]
        public decimal Price { get; set; }

        [Column("weight")]
        public byte Weight { get; set; }

        [Column("battery_life")]
        public byte BatteryLife { get; set; }

        [Column("display_size")]
        public decimal DisplaySize { get; set; }

        [Column("cpu_id")]
        public int CpuId { get; set; }

        [Column("hdd_id")]
        public int HddId { get; set; }

        [Column("gpu_id")]
        public int GpuId { get; set; }

        public virtual CPU CPU { get; set; }

        public virtual GPU GPU { get; set; }

        public virtual HDD HDD { get; set; }

        public virtual ICollection<LaptopRamModule> LaptopRamModules { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
    }
}
