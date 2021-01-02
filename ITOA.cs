using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewSystem
{
    class ITOA
    {
        private const decimal Eps = (decimal)1e-10;

        public static string ConvertFraction(string inputNum, string sys)
        {
            if (!decimal.TryParse(inputNum, out var num) || !int.TryParse(sys, out var newSys))
                throw new ArgumentException("Incorrect input");

            if (newSys < 2 || newSys > 36)
                throw new ArgumentException("Incorrect system: not in [2, 36]");

            var sign = num < 0 ? "-" : "";
            num = Math.Abs(num);

            var newNum = new StringBuilder($"{sign}{num}(10) = {sign}{ConvertInt((int)Math.Truncate(num), newSys)},");
            num -= Math.Truncate(num);

            var doublePart = new StringBuilder();
            var used = new List<decimal>();

            while (num > Eps && !used.Contains(num))
            {
                used.Add(num);
                var current = decimal.Multiply(num, newSys);

                if (Math.Truncate(current) > 9)
                    doublePart.Append((char)(Math.Truncate(current) + 55));
                else
                    doublePart.Append(Math.Truncate(current));

                num = current - Math.Truncate(current);
            }

            if (!used.Contains(num)) return newNum.Append($"{doublePart}({sys})").ToString();

            for (var i = 0; i < used.Count; i++)
                if (used[i] == num)
                    doublePart.Insert(i, '(');

            doublePart.Append(")");

            return newNum.Append($"{doublePart}({sys})").ToString();
        }

        private static string ConvertInt(int num, int newSys)
        {
            var newNum = new StringBuilder();

            while (true)
            {
                if (num < newSys)
                {
                    if (num > 9)
                        newNum.Append((char)(num + 55));
                    else
                        newNum.Append(num);
                    break;
                }

                if (num % newSys > 9)
                    newNum.Append((char)(num % newSys + 55));
                else
                    newNum.Append(num % newSys);

                num /= newSys;
            }

            return new string(newNum.ToString().Reverse().ToArray());
        }
    }
}
