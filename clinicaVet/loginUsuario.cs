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
    public partial class loginUsuario : Form
    {
        DAO conexaoDao;

        public loginUsuario()
        {
            InitializeComponent();
            conexaoDao = new DAO();

            
            panel1.BackColor = Color.FromArgb(120, 255, 255, 255);
            textBox2.UseSystemPasswordChar = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Por favor, preencha o e-mail e a senha!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string email = textBox1.Text.Trim();
                string senha = textBox2.Text.Trim();

                string query = "SELECT COUNT(*) FROM tutor WHERE email = @email AND senha = @senha";

                using (MySqlCommand cmd = new MySqlCommand(query, conexaoDao.conexao))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    int resultado = Convert.ToInt32(cmd.ExecuteScalar());

                    if (resultado > 0)
                    {
                        MessageBox.Show("Login efetuado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        usuario telaUsuario = new usuario(email);
                        telaUsuario.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("E-mail ou senha incorretos!", "Erro de Autenticação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao realizar login: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//fim proximo

        private void button2_Click(object sender, EventArgs e)
        { 
            Form1 principal = new Form1();
            principal.Show();
            this.Close();
        }//fim voltar

        private void button4_Click(object sender, EventArgs e)
        {
            recuperacaoSenha telaRecuperacao = new recuperacaoSenha();
            telaRecuperacao.Show();
            this.Hide();
        }//fim recuperação de senha

        private void button3_Click(object sender, EventArgs e)
        {
            primeiroAcesso telaPrimeiro = new primeiroAcesso();
            telaPrimeiro.Show();
            this.Hide();
        }//fim primeiro acesso

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }// Fim email

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }// Fim senha

        private void tutor_Click(object sender, EventArgs e) 
        { 

        }
        private void texto1_Click(object sender, EventArgs e) 
        { 
        
        }
        private void panel1_Paint(object sender, PaintEventArgs e) 
        { 
        
        }
    }
}
