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
    public partial class cadastroAnimal : Form
    {
        DAO conexaoDao; 

        public cadastroAnimal()
        {
            InitializeComponent();
            conexaoDao = new DAO(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loginUsuario login = new loginUsuario();
            login.Show();
            this.Close();
        }//fim Logout

        private void button2_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox4.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(textBox5.Text))
                {
                    MessageBox.Show("Por favor, preencha todos os campos!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nomeAnimal = textBox1.Text.Trim();
                string especie = textBox2.Text.Trim();
                string raca = maskedTextBox1.Text.Trim();

                if (!decimal.TryParse(textBox4.Text.Replace(',', '.'), out decimal peso))
                {
                    MessageBox.Show("Peso inválido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!decimal.TryParse(textBox3.Text.Replace(',', '.'), out decimal altura))
                {
                    MessageBox.Show("Altura inválida!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(textBox5.Text.Trim(), out int tutorCodigo))
                {
                    MessageBox.Show("Código do tutor inválido! Insira o ID numérico do tutor responsável.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                
                string queryVerificaTutor = "SELECT COUNT(*) FROM tutor WHERE codigo = @tutorCodigo";
                using (MySqlCommand cmdVerifica = new MySqlCommand(queryVerificaTutor, conexaoDao.conexao))
                {
                    cmdVerifica.Parameters.AddWithValue("@tutorCodigo", tutorCodigo);
                    int existe = Convert.ToInt32(cmdVerifica.ExecuteScalar());

                    if (existe == 0)
                    {
                        MessageBox.Show("O tutor informado não está cadastrado no sistema! Verifique o ID.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

               
                string query = "INSERT INTO animal (nome, especie, raca, dataDeNascimento, peso, altura, tutorCodigo) VALUES (@nome, @especie, @raca, NOW(), @peso, @altura, @tutorCodigo)";

                using (MySqlCommand cmd = new MySqlCommand(query, conexaoDao.conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", nomeAnimal);
                    cmd.Parameters.AddWithValue("@especie", especie);
                    cmd.Parameters.AddWithValue("@raca", raca);
                    cmd.Parameters.AddWithValue("@peso", peso);
                    cmd.Parameters.AddWithValue("@altura", altura);
                    cmd.Parameters.AddWithValue("@tutorCodigo", tutorCodigo);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Animal cadastrado e vinculado ao tutor com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimparCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar animal: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//fim cadastrar animal

       
        private void button4_Click(object sender, EventArgs e)
        {
           
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox5.Text))
                {
                    MessageBox.Show("Informe o nome do animal e o código do tutor para atualizar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nomeAnimal = textBox1.Text.Trim();
                string especie = textBox2.Text.Trim();
                string raca = maskedTextBox1.Text.Trim();

                if (!decimal.TryParse(textBox4.Text.Replace(',', '.'), out decimal peso) ||
                    !decimal.TryParse(textBox3.Text.Replace(',', '.'), out decimal altura) ||
                    !int.TryParse(textBox5.Text.Trim(), out int tutorCodigo))
                {
                    MessageBox.Show("Verifique se peso, altura e código do tutor são válidos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string query = "UPDATE animal SET especie = @especie, raca = @raca, peso = @peso, altura = @altura, tutorCodigo = @tutorCodigo WHERE nome = @nome";

                using (MySqlCommand cmd = new MySqlCommand(query, conexaoDao.conexao))
                {
                    cmd.Parameters.AddWithValue("@especie", especie);
                    cmd.Parameters.AddWithValue("@raca", raca);
                    cmd.Parameters.AddWithValue("@peso", peso);
                    cmd.Parameters.AddWithValue("@altura", altura);
                    cmd.Parameters.AddWithValue("@tutorCodigo", tutorCodigo);
                    cmd.Parameters.AddWithValue("@nome", nomeAnimal);

                    int linhasAfetadas = cmd.ExecuteNonQuery();

                    if (linhasAfetadas > 0)
                    {
                        MessageBox.Show("Dados do animal atualizados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimparCampos();
                    }
                    else
                    {
                        MessageBox.Show("Animal não encontrado para edição. Verifique o nome.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar animal: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }//fim voltar

        private void LimparCampos()
        {
            textBox1.Clear();
            textBox2.Clear();
            maskedTextBox1.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox1.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) 
        { 

        }//fim nome animal
        private void textBox2_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim especie
        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e) 
        { 
        
        }//fim raça
        private void textBox4_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim peso
        private void textBox3_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim altura
        private void textBox5_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim tutor
    }
}
