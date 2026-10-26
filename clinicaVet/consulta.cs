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
    public partial class consulta : Form
    {
        DAO conexaoDao; 

        public consulta()
        {
            InitializeComponent();
            conexaoDao = new DAO();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            try
            {
                
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(maskedTextBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox4.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Por favor, preencha todos os campos!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                
                if (!int.TryParse(textBox1.Text.Trim(), out int animalCodigo))
                {
                    MessageBox.Show("Código do animal inválido! Insira o ID numérico.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

               
                if (!int.TryParse(textBox2.Text.Trim(), out int veterinarioCodigo))
                {
                    MessageBox.Show("Código do veterinário inválido! Insira o ID numérico.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string diagnostico = maskedTextBox1.Text.Trim();
                string tratamento = textBox4.Text.Trim();

                
                if (!DateTime.TryParse(textBox3.Text.Trim(), out DateTime dataAtendimento))
                {
                    MessageBox.Show("Data inválida! Utilize o formato correto (ex: AAAA-MM-DD HH:MM:SS ou DD/MM/AAAA).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                
                string queryVerificaAnimal = "SELECT COUNT(*) FROM animal WHERE codigo = @animalCodigo";
                using (MySqlCommand cmdAnim = new MySqlCommand(queryVerificaAnimal, conexaoDao.conexao))
                {
                    cmdAnim.Parameters.AddWithValue("@animalCodigo", animalCodigo);
                    int existeAnimal = Convert.ToInt32(cmdAnim.ExecuteScalar());
                    if (existeAnimal == 0)
                    {
                        MessageBox.Show("O animal informado não está cadastrado no sistema!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

               
                string queryVerificaVet = "SELECT COUNT(*) FROM veterinario WHERE codigo = @vetCodigo";
                using (MySqlCommand cmdVet = new MySqlCommand(queryVerificaVet, conexaoDao.conexao))
                {
                    cmdVet.Parameters.AddWithValue("@vetCodigo", veterinarioCodigo);
                    int existeVet = Convert.ToInt32(cmdVet.ExecuteScalar());
                    if (existeVet == 0)
                    {
                        MessageBox.Show("O veterinário informado não está cadastrado no sistema!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

               
                string query = "INSERT INTO atendimento (diagnostico, tratamento, dataAtendimento, animalCodigo, veterinarioCodigo) VALUES (@diagnostico, @tratamento, @dataAtendimento, @animalCodigo, @veterinarioCodigo)";

                using (MySqlCommand cmd = new MySqlCommand(query, conexaoDao.conexao))
                {
                    cmd.Parameters.AddWithValue("@diagnostico", diagnostico);
                    cmd.Parameters.AddWithValue("@tratamento", tratamento);
                    cmd.Parameters.AddWithValue("@dataAtendimento", dataAtendimento);
                    cmd.Parameters.AddWithValue("@animalCodigo", animalCodigo);
                    cmd.Parameters.AddWithValue("@veterinarioCodigo", veterinarioCodigo);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Nova consulta criada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    textBox1.Clear();
                    textBox2.Clear();
                    maskedTextBox1.Clear();
                    textBox4.Clear();
                    textBox3.Clear();
                    textBox1.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao criar consulta: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//fim criar nova consulta

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }//fim voltar

        private void button1_Click(object sender, EventArgs e)
        {
            loginAdm login = new loginAdm();
            login.Show();
            this.Close();
        }//fim logout

        private void textBox1_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim animal
        private void textBox2_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim veterinario
        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e) 
        { 
        
        }//fim diagnostico
        private void textBox4_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim tratamento
        private void textBox3_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim data
    }
}
