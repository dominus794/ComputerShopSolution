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
        public int gpu_id { get; set; }

        public int manufacturer_id { get; set; }

        [Required]
        [StringLength(50)]
        public string model { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal price { get; set; }

        [Required]
        [StringLength(10)]
        public string gpu_model { get; set; }

        public short gpu_clock_speed { get; set; }

        public short vram_size { get; set; }

        public short vram_clock_speed { get; set; }

        [Required]
        [StringLength(10)]
        public string vram_type { get; set; }

        [Required]
        [StringLength(10)]
        public string gpu_type { get; set; }

        public virtual ICollection<DesktopGPU> DesktopGPUs { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual ICollection<Laptop> Laptops { get; set; }
    }
}
