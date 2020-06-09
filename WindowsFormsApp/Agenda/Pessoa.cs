using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp
{
    public class Pessoa
    {
        public string Cpf;
        public string Nome;
        public string Endereco;
        public string Telefone;
        public string Email;

        public Pessoa()
        {

        }

        public Pessoa(string cpf, string nome, string endereco, string telefone, string email)
        {
            Cpf = cpf;
            Nome = nome;
            Endereco = endereco;
            Telefone = telefone;
            Email = email;
        }
    }
}
