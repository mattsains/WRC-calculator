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
    enum Operation { None = 0, Addition, Subtraction, Multiplication, Division }
    public partial class Form1 : Form
    {
        bool[] display = new bool[32]; //stores the binary equivalent of the display at <del>all</div>most times
        bool[] memory = new bool[32]; //stores the last display before pressing an operator
        Operation operation = Operation.None; //for persistance


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

            ((Label)pnlConvert.Controls[pnlConvert.Controls.Count - 1]).AutoSize = true;

            pnlConvert.Controls[pnlConvert.Controls.Count - 1].Text = text;
            panelTopElement += 20;
        }
        /// <summary>
        /// Sets the display taking representation into account
        /// </summary>
        /// <param name="num">binary or int, anything!</param>
        public void SetDisplay(string num, bool display = false)
        {
            if (cmbBase.Text == "base 2")
                SetDisplay(Converter.ToBinary(num), display);
            else SetDisplay(Converter.Bits(int.Parse(num)), display);
        }
        public void SetDisplay(int num, bool display = false)
        {
            SetDisplay(Converter.Bits(num, display), display);
        }
        public void SetDisplay(bool[] num, bool display = false)
        {
            if (cmbBase.Text == "base 2")
            {
                NumDisp.Text = Converter.ToString(num);
                //do a dummy conversion from the int, just for show
                Converter.Bits(Converter.Int(num), true);
            }
            else NumDisp.Text = Converter.Int(num, display).ToString();
            this.display = num; //if we don't have pointer issues here, I will buy you an alcohol beverage of your choice
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == bc)
            {
                SetDisplay(0);
            }
            else if (sender == bp)
            {
                //ADDITION
                memory = display; //keep this number before clearing it in preparation for the next number

                operation = Operation.Addition;

                SetDisplay(0);
            }
            else if (sender == bm)
            {
                //minus
                memory = display; //keep this number before clearing it in preparation for the next number

                operation = Operation.Subtraction;

                SetDisplay(0);
            }
            else if (sender == bt)
            {
                //multiply
                memory = display; //keep this number before clearing it in preparation for the next number

                operation = Operation.Multiplication;

                SetDisplay(0);
            }
            else if (sender == bd)
            {
                //divide
                memory = display; //keep this number before clearing it in preparation for the next number

                operation = Operation.Division;

                SetDisplay(0);
            }
            else if (sender == be)
            {
                switch (operation)
                {
                    case Operation.Addition:
                        SetDisplay(MathsDo.binaryAdd(display, memory));
                        break;
                    case Operation.Multiplication:
                        SetDisplay(MathsDo.multiply(display, memory));
                        break;
                    default:
                        break;
                }
            }
            else
            {
                // A number button was pressed
                if (sender == b0) SetDisplay(NumDisp.Text + "0");
                else if (sender == b1) SetDisplay(NumDisp.Text + "1");
                else if (sender == b2) SetDisplay(NumDisp.Text + "2");
                else if (sender == b3) SetDisplay(NumDisp.Text + "3");
                else if (sender == b4) SetDisplay(NumDisp.Text + "4");
                else if (sender == b5) SetDisplay(NumDisp.Text + "5");
                else if (sender == b6) SetDisplay(NumDisp.Text + "6");
                else if (sender == b7) SetDisplay(NumDisp.Text + "7");
                else if (sender == b8) SetDisplay(NumDisp.Text + "8");
                else if (sender == b9) SetDisplay(NumDisp.Text + "9");
            }

        }

        private void cmbBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBase.Text == "base 2")
            {
                b0.Enabled = true;
                b1.Enabled = true;
                b2.Enabled = false;
                b3.Enabled = false;
                b4.Enabled = false;
                b5.Enabled = false;
                b6.Enabled = false;
                b7.Enabled = false;
                b8.Enabled = false;
                b9.Enabled = false;
                pnlConvert.Controls.Clear();
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
            }
            SetDisplay(display, true);
        }

    }
}