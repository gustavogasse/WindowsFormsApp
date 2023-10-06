using WindowsFormsAppWithFirebird.Domain.Data;
using WindowsFormsAppWithFirebird.Domain.Entities;

namespace WindowsFormsAppWithFirebird.Infra.Repositories
{
    public class ClienteHistoricoRepository : BaseRepository<ClienteHistorico>, IClienteHistoricoRepository
    {
        public ClienteHistoricoRepository(DataContext context) : base(context)
        {
        }
    }
}
