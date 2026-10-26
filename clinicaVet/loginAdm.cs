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
    public partial class loginAdm : Form
    {
        DAO conexaoDao; 

        public loginAdm()
        {
            InitializeComponent();
            conexaoDao = new DAO(); 
            textBox3.UseSystemPasswordChar = true; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Por favor, preencha o CRMV/ID e a Senha!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!long.TryParse(textBox1.Text.Trim(), out long idInformado))
                {
                    MessageBox.Show("O CRMV/ID deve ser um número válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string senha = textBox3.Text.Trim();

                
                string query = "SELECT codigo, CRMV, nome, area FROM veterinario " +
                               "WHERE (CRMV = @id OR codigo = @id) AND senha = @senha LIMIT 1";

                bool achou = false;
                string nome = "";
                string area = "";

                using (MySqlCommand cmd = new MySqlCommand(query, conexaoDao.conexao))
                {
                    cmd.Parameters.AddWithValue("@id", idInformado);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            achou = true;
                            nome = dr["nome"].ToString();
                            area = dr["area"].ToString();
                        }
                    }
                }

                if (!achou)
                {
                    MessageBox.Show("Credenciais inválidas! Verifique o CRMV/ID e a senha informados.", "Erro de Autenticação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show($"Login efetuado com sucesso! Bem-vindo(a).", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

              
                string areaLower = area.ToLower().Trim();

                
                if (areaLower.Contains("adm") || areaLower.Contains("gestao") || areaLower.Contains("administração"))
                {
                    telaAdministração painelAdmin = new telaAdministração();
                    painelAdmin.Show();
                    this.Hide();
                }
                else if (areaLower.Contains("vet") || areaLower.Contains("medico") || areaLower.Contains("médico") || areaLower.Contains("doutor"))
                {
                    telaDoutor painelDoutor = new telaDoutor();
                    painelDoutor.Show();
                    this.Hide();
                }
                else if (areaLower.Contains("recepção") || areaLower.Contains("recepcao") || areaLower.Contains("atendimento"))
                {
                    recepcao painelRecepcao = new recepcao();
                    painelRecepcao.Show();
                    this.Hide();
                }
                else
                {
                    telaAdministração painelAdmin = new telaAdministração();
                    painelAdmin.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao realizar login: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//fim entrar

        private void button2_Click(object sender, EventArgs e)
        {
           
            Form1 principal = new Form1();
            principal.Show();
            this.Close();
        }//fim voltar login

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }// Fim crmv/id

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }// Fim senha
    }
}
