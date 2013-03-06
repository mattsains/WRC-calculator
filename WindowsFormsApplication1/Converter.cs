using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Converter
    {
        static Control.ControlCollection controls = Program.form1.Controls["pnlConvert"].Controls;
        public static bool[] Bits(int num)
        {
            //remove all stuff from the Convert panel
            Program.form1.Controls["pnlConvert"].Controls.Clear();
            //start drawing labels here
            int controlTop = 20;

            bool[] output=new bool[32];
            int numDig = num.ToString().Length;
            for (int i=0; num>0&&i<32;i++)
            {
                //do binaries
                
                //display
                controls.Add(new Label());
                
                controls[controls.Count - 1].Top = controlTop;
                controls[controls.Count - 1].Left = 10;

                controls[controls.Count - 1].Height = 20;
                controls[controls.Count - 1].Width = Program.form1.Controls["pnlConvert"].ClientRectangle.Width;

                controls[controls.Count - 1].Text = string.Format("{0,"+numDig+"}÷2={1,"+numDig+"}  rem {2}", num,num >> 1, num & 1);
                output[31 - i] = (num & 1)==1;
                num = num >> 1;
                controlTop += 20;
             }
            return output;
        }
        public static int Int(bool[] binary)
        {
            //TODO: make interactive
            int output=0;
            for (int i = 0; i < 32; i++)
                if (binary[i])
                    output += (1 << i);
            return output;
        }
        public static string ToString(bool[] binary)
        {
            string output = "";
            int i;
            for (i = 0; (!binary[i]) && i < 32; i++) { }//trim leading zeros
            for (; i < 32; i++)
            {
                output += binary[i] ? "1" : "0";
            }
            return output;
        }
        public static bool[] ToBinary(string s)
        {
            bool[] output = new bool[32];
            for (int i = s.Length - 1; i >= 0; i--)
            {
                output[31-i] = s[i] == '1';
            }
            return output;
        }
    }
}
