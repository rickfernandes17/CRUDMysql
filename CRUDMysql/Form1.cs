using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Parameters;
using System.Data;

namespace CRUDMysql
{
    public partial class Form1 : Form
    {

        DataTable dt = new DataTable();
        public Form1()
        {
            InitializeComponent();
            Inicializar();
        }

        private void Inicializar() {
            dt = Livro.GetLivros();
            dgvLivros.DataSource= dt;
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmCadastroLivro(0))
            {
                frm.ShowDialog();
                Inicializar();
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(dgvLivros.Rows[dgvLivros.CurrentCell.RowIndex].Cells["Codigo"].Value);
            using (var frm = new FrmCadastroLivro(id)) {
                frm.ShowDialog();
                Inicializar();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dt = Livro.GetLivros(txtFiltro.Text);
            dgvLivros.DataSource = dt;
        }
    }
}