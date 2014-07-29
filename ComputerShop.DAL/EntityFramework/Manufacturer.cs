namespace ComputerShop.DAL.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Manufacturer
    {
        public Manufacturer()
        {
            CPUs = new HashSet<CPU>();
            Desktops = new HashSet<Desktop>();
            GPUs = new HashSet<GPU>();
            HDDs = new HashSet<HDD>();
            Laptops = new HashSet<Laptop>();
            Monitors = new HashSet<Monitor>();
            RamModules = new HashSet<RamModule>();
        }

        [Key]
        [Column("manufacturer_id")]
        public int ManufacturerId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("name")]
        public string Name { get; set; }

        public virtual ICollection<CPU> CPUs { get; set; }

        public virtual ICollection<Desktop> Desktops { get; set; }

        public virtual ICollection<GPU> GPUs { get; set; }

        public virtual ICollection<HDD> HDDs { get; set; }

        public virtual ICollection<Laptop> Laptops { get; set; }

        public virtual ICollection<Monitor> Monitors { get; set; }

        public virtual ICollection<RamModule> RamModules { get; set; }
    }
}
