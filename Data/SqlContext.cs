using Dominio.Entidade;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class SqlContext : DbContext
    {
        public SqlContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public SqlContext(DbContextOptions<SqlContext> options) : base(options) { }
        public DbSet<transacao> transacao { get; set; }
    }
}
