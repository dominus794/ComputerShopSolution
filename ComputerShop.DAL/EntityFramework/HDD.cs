namespace ComputerShop.DAL.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HDD
    {
        public HDD()
        {
            DesktopHDDs = new HashSet<DesktopHDD>();
            Laptops = new HashSet<Laptop>();
        }

        [Key]
        [Column("hdd_id")]
        public int HddId { get; set; }

        [Column("manufacturer_id")]
        public int ManufacturerId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("model")]
        public string Model { get; set; }

        [Column("price", TypeName = "smallmoney")]
        public decimal Price { get; set; }

        [Column("capacity")]
        public short Capacity { get; set; }

        [Column("speed")]
        public short Speed { get; set; }

        [Required]
        [StringLength(10)]
        [Column("hdd_type")]
        public string HddType { get; set; }

        [Required]
        [StringLength(10)]
        [Column("hdd_form_factor")]
        public string HddFormFactor { get; set; }

        [Required]
        [StringLength(10)]
        [Column("hdd_interface")]
        public string HddInterface { get; set; }

        public virtual ICollection<DesktopHDD> DesktopHDDs { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual ICollection<Laptop> Laptops { get; set; }
    }
}
