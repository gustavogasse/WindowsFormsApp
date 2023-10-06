using System;
using WindowsFormsAppWithFirebird.Domain.Entities;
using WindowsFormsAppWithFirebird.Domain.Services.Interfaces;

namespace WindowsFormsAppWithFirebird.Domain.Services.Interfaces
{
    public interface IEnderecoService : IBaseService<Endereco>
    {
        Endereco GetByClienteId(Guid clienteId);
    }
}
