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
        public Form1()
        {
            InitializeComponent();
        }
        private void Button_Click (object sender, EventArgs e)
        {
            if (sender == bc)
            {
                NumDisp.Text = "0";
            }
            else
            {
                int n = 0;
                if (sender == b0) n = 0;
                else if (sender == b1) n = 1;
                else if (sender == b2) n = 2;
                else if (sender == b3) n = 3;
                else if (sender == b4) n = 4;
                else if (sender == b5) n = 5;
                else if (sender == b6) n = 6;
                else if (sender == b7) n = 7;
                else if (sender == b8) n = 8;
                else if (sender == b9) n = 9;

                var num = int.Parse(NumDisp.Text);

                num = num * 10 + n;

                NumDisp.Text = num.ToString();
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
                //convert to base ten
                //TODO: error here
                NumDisp.Text = Converter.Int(Converter.ToBinary(NumDisp.Text)).ToString();
            }
        }

    }
}
