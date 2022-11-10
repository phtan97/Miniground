using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MiniGround.API.ContextModels.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGround.API.ContextModels
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<TableUser> TableUsers { get; set; }
        public virtual DbSet<TableUserBank> TableUserBanks { get; set; }
        public virtual DbSet<TableSaleAgent> TableSaleAgents { get; set; }
        public virtual DbSet<TableFootBallField> TableFootBallFields { get; set; }
        public virtual DbSet<TableFieldPrice> TableFieldPrice { get; set; }
        public virtual DbSet<TableMatchInfo> TableMatchInfos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            if (!optionsbuilder.IsConfigured)
            {
                var absolutePath = AppContext.BaseDirectory;
                IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(absolutePath).AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true).Build();
                optionsbuilder.UseSqlServer(configuration.GetConnectionString("MiniGround"));
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TableUser>();
            builder.Entity<TableMatchInfo>();
            builder.Entity<TableFootBallField>();
            builder.Entity<TableFieldPrice>();
            builder.Entity<TableUserBank>();
            builder.Entity<TableSaleAgent>();
        }
    }
}
