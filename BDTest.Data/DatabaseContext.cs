using System;
using Microsoft.EntityFrameworkCore;

using BDTest.Core.Models;
using Microsoft.Extensions.Logging;

namespace BDTest.Data
{
    public class DatabaseContext : DbContext
    {
        private ILoggerFactory _loggerFactory;
        public DbSet<Batch> Batches { get; set; }
        public DbSet<BatchNumber> BatchNumbers { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options, ILoggerFactory loggerFactory) 
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)  
        {
            if (_loggerFactory != null)
            { 
                optionsBuilder.UseLoggerFactory(_loggerFactory); 
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BatchNumber>()
                .HasOne(x => x.Batch)
                .WithMany(x => x.Numbers)
                .IsRequired()
                .HasForeignKey(x => x.BatchId);
        }
    }
}