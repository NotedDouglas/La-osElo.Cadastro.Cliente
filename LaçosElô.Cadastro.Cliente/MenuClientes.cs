using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaçosElô.Cadastro.Cliente {
    public partial class MenuClientes : Form {
        public MenuClientes() {
            InitializeComponent();
        }

        private void MenuClientes_Load(object sender, EventArgs e) {
            // Abre maximizado
            this.WindowState = FormWindowState.Maximized;

            // Opcional: impedir redimensionamento manual
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Se quiser realmente ocupar 100% da tela sem borda:
            // this.FormBorderStyle = FormBorderStyle.None;
            // this.WindowState = FormWindowState.Maximized;
        }

        private void btAdd_Click(object sender, EventArgs e) {

            ClientesLacosElo formAdd = new ClientesLacosElo();
            formAdd.ShowDialog();
        }


    }
}
