using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WindowsFormsAppWithFirebird.Domain.Data;
using WindowsFormsAppWithFirebird.Domain.Entities;
using WindowsFormsAppWithFirebird.Domain.Services.Interfaces;

namespace WindowsFormsAppWithFirebird.Domain.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _repository;
        private readonly IEnderecoHistoricoRepository _enderecoHistoricoRepository;

        public EnderecoService(IEnderecoRepository repository, IEnderecoHistoricoRepository enderecoHistoricoRepository) 
        { 
            _repository = repository;
            _enderecoHistoricoRepository = enderecoHistoricoRepository;
        }
        public void Add(Endereco entity)
        {
            if (entity.ClienteId == null || entity.ClienteId == Guid.Empty)
                throw new ArgumentNullException("Endereço não foi registrado para um cliente.");

            _repository.Add(entity);            
            _enderecoHistoricoRepository.Add(EnderecoToHistorico(entity));
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public Endereco Get(Endereco entity)
        {
            return _repository.Get(entity);
        }

        public IEnumerable<Endereco> GetAll()
        {
            return _repository.GetAll();
        }

        public Endereco GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public Endereco GetByClienteId(Guid clienteId)
        {
            return _repository.GetAll()
                .Where(e => e.ClienteId == clienteId)
                .FirstOrDefault();
        }

        public void Update(Endereco entity)
        {
            _repository.Update(entity);            
            _enderecoHistoricoRepository.Add(EnderecoToHistorico(entity));
        }

        private EnderecoHistorico EnderecoToHistorico(Endereco endereco)
        {
            return new EnderecoHistorico(
                endereco.CEP,
                endereco.Logradouro,
                endereco.Complemento,
                endereco.Numero,
                endereco.Bairro,
                endereco.Cidade,
                endereco.Estado,
                endereco.ClienteId
            );
        }
    }
}
