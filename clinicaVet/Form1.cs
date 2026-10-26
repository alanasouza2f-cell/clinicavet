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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            panel1.BackColor = Color.FromArgb(120, 255, 255, 255);
        }

        private void tutor_Click(object sender, EventArgs e)
        {
            loginUsuario telaLogin = new loginUsuario();
            telaLogin.Show();
            this.Hide();
        }//fim botão tutor

        private void clinicaVet_Click(object sender, EventArgs e)
        {
            loginAdm telaAdm = new loginAdm();
            telaAdm.Show();
            this.Hide();
        }//fim botão clínica vet

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}