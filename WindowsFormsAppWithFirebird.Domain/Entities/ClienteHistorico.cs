using System;
using WindowsFormsAppWithFirebird.Domain.DomainObjects;
using WindowsFormsAppWithFirebird.Domain.Extensions;

namespace WindowsFormsAppWithFirebird.Domain.Entities
{
    public class ClienteHistorico : BaseEntity
    {
        public ClienteHistorico() { }
        public ClienteHistorico(string nome, string documento, string email)
        {
            Nome = nome;
            Documento = documento.RemoverCaracteres();
            Email = email;
            DataAlteracao = DateTime.Now;
        }

        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
