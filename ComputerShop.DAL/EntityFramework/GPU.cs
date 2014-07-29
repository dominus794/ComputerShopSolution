namespace ComputerShop.DAL.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GPU
    {
        public GPU()
        {
            DesktopGPUs = new HashSet<DesktopGPU>();
            Laptops = new HashSet<Laptop>();
        }

        [Key]
        [Column("gpu_id")]
        public int GpuId { get; set; }

        [Column("manufacturer_id")]
        public int ManufacturerId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Model")]
        public string model { get; set; }

        [Column("price", TypeName = "smallmoney")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(10)]
        [Column("gpu_model")]
        public string GpuModel { get; set; }

        [Column("gpu_clock_speed")]
        public short GpuClockSpeed { get; set; }

        [Column("vram_size")]
        public short VramSize { get; set; }

        [Column("vram_clock_speed")]
        public short VramClockSpeed { get; set; }

        [Required]
        [StringLength(10)]
        [Column("vram_type")]
        public string VramType { get; set; }

        [Required]
        [StringLength(10)]
        [Column("gpu_type")]
        public string GpuType { get; set; }

        public virtual ICollection<DesktopGPU> DesktopGPUs { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual ICollection<Laptop> Laptops { get; set; }
    }
}
