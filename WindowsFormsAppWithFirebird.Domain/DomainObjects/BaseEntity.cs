using System;

namespace WindowsFormsAppWithFirebird.Domain.DomainObjects
{
    public abstract class BaseEntity : IAggregateRoot
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            DataCadastro = DateTime.Now;
        }

        public Guid Id { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
