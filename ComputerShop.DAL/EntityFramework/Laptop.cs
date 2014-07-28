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
        public int laptop_id { get; set; }

        public int manufacturer_id { get; set; }

        [Required]
        [StringLength(50)]
        public string model { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal price { get; set; }

        public byte weight { get; set; }

        public byte battery_life { get; set; }

        public decimal display_size { get; set; }

        public int cpu_id { get; set; }

        public int hdd_id { get; set; }

        public int gpu_id { get; set; }

        public virtual CPU CPU { get; set; }

        public virtual GPU GPU { get; set; }

        public virtual HDD HDD { get; set; }

        public virtual ICollection<LaptopRamModule> LaptopRamModules { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
    }
}
