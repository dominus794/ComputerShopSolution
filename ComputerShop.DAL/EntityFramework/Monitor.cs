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
        [Column("monitor_id")]
        public int MonitorId { get; set; }

        [Column("manufacturer_id")]
        public int ManufacturerId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("model")]
        public string Model { get; set; }

        [Column("price", TypeName = "smallmoney")]
        public decimal Price { get; set; }

        [Column("size")]
        public byte Size { get; set; }

        public virtual ICollection<Desktop> Desktops { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
    }
}
