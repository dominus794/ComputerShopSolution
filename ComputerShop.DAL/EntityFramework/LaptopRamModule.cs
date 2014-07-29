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
        [Column("laptop_id", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LaptopId { get; set; }

        [Key]
        [Column("ram_id", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RamId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        public virtual Laptop Laptop { get; set; }

        public virtual RamModule RamModule { get; set; }
    }
}
