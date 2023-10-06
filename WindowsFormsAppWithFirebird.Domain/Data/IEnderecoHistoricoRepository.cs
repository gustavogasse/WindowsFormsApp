using System.Collections.Generic;
using System;
using WindowsFormsAppWithFirebird.Domain.Entities;

namespace WindowsFormsAppWithFirebird.Domain.Data
{
    public interface IEnderecoHistoricoRepository : IDisposable
    {
        IEnumerable<EnderecoHistorico> GetAll();
        EnderecoHistorico Get(EnderecoHistorico EnderecoHistorico);
        EnderecoHistorico GetById(Guid id);
        void Add(EnderecoHistorico EnderecoHistorico);
        void Update(EnderecoHistorico EnderecoHistorico);
        int SaveChanges();
        IUnitOfWork UnitOfWork { get; }
    }
}
