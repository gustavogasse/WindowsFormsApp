using System;
using System.Collections.Generic;
using WindowsFormsAppWithFirebird.Domain.DomainObjects;

namespace WindowsFormsAppWithFirebird.Domain.Data
{
    public interface IBaseRepository<T> : IDisposable where T : IAggregateRoot
    {
        IEnumerable<T> GetAll();
        T Get(T entity);        
        T GetById(Guid id);
        void Add(T entity);
        void Update(T entity);
        int SaveChanges();
        IUnitOfWork UnitOfWork { get; }
    }
}
