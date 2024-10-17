using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

using Dapper;
using SistemaBiblioteca.Infraestruture;

namespace SistemaBiblioteca.Models
{

    public class Biblioteca//Faz o gerenciamento da biblioteca
    {

        public Biblioteca()
        {
            Usuarios = new();
            Livros = new();
            Emprestimos = new();
        }
        //Dados do usuário, livro somente serão adicionados no banco de dados, a lista presente nesta classe só é atualizada quando o objeto Biblioteca quando chamado o método de exibição dos dados da respectiva lista
        public List<Usuario> Usuarios { get; set; }
        public List<Livro> Livros { get; set; }
        public List<Emprestimo> Emprestimos { get; set; }


        //Funções get retornam os dados do meu banco de dados de acordo com o tipo de lista desejada.
        private List<Usuario> GetUser()
        {
            using var conn = new DbConnection();
            string query = @"Select * from usuarios;";

            var user = conn.Connection.Query<Usuario>(sql: query);

            return user.ToList();
        }

        private List<Livro> GetBook()
        {
            using var conn = new DbConnection();
            string query = @"Select * from livros;";

            var book = conn.Connection.Query<Livro>(sql: query);

            return book.ToList();
        }

        private List<Emprestimo> GetLoan()
        {
            using var conn = new DbConnection();
            string query = @"Select * from emprestimos;";

            var loan = conn.Connection.Query<Emprestimo>(sql: query);

            return loan.ToList();
        }

        //Funções Set atribuem os dados obtidos pelas funções Get e atribuem as minhas listas especificas no meu programa a fim de exibi-las aos usuários  
        private void SetUser()
        {
            Usuarios = GetUser();
        }

        private void SetBook()
        {
            Livros = GetBook();
        }

        private void SetLoan()
        {
            Emprestimos = GetLoan();
        }


        //Realiza a adição de um novo usuário em meu banco de dados
        public void AdicionarUsuario()
        {
            //Parte da leitura dos dados
            Usuario usuario = new();
            Console.Write("Nome: ");
            usuario.Nome = Console.ReadLine();
            Console.Write("E-mail: ");
            usuario.Email = Console.ReadLine();
            Console.Write("Data de Nascimento: ");
            usuario.DataNascimento = DateTime.Parse(Console.ReadLine());
            Console.Write("Telefone: ");
            usuario.Telefone = Console.ReadLine();

            //Parte da escrita dos dados no banco 
            using var conn = new DbConnection();
            string query = @"Insert Into public.usuarios(
                            Nome, Email, DataNascimento, Telefone)
                            Values(@Nome, @Email, @DataNascimento, @Telefone)";
            var result = conn.Connection.Execute(query, usuario);
            if (result == 1)
            {
                Console.WriteLine("Usuario adicionado com sucesso");
            }
            else
            {
                Console.WriteLine("Erro ao adicionar usuario");
            }
        }

        //Realiza a adição de um novo livro em meu banco de dados 
        public void AdicionarLivro()
        {
            Livro livro = new();
            Console.Write("Título: ");
            livro.Titulo = Console.ReadLine();
            Console.Write("Autor: ");
            livro.Autor = Console.ReadLine();
            Console.Write("Gênero: ");
            livro.Genero = Console.ReadLine();
            Console.Write("Ano de Publicação: ");
            livro.AnoPublicacao = int.Parse(Console.ReadLine());


            using var conn = new DbConnection();
            string query = @"Insert Into public.livros(
                            Titulo, Autor, Genero, AnoPublicacao, Disponivel)
                            Values(@Titulo, @Autor, @Genero, @AnoPublicacao, @Disponivel)";
            var result = conn.Connection.Execute(query, livro);
            if (result == 1)
            {
                Console.WriteLine("Livro cadastrado com sucesso");
            }
            else
            {
                Console.WriteLine("Erro ao cadastrar livro");
            }
        }

        //Funções de listar carregam os dados do meu banco de dados para as listas locais para que seja feita a exibição dos dados usando as
        public void ListarUsuariosCadastrados()
        {
            SetUser();//Para que a lista de usuários seja atualizada localmente para exibir os dados atualizados
            if (Usuarios.Count > 0)
            {
                foreach (Usuario usuario in Usuarios)
                {
                    usuario.Apresentar();
                }
            }
            else
            {
                Console.WriteLine("Nenhum Usuário cadastrado!!");
            }

        }

        public void ListarLivrosCadastrados()
        {
            SetBook();
            if (Livros.Count > 0)
            {
                foreach (Livro livro in Livros)
                {
                    livro.Apresentar();
                }
            }
            else
            {
                Console.WriteLine("Nenhum Livro cadastrado!!");
            }
        }
        public void ListarLivrosDisponiveis()
        {
            SetBook();
            if (Livros.Count > 0)
            {
                foreach (Livro livro in Livros)
                {
                    if (livro.Disponivel == true)
                    {
                        livro.Apresentar();
                    }
                }
            }
            else
            {
                Console.WriteLine("Nenhum Livro cadastrado!");
            }
        }

        public void ListarEmprestimosEmAndamento()
        {
            //Carrego as três listas do meu sistema com os dados do banco de dados para fornecer uma informação mais rica ao meu usuário
            SetLoan();
            if (Emprestimos.Count > 0)
            {
                //somente após verificar se existem emprestimos que é carregado as listas de livros e usuários, para evitar que sejam carregadas sem necessidade
                SetBook();
                SetUser();
                foreach (Emprestimo emprestimo in Emprestimos)
                {
                    //Verificação se o emprestimo ainda está em andamento
                    if (emprestimo.DataDevolucao.ToString() == "01/01/0001 00:00:00")//Por padrão o valor estabelecido no banco dados a esse campo é esse, porem ao tentar obte-lo no banco de dados, visualmente é dito ser null
                    {
                        //buscando o livro que tenha o mesmo id do emprestimo para exibir seu título
                        Livro livroEncontrado = Livros.Find(livro => livro.IdLivro == emprestimo.IdLivro);
                        //buscando o usuário que tenha o mesmo id do emprestimo para exibir seu Nome
                        Usuario usuarioEncontrado = Usuarios.Find(usuario => usuario.IdUsuario == emprestimo.IdUsuario);
                        Console.WriteLine($"Nome do usuário: {usuarioEncontrado.Nome}\nTitulo do livro: {livroEncontrado.Titulo}\nData do Emprestimo: {emprestimo.DataEmprestimo.ToString("dd/MM/yyyy HH:mm")}\n");

                    }

                }
            }
            else
            {
                Console.WriteLine("Nenhum Emprestimo foi feito!!");
            }
        }

        public void ListarEmprestimosUsuario()
        {
            Console.Write("Informa um nome cadastrado: ");
            string nomeUsuario = Console.ReadLine().ToUpper();
            SetUser();
            if (Usuarios.Count > 0)
            {

                SetLoan();
                foreach (Usuario usuario in Usuarios)
                {
                    if (usuario.Nome == nomeUsuario)
                    {
                        Console.Write($"Emprestimos realizados por: {usuario.Nome}\n\n");
                        //É rretonada uma lista de dados do tipo emprestimo onde o emprestimo.IdUsuario é igual ao IdUsuario que esta sendo requisitado
                        List<Emprestimo> listaUserEmprestimos = Emprestimos.FindAll(emprestimo => emprestimo.IdUsuario == usuario.IdUsuario);
                        if (listaUserEmprestimos.Count > 0)
                        {
                            foreach (Emprestimo e in listaUserEmprestimos)
                            {
                                e.Apresentar();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nenhum emprestimo foi feito por esse usuário!!");
                        }

                        break;
                    }
                }

            }
            else
            {
                Console.WriteLine("Nenhum Usuário cadastrado!");
            }
        }


        public void PegarLivroEmprestado()
        {
            Console.Write("Informe o título de um livro cadastrado: ");
            string nomeLivro = Console.ReadLine().ToUpper();
            Console.Write("Informe o email do usuário que irá pegar livro: ");
            string emailUsuario = Console.ReadLine();


            using var conn = new DbConnection();
            string query = @"SELECT l.IdLivro
                     FROM public.livros AS l
                     WHERE l.Titulo = @nomeLivro;";

            // Passando o parâmetro como um objeto anônimo
            var livroId = conn.Connection.Query<int>(query, new { nomeLivro }).FirstOrDefault();//Esse FirstOrDefault esta sendo utilizado para retornar a variável o primeiro valor encontrado, mesmo que seja o id que esteja sendo buscado que é único, o tipo de dado permanece um IEnumerable se ritado esse método, para facilicar ele é utilizado

            query = @"select u.IdUsuario from public.usuarios as u
                    where u.Email = @emailUsuario;";

            var usuarioId = conn.Connection.Query<int>(query, new { emailUsuario }).FirstOrDefault();

            // Verificando se foi encontrado um ID
            if (livroId != 0 && usuarioId != 0)
            {
                query = @"select l.Disponivel
                        from public.livros as l where l.IdLivro = @livroId";

                var livroDisponivel = conn.Connection.Query<bool>(query, new { livroId }).FirstOrDefault();
                //verificação da disponibilidade do livro para pegar emprestado
                if (livroDisponivel)
                {
                    DateTime horaDoEmprestimo = DateTime.Now;

                    query = @"insert into public.emprestimos(IdLivro, IdUsuario, DataEmprestimo)
                            values(@livroId, @usuarioId, @horaDoEmprestimo)";
                    var result = conn.Connection.Execute(query, new { livroId, usuarioId, horaDoEmprestimo });

                    query = @"UPDATE public.livros
                            SET Disponivel = false
                            WHERE IdLivro = @livroId;";
                    var result2 = conn.Connection.Execute(query, new { livroId });

                    if (result == 1 && result2 == 1)
                    {
                        Console.WriteLine("Livro emprestado com sucesso");
                    }
                    else
                    {
                        Console.WriteLine("Erro ao pegar livro");
                    }

                }
                else
                {
                    Console.WriteLine("O livro não está diponível");
                }
            }
            else
            {
                throw new Exception("Livro ou Usuário não encontrados.");
            }

        }

        public void DevolverLivro()
        {
            Console.Write("Informe o título do livro: ");
            string nomeLivro = Console.ReadLine().ToUpper();
            Console.Write("Informe o email do usuário que pegou o livro: ");
            string emailUsuario = Console.ReadLine();

            using var conn = new DbConnection();
            string query = @"SELECT l.IdLivro
                     FROM public.livros AS l
                     WHERE l.Titulo = @nomeLivro;";

            // Passando o parâmetro como um objeto anônimo
            var livroId = conn.Connection.Query<int>(query, new { nomeLivro }).FirstOrDefault();

            query = @"select u.IdUsuario from public.usuarios as u
                    where u.Email = @emailUsuario;";

            var usuarioId = conn.Connection.Query<int>(query, new { emailUsuario }).FirstOrDefault();

            if (livroId != 0 && usuarioId != 0)
            {
                query = @"select e.IdEmprestimo from public.emprestimos as e
                        where e.IdLivro = @livroId and e.IdUsuario = @usuarioId and e.DataDevolucao is null";//data de devolução deve ser igual a null para que se o livro for pego pelo mesmo usuário o programa deve alterar a data de devolução do novo emprestimo, não do antigo se existir
                var emprestimoId = conn.Connection.Query<int>(query, new { livroId, usuarioId }).FirstOrDefault();

                if (emprestimoId != 0)
                {
                    DateTime horaDaDevolucao = DateTime.Now;
                    query = @"Update public.emprestimos
                            set DataDevolucao = @horaDaDevolucao
                            where IdEmprestimo = @emprestimoId";
                    var result = conn.Connection.Execute(query, new { horaDaDevolucao, emprestimoId });

                    query = @"UPDATE public.livros
                            SET Disponivel = true
                            WHERE IdLivro = @livroId;";
                    var result2 = conn.Connection.Execute(query, new { livroId });

                    if (result == 1 && result2 == 1)
                    {
                        Console.WriteLine("Livro devolvido com sucesso");
                    }
                    else
                    {
                        Console.WriteLine("Erro ao pegar livro");
                    }


                }
                else
                {
                    Console.WriteLine("Livro não foi emprestado ou ja foi devolvido");//essa parte da verificação está muito abrangente, falta informação do que não foi achado
                }
            }
            else
            {
                Console.WriteLine("Usuário ou livro não encontrados.");
            }
        }


    }
}