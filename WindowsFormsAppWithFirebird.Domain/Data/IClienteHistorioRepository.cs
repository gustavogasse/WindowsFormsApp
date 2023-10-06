using System.Collections.Generic;
using System;
using WindowsFormsAppWithFirebird.Domain.Entities;

namespace WindowsFormsAppWithFirebird.Domain.Data
{
    public interface IClienteHistoricoRepository : IDisposable
    {
        IEnumerable<ClienteHistorico> GetAll();
        ClienteHistorico Get(ClienteHistorico ClienteHistorico);
        ClienteHistorico GetById(Guid id);
        void Add(ClienteHistorico ClienteHistorico);
        void Update(ClienteHistorico ClienteHistorico); 
        int SaveChanges();
        IUnitOfWork UnitOfWork { get; }
    }
}
