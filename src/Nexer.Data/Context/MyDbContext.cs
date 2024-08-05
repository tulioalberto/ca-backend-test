using Microsoft.EntityFrameworkCore;
using Nexer.Business.Models;

namespace Nexer.Data.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<BillingLine> BillingLines { get; set; }

        // vai pegar todos DbSet aqui para todos os mapeamentos para criar a colecao da memoria
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //definir o padrao de varchar 100 quando nao colocar...
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(180)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyDbContext).Assembly);

            //evitar delacao em cascata no sql
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
    }
}
