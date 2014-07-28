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

        public int manufacturer_id { get; set; }

        [Required]
        [StringLength(50)]
        public string model { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal price { get; set; }

        public decimal clock_speed { get; set; }

        [Required]
        [StringLength(10)]
        public string no_of_cores { get; set; }

        [Required]
        [StringLength(10)]
        public string cpu_form_factor { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual ICollection<Desktop> Desktops { get; set; }

        public virtual ICollection<Laptop> Laptops { get; set; }
    }
}
