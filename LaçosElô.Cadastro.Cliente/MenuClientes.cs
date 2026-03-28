using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LaçosElô.Cadastro.Cliente {

    public partial class MenuClientes : Form {
        public MenuClientes() {
            InitializeComponent();
        }
        string caminhoFotos = AppDomain.CurrentDomain.BaseDirectory + "Fotos/";
        private void MenuClientes_Load(object sender, EventArgs e) {
            // Abre maximizado
           // this.WindowState = FormWindowState.Maximized;

            // impedir redimensionamento manual
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            BuscarCliente();
        }
        private void btAdd_Click(object sender, EventArgs e) {

            ClientesLacosElo formAdd = new ClientesLacosElo();
            formAdd.ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e) {

        }

        private void dgLista_CellContentClick(object sender, DataGridViewCellEventArgs e) {


        }

        private void dgLista_Sorted_1(object sender, EventArgs e) {
            ReorganizarDataGridView();
        }
        private void ReorganizarDataGridView() {
            foreach (DataGridViewRow lin in dgLista.Rows) {
                if (lin.Cells["situacao"].Value != null) {
                    string situacao = lin.Cells["situacao"].Value.ToString();
                    if (situacao == "Cancelado") {
                        lin.DefaultCellStyle.ForeColor = Color.Crimson;
                    } else if (situacao == "Ativo") {
                        lin.DefaultCellStyle.ForeColor = Color.Blue;
                    }
                }
                if (File.Exists(caminhoFotos + lin.Cells["id"].Value.ToString() + ".png")) {
                    lin.Cells["foto"].Value =
                        Image.FromFile(caminhoFotos + lin.Cells["id"].Value.ToString() + ".png");
                } else {
                    lin.Cells["foto"].Value = Properties.Resources.avatar_icone;
                }

                dgLista.ClearSelection();
                btEditar.Enabled = false;

            }
        }
        private void dgLista_CellClick(object sender, DataGridViewCellEventArgs e) {

            btEditar.Enabled = true;
        }

        private void btEditar_Click(object sender, EventArgs e) {
            ClientesLacosElo form = new ClientesLacosElo();

            form.TxtId.Text = dgLista.CurrentRow.Cells["id"].Value.ToString();

            form.ShowDialog();
            BuscarCliente();
        }

        private void BuscarCliente() {

            dgLista.DataSource = Funcoes.BuscaSQL("SELECT * FROM clientes");
            ReorganizarDataGridView();
        }
    }
}
