namespace LaçosElô.Cadastro.Cliente {
    partial class MenuClientes {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuClientes));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btAdd = new System.Windows.Forms.Button();
            this.btEditar = new System.Windows.Forms.Button();
            this.btPdf = new System.Windows.Forms.Button();
            this.btCadastro = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btCadastro);
            this.groupBox1.Controls.Add(this.btPdf);
            this.groupBox1.Controls.Add(this.btEditar);
            this.groupBox1.Controls.Add(this.btAdd);
            this.groupBox1.ForeColor = System.Drawing.Color.Gray;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(235, 92);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ações";
            // 
            // btAdd
            // 
            this.btAdd.BackColor = System.Drawing.Color.Transparent;
            this.btAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btAdd.BackgroundImage")));
            this.btAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btAdd.FlatAppearance.BorderSize = 0;
            this.btAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btAdd.Location = new System.Drawing.Point(9, 37);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(48, 48);
            this.btAdd.TabIndex = 1;
            this.btAdd.UseVisualStyleBackColor = false;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btEditar
            // 
            this.btEditar.BackColor = System.Drawing.Color.Transparent;
            this.btEditar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btEditar.BackgroundImage")));
            this.btEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btEditar.Enabled = false;
            this.btEditar.FlatAppearance.BorderSize = 0;
            this.btEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btEditar.Location = new System.Drawing.Point(63, 37);
            this.btEditar.Name = "btEditar";
            this.btEditar.Size = new System.Drawing.Size(48, 48);
            this.btEditar.TabIndex = 1;
            this.btEditar.UseVisualStyleBackColor = false;
            // 
            // btPdf
            // 
            this.btPdf.BackColor = System.Drawing.Color.Transparent;
            this.btPdf.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btPdf.BackgroundImage")));
            this.btPdf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btPdf.Enabled = false;
            this.btPdf.FlatAppearance.BorderSize = 0;
            this.btPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btPdf.Location = new System.Drawing.Point(117, 38);
            this.btPdf.Name = "btPdf";
            this.btPdf.Size = new System.Drawing.Size(48, 48);
            this.btPdf.TabIndex = 1;
            this.btPdf.UseVisualStyleBackColor = false;
            // 
            // btCadastro
            // 
            this.btCadastro.BackColor = System.Drawing.Color.Transparent;
            this.btCadastro.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btCadastro.BackgroundImage")));
            this.btCadastro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btCadastro.Enabled = false;
            this.btCadastro.FlatAppearance.BorderSize = 0;
            this.btCadastro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCadastro.Location = new System.Drawing.Point(171, 37);
            this.btCadastro.Name = "btCadastro";
            this.btCadastro.Size = new System.Drawing.Size(48, 48);
            this.btCadastro.TabIndex = 1;
            this.btCadastro.UseVisualStyleBackColor = false;
            // 
            // MenuClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LaçosElô.Cadastro.Cliente.Properties.Resources.fundo_Azul;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Consolas", 11F);
            this.ForeColor = System.Drawing.Color.SteelBlue;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MenuClientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu Clientes Laços Elô";
            this.Load += new System.EventHandler(this.MenuClientes_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btCadastro;
        private System.Windows.Forms.Button btPdf;
        private System.Windows.Forms.Button btEditar;
    }
}