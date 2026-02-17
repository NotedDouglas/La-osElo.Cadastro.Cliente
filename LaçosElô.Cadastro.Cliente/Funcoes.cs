
using System.Globalization;
using System.Windows.Forms;

namespace LaçosElô.Cadastro.Cliente {
    internal class Funcoes {

        public static void MsgErro(string msg) {
            System.Windows.Forms.MessageBox.Show(msg, "Laços Elô", 
            System.Windows.Forms.MessageBoxButtons.OK, 
            System.Windows.Forms.MessageBoxIcon.Error);
        }

        public static void MsgAlerta(string msg) {
            System.Windows.Forms.MessageBox.Show(msg, "Laços Elô",
            System.Windows.Forms.MessageBoxButtons.OK,
            System.Windows.Forms.MessageBoxIcon.Warning);
        }

        public static void MsgInformacao(string msg) {
            System.Windows.Forms.MessageBox.Show(msg, "Laços Elô",
            System.Windows.Forms.MessageBoxButtons.OK,
            System.Windows.Forms.MessageBoxIcon.Information);
        }

        public static bool Pergunta(string msg) {

            if (MessageBox.Show(msg,
                     "Laços Elô",
                     MessageBoxButtons.YesNo,
                     MessageBoxIcon.Question) == DialogResult.Yes) 
                return true;
            else 
                return false; 
                
        }

        public static void PrimeiraMaius(Control ctr) {
            if (string.IsNullOrWhiteSpace(ctr.Text))
                return;

            TextInfo textInfo = new CultureInfo("pt-BR").TextInfo;

            string texto = textInfo.ToTitleCase(ctr.Text.ToLower());

            texto = texto.Replace(" De ", " de ")
                         .Replace(" Da ", " da ")
                         .Replace(" Do ", " do ")
                         .Replace(" E ", " e ")
                         .Replace(" Em ", " em ")
                         .Replace(" Na ", " na ")
                         .Replace(" No ", " no ");

            ctr.Text = texto;

            if (ctr is TextBox txt) {
                txt.SelectionStart = txt.Text.Length;
            } else if (ctr is ComboBox cmb && cmb.DropDownStyle != ComboBoxStyle.DropDownList) {
                cmb.SelectionStart = cmb.Text.Length;
            }
        }
    }
}
