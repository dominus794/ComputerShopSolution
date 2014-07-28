namespace ComputerShop.DAL.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LaptopRamModule
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int laptop_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ram_id { get; set; }

        public int quantity { get; set; }

        public virtual Laptop Laptop { get; set; }

        public virtual RamModule RamModule { get; set; }
    }
}
