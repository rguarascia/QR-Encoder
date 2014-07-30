using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR_Encode
{
    class DataEncode
    {
        int encodeType = 0;

        Dictionary<String, short> AlphaNum = new Dictionary<String, short>();

        private void Alphanumeric()
        {
            AlphaNum.Add("0", 0);
            AlphaNum.Add("1", 1);
            AlphaNum.Add("2", 2);
            AlphaNum.Add("3", 3);
            AlphaNum.Add("4", 4);
            AlphaNum.Add("5", 5);
            AlphaNum.Add("6", 6);
            AlphaNum.Add("7", 7);
            AlphaNum.Add("8", 8);
            AlphaNum.Add("9", 9);
            AlphaNum.Add("A", 10);
            AlphaNum.Add("B", 11);
            AlphaNum.Add("C", 12);
            AlphaNum.Add("D", 13);
            AlphaNum.Add("E", 14);
            AlphaNum.Add("F", 15);
            AlphaNum.Add("G", 16);
            AlphaNum.Add("H", 17);
            AlphaNum.Add("I", 18);
            AlphaNum.Add("J", 19);
            AlphaNum.Add("K", 20);
            AlphaNum.Add("L", 21);
            AlphaNum.Add("M", 22);
            AlphaNum.Add("N", 23);
            AlphaNum.Add("O", 24);
            AlphaNum.Add("P", 25);
            AlphaNum.Add("Q", 26);
            AlphaNum.Add("R", 27);
            AlphaNum.Add("S", 28);
            AlphaNum.Add("T", 29);
            AlphaNum.Add("U", 30);
            AlphaNum.Add("V", 31);
            AlphaNum.Add("W", 32);
            AlphaNum.Add("X", 33);
            AlphaNum.Add("Y", 34);
            AlphaNum.Add("Z", 35);
            AlphaNum.Add(" ", 36);
            AlphaNum.Add("$", 37);
            AlphaNum.Add("%", 38);
            AlphaNum.Add("*", 39);
            AlphaNum.Add("+", 40);
            AlphaNum.Add("-", 41);
            AlphaNum.Add(".", 42);
            AlphaNum.Add("/", 43);
            AlphaNum.Add(":", 44);
            //That was pain staking. 
        }

        private int EightBit(string Data)
        {
            StringBuilder Pile = new StringBuilder();
            char[] values = Data.ToCharArray();
            foreach (char letter in values)
            {
                // Get the integral value of the character. 
                int value = Convert.ToInt32(letter);
                // Convert the decimal value to a hexadecimal value in string form. 
                string hexOutput = String.Format("{0:X}", value);
                Pile.Append(Convert.ToString(Convert.ToInt32(hexOutput, 16), 2));
            }
            return Convert.ToInt16(Pile.ToString());
        }

        private void EncodeType(string data)
        {
            if (data.All(Char.IsDigit))
                encodeType = 0001;
            else if (data.All(char.IsLetterOrDigit))
                encodeType = 0010;
            else
                encodeType = 0100;
        }

    }
}
