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
            SalvarClinteMySql();
        }

        private void ClientesLacosElo_KeyDown(object sender, KeyEventArgs e) {

            if (e.KeyCode == Keys.Enter) {
                e.SuppressKeyPress = true; // evita o "beep"
                SendKeys.Send("{TAB}");
            }
        }

        private void button6_Click(object sender, EventArgs e) {

            using (MySqlConnection conexao = new MySqlConnection("server=localhost;Port=3306;database=dados_ clientes;user=root;Password=")) {

                conexao.Open();

                using (MySqlCommand comando = conexao.CreateCommand()) {
                    comando.CommandText = "INSERT INTO clientes (nome, documento, genero, rg, estado_civil, data_nascimento, cep, endereco," +
                        "numero, bairro, cidade, celular, email, obs, situacao) " +
                        "VALUES " +
                        "(@nome, @doc, @genero, @rg, @es_civil, @data_nasc, @cep, @endereco, @numero, @bairro, @cidade, @celular, @email, @obs, @situacao)";

                    comando.Parameters.AddWithValue("@nome", TxtNome.Text);
                    comando.Parameters.AddWithValue("@doc", TxtDoc.Text);

                    comando.Parameters.AddWithValue("@genero", "genero");

                    comando.Parameters.AddWithValue("@rg", TxtRg.Text);
                    comando.Parameters.AddWithValue("@es_civil", comboEstadoCivil.Text);
                    comando.Parameters.AddWithValue("@data_nasc", TxtNascimento.Text);
                    comando.Parameters.AddWithValue("@cep", TxtCep.Text);
                    comando.Parameters.AddWithValue("@endereco", ComboEndereco.Text);
                    comando.Parameters.AddWithValue("@numero", TxtNumero.Text);
                    comando.Parameters.AddWithValue("@bairro", comboBairro.Text);
                    comando.Parameters.AddWithValue("@cidade", comboCidade.Text);
                    comando.Parameters.AddWithValue("@celular", TxtCelular.Text);
                    comando.Parameters.AddWithValue("@email", TxtEmail.Text);
                    comando.Parameters.AddWithValue("@obs", TxtObs.Text);
                    comando.Parameters.AddWithValue("@situacao", Cksituacao.Text);

                    try {
                        int resultado = comando.ExecuteNonQuery();
                        if (resultado > 0) {
                            MessageBox.Show("Cliente cadastrado com sucesso!");
                        } else {
                            MessageBox.Show("Falha ao cadastrar cliente.");
                        }
                    } catch (Exception ex) {
                        MessageBox.Show("Erro ao executar comando: " + ex.Message);
                    }
                }
            }

        }

        private void SalvarClinteMySql() {
            using (MySqlConnection conexao = new MySqlConnection("server=localhost;Port=3306;database=dados_ clientes;user=root;Password=")) {

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

                    comando.Parameters.AddWithValue("@nome", TxtNome.Text);
                    comando.Parameters.AddWithValue("@doc", TxtDoc.Text);

                    comando.Parameters.AddWithValue("@genero", (object)genero ?? DBNull.Value);

                    comando.Parameters.AddWithValue("@rg", TxtRg.Text);
                    comando.Parameters.AddWithValue("@es_civil", comboEstadoCivil.Text);

                    comando.Parameters.Add("@nasc", MySqlDbType.Date).Value =DateTime.ParseExact(TxtNascimento.Text, "dd/MM/yyyy",System.Globalization.CultureInfo.InvariantCulture);


                    comando.Parameters.AddWithValue("@cep", TxtCep.Text);
                    comando.Parameters.AddWithValue("@endereco", ComboEndereco.Text);
                    comando.Parameters.AddWithValue("@numero", TxtNumero.Text);
                    comando.Parameters.AddWithValue("@bairro", comboBairro.Text);
                    comando.Parameters.AddWithValue("@cidade", comboCidade.Text);
                    comando.Parameters.AddWithValue("@celular", TxtCelular.Text);
                    comando.Parameters.AddWithValue("@email", TxtEmail.Text);
                    comando.Parameters.AddWithValue("@obs", TxtObs.Text);
                    comando.Parameters.AddWithValue("@situacao", Cksituacao.Text);

                    try {
                        int resultado = comando.ExecuteNonQuery();
                        if (resultado > 0) {
                            MessageBox.Show("Cliente cadastrado com sucesso!");
                        } else {
                            MessageBox.Show("Falha ao cadastrar cliente.");
                        }
                    } catch (Exception ex) {
                        MessageBox.Show("Erro ao executar comando: " + ex.Message);
                    }
                }
            }

        }

        private void TxtNascimento_MaskInputRejected(object sender, MaskInputRejectedEventArgs e) {

        }
    }
}
