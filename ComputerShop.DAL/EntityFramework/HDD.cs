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
        public int hdd_id { get; set; }

        public int manufacturer_id { get; set; }

        [Required]
        [StringLength(50)]
        public string model { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal price { get; set; }

        public short capacity { get; set; }

        public short speed { get; set; }

        [Required]
        [StringLength(10)]
        public string hdd_type { get; set; }

        [Required]
        [StringLength(10)]
        public string hdd_form_factor { get; set; }

        [Required]
        [StringLength(10)]
        public string hdd_interface { get; set; }

        public virtual ICollection<DesktopHDD> DesktopHDDs { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual ICollection<Laptop> Laptops { get; set; }
    }
}
