using System;
using WindowsFormsAppWithFirebird.Domain.DomainObjects;
using WindowsFormsAppWithFirebird.Domain.Extensions;

namespace WindowsFormsAppWithFirebird.Domain.Entities
{    
    public class Cliente : BaseEntity
    {
        public Cliente() { }
        public Cliente(string nome, string documento, string email)
        {
            Nome = nome;
            Documento = documento.RemoverCaracteres();
            Email = email;
        }        

        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
    }
}
