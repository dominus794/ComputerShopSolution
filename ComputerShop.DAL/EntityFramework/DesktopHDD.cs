namespace ComputerShop.DAL.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DesktopHDD
    {
        [Key]
        [Column("desktop_id", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DesktopId { get; set; }

        [Key]
        [Column("hdd_id", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HddId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        public virtual Desktop Desktop { get; set; }

        public virtual HDD HDD { get; set; }
    }
}
