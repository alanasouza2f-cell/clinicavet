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
    public partial class telaAdministração : Form
    {
        public telaAdministração()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            funcionarios telaFunc = new funcionarios();
            telaFunc.ShowDialog();
        }//fim lista de fincionarios

        private void button3_Click(object sender, EventArgs e)
        {
            medicamentos telaMed = new medicamentos();
            telaMed.ShowDialog();
        }//fim lista de medicamentos

        private void button4_Click(object sender, EventArgs e)
        {
            enderecos telaEnd = new enderecos();
            telaEnd.ShowDialog();
        }//fim lista de enderecos

        private void button1_Click(object sender, EventArgs e)
        {
            loginAdm login = new loginAdm();
            login.Show();
            this.Close();
        }//fim logout

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}