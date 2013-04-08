using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    static class MathsDo
    {
        static Control.ControlCollection controls = Program.form1.Controls["pnlConvert"].Controls;

        private static bool addBits(bool a, bool b, ref bool carry)
        {
            bool value = (a ^ b) ^ carry; //this must be first or new carry will interfere

            carry = (a && b) || ((a ^ b) && carry);
            return value;
        }
        public static bool[] binaryAdd(bool[] num1, bool[] num2, bool display = true)
        {
            bool[] added = new bool[32];

            bool carry = false;
            bool oldcarry = false;

            if (display)
                Program.form1.ClearLines();

            //add em up
            for (int i = 31; i >= 0; i--)
            {
                oldcarry = carry;

                //adding
                added[i] = addBits(num1[i], num2[i], ref carry);

                //displaying
                if (display)
                    if (i == 31)//no carry on first go
                        Program.form1.AddLine(string.Format("2^{0,-2} |{2} + {1}      = c{3} + {4}", 31 - i, num1[i] ? 1 : 0, num2[i] ? 1 : 0, carry ? 1 : 0, added[i] ? 1 : 0));
                    else
                        Program.form1.AddLine(string.Format("2^{0,-2} |{2} + {1} + c{3} = c{4} + {5}", 31 - i, num1[i] ? 1 : 0, num2[i] ? 1 : 0, oldcarry ? 1 : 0, carry ? 1 : 0, added[i] ? 1 : 0));

            }

            //removing unnecessary labels
            //TODO: see if we can remove this
            if (display)
                for (int i = 0; i < 32; i++)
                    if (!added[i])
                        controls[31 - i].Dispose();
                    else
                        break; //break as soon as a 1 is found

            return added;
        }

        public static bool[] multiply(bool[] a, bool[] b) //assuming positive numbers
        {
            bool[] temp = new bool[32];
            int digs = Converter.ToString(a).Length + Converter.ToString(b).Length;
            Program.form1.ClearLines();
            Program.form1.AddLine(string.Format("{0," + (digs + 1) + "}", Converter.ToString(a)));
            Program.form1.AddLine(string.Format("×{0," + digs + "}", Converter.ToString(b)));
            Program.form1.AddLine("-----------------------");
            for (int i = 31; i > 0; i--)
            {
                if (a[i])
                {
                    temp = binaryAdd(temp, Converter.SHL(b, 31 - i), false);
                    if (Converter.Int(temp) == 0)
                        Program.form1.AddLine(string.Format("{0," + (digs + 1) + "}", Converter.ToString(Converter.SHL(b, 31 - i))));
                    else Program.form1.AddLine(string.Format("+{0," + digs + "}", Converter.ToString(Converter.SHL(b, 31 - i))));
                }
            }
            Program.form1.AddLine("-----------------------");
            Program.form1.AddLine(string.Format("{0," + digs + "}", Converter.ToString(temp)));
            return temp;
        }

        public static bool[] subtract(bool[] num1, bool[] num2, bool display = true)
        {
            return binaryAdd(num1, num2, display); //lol
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="num1">dividend</param>
        /// <param name="num2">divisor</param>
        /// <returns>quotient</returns>
        public static bool[] divide(bool[] num1, bool[] num2) //assuming positive numbers
        {
            return new bool[32];
        }

        private static bool[] changeSign(bool[] a)
        {
            for (int i = 0 ; i < 32 ; i++)
            {
                a[i] = !a[i];
            }
            return Converter.plusOne(a);
        }
    }
}