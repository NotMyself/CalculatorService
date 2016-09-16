using System;
using System.ServiceModel;
using CalculatorService.Extensions;

namespace CalculatorService.Service
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = false,
         AddressFilterMode = AddressFilterMode.Any,
         InstanceContextMode = InstanceContextMode.Single,
         ConcurrencyMode = ConcurrencyMode.Single)]
    public class Calculator : ICalculator
    {
        public CalculationResult Add(string n1, string n2)
        {
            Console.WriteLine("received request to Add " + n1 + " to " + n2);
            Func<double, double, double> add = (value1, value2)
                => (value1 + value2);
            return Calculate(n1, n2, add);
        }

        public CalculationResult Subtract(string n1, string n2)
        {
            Console.WriteLine("received request to Subtract " + n2 + " from " + n1);
            Func<double, double, double> subtract = (value1, value2)
                => (value1 - value2);
            return Calculate(n1, n2, subtract);
        }

        public CalculationResult Multiply(string n1, string n2)
        {
            Console.WriteLine("received request to Multiply " + n1 + " by " + n2);
            Func<double, double, double> multiply = (value1, value2)
                => (value1 * value2);
            return Calculate(n1, n2, multiply);
        }

        public CalculationResult Divide(string n1, string n2)
        {
            Console.WriteLine("received request to Divide " + n1 + " by " + n2);
            Func<double, double, double> divide = (value1, value2)
                => value2 == 0 ? Double.NaN : (value1 / value2);
            return Calculate(n1, n2, divide);
        }

        private static CalculationResult Calculate(
            string n1,
            string n2,
            Func<double, double, double> calculate)
        {
            var value1 = n1.ToDouble();
            if (!value1.HasValue)
            {
                return GetCouldNotConvertToDoubleResult(n1);
            }

            var value2 = n2.ToDouble();
            if (!value2.HasValue)
            {
                return GetCouldNotConvertToDoubleResult(n2);
            }

            double result = calculate(value1.Value, value2.Value);
            return new CalculationResult
            {
                Answer = result
            };
        }

        private static CalculationResult GetCouldNotConvertToDoubleResult(string input)
        {
            return new CalculationResult
            {
                Message = "Could not convert '" + input + "' to a double"
            };
        }
    }
}