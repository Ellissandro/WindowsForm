using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public class Agenda
    {
        List<Pessoa> pessoas = new List<Pessoa>();

        public Agenda()
        {

        }

        public bool Salvar(string cpf, string nome, string endereco, string telefone, string email)
        {
            var p = pessoas.Any(x => x.Cpf == cpf);
            if (p)
            {
                var i = pessoas.FindIndex(x => x.Cpf == cpf);
                pessoas[i].Nome = nome;
                pessoas[i].Endereco = endereco;
                pessoas[i].Telefone = telefone;
                pessoas[i].Email = email;

                try
                {
                    var sqlConnection = Conexao();
                    string comand = $"UPDATE agenda SET nome = '{nome}', endereco = '{endereco}',telefone = '{telefone}', email = '{email}' WHERE cpf = '{cpf}';";

                    SqlCommand sqlCommand = new SqlCommand(comand, sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

              
                return false;
            }
            else { 
            Pessoa pessoa = new Pessoa(cpf, nome, endereco, telefone, email);
            pessoas.Add(pessoa);
                
                try
                {
                    var sqlConnection = Conexao();
                    string comand = $"insert into agenda(cpf, nome, endereco,telefone,email) values('{cpf}','{nome}','{endereco}','{telefone}','{email}')";

                    SqlCommand sqlCommand = new SqlCommand(comand, sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                return true;
            }
        }     
        public void Excluir(string nome)
        {
            pessoas.RemoveAll(x => x.Nome == nome);

            try
            {
                var sqlConnection = Conexao();
                string comand = $"DELETE FROM agenda WHERE nome = '{nome}';";

                SqlCommand sqlCommand = new SqlCommand(comand, sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Ordenar()
        {
            pessoas = pessoas.OrderBy(x => x.Nome).ToList();
            pessoas = pessoas.OrderByDescending(x => x.Nome).ToList();
        }

        public int Size()
        {
            return pessoas.Count;
        }

        public Pessoa RetornaPessoas(string nome)
        {
            var pessoa = pessoas.Find(x => x.Nome == nome);
            try
            {
                var sqlConnection = Conexao();
                string comand = $"select * from agenda where nome = '{nome}';";

                SqlCommand sqlCommand = new SqlCommand(comand, sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return pessoa;
        }
        public Pessoa RetornaPessoas(int index)
        {
            var pessoa = pessoas[index];
            return pessoa;
        }
        public void Editar(string id, Pessoa pessoa)
        {
           
        }

        public static SqlConnection Conexao()
        {
            string conexao = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BancoParaTestes;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection sqlConnection = new SqlConnection(conexao);
            sqlConnection.Open();
            return sqlConnection;
        }
    }

}
