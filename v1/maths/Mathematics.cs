using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euclid2d.Euclid.maths
{
    public static class Mathematics
    {
        /*===================================================================================================*/
        #region RandomNumbers

        private static Random random = new Random();

        /* Returns a random INTEGER between "min" and "max" (inclusive) */
        public static int RandomInt(int min, int max)
        {
            return random.Next(min, max + 1);
        }
        /* Returns a random FLOAT between "min" and "max" */
        public static float RandomFloat(float min = 0.0000f, float max = 0.9999f)
        {
            return (float)(random.NextDouble() * (max - min) + min);
        }
        /* Returns a random double */
        public static Double RandomDouble()
        {
            return random.NextDouble();
        }
        /* Returns a random BYTE */
        public static Byte RandomByte()
        {
            return (Byte)(random.Next(0, 256));
        }
        /* Returns a random BOOLEAN */
        public static Boolean RandomBool()
        {
            return ((random.Next(0, 1000) % 2 == 0) ? true : false);
        }

        #endregion
        /*===================================================================================================*/
        #region FloatingNumbers

        private const int decimalPositionsFloat = 3;
        private const int decimalPositionsDouble = 4;

        public const float epsilonFloat = 0.001f;
        public const float epsilonDouble = 0.00001f;

        /* Rounds a float to "dec" decimals */
        public static float RoundFloat(float x, int dec = decimalPositionsFloat)
        {
            return (float)Math.Round(x, dec);
        }

        /* Rounds a double to "dec" decimals */
        public static Double RoundDouble(Double x, int dec = decimalPositionsDouble)
        {
            return Math.Round(x, dec);
        }

        /* Check if is NaN */
        public static Boolean isNan(float x)
        {
            return float.IsNaN(x);
        }

        /* Check is is equal */
        public static Boolean Equal(float x, float y, float epsilon = epsilonFloat)
        {
            if (x == y)
            {
                return true;
            }

            if(isNan(x) && isNan(y))
            {
                return true;
            }

            if(isNan(x) && !isNan(y))
            {
                return false;
            }

            if(!isNan(x) && isNan(y))
            {
                return false;
            }

            float newX = RoundFloat(x);
            float newY = RoundFloat(y);

            if (newX == newY)
            {
                return true;
            }

            if (IsZero(x) && IsZero(y))
            {
                return true;
            }

            if (IsZero(x) && !IsZero(y))
            {
                return false;
            }

            if (!IsZero(x) && IsZero(y))
            {
                return false;
            }

            float ab = Math.Abs(newX - newY);
            float AB = Math.Abs(newX) + Math.Abs(newY);

            if((ab / AB) < epsilon)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Boolean IsZero(float x, float epsilon = epsilonFloat)
        {
            if(x == 0)
            {
                return true;
            }

            float newX = RoundFloat(x);

            if (newX == 0)
            {
                return true;
            }

            else if (isIn(Math.Abs(newX), -epsilon, epsilon))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
        /*===================================================================================================*/
        #region CheckingIntervals

        /* Check a number if it belongs to an interval (min, max) */

        /* INTEGERS */
        public static Boolean isIn(int number, int min, int max, int type = 0)
        {
            if (type == 0)
            {
                return ((min <= number) && (number <= max));
            }
            else
            {
                return ((min < number) && (number < max));
            }
        }
        /* FLOATING POINT NUMBERS */
        public static Boolean isIn(float number, float min, float max, int type = 0)
        {

            float newNum = RoundFloat(number);

            if (type == 0)
            {
                return ((min <= newNum) && (newNum <= max));
            }
            else
            {
                return ((min < newNum) && (newNum < max));
            }
        }
        /* DOUBLE FLOATING POINT */
        public static Boolean isIn(Double number, Double min, Double max, int type = 0)
        {

            Double newNum = RoundDouble(number);

            if (type == 0)
            {
                return ((min <= newNum) && (newNum <= max));
            }
            else
            {
                return ((min < newNum) && (newNum < max));
            }
        }
        /* BYTES */
        public static Boolean isIn(Byte number, Byte min, Byte max, int type = 0)
        {
            if (type == 0)
            {
                return ((min <= number) && (number <= max));
            }
            else
            {
                return ((min < number) && (number < max));
            }
        }

        #endregion
        /*===================================================================================================*/
        #region minMax

        /* INTEGERS */
        public static int Min(int a, int b)
        {
            return ((a < b) ? a : b);
        }
        public static int Max(int a, int b)
        {
            return ((a > b) ? a : b);
        }
        //----------------------------------------------//
        public static int Min(int a, int b, int c)
        {
            return Min(Min(a, b), c);
        }
        public static int Max(int a, int b, int c)
        {
            return Max(Max(a, b), c);
        }

        /* FLOATING POINT NUMBERS */
        public static float Min(float a, float b)
        {
            return ((a < b) ? a : b);
        }
        public static float Max(float a, float b)
        {
            return ((a > b) ? a : b);
        }
        //----------------------------------------------//
        public static float Min(float a, float b, float c)
        {
            return Min(Min(a, b), c);
        }
        public static float Max(float a, float b, float c)
        {
            return Max(Max(a, b), c);
        }

        /* DOUBLE PRECISION NUMBERS */
        public static Double Min(Double a, Double b)
        {
            return ((a < b) ? a : b);
        }
        public static Double Max(Double a, Double b)
        {
            return ((a > b) ? a : b);
        }
        //----------------------------------------------//
        public static Double Min(Double a, Double b, Double c)
        {
            return Min(Min(a, b), c);
        }
        public static Double Max(Double a, Double b, Double c)
        {
            return Max(Max(a, b), c);
        }

        /* BYTES */
        public static Byte Min(Byte a, Byte b)
        {
            return ((a < b) ? a : b);
        }
        public static Byte Max(Byte a, Byte b)
        {
            return ((a > b) ? a : b);
        }
        //----------------------------------------------//
        public static Byte Min(Byte a, Byte b, Byte c)
        {
            return Min(Min(a, b), c);
        }
        public static Byte Max(Byte a, Byte b, Byte c)
        {
            return Max(Max(a, b), c);
        }

        #endregion
        /*===================================================================================================*/
        #region ClampToInterval

        /* Return clamped value to specified interval */

        /* INTEGERS */
        public static int Clamp(int number, int min, int max)
        {
            return Max(min, Min(number, max));
        }
        /* FLOATING POINT NUMBERS */
        public static float Clamp(float number, float min, float max)
        {
            return Max(RoundFloat(min), Min(RoundFloat(number), RoundFloat(max)));
        }
        /* DOUBLE FLOATING POINT */
        public static Double Clamp(Double number, Double min, Double max)
        {
            return Max(RoundDouble(min), Min(RoundDouble(number), RoundDouble(max)));
        }
        /* BYTES */
        public static Byte Clamp(Byte number, Byte min = 0, Byte max = 255)
        {
            return Max(min, Min(number, max));
        }

        #endregion
        /*===================================================================================================*/
    }
}
