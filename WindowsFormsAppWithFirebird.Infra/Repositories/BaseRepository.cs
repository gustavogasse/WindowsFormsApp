using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using WindowsFormsAppWithFirebird.Domain.Data;
using WindowsFormsAppWithFirebird.Domain.DomainObjects;

namespace WindowsFormsAppWithFirebird.Infra.Repositories
{
    public abstract class BaseRepository<T> where T : BaseEntity
    {
        private readonly DataContext _context;
        private readonly DbSet<T> DbSet;
        public IUnitOfWork UnitOfWork => _context;

        public BaseRepository(DataContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.AsNoTracking().ToList();
        }

        public T GetById(Guid id)
        {
            return DbSet.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public T Get(T entity)
        {
            return DbSet.AsNoTracking().Where(p => p.Equals(entity)).FirstOrDefault();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
            SaveChanges();
        }

        public void Update(T entity)
        {
            DbSet.AddOrUpdate(entity);
            SaveChanges();
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
            SaveChanges();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
