
using System;
using System.Text;
using SistemaBiblioteca.Models;

Biblioteca Bb = new();

Console.OutputEncoding = System.Text.Encoding.Latin1;
Console.InputEncoding = System.Text.Encoding.Latin1;


do
{

    Console.Clear();
    Console.WriteLine("---- Bem Vindo a Biblioteca ----");
    Console.WriteLine("1 - Cadastrar Livro");
    Console.WriteLine("2 - Cadastrar Usuário");
    Console.WriteLine("3 - Listar Usuários");
    Console.WriteLine("4 - Listar Livros");
    Console.WriteLine("5 - Listar Livros Disponíveis");
    Console.WriteLine("6 - Listar Emprestimos em Andamento");
    Console.WriteLine("7 - Listar Emprestimos de um Usuário");
    Console.WriteLine("8 - Pegar Livro Emprestado");
    Console.WriteLine("9 - Devolver Livro");
    Console.WriteLine("0 - Sair");
    Console.Write("Escolha uma opção: ");
    try
    {
        int opcao = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        switch (opcao)
        {
            case 1:
                Console.WriteLine("--- Cadastramento de Livro ----");
                Bb.AdicionarLivro();
                break;
            case 2:
                Console.WriteLine("--- Cadastramento de Usuário ----");
                Bb.AdicionarUsuario();
                break;
            case 3:
                Console.WriteLine("--- Listar Usuários ---");
                Bb.ListarUsuariosCadastrados();
                break;
            case 4:
                Console.WriteLine("--- Listar Livros ---");
                Bb.ListarLivrosCadastrados();
                break;
            case 5:
                Console.WriteLine("--- Listar Livros Disponíveis ---");
                Bb.ListarLivrosDisponiveis();
                break;
            case 6:
                Console.WriteLine("--- Listar Emprestimos em Andamento ---");
                Bb.ListarEmprestimosEmAndamento();
                break;
            case 7:
                Console.WriteLine("--- Listar Emprestimos de um Usuário ---");
                Bb.ListarEmprestimosUsuario();
                break;
            case 8:
                Console.WriteLine("--- Pegar Livro Emprestado ---");
                Bb.PegarLivroEmprestado();
                break;
            case 9:
                Console.WriteLine("--- Devolver Livro ---");
                Bb.DevolverLivro();
                break;
            case 0:
                return;
            default:
                Console.WriteLine("Opção inválida!");
                break;
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Opção inválida!");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }

    //Para que o resultado da opção escolhida seja exibida antes do console ser limpo
    Console.WriteLine("\nDigite qualquer tecla para prosseguir . . .");
    Console.ReadLine();
} while (true);
// try
// {
//     //Usuario u1 = new("João Victor", "jvsilva13.jv@gmail.com", DateTime.Parse("20/11/2001"), "34998296782");
//     //Livro l1 = new("O ladrão de Capanga", "Lord Mickey", "Drama", 2007);
//     //Console.WriteLine(u1.DataNascimento);
//     //Bb.AdicionarUsuario(u1);
//     //Bb.AdicionarLivro(l1);
//     //Bb.ListarUsuariosCadastrados();
//     //Bb.ListarLivrosCadastrados();
//     //Bb.PegarLivroEmprestado("O LADRÃO DE CAPANGA", "jvsilva13.jv@gmail.com");
//     //Bb.DevolverLivro("O LADRÃO DE CAPANGA", "jvsilva13.jv@gmail.com");

//     //Bb.ListarEmprestimosEmAndamento();
//     Bb.ListarEmprestimosUsuario("JOÃO VICTOR");
// }
// catch (Exception ex)
// {

//     Console.WriteLine(ex.Message);
// }



