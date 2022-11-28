using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using Trading.Data.Entities;

namespace Trading.Data.Database
{
    public class TradingContext : DbContext
    {
        public TradingContext(DbContextOptions<TradingContext> options) : base(options)
        {

        }
        public DbSet<ItemMaster> ItemMaster { get; set; }
        public DbSet<UserMaster> UserMaster { get; set; }

        public DbSet<CompanyMaster> CompanyMaster { get; set; }

        public DbSet<UnitMaster> UnitMaster { get; set; }

        public DbSet<ItemInventory> ItemInventory { get; set; }

        public DbSet<UnitConversion> UnitConversion { get; set; }

        public DbSet<ItemBoMMaster> ItemBoMMaster { get; set; }

        public DbSet<ItemBoMChild> ItemBoMChild { get; set; }


        private IDbContextTransaction _transaction;

        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        public void Commit()
        {

            try
            {
                SaveChanges();
                _transaction.Commit();
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }
    }
}
