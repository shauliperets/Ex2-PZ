using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=database.db");
        }
        public DbSet<ProductModel> Products {get; set;}
        public DbSet<LogModel> Logs { get; set; }
    }
}