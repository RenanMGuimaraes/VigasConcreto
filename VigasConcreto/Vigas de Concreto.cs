using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VigasConcreto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void deToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string titulo = "Aviso sobre autoria";
            string autor = "Este aplicativo foi desenvolvido pelo Eng. Renan M Guimarães com base na NBR 6118/2014. \n" +
                            "Email: renanguimaraes@live.com \n" +"\n"+
                            "O autor não se responsabiliza pelo uso dos resultados emitidos por este aplicativo. \r\n"+
                            "Versão da aplicação: V1.0";
            MessageBox.Show(autor, titulo,MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            F_VigasRet f_VigasRet = new F_VigasRet();
            f_VigasRet.Show();
        }

       
    }
}
