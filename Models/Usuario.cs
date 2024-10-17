using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SistemaBiblioteca.Models
{
    public class Usuario
    {
        private string _nome;
        private string _email;
        private string _telefone;
        private DateTime _data;

        public Usuario()
        {

        }
        public Usuario(string nome, string email, DateTime dataNasc, string telefone)
        {
            Nome = nome;
            Email = email;
            DataNascimento = dataNasc;
            Telefone = telefone;
        }
        public int IdUsuario { get; set; }
        public string Nome
        {
            get => _nome.ToUpper();
            set
            {
                if (value == "")
                {
                    throw new ArgumentException("O nome não pode ser vazio");
                }
                _nome = value;
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                string padraoEmail = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
                if (value == "" || !Regex.IsMatch(value, padraoEmail))//faz a verificação se o campo do email informado é nulo ou não é válido e dispara uma exceção
                {
                    throw new ArgumentException("O e-mail não é válido");
                }
                _email = value;
            }
        }
        public DateTime DataNascimento
        {
            get => _data;
            set
            {
                if (DateTime.TryParseExact(value.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime data))//faz a verificação se a string de data de nascimento se comporta com o tipo de dado DateTime, se sim retorna a string convertida no tipo DateTime
                {
                    _data = value;//Data que está no tipo DateTime recebe data do mesmo tipo
                }
                else
                {
                    throw new ArgumentException("Formato da data de nascimento inválida");//Se formato da data inválida levanta um erro
                }
            }
        }
        public string Telefone
        {
            get => _telefone;
            set
            {
                string _padraoTelefone = @"^\(?\d{2}\)?[\s-]?9?\d{4}[\s-]?\d{4}$";
                if (!Regex.IsMatch(value, _padraoTelefone))//se i padrão do telefone não condiz com o regex irá disparar uma exeção
                {
                    throw new ArgumentException("Telefone Inválido");

                }
                _telefone = value;
            }
        }

        public void Apresentar()
        {
            Console.WriteLine($"Nome: {Nome}\nEmail: {Email}\nData de nascimento: {DataNascimento.ToString("dd/MM/yyyy")}\nTelefone: {Telefone}\n");
        }

    }
}