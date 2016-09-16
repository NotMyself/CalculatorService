using System;

namespace CalculatorService.Extensions
{
    public static class StringExtensions
    {
        public static double? ToDouble(this string input)
        {
            double value1;

            return !Double.TryParse(input, out value1) ? (double?) null : value1;
        }
    }
}