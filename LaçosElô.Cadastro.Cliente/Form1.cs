using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Parameters;
using System.Globalization;

namespace LaçosElô.Cadastro.Cliente {
    public partial class ClientesLacosElo : Form {
        public ClientesLacosElo() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e) {

        }

        private void Genero_Enter(object sender, EventArgs e) {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) {

        }

        private void button3_Click(object sender, EventArgs e) {

            if (Validacoes() == true) {
                return;
            }

            SalvarClinteMySql();
        }

        private void ClientesLacosElo_KeyDown(object sender, KeyEventArgs e) {

            if (e.KeyCode == Keys.Enter) {
                e.SuppressKeyPress = true; // evita o "beep"
                SendKeys.Send("{TAB}");
            }
        }
        private void SalvarClinteMySql() {
            using (MySqlConnection conexao = new MySqlConnection("server=localhost;Port=3306;database=cliente;user=root;Password=")) {

                conexao.Open();

                using (MySqlCommand comando = conexao.CreateCommand()) {
                    comando.CommandText = "INSERT INTO clientes (nome, documento, genero, rg, estado_civil, nasc, cep, endereco," +
                        "numero, bairro, cidade, celular, email, obs, situacao) " +
                        "VALUES " +
                        "(@nome, @doc, @genero, @rg, @es_civil, @nasc, @cep, @endereco, @numero, @bairro, @cidade, @celular, @email, @obs, @situacao)";

                    string genero = null;
                    if (OpMasculino.Checked) genero = "Masculino";
                    else if (OpFeminino.Checked) genero = "Feminino";
                    else if (OpOutros.Checked) genero = "Outro";

                    if(Cksituacao.Checked == true) {
                        Cksituacao.Text = "Ativo";
                    } else {
                        Cksituacao.Text = "Inativo";
                    }

                    comando.Parameters.AddWithValue("@nome", TxtNome.Text);
                    comando.Parameters.AddWithValue("@doc", TxtDoc.Text);

                    comando.Parameters.AddWithValue("@genero", (object)genero ?? DBNull.Value);

                    comando.Parameters.AddWithValue("@rg", TxtRg.Text);
                    comando.Parameters.AddWithValue("@es_civil", comboEstadoCivil.Text);

                    if (TxtNascimento.Text == "  /  /") 
                        comando.Parameters.AddWithValue("@nasc", DBNull.Value);
                    else 
                    comando.Parameters.AddWithValue("@nasc", Convert.ToDateTime(TxtNascimento.Text));


                    comando.Parameters.AddWithValue("@cep", TxtCep.Text);
                    comando.Parameters.AddWithValue("@endereco", ComboEndereco.Text);
                    comando.Parameters.AddWithValue("@numero", TxtNumero.Text);
                    comando.Parameters.AddWithValue("@bairro", comboBairro.Text);
                    comando.Parameters.AddWithValue("@cidade", comboCidade.Text);
                    comando.Parameters.AddWithValue("@celular", TxtCelular.Text);
                    comando.Parameters.AddWithValue("@email", TxtEmail.Text);
                    comando.Parameters.AddWithValue("@obs", TxtObs.Text);
                    comando.Parameters.AddWithValue("@situacao", Cksituacao.Text);

                    int resultado = comando.ExecuteNonQuery();

                    if (resultado > 0) {
                        long idGerado = comando.LastInsertedId;
                        TxtId.Text = idGerado.ToString();

                        MessageBox.Show("Cliente cadastrado com sucesso!");
                    } else {
                        MessageBox.Show("Falha ao cadastrar cliente.");
                    }

                }
            }

        }

        private bool Validacoes() {

            // Implementar validações aqui
            //validar campo nome
            if (string.IsNullOrWhiteSpace(TxtNome.Text)) {
                MessageBox.Show("O campo Nome é obrigatório.");
                TxtNome.Focus();
                return true;
            }

            //validar CPF ou CNPJ
            if (!OpCpf.Checked && !OpCnpj.Checked) {
                MessageBox.Show("Marque o tipo de documentação.\nCPF ou CNPJ");
                return true;
            }

            //validar documento
            if (TxtDoc.Text == "") {
                MessageBox.Show("O campo Documento é obrigatório.");
                TxtDoc.Focus();
                return true;
            }
            //validar gênero
            if (!OpMasculino.Checked && !OpFeminino.Checked && !OpOutros.Checked) {
                MessageBox.Show("Marque o gênero do cliente.");
                return true;
            }

            //validar data de nascimento
            if (TxtNascimento.Text != "  /  /") 
                try {
                Convert.ToDateTime(TxtNascimento.Text);
            } catch (Exception) {
                MessageBox.Show("Data de Nascimento Não Valida.");
                return true;
            }
                return false;
        } 
        private void TxtNascimento_MaskInputRejected(object sender, MaskInputRejectedEventArgs e) {

        }
    }
}
