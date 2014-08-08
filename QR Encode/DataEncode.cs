using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR_Encode
{
    class DataEncode
    {
        Dictionary<String, short> AlphaNum = new Dictionary<String, short>();
        string encodedData;
        int version;
        int correctLevel;
        EncodeHelper QrHelper = new EncodeHelper();
        private void dataEncoding(string theMessage)
        {
            switch (EncodeType(theMessage))
            {
                case 1: //Numeric Encoding
                    encodedData = NumericEncoding(theMessage);
                    break;
                case 10: //AlphaNumeric Encoding
                    encodedData = AlphaNumericEncode(theMessage);
                    break;
                case 100: //8-bit byte type
                    encodedData = EightBit(theMessage);
                    break;
            }
            version = QrHelper.versionIdenifier(EncodeType(theMessage), theMessage.Length);
            correctLevel = QrHelper.GetErrorCorrection(); //Must be called after or wil return null
        }

        //Finished
        private string AlphaNumericEncode(string theInput)
        {
            //Load all the chars into dictionary
            Alphanumeric();
            List<string> choppedData = DataintoTwo(theInput, 2);
            StringBuilder intValues = new StringBuilder("0010 " + charCount(theInput.Length, 10, version) + " ");

            for (int placement = 0; placement < choppedData.Count; placement++)
            {
                char[] cutUp = choppedData[placement].ToCharArray();
                int tempOne = 0;
                int tempTwo = 0;
                if (AlphaNum.ContainsKey(cutUp[0].ToString()))
                    tempOne = AlphaNum[cutUp[0].ToString()];
                if (cutUp.Length >= 2)
                {
                    if (AlphaNum.ContainsKey(cutUp[1].ToString()))
                        tempTwo = AlphaNum[cutUp[1].ToString()];
                    intValues.Append(Convert.ToString(Convert.ToInt16((tempOne * 45) + tempTwo), 2).PadLeft(11, '0'));
                }
                else
                    intValues.Append(Convert.ToString(tempOne, 2).PadLeft(6, '0'));

                intValues.Append(" ");
            }
            return intValues.ToString();
        }

        private List<string> DataintoTwo(string Sentence, int chunkSize)
        {
            //Cuts input into whatever size needed
            List<string> cutUp = new List<string>();
            for (int i = 0; i < Sentence.Length; i += chunkSize)
            {
                if (i + chunkSize > Sentence.Length) chunkSize = Sentence.Length - i;
                cutUp.Add((Sentence.Substring(i, chunkSize)));
            }
            return cutUp;
        }

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
        //Finished
        private string EightBit(string Data)
        {
            //Concerts the string into hex, the binary then pads left if needed
            StringBuilder Pile = new StringBuilder("0100 " + charCount(Data.Length, 100, version) + " ");
            char[] values = Data.ToCharArray();
            foreach (char letter in values)
            {
                int value = Convert.ToInt32(letter);
                string hexOutput = String.Format("{0:X}", value);
                Pile.Append(Convert.ToString(Convert.ToInt32(hexOutput, 16), 2).PadLeft(8, '0'));
            }
            return Pile.ToString();
        }

        private int EncodeType(string data)
        {
            if (data.All(Char.IsDigit))
                return 1; //Numeric
            else if (alphaCheck(data))
                return 10; //AlphaNumeric
            else
                return 100; //8-bit byte type
        }

        private bool alphaCheck(string theInput)
        {
            //Gets string and checks if it meets the needs of alphanumeric
            //Probably more efficent ways
            bool checkThis = true;
            string alphaCheck = "ABCDEFGHIJKLMNOPQRSTUVWXYZ$%*+-./: ";
            foreach (char letters in theInput)
            {
                foreach (char lets in alphaCheck)
                {
                    if (letters.Equals(lets))
                    {
                        checkThis = true;
                        break;
                    }
                    else if (!letters.Equals(lets))
                        checkThis = false;
                }
            }
            return checkThis;
        }
        //Finished
        private string NumericEncoding(string data)
        {
            StringBuilder total = new StringBuilder("001 " + charCount(data.Length, 1, version) + " ");
            List<string> numericValues = DataintoTwo(data, 3);
            for (int x = 0; x < numericValues.Count; x++)
            {
                total.Append(Convert.ToString((Convert.ToInt16(numericValues[x])), 2));
                total.Append(" ");
            }
            return total.ToString();
        }

        private string charCount(int inputLength, int dataType, int versionNum)
        {
            if (versionNum <= 9)
            {
                switch (dataType)
                {
                    case 1:
                        return Convert.ToString(inputLength, 2).PadLeft(10, '0');
                    case 10:
                        return Convert.ToString(inputLength, 2).PadLeft(9, '0');
                    case 100:
                        return Convert.ToString(inputLength, 2).PadLeft(8, '0');
                }
            }
            else if (versionNum <= 26)
            {
                switch (dataType)
                {
                    case 1:
                        return Convert.ToString(inputLength, 2).PadLeft(12, '0');
                    case 10:
                        return Convert.ToString(inputLength, 2).PadLeft(11, '0');
                    case 100:
                        return Convert.ToString(inputLength, 2).PadLeft(16, '0');
                }
            }
            return "1"; //error return? I am guessing ?:o
        }
    }
}
