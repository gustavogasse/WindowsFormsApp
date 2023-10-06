using System.Data.Entity;
using WindowsFormsAppWithFirebird.Domain.Data;
using WindowsFormsAppWithFirebird.Domain.Entities;

namespace WindowsFormsAppWithFirebird.Infra
{
    public class DataContext : DbContext, IUnitOfWork
    {
        public DataContext() : base("CRUD-Clientes")
        {            
        }

        public DataContext(string dbName) : base(dbName)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ClienteHistorico> ClientesHistorico { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<EnderecoHistorico> EnderecosHistorico { get; set; }

        public bool Commit()
        {
            return base.SaveChanges() > 0;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
