using AutoSkola.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSkola.Data
{
    public class DataContext : IdentityDbContext<User, AppRole, int>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Automobil> automobili { get; set; }
        public DbSet<Čas>casovi { get; set; }
        public DbSet<Kategorija> kategorije { get; set; }
        public DbSet<Kvar> kvar { get; set; }
        public DbSet<Raspored>raspored { get; set; }
        public DbSet<UserKategorija> userkategorija { get; set; }
        public DbSet<UserRaspored>userraspored { get; set; }
        public DbSet<PolaznikInstuktor> polaznikinstuktor { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserKategorija>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<UserKategorija>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.userkategorija)
                .HasForeignKey(bc => bc.UserId)
                .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserKategorija>()
                .HasOne(bc => bc.Kategorija)
                .WithMany(c => c.userkategorija)
                .HasForeignKey(bc => bc.KategorijaId)
                .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Raspored>()
                .HasOne(r => r.Instruktor)
                .WithMany()
                .HasForeignKey(r => r.InstruktorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Raspored>()
                .HasOne(r => r.Polaznik)
                .WithMany()
                .HasForeignKey(r => r.PolaznikId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PolaznikInstuktor>()
               .HasOne(r => r.Instruktor)
               .WithMany()
               .HasForeignKey(r => r.InstruktorId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PolaznikInstuktor>()
                .HasOne(r => r.Polaznik)
                .WithMany()
                .HasForeignKey(r => r.PolaznikId)
                .OnDelete(DeleteBehavior.NoAction);




            modelBuilder.Entity<UserRaspored>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<UserRaspored>()
                .HasOne(ur => ur.User)
                .WithMany()
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();


            //modelBuilder.Entity<UserRaspored>()
            //    .HasOne(ur => ur.Raspored)
            //    .WithMany(r => r.UserRaspored)
            //    .HasForeignKey(ur => ur.RasporedId)
            //    .IsRequired();


          




        }
    }
}
