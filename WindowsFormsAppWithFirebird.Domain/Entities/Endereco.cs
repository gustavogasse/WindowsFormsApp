using System;
using WindowsFormsAppWithFirebird.Domain.DomainObjects;
using WindowsFormsAppWithFirebird.Domain.Extensions;

namespace WindowsFormsAppWithFirebird.Domain.Entities
{
    public class Endereco : BaseEntity
    {
        public Endereco() { }
        public Endereco(string cep, string logradouro, string complemento, string numero, string bairro, string cidade, string estado)
        {
            CEP = cep.RemoverCaracteres();
            Logradouro = logradouro;
            Complemento = complemento;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }

        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public Guid ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
