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
    public partial class telaDoutor : Form
    {
        DAO conexaoDao; 

        public telaDoutor()
        {
            InitializeComponent();
            conexaoDao = new DAO(); 
            CarregarConsultasVeterinario(); 
        }

        
        private void CarregarConsultasVeterinario()
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
                MessageBox.Show("Erro ao carregar lista de consultas: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
           
        }//fim tela de consultas

        private void button2_Click(object sender, EventArgs e)
        {
            consulta novaConsulta = new consulta();
            novaConsulta.ShowDialog();
            CarregarConsultasVeterinario(); 
        }//fim nova consulta

        private void button3_Click(object sender, EventArgs e)
        {
            editarConsulta editar = new editarConsulta();
            editar.ShowDialog();
            CarregarConsultasVeterinario();
        }//fim editar consulta
    }
}