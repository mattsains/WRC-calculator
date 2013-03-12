using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        string calculatorMemory = "0";
        int operation = 0;

        int panelTopElement = 20;//this stores where to put the next line in the conversion display

        public Form1()
        {
            InitializeComponent();
        }
        public void ClearLines()
        {
            panelTopElement = 20;
            pnlConvert.Controls.Clear();
        }
        public void AddLine(string text)
        {
            pnlConvert.Controls.Add(new Label());

            pnlConvert.Controls[pnlConvert.Controls.Count - 1].Top = panelTopElement;
            pnlConvert.Controls[pnlConvert.Controls.Count - 1].Left = 10;

            pnlConvert.Controls[pnlConvert.Controls.Count - 1].Height = 20;
            pnlConvert.Controls[pnlConvert.Controls.Count - 1].Width = pnlConvert.ClientRectangle.Width - 10; //why these go over the panel end?

            pnlConvert.Controls[pnlConvert.Controls.Count - 1].Text = text;
            panelTopElement += 20;
        }
        private void Button_Click (object sender, EventArgs e)
        {
            if (sender == bc)
            {
                NumDisp.Text = "0";
            }
            else if (sender == bp)
            {
                calculatorMemory = NumDisp.Text;
                operation = 1;
                NumDisp.Text = "0";
            }
            else if (sender == bm)
            {
                //minus
            }
            else if (sender == bt)
            {
                //multiply
            }
            else if (sender == bd)
            {
                //divide
            }
            else if (sender == be)
            {
                switch (operation)
                {
                    case 1:
                        if (cmbBase.SelectedIndex == 0)
                            // this is base two addition
                            NumDisp.Text = Converter.ToString(MathsDo.binaryAdd(NumDisp.Text, calculatorMemory));
                        else
                        {
                            // This is base ten addition
                            AddLine(calculatorMemory + " + " + NumDisp.Text + " = " + (int.Parse(NumDisp.Text) + int.Parse(calculatorMemory)).ToString());

                            //this must be last or .Text will be changed
                            NumDisp.Text = (int.Parse(NumDisp.Text) + int.Parse(calculatorMemory)).ToString();
                        }

                        break;
                    default:
                        break;
                }
            }
            else
            {
                if (sender == b0) NumDisp.Text += "0";
                else if (sender == b1) NumDisp.Text += "1";
                else if (sender == b2) NumDisp.Text += "2";
                else if (sender == b3) NumDisp.Text += "3";
                else if (sender == b4) NumDisp.Text += "4";
                else if (sender == b5) NumDisp.Text += "5";
                else if (sender == b6) NumDisp.Text += "6";
                else if (sender == b7) NumDisp.Text += "7";
                else if (sender == b8) NumDisp.Text += "8";
                else if (sender == b9) NumDisp.Text += "9";

                if (NumDisp.Text[0] == '0')
                    NumDisp.Text = NumDisp.Text.Substring(1);//solves leading zeros

            }
  
        }

        private void cmbBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBase.Text == "base 2")
            {
                b0.Enabled = true;
                b1.Enabled = true;
                b2.Enabled=false;
                b3.Enabled=false;
                b4.Enabled=false;
                b5.Enabled=false;
                b6.Enabled=false;
                b7.Enabled=false;
                b8.Enabled=false;
                b9.Enabled = false;
                //convert to binary
                NumDisp.Text = Converter.ToString(Converter.Bits(int.Parse(NumDisp.Text)));
            }
            else if (cmbBase.Text == "base 10")
            {
                b0.Enabled = true;
                b1.Enabled = true;
                b2.Enabled = true;
                b3.Enabled = true;
                b4.Enabled = true;
                b5.Enabled = true;
                b6.Enabled = true;
                b7.Enabled = true;
                b8.Enabled = true;
                b9.Enabled = true;
                //convert to base tens
                NumDisp.Text = Converter.Int(Converter.ToBinary(NumDisp.Text)).ToString();
            }
        }

    }
}
