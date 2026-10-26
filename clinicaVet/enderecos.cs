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
    public partial class enderecos : Form
    {
        DAO conexaoDao; 
        public enderecos()
        {
            InitializeComponent();
            conexaoDao = new DAO(); 
            CarregarEnderecosGrid(); 
        }

        private void CarregarEnderecosGrid()
        {
            try
            {
                string query = "SELECT e.codigo AS Código, t.nome AS Tutor, e.CEP AS CEP, e.logradouro AS Logradouro, e.numero AS Número, e.cidade AS Cidade, e.estado AS Estado FROM endereco e INNER JOIN tutor t ON t.enderecoCodigo = e.codigo";

                using (MySqlCommand cmd = new MySqlCommand(query, conexaoDao.conexao))
                {
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar endereços: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["Tutor"].Value.ToString();
                maskedTextBox1.Text = row.Cells["CEP"].Value.ToString();
                textBox3.Text = row.Cells["Logradouro"].Value.ToString();
                textBox2.Text = row.Cells["Número"].Value.ToString(); 
                textBox4.Text = row.Cells["Cidade"].Value.ToString();
            }
        }//fim view enderecos

        private void button1_Click(object sender, EventArgs e)
        {
            loginAdm login = new loginAdm();
            login.Show();
            this.Close();
        }//fim logout

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Por favor, informe o Nome do Tutor para realizar a busca!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nomeTutor = textBox1.Text.Trim();
                bool encontrou = false;

                string query = "SELECT e.CEP, e.logradouro, e.numero, e.cidade FROM endereco e INNER JOIN tutor t ON t.enderecoCodigo = e.codigo WHERE t.nome LIKE @nome LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(query, conexaoDao.conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", "%" + nomeTutor + "%");

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            encontrou = true;
                            maskedTextBox1.Text = dr["CEP"].ToString();
                            textBox3.Text = dr["logradouro"].ToString();
                            textBox2.Text = dr["numero"].ToString();    
                            textBox4.Text = dr["cidade"].ToString();
                        }
                    }
                }

                if (encontrou)
                {
                    MessageBox.Show("Endereço localizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Nenhum endereço encontrado para este tutor.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao localizar endereço: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//fim lupa


        private void button5_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(maskedTextBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    MessageBox.Show("Por favor, preencha todos os campos para editar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nomeTutor = textBox1.Text.Trim();
                string logradouro = textBox3.Text.Trim();
                string cidade = textBox4.Text.Trim();

                string cepStr = maskedTextBox1.Text.Replace("-", "").Trim();
                if (!long.TryParse(cepStr, out long cep))
                {
                    MessageBox.Show("CEP inválido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

               
                string queryBuscaId = "SELECT enderecoCodigo FROM tutor WHERE nome LIKE @nome LIMIT 1";
                int enderecoCodigo = -1;

                using (MySqlCommand cmdBusca = new MySqlCommand(queryBuscaId, conexaoDao.conexao))
                {
                    cmdBusca.Parameters.AddWithValue("@nome", "%" + nomeTutor + "%");
                    object res = cmdBusca.ExecuteScalar();
                    if (res != null)
                    {
                        enderecoCodigo = Convert.ToInt32(res);
                    }
                }

                if (enderecoCodigo == -1)
                {
                    MessageBox.Show("Tutor não encontrado para atualizar o endereço.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                
                string queryUpdate = "UPDATE endereco SET CEP = @cep, logradouro = @logradouro, cidade = @cidade WHERE codigo = @codigo";

                using (MySqlCommand cmdUpdate = new MySqlCommand(queryUpdate, conexaoDao.conexao))
                {
                    cmdUpdate.Parameters.AddWithValue("@cep", cep);
                    cmdUpdate.Parameters.AddWithValue("@logradouro", logradouro);
                    cmdUpdate.Parameters.AddWithValue("@cidade", cidade);
                    cmdUpdate.Parameters.AddWithValue("@codigo", enderecoCodigo);

                    cmdUpdate.ExecuteNonQuery();

                    MessageBox.Show("Endereço atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregarEnderecosGrid();
                    LimparCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao editar endereço: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//fim editar endereco

        
        private void exluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Informe o Nome do Tutor para excluir o endereço associado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nomeTutor = textBox1.Text.Trim();

                DialogResult confirmacao = MessageBox.Show("Tem certeza que deseja excluir este endereço? (Isso afetará o cadastro do tutor vinculado)", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacao == DialogResult.Yes)
                {
                    
                    string queryBuscaId = "SELECT enderecoCodigo FROM tutor WHERE nome LIKE @nome LIMIT 1";
                    int enderecoCodigo = -1;

                    using (MySqlCommand cmdBusca = new MySqlCommand(queryBuscaId, conexaoDao.conexao))
                    {
                        cmdBusca.Parameters.AddWithValue("@nome", "%" + nomeTutor + "%");
                        object res = cmdBusca.ExecuteScalar();
                        if (res != null)
                        {
                            enderecoCodigo = Convert.ToInt32(res);
                        }
                    }

                    if (enderecoCodigo != -1)
                    {
                        
                        string queryDelTutorRef = "UPDATE tutor SET enderecoCodigo = NULL WHERE enderecoCodigo = @codigo";
                        using (MySqlCommand cmdRef = new MySqlCommand(queryDelTutorRef, conexaoDao.conexao))
                        {
                            cmdRef.Parameters.AddWithValue("@codigo", enderecoCodigo);
                            cmdRef.ExecuteNonQuery();
                        }

                        
                        string queryDelEnd = "DELETE FROM endereco WHERE codigo = @codigo";
                        using (MySqlCommand cmdDel = new MySqlCommand(queryDelEnd, conexaoDao.conexao))
                        {
                            cmdDel.Parameters.AddWithValue("@codigo", enderecoCodigo);
                            cmdDel.ExecuteNonQuery();
                        }

                        MessageBox.Show("Endereço excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CarregarEnderecosGrid();
                        LimparCampos();
                    }
                    else
                    {
                        MessageBox.Show("Endereço não encontrado para exclusão.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir endereço: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//fim excluir

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }//fim voltar

        private void LimparCampos()
        {
            textBox1.Clear();
            maskedTextBox1.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox1.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim nome
        private void label5_Click(object sender, EventArgs e) 
        { 
        
        }//fim CEP
        private void label6_Click(object sender, EventArgs e) 
        { 
        
        }
        private void textBox3_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim logradouro
        private void textBox4_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim cidade

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }//fim numero

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }//fim cep
    }
}