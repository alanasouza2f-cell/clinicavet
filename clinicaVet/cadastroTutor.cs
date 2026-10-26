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
    public partial class cadastroTutor : Form
    {
        DAO conexaoDao;

        public cadastroTutor()
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
                    string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(textBox5.Text) ||
                    string.IsNullOrWhiteSpace(textBox6.Text) ||
                    string.IsNullOrWhiteSpace(textBox7.Text) ||
                    string.IsNullOrWhiteSpace(textBox8.Text))
                {
                    MessageBox.Show("Por favor, preencha todos os campos!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nomeTutor = textBox1.Text.Trim();
                string telefone = textBox2.Text.Trim();

                string cpfStr = maskedTextBox1.Text.Replace(".", "").Replace("-", "").Trim();
                if (!long.TryParse(cpfStr, out long cpf))
                {
                    MessageBox.Show("CPF inválido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string email = textBox4.Text.Trim();
                string logradouro = textBox3.Text.Trim();

                string cepStr = textBox5.Text.Replace("-", "").Trim();
                if (!long.TryParse(cepStr, out long cep))
                {
                    MessageBox.Show("CEP inválido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(textBox6.Text.Trim(), out int numero))
                {
                    MessageBox.Show("Número do endereço inválido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string cidade = textBox7.Text.Trim();
                string estado = textBox8.Text.Trim();
                string pais = "Brasil";
                string senhaPadrao = "123456"; 

               
                string queryEndereco = "INSERT INTO endereco (logradouro, numero, complemento, cidade, estado, pais, CEP) VALUES (@logradouro, @numero, null, @cidade, @estado, @pais, @cep); SELECT LAST_INSERT_ID();";
                int enderecoCodigo = -1;

                using (MySqlCommand cmdEnd = new MySqlCommand(queryEndereco, conexaoDao.conexao))
                {
                    cmdEnd.Parameters.AddWithValue("@logradouro", logradouro);
                    cmdEnd.Parameters.AddWithValue("@numero", numero);
                    cmdEnd.Parameters.AddWithValue("@cidade", cidade);
                    cmdEnd.Parameters.AddWithValue("@estado", estado);
                    cmdEnd.Parameters.AddWithValue("@pais", pais);
                    cmdEnd.Parameters.AddWithValue("@cep", cep);

                    enderecoCodigo = Convert.ToInt32(cmdEnd.ExecuteScalar());
                }

                if (enderecoCodigo <= 0)
                {
                    MessageBox.Show("Erro ao cadastrar o endereço.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

               
                string queryTutor = "INSERT INTO tutor (nome, CPF, telefone, email, senha, enderecoCodigo) VALUES (@nome, @cpf, @telefone, @email, @senha, @enderecoCodigo)";

                using (MySqlCommand cmdTutor = new MySqlCommand(queryTutor, conexaoDao.conexao))
                {
                    cmdTutor.Parameters.AddWithValue("@nome", nomeTutor);
                    cmdTutor.Parameters.AddWithValue("@cpf", cpf);
                    cmdTutor.Parameters.AddWithValue("@telefone", telefone);
                    cmdTutor.Parameters.AddWithValue("@email", email);
                    cmdTutor.Parameters.AddWithValue("@senha", senhaPadrao);
                    cmdTutor.Parameters.AddWithValue("@enderecoCodigo", enderecoCodigo);

                    cmdTutor.ExecuteNonQuery();

                    MessageBox.Show("Tutor cadastrado com sucesso! A senha inicial padrão é '123456'.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar tutor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Por favor, informe o Nome do tutor que deseja editar no primeiro campo!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nomeTutor = textBox1.Text.Trim();
                string telefone = textBox2.Text.Trim();

                string cpfStr = maskedTextBox1.Text.Replace(".", "").Replace("-", "").Trim();
                if (!long.TryParse(cpfStr, out long cpf))
                {
                    MessageBox.Show("CPF inválido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string email = textBox4.Text.Trim();
                string logradouro = textBox3.Text.Trim();

                string cepStr = textBox5.Text.Replace("-", "").Trim();
                if (!long.TryParse(cepStr, out long cep))
                {
                    MessageBox.Show("CEP inválido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(textBox6.Text.Trim(), out int numero))
                {
                    MessageBox.Show("Número inválido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string cidade = textBox7.Text.Trim();
                string estado = textBox8.Text.Trim();

               
                string queryBuscaEnd = "SELECT enderecoCodigo FROM tutor WHERE nome = @nome LIMIT 1";
                int enderecoCodigo = -1;

                using (MySqlCommand cmdBusca = new MySqlCommand(queryBuscaEnd, conexaoDao.conexao))
                {
                    cmdBusca.Parameters.AddWithValue("@nome", nomeTutor);
                    object resultado = cmdBusca.ExecuteScalar();

                    if (resultado != null)
                    {
                        enderecoCodigo = Convert.ToInt32(resultado);
                    }
                }

                if (enderecoCodigo == -1)
                {
                    MessageBox.Show("Tutor não encontrado pelo nome informado. Verifique a grafia.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                
                string queryUpdateEnd = "UPDATE endereco SET logradouro = @logradouro, numero = @numero, cidade = @cidade, estado = @estado, CEP = @cep WHERE codigo = @endCodigo";
                using (MySqlCommand cmdUpEnd = new MySqlCommand(queryUpdateEnd, conexaoDao.conexao))
                {
                    cmdUpEnd.Parameters.AddWithValue("@logradouro", logradouro);
                    cmdUpEnd.Parameters.AddWithValue("@numero", numero);
                    cmdUpEnd.Parameters.AddWithValue("@cidade", cidade);
                    cmdUpEnd.Parameters.AddWithValue("@estado", estado);
                    cmdUpEnd.Parameters.AddWithValue("@cep", cep);
                    cmdUpEnd.Parameters.AddWithValue("@endCodigo", enderecoCodigo);
                    cmdUpEnd.ExecuteNonQuery();
                }

                
                string queryUpdateTutor = "UPDATE tutor SET telefone = @telefone, CPF = @cpf, email = @email WHERE nome = @nome";
                using (MySqlCommand cmdUpTutor = new MySqlCommand(queryUpdateTutor, conexaoDao.conexao))
                {
                    cmdUpTutor.Parameters.AddWithValue("@telefone", telefone);
                    cmdUpTutor.Parameters.AddWithValue("@cpf", cpf);
                    cmdUpTutor.Parameters.AddWithValue("@email", email);
                    cmdUpTutor.Parameters.AddWithValue("@nome", nomeTutor);
                    cmdUpTutor.ExecuteNonQuery();

                    MessageBox.Show("Dados do tutor atualizados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao editar tutor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//Fim editar Tutor

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }//Fim Voltar

        private void button1_Click(object sender, EventArgs e)
        {
            loginUsuario login = new loginUsuario();
            login.Show();
            this.Close();
        }//Fim logout

        private void LimparCampos()
        {
            textBox1.Clear();
            textBox2.Clear();
            maskedTextBox1.Clear();
            textBox4.Clear();
            textBox3.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox1.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim nome
        private void textBox2_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim telefone
        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e) 
        { 
        
        }//fim CPF
        private void textBox4_TextChanged(object sender, EventArgs e) 
        { 
        
        }//Fim email
        private void textBox3_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim endereco
        private void textBox5_TextChanged(object sender, EventArgs e) 
        { 
        
        }//Fim CEP
        private void textBox6_TextChanged(object sender, EventArgs e) 
        { 
        
        }//Fim numero
        private void textBox7_TextChanged(object sender, EventArgs e) 
        { 
        
        }//Fim Cidade
        private void textBox8_TextChanged(object sender, EventArgs e) 
        {
        
        }//Fim Estado
    }
}