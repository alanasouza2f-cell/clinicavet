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
    public partial class medicamentos : Form
    {
        DAO conexaoDao; 

        public medicamentos()
        {
            InitializeComponent();
            conexaoDao = new DAO(); 
            CarregarMedicamentosGrid(); 
        }

        
        private void CarregarMedicamentosGrid()
        {
            try
            {
                string query = "SELECT codigo AS Número, dosagem AS Dosagem, principioAtivo AS 'Princípio Ativo', DATE_FORMAT(validade, '%Y-%m-%d') AS Validade FROM medicamento";

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
                MessageBox.Show("Erro ao carregar medicamentos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["Número"].Value.ToString();
                textBox2.Text = row.Cells["Dosagem"].Value.ToString();
                textBox3.Text = row.Cells["Princípio Ativo"].Value.ToString();
                textBox4.Text = row.Cells["Validade"].Value.ToString();
            }
        }//fim view medicamentos

        private void button1_Click(object sender, EventArgs e)
        {
            loginAdm login = new loginAdm();
            login.Show();
            this.Close();
        }//fim logout

      
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    MessageBox.Show("Por favor, preencha todos os campos!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBox1.Text.Trim(), out int codigo))
                {
                    MessageBox.Show("O Número/Código do medicamento deve ser um valor numérico inteiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string dosagem = textBox2.Text.Trim();
                string principioAtivo = textBox3.Text.Trim();
                string validade = textBox4.Text.Trim(); 

                string query = "INSERT INTO medicamento (codigo, dosagem, principioAtivo, validade) VALUES (@codigo, @dosagem, @principioAtivo, @validade)";

                using (MySqlCommand cmd = new MySqlCommand(query, conexaoDao.conexao))
                {
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                    cmd.Parameters.AddWithValue("@dosagem", dosagem);
                    cmd.Parameters.AddWithValue("@principioAtivo", principioAtivo);
                    cmd.Parameters.AddWithValue("@validade", validade);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Medicamento cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregarMedicamentosGrid();
                    LimparCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar medicamento: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//fim adicionar medicamentos

        
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Informe o Número/Código do medicamento que deseja editar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBox1.Text.Trim(), out int codigo))
                {
                    MessageBox.Show("Número/Código inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string dosagem = textBox2.Text.Trim();
                string principioAtivo = textBox3.Text.Trim();
                string validade = textBox4.Text.Trim();

                string query = "UPDATE medicamento SET dosagem = @dosagem, principioAtivo = @principioAtivo, validade = @validade WHERE codigo = @codigo";

                using (MySqlCommand cmd = new MySqlCommand(query, conexaoDao.conexao))
                {
                    cmd.Parameters.AddWithValue("@dosagem", dosagem);
                    cmd.Parameters.AddWithValue("@principioAtivo", principioAtivo);
                    cmd.Parameters.AddWithValue("@validade", validade);
                    cmd.Parameters.AddWithValue("@codigo", codigo);

                    int linhasAfetadas = cmd.ExecuteNonQuery();

                    if (linhasAfetadas > 0)
                    {
                        MessageBox.Show("Medicamento atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CarregarMedicamentosGrid();
                        LimparCampos();
                    }
                    else
                    {
                        MessageBox.Show("Medicamento não encontrado para edição com este Número/Código.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar medicamento: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Informe o Número/Código para realizar a busca!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBox1.Text.Trim(), out int codigo))
                {
                    MessageBox.Show("Número/Código inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool encontrou = false;
                string query = "SELECT dosagem, principioAtivo, validade FROM medicamento WHERE codigo = @codigo LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(query, conexaoDao.conexao))
                {
                    cmd.Parameters.AddWithValue("@codigo", codigo);

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            encontrou = true;
                            textBox2.Text = dr["dosagem"].ToString();
                            textBox3.Text = dr["principioAtivo"].ToString();
                            textBox4.Text = dr["validade"].ToString();
                        }
                    }
                }

                if (encontrou)
                {
                    MessageBox.Show("Medicamento localizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Nenhum medicamento encontrado com este Número/Código.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao localizar medicamento: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//fim lupa

        
        private void exluir_Click(object sender, EventArgs e)
        { 
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Informe o Número/Código do medicamento que deseja excluir!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBox1.Text.Trim(), out int codigo))
                {
                    MessageBox.Show("Número/Código inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult confirmacao = MessageBox.Show("Tem certeza que deseja excluir este medicamento?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacao == DialogResult.Yes)
                {
                    string query = "DELETE FROM medicamento WHERE codigo = @codigo";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexaoDao.conexao))
                    {
                        cmd.Parameters.AddWithValue("@codigo", codigo);

                        int linhasAfetadas = cmd.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                        {
                            MessageBox.Show("Medicamento excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CarregarMedicamentosGrid();
                            LimparCampos();
                        }
                        else
                        {
                            MessageBox.Show("Medicamento não encontrado para exclusão.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir medicamento: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//fim lixeira

        private void button4_Click(object sender, EventArgs e)
        {
            telaAdministração adm = new telaAdministração();
            adm.Show();
            this.Close();
        }//fim voltar

        private void LimparCampos()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox1.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim numero
        private void textBox2_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim dosagem
        private void textBox3_TextChanged(object sender, EventArgs e) 
        {
        
        }//fim principio ativo
        private void textBox4_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim validade
    }
}