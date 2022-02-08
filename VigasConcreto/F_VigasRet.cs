using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace VigasConcreto
{
    public partial class F_VigasRet : Form
    {
        public F_VigasRet()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Captura erros no preenchimento dos valores
             try
            {
                if (double.Parse(txt_d.Text) >= double.Parse(txt_h.Text))
                {
                    MessageBox.Show("A altura útil não pode ser superior a altura total da viga!", "Erro no preenchimento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_h.Text = "";
                    txt_d.Text = "";
                    txt_h.Focus();
                }
                if (double.Parse(txt_d.Text) <= 0 || double.Parse(txt_h.Text) <= 0 || double.Parse(txt_bw.Text) <= 0 || double.Parse(txt_momento.Text) <= 0)
                {
                    MessageBox.Show("Atenção! Não inserir valores negativos ou iguais a 0!", "Erro no preenchimento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_momento.Focus();
                }
                else
                {
                    Dimensionar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro no preenchimento", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void Dimensionar()
        {
            try
            {
                //Declaração das variáveis
                double md = double.Parse(txt_momento.Text);
                double bw = double.Parse(txt_bw.Text);
                double h = double.Parse(txt_h.Text);
                double d = double.Parse(txt_d.Text);
                double fck = double.Parse(cb_fck.Text);
                double fcd;
                double armcal; // area da armadura de calculo
                double armmin; // area armadura minima
                double arm; //area armadura final
                double[] areaarm = new double[]{0.5, 0.8, 1.25, 2.0, 3.15, 5.0, 8.0, 12.5}; //areas das barras, de 8 à 40 mm
                double[] darm = new double[] { 8.0, 10.0, 12.5, 16.0, 20.0, 25.0, 32.0, 40.0 }; //diâmetro de duas barras, de 8 à 40 mm
                int numbarras; //numeros de barras adotadas
                double romin = 0;
                double x1;
                double x2;
                double x = 0;
                string raiz = "";
                string resultado;



                //Calcula a LN
                fcd = fck / 14.0; //converte de MPa para kN/cm2
                x1 = (0.68 * d + Math.Sqrt(Math.Pow(0.68 * d, 2.0) - 4 * 0.272 * (md / (bw * fcd)))) / 0.544;
                x2 = (0.68 * d - Math.Sqrt(Math.Pow(0.68 * d, 2.0) - 4 * 0.272 * (md / (bw * fcd)))) / 0.544;

                //Verifica a validade das raízes
                if (x1 <= 0 && x2 <= 0)
                {
                    raiz = "Não há raízes reais";
                }
                if (x1 < 0 && x2 > 0)
                {
                    x = x2;
                    raiz = x.ToString("F2");
                }
                if (x1 > 0 && x2 < 0)
                {
                    x = x1;
                    raiz = x.ToString("F2");
                }
                if (x1 < x2)
                {
                    x = x1;
                    raiz = x.ToString("F2");
                }
                if (x1 > x2)
                {
                    x = x2;
                    raiz = x.ToString("F2");
                }

                //Calcula a armadura minima
                switch (cb_fck.Text)
                {
                    case "20":
                        romin = 0.15 / 100.0;
                        break;
                    case "25":
                        romin = 0.15 / 100.0;
                        break;
                    case "30":
                        romin = 0.15 / 100.0;
                        break;
                    case "35":
                        romin = 0.164 / 100.0;
                        break;
                    case "40":
                        romin = 0.179 / 100.0;
                        break;
                    case "45":
                        romin = 0.194 / 100.0;
                        break;
                }
                armmin = romin * bw * h;

                //Calcula a armadura
                armcal = md / ((50.0 / 1.15) * (d - 0.4 * x));

                //Armadura final
                arm = Math.Max(armcal, armmin);

                

                //Calcular armadura de pele
                if (h >= 60)
                {
                    //CalcArmPele();
                }

                //Preenche os valores de resultado
                resultado = "RESULTADO: \r\n \r\n" +
                                     "Os valores das raízes são: \r\n" +
                                     $"x1: {x1.ToString("F2")} cm e x2: {x2.ToString("F2")} cm \r\n \r\n" +
                                     $"A profundidade da Linha Neutra é: {raiz} cm \r\n \r\n" +
                                     $"A área de aço calculada é de {armcal.ToString("F2")} cm \r\n" +
                                     $"A armadura mínima é de {armmin.ToString("F2")} cm \r\n \r\n" +
                                     $"A armadura final adotada é de {arm.ToString("F2")} cm \r\n \r\n"+
                                     "Sugestão de armaduras: \r\n";

                //Sugestão de barras a adotar
                for (int i = 0; i < darm.Length; i++)
                {
                    numbarras = (int)Math.Ceiling(arm / areaarm[i]);
                    if (numbarras >= 2)
                    {
                        resultado += $"{numbarras} barras de {darm[i]} mm \r\n";
                    }
                }

                //Plota o texto no txt_resultado
                txt_resultado.Text = resultado;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro no preenchimento", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_bitolas_Click(object sender, EventArgs e)
        {
            Bitolas bitolas = new Bitolas();
            bitolas.Show();
        }

        private void F_VigasRet_Load(object sender, EventArgs e)
        {
            cb_fck.SelectedIndex = 0;
            cb_barras.SelectedIndex = 1;
        }
    }
}
