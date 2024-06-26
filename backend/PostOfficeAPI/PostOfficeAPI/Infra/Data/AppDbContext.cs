﻿using Microsoft.EntityFrameworkCore;
using PostOfficeAPI.ApplicationCore.Models;

namespace PostOfficeAPI.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<Bag> Bags { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bag>(entity =>
            {
                entity.ToTable("Bags");
                entity.HasKey(b => b.Id);
                entity.HasIndex(b => b.Id).IsUnique();
                entity.Property(b => b.ShipmentId).IsRequired(false);
                entity.HasDiscriminator<string>("BagType")
                    .HasValue<BagWithParcels>("parcel")
                    .HasValue<BagWithLetters>("letter");
            });
            modelBuilder.Entity<Parcel>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.HasOne(a => a.BagWithParcels)
                    .WithMany(b => b.Parcels)
                    .HasForeignKey(p => p.BagId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.HasIndex(a => a.Id)
                    .IsUnique();
                entity.HasMany(b => b.Bags)
                    .WithOne(s => s.Shipment)
                    .HasForeignKey(p => p.ShipmentId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
