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
    public partial class recepcao : Form
    {
        DAO conexaoDao; 

        public recepcao()
        {
            InitializeComponent();
            conexaoDao = new DAO(); 
            CarregarTutoresRecepcao(); 
        }

        private void CarregarTutoresRecepcao()
        {
            try
            {
                string query = "SELECT codigo AS Código, nome AS Nome, telefone AS Telefone, CPF AS CPF, email AS `E-mail`, enderecoCodigo AS Endereço FROM tutor";

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
                MessageBox.Show("Erro ao carregar lista de tutores: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        }// Fim lista de tutores

        private void button2_Click(object sender, EventArgs e)
        {
            cadastroTutor telaTutor = new cadastroTutor();
            telaTutor.ShowDialog();
            CarregarTutoresRecepcao(); 
        }//fim cadastrar tutor

        private void button3_Click(object sender, EventArgs e)
        {
            cadastroAnimal telaAnimal = new cadastroAnimal();
            telaAnimal.ShowDialog();
            CarregarTutoresRecepcao();
        }//fim cadastrar animal
    }
}