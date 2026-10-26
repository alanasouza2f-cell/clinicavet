namespace clinicaVet
{
    partial class primeiroAcesso
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.email = new System.Windows.Forms.TextBox();
            this.senha = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.botaoSalvar = new System.Windows.Forms.Button();
            this.botaoVoltar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(237, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(279, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Primeiro Acesso ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(191, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "E-Mail Cadastrado";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(191, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Criar Senha";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(191, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = "Repetir Senha";
            // 
            // email
            // 
            this.email.Location = new System.Drawing.Point(191, 117);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(325, 20);
            this.email.TabIndex = 4;
            this.email.TextChanged += new System.EventHandler(this.email_TextChanged);
            // 
            // senha
            // 
            this.senha.Location = new System.Drawing.Point(193, 163);
            this.senha.Name = "senha";
            this.senha.Size = new System.Drawing.Size(323, 20);
            this.senha.TabIndex = 5;
            this.senha.TextChanged += new System.EventHandler(this.senha_TextChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(194, 209);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(322, 20);
            this.textBox3.TabIndex = 6;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // botaoSalvar
            // 
            this.botaoSalvar.BackColor = System.Drawing.SystemColors.Highlight;
            this.botaoSalvar.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botaoSalvar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.botaoSalvar.Location = new System.Drawing.Point(191, 261);
            this.botaoSalvar.Name = "botaoSalvar";
            this.botaoSalvar.Size = new System.Drawing.Size(325, 34);
            this.botaoSalvar.TabIndex = 7;
            this.botaoSalvar.Text = "Salvar";
            this.botaoSalvar.UseVisualStyleBackColor = false;
            this.botaoSalvar.Click += new System.EventHandler(this.botaoSalvar_Click);
            // 
            // botaoVoltar
            // 
            this.botaoVoltar.BackColor = System.Drawing.Color.Transparent;
            this.botaoVoltar.FlatAppearance.BorderSize = 0;
            this.botaoVoltar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botaoVoltar.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botaoVoltar.Location = new System.Drawing.Point(194, 398);
            this.botaoVoltar.Margin = new System.Windows.Forms.Padding(0);
            this.botaoVoltar.Name = "botaoVoltar";
            this.botaoVoltar.Size = new System.Drawing.Size(322, 37);
            this.botaoVoltar.TabIndex = 8;
            this.botaoVoltar.Text = "Voltar para o Login";
            this.botaoVoltar.UseVisualStyleBackColor = false;
            this.botaoVoltar.Click += new System.EventHandler(this.botaoVoltar_Click);
            // 
            // primeiroAcesso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 473);
            this.Controls.Add(this.botaoVoltar);
            this.Controls.Add(this.botaoSalvar);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.senha);
            this.Controls.Add(this.email);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "primeiroAcesso";
            this.Text = "primeiroAcesso";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox email;
        private System.Windows.Forms.TextBox senha;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button botaoSalvar;
        private System.Windows.Forms.Button botaoVoltar;
    }
}