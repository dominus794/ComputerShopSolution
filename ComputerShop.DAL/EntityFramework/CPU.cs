namespace ComputerShop.DAL.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CPU
    {
        public CPU()
        {
            Desktops = new HashSet<Desktop>();
            Laptops = new HashSet<Laptop>();
        }

        [Key]
        public int cpu_id { get; set; }

        [Column("manufacturer_id")]
        public int ManufacturerId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("model")]
        public string Model { get; set; }

        [Column("Price", TypeName = "smallmoney")]
        public decimal Price { get; set; }

        [Column("clock_speed")]
        public decimal ClockSpeed { get; set; }

        [Required]
        [StringLength(10)]
        [Column("no_of_cores")]
        public string NoOfCores { get; set; }

        [Required]
        [StringLength(10)]
        [Column("cpu_form_factor")]
        public string CpuFormFactor { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual ICollection<Desktop> Desktops { get; set; }

        public virtual ICollection<Laptop> Laptops { get; set; }
    }
}
