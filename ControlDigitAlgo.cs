using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.ControlDigit
{
    public static class ControlDigitAlgo
    {
        public static int Upc(long number)
        {
            return number.GetSum(3, factor => 4 - factor).ControlDigit(10);
        }

        public static int Isbn10(long number)
        {
            var controlDigit = number.GetSum(2, factor => factor + 1).ControlDigit(11);
            switch (controlDigit)
            {
                case 10: return 'X';
                default: return '0' + controlDigit;
            }
        }

        public static int Isbn13(long number)
        {
            return number.GetSum(1, factor => 4 - factor).ControlDigit(10);
        }
    }

    public static class Extensions
    {
        public static int GetSum(this long number, int factor, Func<int, int> nextPosition)
        {
            var sum = 0;
            do
            {
                sum += factor * (int)(number % 10);
                number /= 10;
                factor = nextPosition(factor);
            } while (number > 0);
            return sum;
        }

        public static int ControlDigit(this int sum, int module)
        {
            if (sum % module == 0) return 0;
            return module - sum % module;
        }
    }
}