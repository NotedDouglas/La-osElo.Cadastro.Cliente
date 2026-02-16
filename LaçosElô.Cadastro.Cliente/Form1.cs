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

        private void Genero_Enter(object sender, EventArgs e) {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) {

        }

        private void button3_Click(object sender, EventArgs e) {
            if (Validacoes()) {
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
        private void LimparCampos() {
            TxtId.Clear();
            TxtNome.Clear();
            TxtDoc.Clear();
            TxtRg.Clear();
            TxtNascimento.Clear();
            TxtCep.Clear();
            TxtNumero.Clear();
            TxtCelular.Clear();
            TxtEmail.Clear();
            TxtObs.Clear();

            comboEstadoCivil.SelectedIndex = -1;
            comboBairro.SelectedIndex = -1;
            comboCidade.SelectedIndex = -1;
            ComboEndereco.SelectedIndex = -1;

            OpCpf.Checked = false;
            OpCnpj.Checked = false;

            OpMasculino.Checked = false;
            OpFeminino.Checked = false;
            OpOutros.Checked = false;

            Cksituacao.Checked = true; // deixa como padrão ativo

            TxtNome.Focus();
        }

        private void BtNovo_Click(object sender, EventArgs e) {
            var resposta = MessageBox.Show(
                "Limpar campos para novo cadastro?",
                "Laços Elô",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resposta == DialogResult.Yes) {
                LimparCampos();
            
            }
        }
        private void BtFechar_Click(object sender, EventArgs e) {
            Close();
        }

        private void OpCpf_CheckedChanged(object sender, EventArgs e) {
            if (OpCpf.Checked) {
                TxtDoc.Mask = "000,000,000-00";
                TxtDoc.Focus();
                TxtDoc.Clear();
            }
        }

        private void OpCNPJ_CheckedChanged(object sender, EventArgs e) {
            if (OpCnpj.Checked) {
                TxtDoc.Mask = "00,000,000/0000-00";
                TxtDoc.Focus();
                TxtDoc.Clear();
            }
        }

        private void OpMasculino_CheckedChanged(object sender, EventArgs e) {
            TxtRg.Focus();
        }

        private void OpFeminino_CheckedChanged(object sender, EventArgs e) {
            TxtRg.Focus();
        }

        private void OpOutros_CheckedChanged(object sender, EventArgs e) {
            TxtRg.Focus();
        }

        private void comboEstadoCivil_SelectedIndexChanged(object sender, EventArgs e) {
            
        }

        private void TxtNascimento_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            e.Cancel = false; // Permite que o foco seja perdido mesmo que a validação falhe
            if (TxtNascimento.Text != "  /  /") {
                try {
                    Convert.ToDateTime(TxtNascimento.Text);
                } catch (Exception) {
                    MessageBox.Show("Data de Nascimento Não Valida.");
                    TxtNascimento.Focus();
                }
            }
        }
        private void comboEstado_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            if (comboEstado.Text == "") 
                return;
            
            if (comboEstado.SelectedIndex == -1) {
                MessageBox.Show("Selecione um estado válido.");
                e.Cancel = true;
            }
        }

        TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;
        private void TxtNome_TextChanged(object sender, EventArgs e) {
            string texto = TxtNome.Text;

            texto = textInfo.ToTitleCase(texto);

            texto = texto.Replace(" De ", " de ")
                         .Replace(" Da ", " da ")
                         .Replace(" Do ", " do ")
                         .Replace(" E ", " e ")
                         .Replace(" I ", " i ")
                         .Replace(" Em ", " em ")
                         .Replace(" Na ", " na ")
                         .Replace(" No ", " no ");  
        }

        private void TxtCep_Validating(object sender, System.ComponentModel.CancelEventArgs e) {

             if (TxtCep.Text.Length == 0) {
                return;
             }
             else if (TxtCep.Text.Length < 8) { 
                MessageBox.Show("CEP deve conter 8 dígitos.");
                e.Cancel = true;
             }
        }

        private void TxtDoc_Validating(object sender, System.ComponentModel.CancelEventArgs e) {

            if (TxtDoc.Text.Length == 0) {
                return;
            }

            if (OpCpf.Checked == true) {
                if (TxtDoc.Text.Length < 11) {
                    MessageBox.Show("CPF deve conter 11 dígitos.");
                    e.Cancel = true;
                }
            } else if (OpCnpj.Checked == true) {
                if (TxtDoc.Text.Length < 14) {
                    MessageBox.Show("CNPJ deve conter 14 dígitos.");
                    e.Cancel = true;
                }
            }
        }
    }
    
}
