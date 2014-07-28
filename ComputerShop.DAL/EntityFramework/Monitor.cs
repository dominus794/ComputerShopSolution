namespace ComputerShop.DAL.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Monitor
    {
        public Monitor()
        {
            Desktops = new HashSet<Desktop>();
        }

        [Key]
        public int monitor_id { get; set; }

        public int manufacturer_id { get; set; }

        [Required]
        [StringLength(50)]
        public string model { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal price { get; set; }

        public byte size { get; set; }

        public virtual ICollection<Desktop> Desktops { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
    }
}
