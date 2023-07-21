
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IconBetAuth.Domain.Models;

namespace IconBetAuth.Domino.Domain
{
    public partial class IconBetAuthContext : DbContext
    {
        private string DB { get; set; }
        public IconBetAuthContext(string stringConnDB)
        {
            DB = stringConnDB;
        }

        public IconBetAuthContext(DbContextOptions<IconBetAuthContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Hall> Hall { get; set; }
        public virtual DbSet<User> User { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DB);

            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");
                entity.HasKey(e => e.TransactionId);
                entity.Property(e => e.UserName).HasColumnName("UserName");
                entity.Property(e => e.TransactionsType).HasColumnName("TransactionsType");
                entity.Property(e => e.Amount).HasColumnName("Amount");
                entity.Property(e => e.TicketUUID).HasColumnName("TicketUUID");
                entity.Property(e => e.UUID).HasColumnName("UUID");
                entity.Property(e => e.CreationDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserName).HasColumnName("UserName");
                entity.Property(e => e.Password).HasColumnName("Password");
                entity.Property(e => e.Active).HasColumnName("Active");
                entity.Property(e => e.Rol).HasColumnName("Rol");
                entity.Property(e => e.Balance).HasColumnName("Balance");
                entity.Property(e => e.Currency).HasColumnName("Currency");
                entity.Property(e => e.Country).HasColumnName("Country");
                entity.Property(e => e.CreationDate).HasColumnType("datetime");
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
