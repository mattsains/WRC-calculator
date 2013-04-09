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
            bool[] fix = new bool[32];

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

                fix[i] = (num1[i] || num2[i]);



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
                    if (!fix[i])
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

        public static bool[] divide(bool[] dividend, bool[] divisor) //assuming positive numbers
        {
            Program.form1.ClearLines();
            bool startdisplay = false;
            bool[] result = new bool[32];
            for (int i = 0; i < 32; i++)
            {
                bool[] check = new bool[32];
                Array.ConstrainedCopy(dividend, 0, check, 31 - i, i + 1);
                bool[] minus = MathsDo.binaryAdd(check, changeSign(divisor), false); //see if it goes in by subtracting
                bool goesIn = !minus[0];

                if (goesIn)
                    startdisplay = true; //first postivie gives us a starting point for displaying

                if (startdisplay)
                {
                    result[i] = goesIn;
                    Program.form1.AddLine("Divisor = " + Converter.ToString(dividend));
                    Program.form1.AddLine(string.Format("{0} ÷ {1} = rem {2} + {3}", Converter.ToString(check), Converter.ToString(divisor),
                        minus[0] ? Converter.ToString(divisor) : Converter.ToString(minus), goesIn ? "1" : "0")); //avoid negative remainders
                }

                if (goesIn)
                    for (int j = i; j >= 0; j--)
                    {
                        dividend[j] = minus[j + 31 - i];
                    }
            }

            return result;
        }

        private static bool[] changeSign(bool[] a)
        {
            bool[] b = new bool[32];
            for (int i = 0 ; i < 32 ; i++)
            {
                b[i] = !a[i];
            }
            return Converter.plusOne(b);
        }
    }
}