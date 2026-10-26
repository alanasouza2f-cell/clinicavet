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
    public partial class usuario : Form
    {
        DAO conexaoDao;
        string emailTutorLogado;
        int codigoTutorLogado = -1;

        public usuario(string emailTutor)
        {
            InitializeComponent();
            conexaoDao = new DAO();
            emailTutorLogado = emailTutor;

            Identificar();
        }

        public usuario()
        {
            InitializeComponent();
            conexaoDao = new DAO();
        }

        private void Identificar()
        {
            try
            {
                if (string.IsNullOrEmpty(emailTutorLogado)) return;

                string queryTutor = "SELECT codigo FROM tutor WHERE email = @email LIMIT 1";
                using (MySqlCommand cmdTutor = new MySqlCommand(queryTutor, conexaoDao.conexao))
                {
                    cmdTutor.Parameters.AddWithValue("@email", emailTutorLogado);
                    object resultado = cmdTutor.ExecuteScalar();

                    if (resultado != null)
                    {
                        codigoTutorLogado = Convert.ToInt32(resultado);
                    }
                }

                if (codigoTutorLogado != -1)
                {
                    meusAnimais();
                    minhasConsultas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados do usuário: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void meusAnimais()
        {
            try
            {
                string queryAnimais = "SELECT codigo AS Código, nome AS Nome, especie AS Espécie, raca AS Raça, peso AS Peso, altura AS Altura FROM animal WHERE tutorCodigo = @tutorCodigo";

                using (MySqlCommand cmd = new MySqlCommand(queryAnimais, conexaoDao.conexao))
                {
                    cmd.Parameters.AddWithValue("@tutorCodigo", codigoTutorLogado);

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView2.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar animais: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void minhasConsultas()
        {
            try
            {
                string queryConsultas = "SELECT a.codigo AS Código, an.nome AS Animal, a.veterinarioCodigo AS Veterinário, a.diagnostico AS Diagnóstico, a.tratamento AS Tratamento, a.dataAtendimento AS Data " +
                                        "FROM atendimento a " +
                                        "INNER JOIN animal an ON a.animalCodigo = an.codigo " +
                                        "WHERE an.tutorCodigo = @tutorCodigo";

                using (MySqlCommand cmd = new MySqlCommand(queryConsultas, conexaoDao.conexao))
                {
                    cmd.Parameters.AddWithValue("@tutorCodigo", codigoTutorLogado);

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

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }//fim colsultas

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }//fim view meus animais

        private void button1_Click(object sender, EventArgs e)
        {
            loginUsuario login = new loginUsuario();
            login.Show();
            this.Close();
        }//fim logout


    }
}