using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBiblioteca.Models
{
    public class Livro
    {
        private string _titulo;
        private string _autor;
        public Livro()
        {
            Disponivel = true;
        }
        public Livro(string titulo, string autor, string genero, int anoPublicacao, bool disponivel = true)//Se o disponível não for passado para o construtor, por padrão será atribuido o valor true
        {
            Titulo = titulo;
            Autor = autor;
            Genero = genero;
            AnoPublicacao = anoPublicacao;
            Disponivel = disponivel;
        }
        public int IdLivro { get; set; }
        public string Titulo
        {
            get => _titulo.ToUpper();
            set
            {
                if (value == "")
                {
                    throw new Exception("O Título do livro não pode estar vazio");
                }
                _titulo = value;
            }
        }
        public string Autor
        {
            get => _autor.ToUpper();
            set
            {
                if (value == "")
                {
                    _autor = "Desconhecido";
                }
                else
                {
                    _autor = value;
                }

            }
        }
        public string Genero { get; set; }
        public int AnoPublicacao { get; set; }
        public bool Disponivel { get; set; }

        public void Apresentar()
        {
            Console.WriteLine($"Título: {Titulo}\nAutor: {Autor}\nGênero: {Genero}\nAno de publicação: {AnoPublicacao}\nDisponivel: {(Disponivel ? "Sim" : "Não")}\n");
        }
    }
}