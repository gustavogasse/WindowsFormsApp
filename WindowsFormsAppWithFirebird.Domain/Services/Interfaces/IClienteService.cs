using System;
using System.Collections.Generic;
using WindowsFormsAppWithFirebird.Domain.Entities;

namespace WindowsFormsAppWithFirebird.Domain.Services.Interfaces
{
    public interface IClienteService : IBaseService<Cliente>
    {
        void Add(Cliente cliente, Endereco endereco);
        void Update(Cliente cliente, Endereco endereco);
        Cliente GetByDocumento(string documento);
        IEnumerable<ClienteHistorico> GetHistoricoDeAlteracoes(Guid clienteId);
        IEnumerable<ClienteRelatorioAlteracao> GetAllHistoricoDeAlteracoes();
    }
}
