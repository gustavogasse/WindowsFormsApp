using WindowsFormsAppWithFirebird.Domain.Data;
using WindowsFormsAppWithFirebird.Domain.Entities;

namespace WindowsFormsAppWithFirebird.Infra.Repositories
{
    public class EnderecoHistoricoRepository : BaseRepository<EnderecoHistorico>, IEnderecoHistoricoRepository
    {
        public EnderecoHistoricoRepository(DataContext context) : base(context)
        {
        }
    }
}
