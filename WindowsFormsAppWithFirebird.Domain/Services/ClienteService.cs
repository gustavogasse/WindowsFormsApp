using System;
using System.Collections.Generic;
using System.Linq;
using WindowsFormsAppWithFirebird.Domain.Data;
using WindowsFormsAppWithFirebird.Domain.Entities;
using WindowsFormsAppWithFirebird.Domain.Extensions;
using WindowsFormsAppWithFirebird.Domain.Services.Interfaces;

namespace WindowsFormsAppWithFirebird.Domain.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly IClienteHistoricoRepository _clienteHistoricoRepository;
        private readonly IEnderecoService _enderecoService;

        public ClienteService(IEnderecoService enderecoService, IClienteRepository repository, IClienteHistoricoRepository clienteHistoricoRepository) 
        {
            _enderecoService = enderecoService;
            _repository = repository;
            _clienteHistoricoRepository = clienteHistoricoRepository;    
        }

        public void Add(Cliente entity)
        {
            if (entity.Documento.EhValido())
            {
                var entityEncontrada = GetByDocumento(entity.Documento);
                if (entityEncontrada == null)
                {
                    _repository.Add(entity);                    
                    _clienteHistoricoRepository.Add(ClienteToHistorico(entity));
                }
                else
                {
                    throw new ArgumentException("Cliente já cadastrado.");
                }
            }
            else
            {
                throw new ArgumentException("O documento informado é inválido.");
            }
        }

        public void Add(Cliente cliente, Endereco endereco)
        {
            Add(cliente);
            _enderecoService.Add(endereco);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public Cliente Get(Cliente entity)
        {
            return _repository.Get(entity);
        }

        public IEnumerable<Cliente> GetAll()
        {
            return _repository.GetAll();
        }

        public Cliente GetByDocumento(string documento)
        {
            return GetAll()
                .Where(c => c.Documento.RemoverCaracteres().Equals(documento.RemoverCaracteres()))
                .SingleOrDefault();
        }

        public Cliente GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<ClienteHistorico> GetHistoricoDeAlteracoes(Guid clienteId)
        {
            var cliente = GetById(clienteId);
            return _clienteHistoricoRepository.GetAll()
                .Where(ch => ch.Documento == cliente.Documento);
        }

        public IEnumerable<ClienteRelatorioAlteracao> GetAllHistoricoDeAlteracoes()
        {
            var listaClientes = GetAll();

            var retorno = new List<ClienteRelatorioAlteracao>();
            foreach (var cliente in listaClientes)
            {
                retorno.Add(new ClienteRelatorioAlteracao()
                {
                    Documento = cliente.Documento,
                    Email = cliente.Email,
                    Nome = cliente.Nome,
                    QuantidadeAlteracoes = GetHistoricoDeAlteracoes(cliente.Id).Count()
                });
            }

            return retorno;
        }

        public void Update(Cliente entity)
        {
            _repository.Update(entity);            
            _clienteHistoricoRepository.Add(ClienteToHistorico(entity));            
        }

        public void Update(Cliente cliente, Endereco endereco)
        {
            Update(cliente);
            _enderecoService.Update(endereco);
        }

        private ClienteHistorico ClienteToHistorico(Cliente cliente)
        {
            return new ClienteHistorico(
                cliente.Nome,
                cliente.Documento,
                cliente.Email
            );
        }
    }
}
