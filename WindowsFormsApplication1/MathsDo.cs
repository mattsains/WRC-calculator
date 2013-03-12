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

        public static bool[] binaryAdd(string number1, string number2)
        {
            bool[] added = new bool[32];

            //load em into arrays
            bool[] num1 = Converter.ToBinary(number1);
            bool[] num2 = Converter.ToBinary(number2);

            bool carry = false;
            bool oldcarry = false;

            Program.form1.ClearLines();

            //add em up
            for (int i = 31; i >= 0; i--)
            {               
                oldcarry = carry;

                //adding
                added[i] = addBits(num1[i], num2[i],ref carry);
                 
                //displaying
                Program.form1.AddLine(string.Format("2^{0,-2} |{1} + {2} + c{3} = c{4} + {5}", 31 - i, num1[i] ? 1 : 0, num2[i] ? 1 : 0, oldcarry ? 1 : 0, carry ? 1 : 0, added[i] ? 1 : 0));
            }

            //removing unnecessary labels
            for (int i = 0; i < 32; i++)
            {
                if (!added[i])
                {
                    controls[31 - i].Dispose();
                }
                else
                    break; //break as soon as a 1 is found
            }

            return added;
        }

        private static bool addBits (bool a, bool b, ref bool carry)
        { 
            bool value = (a ^ b) ^ carry; //this must be first or new carry will interfere

            carry = (a && b)|| ((a^b) && carry) ;
            return value ;            
        }

    }
}
