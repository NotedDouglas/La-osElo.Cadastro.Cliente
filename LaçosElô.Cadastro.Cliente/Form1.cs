using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LaçosElô.Cadastro.Cliente {
    public partial class ClientesLacosElo : Form {
        public ClientesLacosElo() {
            InitializeComponent();
        }

        string stringConexao = "server=localhost;Port=3306;database=cliente;user=root;Password=";
        string caminhoFotos = AppDomain.CurrentDomain.BaseDirectory + "Fotos/";

        private void Form1_Load(object sender, EventArgs e) {

            Funcoes.CarregaComboDistinct(ComboEndereco, "clientes", "endereco");
            Funcoes.CarregaComboDistinct(comboBairro, "clientes", "bairro");
            Funcoes.CarregaComboDistinct(comboCidade, "clientes", "cidade");

            if (TxtId.Text == "")
                return;

            btSalvar.Text = "Atualizar";    

            if (int.TryParse(TxtId.Text, out int id)) {
                CarregarCliente(id);
            }

        }
        private void PreencherCampos(DataRow row) {
            TxtId.Text = row["id"].ToString();
            TxtNome.Text = row["nome"].ToString();
            TxtRg.Text = row["rg"].ToString();
            comboEstadoCivil.Text = row["estado_civil"].ToString();
            TxtNascimento.Text = row["nasc"].ToString();
            TxtCep.Text = row["cep"].ToString();
            ComboEndereco.Text = row["endereco"].ToString();
            TxtNumero.Text = row["numero"].ToString();
            comboBairro.Text = row["bairro"].ToString();
            comboCidade.Text = row["cidade"].ToString();
            comboEstado.Text = row["estado"].ToString();
            TxtCelular.Text = row["celular"].ToString();
            TxtEmail.Text = row["email"].ToString();
            TxtObs.Text = row["obs"].ToString();

            // ===== GÊNERO =====
            switch (row["genero"]?.ToString()) {
                case "Masculino":
                    OpMasculino.Checked = true;
                    break;

                case "Feminino":
                    OpFeminino.Checked = true;
                    break;

                case "Outro":
                    OpOutros.Checked = true;
                    break;
            }

            // ===== DOCUMENTO =====
            string documento = row["documento"]?.ToString() ?? "";

            // remove qualquer caractere que não seja número
            string somenteNumeros = new string(documento.Where(char.IsDigit).ToArray());

            if (somenteNumeros.Length == 11) {
                OpCpf.Checked = true;
            } else if (somenteNumeros.Length == 14) {
                OpCnpj.Checked = true;
            }

            TxtDoc.Text = documento;

            // ===== SITUAÇÃO =====
            if (row["situacao"]?.ToString() == "Ativo") {
                Cksituacao.Checked = true;
            } else {
                Cksituacao.Checked = false;
            }

            // ===== IMAGEM =====
            if (File.Exists(caminhoFotos + TxtId.Text + ".png"))
                imgCliente.Image = Image.FromFile(caminhoFotos + TxtId.Text + ".png");

            else {
                imgCliente.Image = Properties.Resources.avatar_icone;
            }

        }

        private void CarregarCliente(int id) {
            ClienteRepository repo = new ClienteRepository();
            DataTable dt = repo.BuscarClientePorId(id);

            if (dt.Rows.Count > 0)
                PreencherCampos(dt.Rows[0]);
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
            using (MySqlConnection conexao = new MySqlConnection(stringConexao)) {
                conexao.Open();

                using (MySqlCommand comando = conexao.CreateCommand()) {
                    bool novoRegistro = string.IsNullOrWhiteSpace(TxtId.Text);

                    if (novoRegistro) {
                        comando.CommandText = @"INSERT INTO clientes 
                (nome, documento, genero, rg, estado_civil, nasc, cep, endereco,
                 numero, bairro, cidade, estado, celular, email, obs, situacao)
                VALUES
                (@nome, @doc, @genero, @rg, @es_civil, @nasc, @cep, @endereco,
                 @numero, @bairro, @cidade, @estado, @celular, @email, @obs, @situacao)";
                    } else {
                        comando.CommandText = @"UPDATE clientes SET
                    nome=@nome,
                    documento=@doc,
                    genero=@genero,
                    rg=@rg,
                    estado_civil=@es_civil,
                    nasc=@nasc,
                    cep=@cep,
                    endereco=@endereco,
                    numero=@numero,
                    bairro=@bairro,
                    cidade=@cidade,
                    estado=@estado,
                    celular=@celular,
                    email=@email,
                    obs=@obs,
                    situacao=@situacao
                    WHERE id=@id";

                        comando.Parameters.AddWithValue("@id", TxtId.Text);
                    }

                    // ===== GÊNERO =====
                    string genero = null;
                    if (OpMasculino.Checked) genero = "Masculino";
                    else if (OpFeminino.Checked) genero = "Feminino";
                    else if (OpOutros.Checked) genero = "Outro";

                    // ===== SITUAÇÃO =====
                    string situacao = Cksituacao.Checked ? "Ativo" : "Inativo";

                    // ===== PARÂMETROS =====
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
                    comando.Parameters.AddWithValue("@estado", comboEstado.Text);
                    comando.Parameters.AddWithValue("@celular", TxtCelular.Text);
                    comando.Parameters.AddWithValue("@email", TxtEmail.Text);
                    comando.Parameters.AddWithValue("@obs", TxtObs.Text);
                    comando.Parameters.AddWithValue("@situacao", situacao);

                    int resultado = comando.ExecuteNonQuery();

                    if (resultado > 0) {
                        if (novoRegistro)
                            TxtId.Text = comando.LastInsertedId.ToString();

                        MessageBox.Show("Cliente salvo com sucesso!");
                    } else {
                        MessageBox.Show("Falha ao salvar cliente.");
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

            comboEstadoCivil.SelectedItem = null;

            ComboEndereco.SelectedItem = null;
            ComboEndereco.ResetText();

            comboBairro.SelectedItem = null;
            comboBairro.ResetText();

            comboCidade.SelectedItem = null;
            comboCidade.ResetText();

            comboEstado.SelectedItem = null;
            comboEstado.ResetText();

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
            btSalvar.Text = "Salvar";
            imgCliente.Image = Properties.Resources.avatar_icone;
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
        private void TxtNome_TextChanged(object sender, EventArgs e) {

            TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;

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

            TxtNome.Text = texto;
            TxtNome.SelectionStart = TxtNome.Text.Length; // Mantém o cursor no final do texto
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
                if (TxtDoc.Text.Replace(" ", "").Length < 11) {
                    MessageBox.Show("CPF deve conter 11 dígitos.");
                    e.Cancel = true;
                }
            } else if (OpCnpj.Checked == true) {
                if (TxtDoc.Text.Replace(" ", "").Length < 14) {
                    MessageBox.Show("CNPJ deve conter 14 dígitos.");
                    e.Cancel = true;
                }
            }
        }

        private void ComboEndereco_TextChanged(object sender, EventArgs e) {
            ComboEndereco.TextChanged -= ComboEndereco_TextChanged;

            Funcoes.PrimeiraMaius(ComboEndereco);

            ComboEndereco.TextChanged += ComboEndereco_TextChanged;
        }

        private void imgCliente_Click(object sender, EventArgs e) {

        }

        private void btImagemAdd_Click(object sender, EventArgs e) {

            if (TxtId.Text == "") {
                Funcoes.MsgErro("Salve os dados do cliente primeiro.");
                return;
            }

            OpenFileDialog caixa = new OpenFileDialog();
            caixa.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.bmp;*gif";

            if (caixa.ShowDialog() == DialogResult.OK ) {
                imgCliente.Image = Image.FromFile(caixa.FileName);

                File.Copy(caixa.FileName, 
                    caminhoFotos + TxtId.Text + ".png");
            }
        }
        private void button2_Click(object sender, EventArgs e) {

            if (TxtId.Text == "") {
                Funcoes.MsgAlerta("Não há fotos para ser removida");
                return;
            }

            if (File.Exists(caminhoFotos + TxtId.Text + ".png") == false) {
                Funcoes.MsgAlerta("Não há foto para remover.");
                return;
            }

            if (Funcoes.Pergunta("Deseja remover a foto do cliente?") == false)
                return;

            imgCliente.Image = Properties.Resources.avatar_icone;
            File.Delete(caminhoFotos + TxtId.Text + ".png");
        }
    }
    
}
