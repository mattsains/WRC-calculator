using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
namespace WindowsFormsApplication1
{
    static class Converter
    {
        static Control.ControlCollection controls = Program.form1.Controls["pnlConvert"].Controls;
        public static bool[] Bits(int num, bool display = false)
        {
            //remove all stuff from the Convert panel
            if (display)
                Program.form1.ClearLines();
            //start drawing labels here

            bool[] output = new bool[32];
            int numDig = num.ToString().Length;
            for (int i = 0; num > 0 && i < 32; i++)
            {
                //display
                if (display)
                    Program.form1.AddLine(string.Format("{0," + numDig + "}÷2={1," + numDig + "}  rem {2}", num, num >> 1, num & 1));

                //do binaries
                output[31 - i] = (num & 1) == 1; // mod 2
                num = num >> 1;
            }
            return output;
        }
        public static int Int(bool[] binary, bool display = false)
        {
            int output = 0;
            //remove all stuff from the Convert panel
            if (display)
                Program.form1.ClearLines();

            for (int i = 0; i < 32; i++)
                if (binary[i])
                    output += (1 << (31 - i));
            if (display)
            {
                for (int i = 0; i < 32; i++)
                {
                    if ((output & (1 << (31 - i))) != 0)
                    {
                        //display
                        Program.form1.AddLine(string.Format((output % (1 << (31 - i)) == 0) ? "1x{0}" : "1×{0}+", 1 << (31 - i)));
                    }
                }
                Program.form1.AddLine(string.Format("={0}", output));
            }
            return output;
        }
        public static string ToString(bool[] binary)
        {
            string output = "";
            int i;
            for (i = 0; i < 32 && (!binary[i]); i++) { }//trim leading zeros
            for (; i < 32; i++)
            {
                output += binary[i] ? "1" : "0";
            }
            if (output == "")
                output = "0";//solves the problem of blank
            return output;
        }
        public static bool[] ToBinary(string s)
        {
            bool[] output = new bool[32];
            for (int i = 0; i < s.Length && i < 32; i++)
            {
                output[31 - i] = s[s.Length - i - 1] == '1';
            }
            return output;
        }
        public static bool[] SHL(bool[] num, int n)
        {
            return Converter.Bits(Converter.Int(num) << n);
        }
    }
}