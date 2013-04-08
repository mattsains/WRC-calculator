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
        static int largestPositive = (int)(Math.Pow(2, 31));

        static Control.ControlCollection controls = Program.form1.Controls["pnlConvert"].Controls;
        public static bool[] Bits(int num, bool display = false)
        {
            //remove all stuff from the Convert panel
            if (display)
                Program.form1.ClearLines();
            //start drawing labels here

            bool[] output = new bool[32];
            int numDig = num.ToString().Length;

            //POSITIVES
            if (num >= 0)
            {
                for (int i = 0; num > 0 && i < 31; i++) //limited to 31 of the 32 bits
                {
                    //display
                    if (display)
                        Program.form1.AddLine(string.Format("{0," + numDig + "}÷2={1," + numDig + "}  rem {2}", num, num >> 1, num & 1));

                    //do binaries                                                    //AKA
                    output[31 - i] = (num & 1) == 1; // mod 2         //output[31 - i] = (num % 2) == 1;
                    num = num >> 1;                                            //num /= 2;                
                }
            }
            else //NEGATIVES
            {
                num = -num;
                //TODO: Add some text here to explain wtf happened to binary

                for (int i = 0; i < 32; i++) //no restriction on num so all bits are flipped
                {
                    //display
                    if (display)
                        Program.form1.AddLine(string.Format("{0," + numDig + "}÷2={1," + numDig + "}  rem {2}", num, num >> 1, num & 1));

                    //do binaries                                                    
                    output[31 - i] = (num & 1) == 0; //opposite to flip bits    
                    num = num >> 1;                                            
                }

                //we flipped the bits now we need only add one
                output = plusOne(output);
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
            //TODO: do some display stuff here to show how negatives come about

            output -= (output / largestPositive) * largestPositive;

            return output;
        }

        public static string ToString(bool[] num)
        {
            string output = "";
            int i;

            bool[] binary = new bool[32];
            Array.Copy(num, binary, 32); //to prevent this from messing around with the original num when negative

            if (!binary[0]) //positive numbers
            {
                for (i = 0; i < 32 && (!binary[i]); i++) { }//trim leading zeros
                for (; i < 32; i++)
                {
                    output += binary[i] ? "1" : "0";
                }
            }
            else //negatives
            {
                //flip bits
                for (i = 0; i < 32; i++)
                { binary[i] = !binary[i];  }

                //add one
                binary = plusOne(binary);

                //display
                output += "-";
                for (i = 0; i < 32 && (!binary[i]); i++) { }//trim leading zeros
                for (; i < 32; i++)
                { output += binary[i] ? "1" : "0"; }                
            }

            if (output == "")
                output = "0";//solves the problem of blank
            return output;
        }

        public static bool[] ToBinary(string s)
        {
            bool[] output = new bool[32];

            if (s[0].Equals('-'))
            {
                //Skip the minus sign in the string, flip the bits
                for (int i = 1;  i < 33; i++)
                {
                    if (i < s.Length)
                        output[32 - i] = s[s.Length - i ] == '0';
                    else
                        output[32 - i] = true;
                }

                output = plusOne(output);
            }
            else
            {
                for (int i = 0; i < s.Length && i < 32; i++)
                {
                    output[31 - i] = s[s.Length - i - 1] == '1';
                }
            }

            return output;
        }

        public static bool[] SHL(bool[] num, int n)
        {
            return Converter.Bits(Converter.Int(num) << n);
        }

        public static bool[] plusOne(bool[] a)
        {
            bool[] one = new bool[32];
            one[31] = true;

            return MathsDo.binaryAdd(a, one, false);

        }
    }
}