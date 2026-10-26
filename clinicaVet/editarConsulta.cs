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
    public partial class editarConsulta : Form
    {
        DAO conexaoDao; 

        public editarConsulta()
        {
            InitializeComponent();
            conexaoDao = new DAO();
            CarregarConsultasGrid(); 
        }

       
        private void CarregarConsultasGrid()
        {
            try
            {
                string query = "SELECT codigo AS Código, animalCodigo AS Animal, veterinarioCodigo AS Veterinário, diagnostico AS Diagnóstico, tratamento AS Tratamento, dataAtendimento AS Data FROM atendimento";

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
                MessageBox.Show("Erro ao carregar consultas: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loginAdm login = new loginAdm();
            login.Show();
            this.Close();
        }//fim logout

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox5.Text = row.Cells["Animal"].Value.ToString();
                textBox1.Text = row.Cells["Veterinário"].Value.ToString();
                textBox2.Text = row.Cells["Diagnóstico"].Value.ToString();
                textBox3.Text = row.Cells["Tratamento"].Value.ToString();
                textBox4.Text = row.Cells["Data"].Value.ToString();
            }
        }//Fim lista de consultas

       
        private void button3_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (string.IsNullOrWhiteSpace(textBox5.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    MessageBox.Show("Informe o Código do Animal e a Data para localizar a consulta!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBox5.Text.Trim(), out int animalCodigo) ||
                    !DateTime.TryParse(textBox4.Text.Trim(), out DateTime dataAtendimento))
                {
                    MessageBox.Show("Verifique se o ID do animal e o formato da data são válidos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string query = "SELECT veterinarioCodigo, diagnostico, tratamento FROM atendimento WHERE animalCodigo = @animalCodigo AND dataAtendimento = @data LIMIT 1";

                bool encontrou = false;

                using (MySqlCommand cmd = new MySqlCommand(query, conexaoDao.conexao))
                {
                    cmd.Parameters.AddWithValue("@animalCodigo", animalCodigo);
                    cmd.Parameters.AddWithValue("@data", dataAtendimento);

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            encontrou = true;
                            textBox1.Text = dr["veterinarioCodigo"].ToString(); 
                            textBox2.Text = dr["diagnostico"].ToString();       
                            textBox3.Text = dr["tratamento"].ToString();       
                        }
                    }
                }

                if (encontrou)
                {
                    MessageBox.Show("Consulta localizada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Nenhuma consulta encontrada para este animal nesta data.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao localizar consulta: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//fim localizar

       
        private void button5_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (string.IsNullOrWhiteSpace(textBox5.Text) ||
                    string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    MessageBox.Show("Por favor, preencha todos os campos!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBox5.Text.Trim(), out int animalCodigo) ||
                    !int.TryParse(textBox1.Text.Trim(), out int veterinarioCodigo) ||
                    !DateTime.TryParse(textBox4.Text.Trim(), out DateTime dataAtendimento))
                {
                    MessageBox.Show("Verifique se os IDs e a data estão em formatos válidos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string diagnostico = textBox2.Text.Trim();
                string tratamento = textBox3.Text.Trim();

                
                string query = "UPDATE atendimento SET veterinarioCodigo = @vet, diagnostico = @diag, tratamento = @trat WHERE animalCodigo = @animal AND dataAtendimento = @data";

                using (MySqlCommand cmd = new MySqlCommand(query, conexaoDao.conexao))
                {
                    cmd.Parameters.AddWithValue("@vet", veterinarioCodigo);
                    cmd.Parameters.AddWithValue("@diag", diagnostico);
                    cmd.Parameters.AddWithValue("@trat", tratamento);
                    cmd.Parameters.AddWithValue("@animal", animalCodigo);
                    cmd.Parameters.AddWithValue("@data", dataAtendimento);

                    int linhasAfetadas = cmd.ExecuteNonQuery();

                    if (linhasAfetadas > 0)
                    {
                        MessageBox.Show("Consulta atualizada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CarregarConsultasGrid();
                        LimparCampos();
                    }
                    else
                    {
                        MessageBox.Show("Consulta não encontrada para atualização com estes dados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao editar consulta: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//fim editar consultas

       
        private void exluir_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (string.IsNullOrWhiteSpace(textBox5.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    MessageBox.Show("Informe o Código do Animal e a Data para excluir a consulta!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBox5.Text.Trim(), out int animalCodigo) ||
                    !DateTime.TryParse(textBox4.Text.Trim(), out DateTime dataAtendimento))
                {
                    MessageBox.Show("Verifique se o ID do animal e a data são válidos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult confirmacao = MessageBox.Show("Tem certeza que deseja excluir esta consulta?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacao == DialogResult.Yes)
                {
                    string query = "DELETE FROM atendimento WHERE animalCodigo = @animal AND dataAtendimento = @data";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexaoDao.conexao))
                    {
                        cmd.Parameters.AddWithValue("@animal", animalCodigo);
                        cmd.Parameters.AddWithValue("@data", dataAtendimento);

                        int linhasAfetadas = cmd.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                        {
                            MessageBox.Show("Consulta excluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CarregarConsultasGrid();
                            LimparCampos();
                        }
                        else
                        {
                            MessageBox.Show("Nenhuma consulta encontrada com este animal e data para exclusão.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir consulta: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//fim excluir

        private void LimparCampos()
        {
            textBox5.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Focus();
        }

        private void textBox5_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim animal
        private void textBox1_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim veterinario
        private void textBox2_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim diagnostico
        private void textBox3_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim tratamento
        private void textBox4_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim data
    }
}