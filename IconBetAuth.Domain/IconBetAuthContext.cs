
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
                entity.Property(e => e.Company).HasColumnType("Company");
                entity.Property(e => e.UserName).HasColumnType("UserName");
                entity.Property(e => e.TransactionsType).HasColumnType("TransactionsType");
                entity.Property(e => e.Amount).HasColumnType("Amount");
                entity.Property(e => e.TicketUUID).HasColumnType("TicketUUID");
                entity.Property(e => e.ClientTransactionId).HasColumnType("ClientTransactionId");
                entity.Property(e => e.CreationDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.HasKey(e => e.CustomerId);
                entity.Property(e => e.APIUrl)
                    .HasMaxLength(1000)
                    .HasColumnName("APIUrl");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.UrlError);
                entity.Property(e => e.Password);
            });
            modelBuilder.Entity<Hall>(entity =>
                {
                    entity.ToTable("Hall");

                    entity.HasKey(e => e.HallId);
                    entity.Property(e => e.Currency)
                        .HasMaxLength(5)
                        .HasColumnName("Currency");

                    entity.Property(e => e.ParentHash)
                        .HasColumnName("ParentHash");

                    entity.Property(e => e.Hash)
                        .HasColumnName("Hash");

                });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
