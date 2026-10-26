using MySql.Data.MySqlClient; 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clinicaVet
{
    public partial class recuperacaoSenha : Form
    {
        DAO conexaoDao; 

        public recuperacaoSenha()
        {
            InitializeComponent();
            conexaoDao = new DAO(); 

            panel1.BackColor = Color.FromArgb(120, 255, 255, 255);

            
            textBox2.UseSystemPasswordChar = true; 
            textBox3.UseSystemPasswordChar = true; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Por favor, preencha todos os campos!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string email = textBox1.Text.Trim();
                string novaSenha = textBox2.Text.Trim();
                string confirmarSenha = textBox3.Text.Trim();

                if (novaSenha != confirmarSenha)
                {
                    MessageBox.Show("As senhas não coincidem! Verifique e tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                
                string queryVerifica = "SELECT COUNT(*) FROM tutor WHERE email = @email";
                int existe = 0;

                using (MySqlCommand cmdVerifica = new MySqlCommand(queryVerifica, conexaoDao.conexao))
                {
                    cmdVerifica.Parameters.AddWithValue("@email", email);
                    existe = Convert.ToInt32(cmdVerifica.ExecuteScalar());
                }

                if (existe == 0)
                {
                    MessageBox.Show("E-mail não encontrado no sistema! Verifique se foi digitado corretamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string queryUpdate = "UPDATE tutor SET senha = @senha WHERE email = @email";

                using (MySqlCommand cmdUpdate = new MySqlCommand(queryUpdate, conexaoDao.conexao))
                {
                    cmdUpdate.Parameters.AddWithValue("@senha", novaSenha);
                    cmdUpdate.Parameters.AddWithValue("@email", email);

                    cmdUpdate.ExecuteNonQuery();

                    MessageBox.Show("Senha alterada com sucesso! Utilize a sua nova senha para fazer login.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    loginUsuario telaLogin = new loginUsuario();
                    telaLogin.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao redefinir a senha: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//fim salvar

        private void button2_Click(object sender, EventArgs e)
        {
            loginUsuario telaLogin = new loginUsuario();
            telaLogin.Show();
            this.Close();
        }//fim voltar para o login

        private void panel1_Paint(object sender, PaintEventArgs e) 
        { 
        
        }
        private void textBox1_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim email
        private void textBox2_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim criar nova senha
        private void textBox3_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim confirmar nova senha
    }
}