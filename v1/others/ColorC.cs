using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Euclid2d.Euclid.maths;

namespace Euclid2d.Euclid.others
{
    public static class ColorC
    {
        /*===================================================================================================*/
        #region CustomColors

        /* From (r,g,b) values */
        public static Color RGB(int r, int g, int b)
        {
            return Color.FromArgb(r, g, b);
        }

        /* From HEX string */
        public static Color HEX(string s)
        {
            return from_hex(s);
        }

        /* Random Color(r,g,b) */
        public static Color RandomColor()
        {
            return Color.FromArgb(Mathematics.RandomByte(), Mathematics.RandomByte(), Mathematics.RandomByte());
        }

        #endregion
        /*===================================================================================================*/
        #region ConvertFromHexToRGB

        /* Some aiding string methos (gets the selected substring of a string) */
        private static string sub_string(string original, int start, int end)
        {
            string subString = "";

            for (int i = start; i < end; i++)
            {
                subString += original[i];
            }

            return subString;
        }
        //----------------------------------------------//

        /* Wrapper for sub_string */
        private static string SubString(string original, int start, int end = -1)
        {
            if (original == "" || original == null)
            {
                return "";
            }
            else if (end == -1 && Mathematics.isIn(start, 0, original.Length))
            {
                return sub_string(original, start, original.Length);
            }
            else if (Mathematics.isIn(start, 0, end) && Mathematics.isIn(end, start, original.Length))
            {
                return sub_string(original, start, end);
            }
            else
            {
                throw new Exception("Betrayal! String has died!");
            }
        }
        //----------------------------------------------//

        /* Converts a hex char to dec int */
        private static int singleChar(char c)
        {
            int number = c - '0';

            if (Mathematics.isIn(number, 0, 9))
            {
                return number;
            }
            else if (Mathematics.isIn(number, 49, 54))
            {
                return number - 39;
            }
            else
            {
                throw new Exception("NaN (No' asta Nu)!");
            }
        }
        //----------------------------------------------//

        /* Converts a Hex String to dec int */
        public static int HexToDec(string hex)
        {
            string tempHex = hex.ToLower();

            int numberIndex = 0;
            int powerIndex = tempHex.Length - 1;

            int decNumber = 0;

            while (numberIndex < tempHex.Length)
            {
                int singleNumber = singleChar(tempHex[numberIndex]);

                decNumber += (int)(singleNumber * Math.Pow(16, powerIndex));

                numberIndex++;
                powerIndex--;
            }

            return decNumber;
        }
        //----------------------------------------------//

        /* Converts a HEC Color to RGB */
        private static Color from_hex(string s)
        {
            if (s.Length == 1)
            {
                int grey = HexToDec(s + s);

                return Color.FromArgb(grey, grey, grey);
            }
            else if (s.Length == 3)
            {
                int r, g, b;

                r = HexToDec(s[0].ToString() + s[0].ToString());
                g = HexToDec(s[1].ToString() + s[1].ToString());
                b = HexToDec(s[2].ToString() + s[2].ToString());

                return Color.FromArgb(r, g, b);
            }
            else if (s.Length == 6)
            {
                int r, g, b;

                r = HexToDec(SubString(s, 0, 2));
                g = HexToDec(SubString(s, 2, 4));
                b = HexToDec(SubString(s, 4, 6));

                return Color.FromArgb(r, g, b);
            }
            else
            {
                throw new Exception("Asta ii culoare ma? Nu ti oleaca rusine!?");
            }
        }

        #endregion
        /*===================================================================================================*/
        #region WebColors

        //----------------------------------------------//
        public static Color Red()
        {
            return HEX("f44336");
        }
        //----------------------------------------------//
        public static Color Pink()
        {
            return HEX("e91e63");
        }
        //----------------------------------------------//
        public static Color Purple()
        {
            return HEX("9c27b0");
        }
        //----------------------------------------------//
        public static Color DeepPurple()
        {
            return HEX("673ab7");
        }
        //----------------------------------------------//
        public static Color Indigo()
        {
            return HEX("3f51b5");
        }
        //----------------------------------------------//
        public static Color Blue()
        {
            return HEX("2196f3");
        }
        //----------------------------------------------//
        public static Color DeepBlue()
        {
            return HEX("0b2f4d");
        }
        //----------------------------------------------//
        public static Color LightBlue()
        {
            return HEX("03a9f4");
        }
        //----------------------------------------------//
        public static Color Cyan()
        {
            return HEX("00bcd4");
        }
        //----------------------------------------------//
        public static Color Teal()
        {
            return HEX("009688");
        }
        //----------------------------------------------//
        public static Color Green()
        {
            return HEX("4caf50");
        }
        //----------------------------------------------//
        public static Color LightGreen()
        {
            return HEX("8bc34a");
        }
        //----------------------------------------------//
        public static Color Lime()
        {
            return HEX("cddc39");
        }
        //----------------------------------------------//
        public static Color Yellow()
        {
            return HEX("ffeb3b");
        }
        //----------------------------------------------//
        public static Color Amber()
        {
            return HEX("ffc107");
        }
        //----------------------------------------------//
        public static Color Orange()
        {
            return HEX("ff9800");
        }
        //----------------------------------------------//
        public static Color DeepOrange()
        {
            return HEX("ff5722");
        }
        //----------------------------------------------//
        public static Color Brown()
        {
            return HEX("795548");
        }
        //----------------------------------------------//
        public static Color Grey()
        {
            return HEX("9e9e9e");
        }
        //----------------------------------------------//
        public static Color White()
        {
            return HEX("ffffff");
        }
        //----------------------------------------------//
        public static Color DimBlack()
        {
            return HEX("262626");
        }
        //----------------------------------------------//
        public static Color Black()
        {
            return HEX("000000");
        }
        //----------------------------------------------//

        #endregion
        /*===================================================================================================*/
    }
}
