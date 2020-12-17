using Microsoft.EntityFrameworkCore;
using WebApiBackEnd.Models;

namespace WebApiBackEnd.Data {
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)        
        {

        }
        public DbSet<Produto> Produtos { get; set; }
    }
}