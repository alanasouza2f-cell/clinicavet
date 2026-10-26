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
    public partial class funcionarios : Form
    {
        DAO conexaoDao; 

        public funcionarios()
        {
            InitializeComponent();
            conexaoDao = new DAO(); 
            CarregarFuncionariosGrid();
        }

        private void CarregarFuncionariosGrid()
        {
            try
            {
                string query = "SELECT codigo AS 'CRMV/ID', nome AS Nome, area AS Área, turno AS Turno, senha AS Senha FROM veterinario";

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
                MessageBox.Show("Erro ao carregar funcionários: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["CRMV/ID"].Value.ToString();
                textBox8.Text = row.Cells["Nome"].Value.ToString();
                textBox7.Text = row.Cells["Área"].Value.ToString();
                textBox6.Text = row.Cells["Turno"].Value.ToString();
                textBox5.Text = row.Cells["Senha"].Value.ToString();
            }
        }//fim view funcionarios

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
                    string.IsNullOrWhiteSpace(textBox8.Text) ||
                    string.IsNullOrWhiteSpace(textBox7.Text) ||
                    string.IsNullOrWhiteSpace(textBox6.Text) ||
                    string.IsNullOrWhiteSpace(textBox5.Text))
                {
                    MessageBox.Show("Por favor, preencha todos os campos!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBox1.Text.Trim(), out int idFuncionario))
                {
                    MessageBox.Show("O CRMV/ID deve ser um número inteiro válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string nome = textBox8.Text.Trim();
                string area = textBox7.Text.Trim();
                string turno = textBox6.Text.Trim();
                string senha = textBox5.Text.Trim();
                int enderecoPadraoCodigo = 1;

                string query = "INSERT INTO veterinario (codigo, nome, email, senha, area, turno, telefone, enderecoCodigo) VALUES (@codigo, @nome, @email, @senha, @area, @turno, @telefone, @enderecoCodigo)";

                using (MySqlCommand cmd = new MySqlCommand(query, conexaoDao.conexao))
                {
                    cmd.Parameters.AddWithValue("@codigo", idFuncionario);
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@email", idFuncionario + "@clinica.com");
                    cmd.Parameters.AddWithValue("@senha", senha);
                    cmd.Parameters.AddWithValue("@area", area);
                    cmd.Parameters.AddWithValue("@turno", turno);
                    cmd.Parameters.AddWithValue("@telefone", "00000000000");
                    cmd.Parameters.AddWithValue("@enderecoCodigo", enderecoPadraoCodigo);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Funcionário cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregarFuncionariosGrid();
                    LimparCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar funcionário: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//fim adicionar funcionario

        
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Informe o CRMV/ID do funcionário que deseja editar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBox1.Text.Trim(), out int idFuncionario))
                {
                    MessageBox.Show("CRMV/ID inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string nome = textBox8.Text.Trim();
                string area = textBox7.Text.Trim();
                string turno = textBox6.Text.Trim();
                string senha = textBox5.Text.Trim();

                string query = "UPDATE veterinario SET nome = @nome, area = @area, turno = @turno, senha = @senha WHERE codigo = @codigo";

                using (MySqlCommand cmd = new MySqlCommand(query, conexaoDao.conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@area", area);
                    cmd.Parameters.AddWithValue("@turno", turno);
                    cmd.Parameters.AddWithValue("@senha", senha);
                    cmd.Parameters.AddWithValue("@codigo", idFuncionario);

                    int linhasAfetadas = cmd.ExecuteNonQuery();

                    if (linhasAfetadas > 0)
                    {
                        MessageBox.Show("Dados do funcionário atualizados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CarregarFuncionariosGrid();
                        LimparCampos();
                    }
                    else
                    {
                        MessageBox.Show("Funcionário não encontrado para edição com este CRMV/ID.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar funcionário: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//fim editar funcionario

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Informe o CRMV/ID para realizar a busca!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBox1.Text.Trim(), out int idFuncionario))
                {
                    MessageBox.Show("CRMV/ID inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool encontrou = false;
                string query = "SELECT nome, area, turno, senha FROM veterinario WHERE codigo = @codigo LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(query, conexaoDao.conexao))
                {
                    cmd.Parameters.AddWithValue("@codigo", idFuncionario);

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            encontrou = true;
                            textBox8.Text = dr["nome"].ToString();
                            textBox7.Text = dr["area"].ToString();
                            textBox6.Text = dr["turno"].ToString();
                            textBox5.Text = dr["senha"].ToString();
                        }
                    }
                }

                if (encontrou)
                {
                    MessageBox.Show("Funcionário localizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Nenhum funcionário encontrado com este CRMV/ID.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao localizar funcionário: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//fim lupa

        private void exluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Informe o CRMV/ID do funcionário que deseja excluir!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBox1.Text.Trim(), out int idFuncionario))
                {
                    MessageBox.Show("CRMV/ID inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult confirmacao = MessageBox.Show("Tem certeza que deseja excluir este funcionário?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacao == DialogResult.Yes)
                {
                    string query = "DELETE FROM veterinario WHERE codigo = @codigo";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexaoDao.conexao))
                    {
                        cmd.Parameters.AddWithValue("@codigo", idFuncionario);

                        int linhasAfetadas = cmd.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                        {
                            MessageBox.Show("Funcionário excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CarregarFuncionariosGrid();
                            LimparCampos();
                        }
                        else
                        {
                            MessageBox.Show("Funcionário não encontrado para exclusão.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)  
            {
                if (ex.Message.Contains("foreign key constraint fails"))
                {
                    MessageBox.Show("Não é possível excluir este funcionário porque ele possui atendimentos associados no sistema!", "Aviso de Restrição", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Erro ao excluir funcionário: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }//fim excluir

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }//fim voltar

        private void LimparCampos()
        {
            textBox1.Clear();
            textBox8.Clear();
            textBox7.Clear();
            textBox6.Clear();
            textBox5.Clear();
            textBox1.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim CRMV/ID
        private void textBox8_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim nome
        private void textBox7_TextChanged(object sender, EventArgs e) 
        { 
        
        }// fim area
        private void textBox6_TextChanged(object sender, EventArgs e) 
        { 
        
        }//fim turno
        private void textBox5_TextChanged(object sender, EventArgs e) 
        {
        
        }//Fim senha
    }
}