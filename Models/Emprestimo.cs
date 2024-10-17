using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBiblioteca.Models
{
    public class Emprestimo
    {
        public int IdEmprestimo { get; set; }
        public int IdUsuario { get; set; }
        public int IdLivro { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }

        //Faz a verificação se a data de devolução está preenchida ou não e retorna uma string para a mensagem de apresetnação adequada 
        private string VerificacaoDataDevolucao()
        {
            //Mensagens diferentes caso o campo de data de devolução ainda não tenha sido preenchido
            if (DataDevolucao.ToString() == "01/01/0001 00:00:00")//Esse horario e data vai por padrao ao campo do banco de dados caso não seja preenchido
            {
                return "Pendente";
            }
            else
            {
                return DataDevolucao.ToString("dd MMMM yyyy HH:mm");
            }
        }

        public void Apresentar()
        {
            Console.WriteLine($"Data de Emprestimo: {DataEmprestimo.ToString("dd MMMM yyyy HH:mm")}\nData de Devolução: {VerificacaoDataDevolucao()}\n");
        }
    }
}