using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class CadastroAgenda : Form
    {
        private int sortColumn = -1;
  
        Agenda listaAgenda = new Agenda();

        public CadastroAgenda()
        {
            InitializeComponent();
        }

        private bool ValidarCampos()
        {
            if (tbNome.Text == "")
            {
                MessageBox.Show("Nome Inválido", "Atenção");
                return false;
            }

            if (tbEndereco.Text == "")
            {
                MessageBox.Show("Endereço Inválido", "Atenção");
                return false;
            }

            if (tbTelefone.Text == "")
            {
                MessageBox.Show("Telefone Inválido", "Atenção");
                return false;
            }

            if (!tbEmail.Text.Contains("@") || tbEmail.Text == "")
            {
                MessageBox.Show("Email Inválido", "Atenção");
                return false;
            }

            return true;
        }

        private void LimparCampos()
        {
            tbNome.Text = "";
            tbEndereco.Text = "";
            tbTelefone.Text = "";
            tbEmail.Text = "";
            tbCpf.Text = "";
        }
       
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                var prox = listaAgenda.Salvar(tbCpf.Text,tbNome.Text, tbEndereco.Text, tbTelefone.Text, tbEmail.Text);

                if (prox)
                {

                ListViewItem item = new ListViewItem(new[] { tbNome.Text,
                                                            tbEndereco.Text,
                                                            tbTelefone.Text,
                                                            tbEmail.Text,
                                                            tbCpf.Text


                });
               
                lvAgenda.Items.Add(item);

                MessageBox.Show($"Usuário adicionado {tbNome.Text}", "Atenção");
                LimparCampos();

                }
                else
                {
                    AtualizarLista();
                }
            }
        }
        
        private void AtualizarLista()
        {
            lvAgenda.Items.Clear();

            int tamanhoLista = listaAgenda.Size();


            Pessoa pessoa = new Pessoa();


            for (int i = 0; i < tamanhoLista; i++)
            {

                pessoa = listaAgenda.RetornaPessoas(i);


                ListViewItem item = new ListViewItem(new[] { pessoa.Nome,
                                                             pessoa.Endereco,
                                                             pessoa.Telefone,
                                                             pessoa.Email,

                    });

                lvAgenda.Items.Add(item);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            for (int itemList = lvAgenda.SelectedItems.Count - 1; itemList >= 0; itemList--)
            {

                ListViewItem lista = lvAgenda.SelectedItems[itemList];
                lvAgenda.Items.Remove(lista);

                string nome = lista.SubItems[0].Text;
                listaAgenda.Excluir(nome);
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void lvAgenda_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnOrdenar_Click(object sender, EventArgs e)
        {

        }

        private void lvAgenda_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != nome.Index)
            {
                sortColumn = e.Column;

                lvAgenda.Sorting = SortOrder.Ascending;
            }

            else
            {
                if (lvAgenda.Sorting == SortOrder.Ascending)

                    lvAgenda.Sorting = SortOrder.Descending;

                else
                    lvAgenda.Sorting = SortOrder.Ascending;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            for (int itemList = lvAgenda.SelectedItems.Count - 1; itemList >= 0; itemList--)
            {

                ListViewItem lista = lvAgenda.SelectedItems[itemList];
                string id = lista.SubItems[0].Text;

                var pessoa = listaAgenda.RetornaPessoas(id);

                listaAgenda.Editar(id, pessoa);

                tbNome.Text = pessoa.Nome;
                tbEndereco.Text = pessoa.Endereco;
                tbTelefone.Text = pessoa.Telefone;
                tbEmail.Text = pessoa.Email;
                tbCpf.Text = pessoa.Cpf;
                tbCpf.Enabled = false;
            }
        }

        private void CadastroAgenda_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            LimparCampos();
        }
    }
}
