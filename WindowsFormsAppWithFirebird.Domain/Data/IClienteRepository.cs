using System.Collections.Generic;
using System;
using WindowsFormsAppWithFirebird.Domain.Entities;

namespace WindowsFormsAppWithFirebird.Domain.Data
{
    public interface IClienteRepository : IDisposable
    {
        IEnumerable<Cliente> GetAll();
        Cliente Get(Cliente cliente);
        Cliente GetById(Guid id);
        void Add(Cliente cliente);
        void Update(Cliente cliente);
        int SaveChanges();
        IUnitOfWork UnitOfWork { get; }
    }
}
