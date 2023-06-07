using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDMysql
{
    public partial class FrmCadastroLivro : Form
    {
        int id;
        Livro livro = new Livro();
        public FrmCadastroLivro(int id)
        {
            InitializeComponent();
            this.id = id;
            livro.GetLivro(this.id);
            if (id > 0) {
                livro.GetLivro(this.id);
                txtID.Text = livro.id.ToString();
                txtTitulo.Text = livro.titulo;
                txtAutor.Text = livro.autores;
                txtUnitario.Text = livro.unitario.ToString("N2");
                txtCriado.Text = livro.dataCriacao.ToLongDateString();
                if (livro.ativo == 'S') {
                    cbxAtivo.Checked= true;
                }
                
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            if (ValidarForm())
            {
                livro.titulo = txtTitulo.Text;
                livro.autores = txtAutor.Text;
                livro.unitario = Convert.ToDecimal(txtUnitario.Text);
                //livro.dataCriacao = Convert.ToDateTime(txtCriado.Text);
                livro.ativo = cbxAtivo.Checked == true ? 'S' : 'N';
                livro.SalvarLivro();
                this.Close();
            }
        }
        private bool ValidarForm()
        {
            if (txtAutor.Text == "") 
            {
                MessageBox.Show("Informe o Autor.");
                txtAutor.Focus();
                return false;
            }
            if (txtUnitario.Text == "")
            {
                MessageBox.Show("Informe valor unitario.");
                txtUnitario.Focus();
                return false;
            }
            
            return true;
        }
    }
}
