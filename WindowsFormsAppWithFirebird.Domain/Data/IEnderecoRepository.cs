using System.Collections.Generic;
using System;
using WindowsFormsAppWithFirebird.Domain.Entities;

namespace WindowsFormsAppWithFirebird.Domain.Data
{
    public interface IEnderecoRepository : IDisposable
    {
        IEnumerable<Endereco> GetAll();
        Endereco Get(Endereco Endereco);
        Endereco GetById(Guid id);
        void Add(Endereco Endereco);
        void Update(Endereco Endereco);
        int SaveChanges();
        IUnitOfWork UnitOfWork { get; }
    }
}
