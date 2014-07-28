namespace ComputerShop.DAL.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ComputerShopContext : DbContext
    {
        public ComputerShopContext()
            : base("name=ComputerShopContext")
        {
        }

        public virtual DbSet<CPU> CPUs { get; set; }
        public virtual DbSet<DesktopGPU> DesktopGPUs { get; set; }
        public virtual DbSet<DesktopHDD> DesktopHDDs { get; set; }
        public virtual DbSet<DesktopRamModule> DesktopRamModules { get; set; }
        public virtual DbSet<Desktop> Desktops { get; set; }
        public virtual DbSet<GPU> GPUs { get; set; }
        public virtual DbSet<HDD> HDDs { get; set; }
        public virtual DbSet<LaptopRamModule> LaptopRamModules { get; set; }
        public virtual DbSet<Laptop> Laptops { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Monitor> Monitors { get; set; }
        public virtual DbSet<RamModule> RamModules { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CPU>()
                .Property(e => e.price)
                .HasPrecision(10, 4);

            modelBuilder.Entity<CPU>()
                .Property(e => e.clock_speed)
                .HasPrecision(3, 2);

            modelBuilder.Entity<CPU>()
                .Property(e => e.no_of_cores)
                .IsFixedLength();

            modelBuilder.Entity<CPU>()
                .Property(e => e.cpu_form_factor)
                .IsFixedLength();

            modelBuilder.Entity<CPU>()
                .HasMany(e => e.Desktops)
                .WithRequired(e => e.CPU)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CPU>()
                .HasMany(e => e.Laptops)
                .WithRequired(e => e.CPU)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Desktop>()
                .Property(e => e.price)
                .HasPrecision(10, 4);

            modelBuilder.Entity<Desktop>()
                .HasMany(e => e.DesktopGPUs)
                .WithRequired(e => e.Desktop)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Desktop>()
                .HasMany(e => e.DesktopHDDs)
                .WithRequired(e => e.Desktop)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Desktop>()
                .HasMany(e => e.DesktopRamModules)
                .WithRequired(e => e.Desktop)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GPU>()
                .Property(e => e.price)
                .HasPrecision(10, 4);

            modelBuilder.Entity<GPU>()
                .Property(e => e.gpu_model)
                .IsFixedLength();

            modelBuilder.Entity<GPU>()
                .Property(e => e.vram_type)
                .IsFixedLength();

            modelBuilder.Entity<GPU>()
                .Property(e => e.gpu_type)
                .IsFixedLength();

            modelBuilder.Entity<GPU>()
                .HasMany(e => e.DesktopGPUs)
                .WithRequired(e => e.GPU)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GPU>()
                .HasMany(e => e.Laptops)
                .WithRequired(e => e.GPU)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HDD>()
                .Property(e => e.price)
                .HasPrecision(10, 4);

            modelBuilder.Entity<HDD>()
                .Property(e => e.hdd_type)
                .IsFixedLength();

            modelBuilder.Entity<HDD>()
                .Property(e => e.hdd_form_factor)
                .IsFixedLength();

            modelBuilder.Entity<HDD>()
                .Property(e => e.hdd_interface)
                .IsFixedLength();

            modelBuilder.Entity<HDD>()
                .HasMany(e => e.DesktopHDDs)
                .WithRequired(e => e.HDD)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HDD>()
                .HasMany(e => e.Laptops)
                .WithRequired(e => e.HDD)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Laptop>()
                .Property(e => e.price)
                .HasPrecision(10, 4);

            modelBuilder.Entity<Laptop>()
                .Property(e => e.display_size)
                .HasPrecision(3, 1);

            modelBuilder.Entity<Manufacturer>()
                .HasMany(e => e.CPUs)
                .WithRequired(e => e.Manufacturer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manufacturer>()
                .HasMany(e => e.GPUs)
                .WithRequired(e => e.Manufacturer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manufacturer>()
                .HasMany(e => e.HDDs)
                .WithRequired(e => e.Manufacturer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manufacturer>()
                .HasMany(e => e.Laptops)
                .WithRequired(e => e.Manufacturer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manufacturer>()
                .HasMany(e => e.Monitors)
                .WithRequired(e => e.Manufacturer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manufacturer>()
                .HasMany(e => e.RamModules)
                .WithRequired(e => e.Manufacturer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Monitor>()
                .Property(e => e.price)
                .HasPrecision(10, 4);

            modelBuilder.Entity<RamModule>()
                .Property(e => e.price)
                .HasPrecision(10, 4);

            modelBuilder.Entity<RamModule>()
                .Property(e => e.ddr_version)
                .IsFixedLength();

            modelBuilder.Entity<RamModule>()
                .Property(e => e.ram_form_factor)
                .IsFixedLength();

            modelBuilder.Entity<RamModule>()
                .HasMany(e => e.DesktopRamModules)
                .WithRequired(e => e.RamModule)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RamModule>()
                .HasMany(e => e.LaptopRamModules)
                .WithRequired(e => e.RamModule)
                .WillCascadeOnDelete(false);
        }
    }
}
