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
    public partial class primeiroAcesso : Form
    {
        DAO conexaoDao; 

        public primeiroAcesso()
        {
            InitializeComponent();
            conexaoDao = new DAO(); 

            senha.UseSystemPasswordChar = true; 
            textBox3.UseSystemPasswordChar = true; 
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void email_TextChanged(object sender, EventArgs e)
        {

        }// Fim email cadastrado

        private void senha_TextChanged(object sender, EventArgs e)
        {

        }// Fim criar senha

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }// Fim repetir senha

        private void botaoSalvar_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (string.IsNullOrWhiteSpace(email.Text) || string.IsNullOrWhiteSpace(senha.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Por favor, preencha todos os campos!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string emailCadastrado = email.Text.Trim();
                string novaSenha = senha.Text.Trim();
                string repetirSenha = textBox3.Text.Trim();

               
                if (novaSenha != repetirSenha)
                {
                    MessageBox.Show("As senhas não coincidem! Verifique e tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                string queryVerifica = "SELECT senha FROM tutor WHERE email = @email";
                string senhaAtual = null;
                bool emailExiste = false;

                using (MySqlCommand cmdVerifica = new MySqlCommand(queryVerifica, conexaoDao.conexao))
                {
                    cmdVerifica.Parameters.AddWithValue("@email", emailCadastrado);
                    using (MySqlDataReader reader = cmdVerifica.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            emailExiste = true;
                            senhaAtual = reader["senha"]?.ToString();
                        }
                    }
                }

                if (!emailExiste)
                {
                    MessageBox.Show("E-mail não encontrado no sistema! Verifique se foi cadastrado previamente pela recepção.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

               
                string queryUpdate = "UPDATE tutor SET senha = @senha WHERE email = @email";

                using (MySqlCommand cmdUpdate = new MySqlCommand(queryUpdate, conexaoDao.conexao))
                {
                    cmdUpdate.Parameters.AddWithValue("@senha", novaSenha);
                    cmdUpdate.Parameters.AddWithValue("@email", emailCadastrado);

                    cmdUpdate.ExecuteNonQuery();

                    MessageBox.Show("Senha cadastrada com sucesso! Agora você já pode fazer login no sistema.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    
                    loginUsuario telaLogin = new loginUsuario();
                    telaLogin.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar a senha: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//fim salvar

        private void botaoVoltar_Click(object sender, EventArgs e)
        {
           
            loginUsuario telaLogin = new loginUsuario();
            telaLogin.Show();
            this.Close();
        }//fim voltar para login
    }
}
